using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecureNative.SDK.Utils;

namespace SecureNative.Tests
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
