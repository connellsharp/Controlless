using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using Microsoft.AspNetCore.Http;

namespace Controlless
{
    public class ModelRequestBinder<TRequest> : IRequestBinder
    {
        private ModelTypeInfo _typeInfo;

        public ModelRequestBinder()
        {
            _typeInfo = new ModelTypeInfo(typeof(TRequest));
        }

        public string Method => _typeInfo.RoutingAttribute.Method;

        public string Route => _typeInfo.RoutingAttribute.RoutePattern;

        public object Bind(HttpRequest request, CancellationToken ct)
        {
            var model = Activator.CreateInstance(typeof(TRequest))
                ?? throw new Exception($"Type {typeof(TRequest).Name} must have a parameterless constructor.");

            var valueProviders = new ValueProviderFactory(request);

            foreach(var property in _typeInfo.Properties)
            {
                var valueProvider = valueProviders.Create(property.BindingSource);
                var value = valueProvider.GetValue(property.Name);
                property.SetValue(model, value);
            }

            return model;
        }
    }

    internal class ModelTypeInfo
    {
        private Type _type;

        public ModelTypeInfo(Type type)
        {
            _type = type;

            RoutingAttribute = _type.GetCustomAttribute<RouteAttribute>()
                ?? throw new Exception($"Type {_type.Name} does not have a valid RouteAttribute");
        }

        public RouteAttribute RoutingAttribute { get; }

        public IEnumerable<ModelProperty> Properties
            => _type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                   .Select(property => new ModelProperty(property));
    }

    internal class ModelProperty
    {
        public ModelProperty(PropertyInfo property)
        {
            PropertyInfo = property;

            BindingAttribute = PropertyInfo.GetCustomAttribute<BindAttribute>();
        }

        private PropertyInfo PropertyInfo { get; }

        private BindAttribute? BindingAttribute { get; }

        public string Name => BindingAttribute?.Key ?? PropertyInfo.Name;

        public BindingSource BindingSource => BindingAttribute?.Source ?? BindingSource.Route;

        public void SetValue(object? model, object value)
        {
            var convertedValue = ConversionUtility.ConvertValue(value, PropertyInfo.PropertyType);
            PropertyInfo.SetValue(model, convertedValue);
        }
    }
}