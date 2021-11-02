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
                property.SetValue(model, valueProvider.GetValue(property.Name));
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
            => _type.GetProperties(BindingFlags.Public | BindingFlags.SetProperty)
                   .Select(property => new ModelProperty(property));
    }

    internal class ModelProperty
    {
        public ModelProperty(PropertyInfo property)
        {
            PropertyInfo = property;
        }

        private PropertyInfo PropertyInfo { get; }

        public string Name => PropertyInfo.Name;

        public BindingSource BindingSource => BindingSource.Body;

        public void SetValue(object? model, object value)
        {
            PropertyInfo.SetValue(model, value);
        }
    }
}