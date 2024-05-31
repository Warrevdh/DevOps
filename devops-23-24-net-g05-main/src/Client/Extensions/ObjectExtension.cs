using System;
using System.Reflection;
using System.Web;

namespace Client.Extensions;

public static class ObjectExtensions
{
    public static string AsQueryString(this object obj)
    {
        var properties = from p in obj.GetType().GetProperties()
                         let value = p.GetValue(obj)
                         where value != null
                         select GetQueryStringForProperty(p, value);

        return string.Join("&", properties.ToArray());
    }

    private static string GetQueryStringForProperty(PropertyInfo property, object value)
    {
        if (value is IList<long> list)
        {
            var listValues = list.Select(item => $"SymptomIds={item}");
            return string.Join("&", listValues);
        }
        else
        {
            return $"{property.Name}={HttpUtility.UrlEncode(value.ToString())}";
        }
    }
}