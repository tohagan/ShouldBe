using System.Collections.Generic;
using System.Diagnostics;
using NUnit.Framework;

namespace ShouldBe 
{
    /// <summary>
    /// Extension methods for IDictionary 
    /// </summary>
    [DebuggerStepThrough]
    [ShouldBeMethods]
    public static class ShouldBeDictionaryTestExtensions 
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dictionary"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static IDictionary<TKey, TValue> ShouldContainKey<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key) 
        {
            if (!dictionary.ContainsKey(key))
            {
                ShouldBeMessage.FailExpecting(key);
            }
            return dictionary;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dictionary"></param>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static IDictionary<TKey, TValue> ShouldContainKeyAndValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue val) 
        {
            if (!dictionary.ContainsKey(key) || !dictionary[key].Equals(val))
            {
                ShouldBeMessage.FailExpectingFormatted(string.Format(@"{{key: {0}, val: {1}}}", key.Inspect(), val.Inspect()));
            }
            return dictionary;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dictionary"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static IDictionary<TKey, TValue> ShouldNotContainKey<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key) 
        {
            if (dictionary.ContainsKey(key))
            {
                ShouldBeMessage.FailExpecting(key);
            }
            return dictionary;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dictionary"></param>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static IDictionary<TKey, TValue> ShouldNotContainValueForKey<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue val)
        {
            dictionary.ShouldContainKey(key);

            if (dictionary[key].Equals(val))
            {
                ShouldBeMessage.FailExpectingFormatted(string.Format(@"{{key: {0}, val: {1}}}", key.Inspect(), val.Inspect()));
            }

            return dictionary;
        }
    }
}
