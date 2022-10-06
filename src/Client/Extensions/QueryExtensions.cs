using System.Reflection;
using Microsoft.AspNetCore.WebUtilities;

namespace PAD.Client.Extensions;

public static class QueryExtensions
{
    public static string AddQueryString(string basePath, object obj)
    {
        Dictionary<string, string> queryParams = obj.GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .ToDictionary(prop => prop.Name, prop => (string)prop.GetValue(obj, null));

        return QueryHelpers.AddQueryString(basePath, queryParams);
    }
}