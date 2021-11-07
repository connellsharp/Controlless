using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Controlless.Custom
{
    internal interface IValueProvider
    {
        object GetValue(string name);
    }

    internal class ValueProviderFactory
    {
        private HttpRequest _request;

        public ValueProviderFactory(HttpRequest request)
        {
            _request = request;
        }

        public IValueProvider Create(BindingSource source) => source switch
        {
            BindingSource.Body => new BodyValueProvider(_request.Body),
            BindingSource.Query => new QueryValueProvider(_request.Query),
            BindingSource.Route => new RouteValueProvider(_request.RouteValues),
            _ => throw new ArgumentOutOfRangeException(nameof(source))
        };
    }

    internal class RouteValueProvider : IValueProvider
    {
        private RouteValueDictionary _routeValues;

        public RouteValueProvider(RouteValueDictionary routeValues)
        {
            _routeValues = routeValues;
        }

        public object GetValue(string name)
        {
            return _routeValues[name] ?? throw new KeyNotFoundException($"No route value called '{name}'");
        }
    }

    internal class QueryValueProvider : IValueProvider
    {
        private IQueryCollection _query;

        public QueryValueProvider(IQueryCollection query)
        {
            _query = query;
        }

        public object GetValue(string name)
        {
            return _query[name];
        }
    }

    internal class BodyValueProvider : IValueProvider
    {
        private Stream _body;

        public BodyValueProvider(Stream body)
        {
            _body = body;
        }

        public object GetValue(string name)
        {
            throw new NotImplementedException();
        }
    }
}