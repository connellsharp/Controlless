using System;

namespace Controlless.Custom
{
    public class BindAttribute : Attribute
    {
        internal BindAttribute(BindingSource source, string key)
        {
            Source = source;
            Key = key;
        }

        internal BindingSource Source { get; }

        internal string Key { get; }
    }

    public class FromBodyAttribute : BindAttribute
    {
        public FromBodyAttribute(string key)
            : base(BindingSource.Body, key) { }
    }

    public class FromQueryAttribute : BindAttribute
    {
        public FromQueryAttribute(string key)
            : base(BindingSource.Query, key) { }
    }

    public class FromRouteAttribute : BindAttribute
    {
        public FromRouteAttribute(string key)
            : base(BindingSource.Route, key) { }
    }
}