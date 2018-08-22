using System;
using NUnit.Framework;
using System.Collections.Generic;

namespace ShouldBe.UnitTests 
{
    [TestFixture]
    public class ShouldBeDictionaryTests 
    {
        IDictionary<string, string> dictionary = new Dictionary<string, string> { { "key", "value" } };

        [Test]
        public void ShouldContainKey_WhenTrue_ShouldSucceed()
        {
            dictionary.ShouldContainKey("key");
        }

        [Test]
        public void ShouldContainKey_WhenFalse_ShouldFail()
        {
            TestHelper.ShouldFailWithError(
                () => dictionary.ShouldContainKey("NOT THERE"),
                @"dictionary should contain key ""NOT THERE"""
            );
        }

        [Test]
        public void ShouldNotContainKey_WhenTrue_ShouldSucceed()
        {
            dictionary.ShouldNotContainKey("rob");
        }

        [Test]
        public void ShouldNotContainKey_WhenFalse_ShouldFail()
        {
            TestHelper.ShouldFailWithError(
                () => dictionary.ShouldNotContainKey("key"),
                @"dictionary should not contain key ""key"""
            );
        }

        [Test]
        public void ShouldContainKeyAndValue_WhenTrue_ShouldSucceed()
        {
            dictionary.ShouldContainKeyAndValue("key", "value");
        }

        [Test]
        public void ShouldContainKeyAndValue_WhenFalse_ShouldFail()
        {
            TestHelper.ShouldFailWithError(
                () => dictionary.ShouldContainKeyAndValue("key1", "value"),
                @"dictionary should contain key and value {key: ""key1"", val: ""value""}");
        }

        [Test]
        public void ShouldNotContainValueForKey_WhenTrue_ShouldSucceed()
        {
            dictionary.ShouldNotContainValueForKey("key", "slurpee");
        }

        [Test]
        public void ShouldNotContainValueForKey_WhenFalse_ShouldFail()
        {
            TestHelper.ShouldFailWithError(
                () => dictionary.ShouldNotContainValueForKey("key", "value"),
                    @"dictionary should not contain value for key {key: ""key"", val: ""value""}"
                );
        }

        [Test]
        public void ShouldContainKeyAndValue_ShouldWorkWithObjects() 
        {
            var guid = Guid.NewGuid();
            var dictionary2 = new Dictionary<string, Guid> {{"key", guid}};

            dictionary2.ShouldContainKeyAndValue("key", guid);
        }
    }
}