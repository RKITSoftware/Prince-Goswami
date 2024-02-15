using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Advance_C__Final_Demo
{
    public static class EnumConverter
    {
        public static T ConvertToEnum<T>(string value) where T : struct
        {
            if (Enum.TryParse<T>(value, out T result))
            {
                return result;
            }

            throw new ArgumentException($"Unable to convert '{value}' to enum type {typeof(T).Name}");
        }
    }

}