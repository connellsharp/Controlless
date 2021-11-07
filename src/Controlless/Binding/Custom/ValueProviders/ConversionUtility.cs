using System;

namespace Controlless.Custom
{
    /// <summary>
    /// An all-powerful super conversion tool to try convert anything from anything.
    /// </summary>
    /// <remarks>
    /// Copied from Firestorm
    /// </remarks>
    internal static class ConversionUtility
    {
        public static object? ConvertValue(object value, Type type)
        {
            if (IsNull(value, type))
                return null;

            if (type.IsInstanceOfType(value))
                return value;

            type = Nullable.GetUnderlyingType(type) ?? type;

            string? strValue = value.ToString();

            return ConvertString(strValue, type);
        }

        private static bool IsNull(object value, Type type)
        {
            if (value == null)
            {
                if (type.IsValueType && Nullable.GetUnderlyingType(type) == null)
                    throw new ArgumentException("Cannot convert the given null value to a value type.");

                return true;
            }

            return false;
        }

        public static T? ConvertValue<T>(object value)
        {
            return (T?) ConvertValue(value, typeof(T));
        }

        public static object? ConvertString(string? strValue, Type type)
        {
            if (type == typeof(string))
                return strValue;

            if (string.IsNullOrEmpty(strValue))
                return null;

            int number;
            if (type.IsEnum && int.TryParse(strValue, out number))
                return Enum.ToObject(type, number);

            return Convert.ChangeType(strValue, type);
        }

        public static T? ConvertString<T>(string strValue)
        {
            return (T?) ConvertString(strValue, typeof(T));
        }
    }
}