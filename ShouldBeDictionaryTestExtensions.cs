using System.Collections.Generic;
using System.Diagnostics;
using NUnit.Framework;

namespace ShouldBe 
{
    [DebuggerStepThrough]
    [ShouldBeMethods]
    public static class ShouldBeDictionaryTestExtensions 
    {
        public static IDictionary<TKey, TValue> ShouldContainKey<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key) 
        {
            if (!dictionary.ContainsKey(key))
            {
                ShouldBeMessage.Fail(key.Inspect());
            }
            return dictionary;
        }

        public static IDictionary<TKey, TValue> ShouldContainKeyAndValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue val) 
        {
            if (!dictionary.ContainsKey(key) || !dictionary[key].Equals(val))
            {
                ShouldBeMessage.Fail(string.Format(@"{{key: {0}, val: {1}}}", key.Inspect(), val.Inspect()));
            }
            return dictionary;
        }

        public static IDictionary<TKey, TValue> ShouldNotContainKey<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key) 
        {
            if (dictionary.ContainsKey(key))
            {
                ShouldBeMessage.Fail(key.Inspect());
            }
            return dictionary;
        }

        public static IDictionary<TKey, TValue> ShouldNotContainValueForKey<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue val)
        {
            dictionary.ShouldContainKey(key);

            if (dictionary[key].Equals(val))
            {
                ShouldBeMessage.Fail(string.Format(@"{{key: {0}, val: {1}}}", key.Inspect(), val.Inspect()));
            }

            return dictionary;
        }
    }
}
