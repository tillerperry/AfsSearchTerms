﻿using Newtonsoft.Json;
using StackExchange.Redis;

namespace Afs.SearchTerms.Web.Extensions;

public static class SerializationExtensions
{
    public static string Serialize<T>(this T @object, JsonSerializerSettings options = null)
        where T : notnull
    {
        if (@object is null)
        {
            return string.Empty;
        }

        return JsonConvert.SerializeObject(@object, Formatting.Indented, options);
    }

    public static T Deserialize<T>(this string json, JsonSerializerSettings settings = null)
    {
        if (string.IsNullOrEmpty(json))
            return default;

        return JsonConvert.DeserializeObject<T>(json, settings);
    }

    public static T Deserialize<T>(this RedisValue json, JsonSerializerSettings settings = null)
    {
        if (json.IsNull || !json.HasValue)
            return default;

        return JsonConvert.DeserializeObject<T>(json!, settings);
    }

    public static IEnumerable<T> Deserialize<T>(this RedisValue[] jsonArray, JsonSerializerSettings settings = null)
    {
        if (jsonArray is null || !jsonArray.Any())
            return Enumerable.Empty<T>();

        var result = new List<T>(jsonArray.Length);
        foreach (var jsonItem in jsonArray)
        {
            if (jsonItem.IsNull || !jsonItem.HasValue)
                continue;

            var item = Deserialize<T>(jsonItem, settings);

            if (item is not null)
                result.Add(item);
        }

        return result;
    }
}