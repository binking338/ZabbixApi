using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZabbixApi.Helper
{
    public static class DictionaryExtensions
    {
        public static void AddIfNotExist<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            if (!dictionary.ContainsKey(key))
                dictionary.Add(key, value);
        }

        public static void AddOrReplace<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            if (!dictionary.ContainsKey(key))
                dictionary.Add(key, value);
            else
                dictionary[key] = value;
        }

        public static void AddIfNotExist<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, object addition)
        {
            var additionDic = JsonConvert.DeserializeObject<Dictionary<TKey, TValue>>(JsonConvert.SerializeObject(addition));
            foreach (var kv in additionDic)
            {
                dictionary.AddIfNotExist(kv.Key, kv.Value);
            }
        }

        public static void AddOrReplace<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, object addition)
        {
            var additionDic = JsonConvert.DeserializeObject<Dictionary<TKey, TValue>>(JsonConvert.SerializeObject(addition));
            foreach (var kv in additionDic)
            {
                dictionary.AddOrReplace(kv.Key, kv.Value);
            }
        }
    }
}
