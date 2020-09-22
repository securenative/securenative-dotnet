using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecureNative.SDK.Utils;

namespace SecureNative.SDK.Tests
{
    [TestClass]
    public class VersionTests
    {
        [TestMethod]
        public void TestVersionExtraction()
        {
            Assert.AreNotEqual("unknown", VersionUtils.GetVersion());
        }
    }
}
