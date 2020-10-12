using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecureNative.SDK.Utils;

namespace SecureNative.Tests
{
    [TestClass]
    public class DateUtilsTests
    {
        [TestMethod]
        public void ToTimestampTest()
        {
            const string iso8601Date = "2000-01-01T00:00:00Z";
            var date = DateTime.Parse(iso8601Date);
            var result = DateUtils.ToTimestamp(date);

            Assert.AreEqual(iso8601Date, result);
        }

        [TestMethod]
        public void GenerateTimestampTest()
        {
            var result = DateUtils.GenerateTimestamp();

            Assert.IsNotNull(result);
        }
    }
}
