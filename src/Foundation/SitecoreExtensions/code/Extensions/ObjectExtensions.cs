using System;
using System.Net;

namespace Foundation.SitecoreExtensions.Extensions
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// Try cast <paramref name="obj"/> value to type <typeparamref name="T"/>,
        /// if can't will return <paramref name="defaultValue"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T To<T>(this object obj, T defaultValue = default(T))
        {
            if (obj == null)
                return defaultValue;

            if (obj is T)
                return (T)obj;

            Type type = typeof(T);

            // Place convert to reference types here

            if (type == typeof(string))
            {
                return (T)(object)obj.ToString();
            }

            Type underlyingType = Nullable.GetUnderlyingType(type);
            if (underlyingType != null)
            {
                return To(obj, defaultValue, underlyingType);
            }

            return To(obj, defaultValue, type);
        }

        private static T To<T>(object obj, T defaultValue, Type type)
        {
            if (obj is bool && type.IsNumericType())
            {
                return (bool)obj ? (T)System.Convert.ChangeType(1, type) : (T)System.Convert.ChangeType(0, type);
            }

            if (type == typeof(int))
            {
                int intValue;
                if (int.TryParse(obj.ToString(), out intValue))
                    return (T)(object)intValue;
                return defaultValue;
            }

            if (type == typeof(decimal))
            {
                decimal decimalValue;
                if (decimal.TryParse(obj.ToString(), out decimalValue))
                    return (T)(object)decimalValue;
                return defaultValue;
            }

            if (type == typeof(IPAddress))
            {
                IPAddress ipValue;
                if (IPAddress.TryParse(obj.ToString(), out ipValue))
                    return (T)(object)ipValue;
                return defaultValue;
            }

            if (type == typeof(double))
            {
                double doubleValue;
                if (double.TryParse(obj.ToString(), out doubleValue))
                    return (T)(object)doubleValue;
                return defaultValue;
            }

            if (type == typeof(long))
            {
                long intValue;
                if (long.TryParse(obj.ToString(), out intValue))
                    return (T)(object)intValue;
                return defaultValue;
            }

            if (type == typeof(bool))
            {
                if (obj.GetType().IsNumericType())
                    return (T)(object)(System.Convert.ToInt64(obj) != 0);
                bool bValue;
                if (bool.TryParse(obj.ToString(), out bValue))
                    return (T)(object)bValue;
                return defaultValue;
            }

            if (type == typeof(byte))
            {
                byte byteValue;
                if (byte.TryParse(obj.ToString(), out byteValue))
                    return (T)(object)byteValue;
                return defaultValue;
            }

            if (type == typeof(short))
            {
                short shortValue;
                if (short.TryParse(obj.ToString(), out shortValue))
                    return (T)(object)shortValue;
                return defaultValue;
            }

            if (type == typeof(DateTime))
            {
                DateTime dateValue;
                if (DateTime.TryParse(obj.ToString(), out dateValue))
                    return (T)(object)dateValue;
                return defaultValue;
            }

            if (type.IsEnum)
            {
                if (Enum.IsDefined(type, obj))
                    return (T)Enum.Parse(type, obj.ToString());
                return defaultValue;
            }

            throw new NotSupportedException(string.Format("Couldn't parse to Type {0}", typeof(T)));
        }

        private static bool IsNumericType(this Type type)
        {
            return type == typeof(byte) || type == typeof(short) || type == typeof(int) || type == typeof(long) ||
                   type == typeof(float) || type == typeof(double) || type == typeof(ushort) || type == typeof(uint) ||
                   type == typeof(ulong);
        }
    }
}