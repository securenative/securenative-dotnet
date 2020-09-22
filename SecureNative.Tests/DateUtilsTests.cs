using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecureNative.SDK.Utils;

namespace SecureNative.SDK.Tests
{
    [TestClass]
    public class DateUtilsTests
    {
        [TestMethod]
        public void ToTimestampTest()
        {
            String iso8601Date = "2000-01-01T00:00:00Z";
            DateTime date = DateTime.Parse(iso8601Date);
            String result = DateUtils.ToTimestamp(date);

            Assert.AreEqual(iso8601Date, result);
        }

        [TestMethod]
        public void GenerateTimestampTest()
        {
            String result = DateUtils.GenerateTimestamp();

            Assert.IsNotNull(result);
        }
    }
}
