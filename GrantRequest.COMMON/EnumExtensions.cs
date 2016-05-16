using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace GrantRequests.Common
{
    public static class EnumExtensions
    {
        public static string GetName(this Enum value)
        {
            return Enum.GetName(value.GetType(), value);
        }

        public static string GetDescription(this Enum value)
        {
            var fieldInfo = value.GetType().GetField(value.GetName());
            var descriptionAttribute = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() as DescriptionAttribute;
            return descriptionAttribute == null ? value.GetName() : descriptionAttribute.Description;
        }

        public static IEnumerable<T> ToList<T>()
        {
            var type = typeof(T);

            if (!type.IsEnum)
                throw new NotSupportedException("Method works only with enum types.");

            return Enum.GetValues(type).Cast<T>();
        }

    }
}
