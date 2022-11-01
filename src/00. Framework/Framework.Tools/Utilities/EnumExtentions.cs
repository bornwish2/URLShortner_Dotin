using Framework.Tools.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Framework.Tools.Utilities
{
    public static class EnumExtensions
    {
        public static IEnumerable<T> GetEnumValues<T>(this T input) where T : struct
        {
            if (!typeof(T).IsEnum)
                throw new NotSupportedException();

            return Enum.GetValues(input.GetType()).Cast<T>();
        }

        public static IEnumerable<T> GetEnumFlags<T>(this T input) where T : struct
        {
            if (!typeof(T).IsEnum)
                throw new NotSupportedException();

            foreach (var value in Enum.GetValues(input.GetType()))
                if ((input as Enum).HasFlag(value as Enum))
                    yield return (T)value;
        }

        public static string ToDisplay(this Enum value, DisplayProperty property = DisplayProperty.Name)
        {
            Assert.NotNull(value, nameof(value));

            var attribute = value.GetType().GetField(value.ToString())
                .GetCustomAttributes<DisplayAttribute>(false).FirstOrDefault();

            if (attribute == null)
                return value.ToString();

            var propValue = attribute.GetType().GetProperty(property.ToString()).GetValue(attribute, null);
            return propValue.ToString();
        }

        public static Dictionary<int, string> ToDictionary(this Enum value)
        {
            return Enum.GetValues(value.GetType()).Cast<Enum>().ToDictionary(p => Convert.ToInt32(p), q => q.ToDisplay());
        }

        public static string GetDescription(this Enum @enum)
        {

            Type genericEnumType = @enum.GetType();
            MemberInfo[] memberInfo =
                        genericEnumType.GetMember(@enum.ToString());

            if (memberInfo != null && memberInfo.Length > 0)
            {

                var Attribs = memberInfo[0].GetCustomAttributes
                      (typeof(System.ComponentModel.DescriptionAttribute), false);
                if (Attribs != null && Attribs.Length > 0)
                {
                    return ((System.ComponentModel.DescriptionAttribute)Attribs[0]).Description;
                }
            }

            return @enum.ToString();
        }
    }

    public enum DisplayProperty
    {
        Description,
        GroupName,
        Name,
        Prompt,
        ShortName,
        Order
    }
}
