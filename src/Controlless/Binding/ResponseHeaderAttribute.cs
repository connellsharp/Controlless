using System;

namespace Controlless
{
    public class ResponseHeaderAttribute : Attribute
    {
        public ResponseHeaderAttribute(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public string Key { get; }

        public string Value { get; }
    }
}