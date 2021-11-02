using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using Microsoft.AspNetCore.Http;

namespace Controlless
{
    public class ModelRequestBinder<T> : IRequestBinder
    {
        private ModelTypeInfo _typeInfo;

        public ModelRequestBinder()
        {
            _typeInfo = new ModelTypeInfo(typeof(T));
        }

        public object? Bind(HttpRequest request, CancellationToken ct)
        {
            if(!_typeInfo.AllowsEndpoint(request.Method, request.Path))
                return null;

            var model = Activator.CreateInstance(typeof(T));
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
            this._type = type;
        }

        internal bool AllowsEndpoint(string method, PathString path)
        {
            return false;
        }

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

        public PropertyInfo PropertyInfo { get; }

        public string Name => PropertyInfo.Name;

        public BindingSource BindingSource => BindingSource.Body;

        public void SetValue(object? model, object p)
        {
            throw new NotImplementedException();
        }
    }
}