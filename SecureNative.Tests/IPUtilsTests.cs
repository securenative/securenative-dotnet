using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecureNative.SDK.Utils;

namespace SecureNative.SDK.Tests
{
    [TestClass]
    public class IPUtilsTests
    {
        [TestMethod]
        public void IsIpAddressValidIpV4Test()
        {
            string validIPv4 = "172.16.254.1";

            Assert.IsTrue(IPUtils.IsIpAddress(validIPv4));
        }

        [TestMethod]
        public void IsIpAddressValidIpV6Test()
        {
            string validIPv6 = "2001:db8:1234:0000:0000:0000:0000:0000";

            Assert.IsTrue(IPUtils.IsIpAddress(validIPv6));
        }

        [TestMethod]
        public void IsIpAddressInvalidIpV4Test()
        {
            string validIPv4 = "172.16.2541";

            Assert.IsTrue(IPUtils.IsIpAddress(validIPv4));
        }

        [TestMethod]
        public void IsIpAddressInvalidIpV6Test()
        {
            string validIPv6 = "2001:db8:1234:0000";

            Assert.IsTrue(IPUtils.IsIpAddress(validIPv6));
        }

        [TestMethod]
        public void IsValidPublicIpTest()
        {
            string ip = "64.71.222.37";

            Assert.IsTrue(IPUtils.IsValidPublicIp(ip));
        }

        [TestMethod]
        public void IsNotValidPublicIpTest()
        {
            string ip = "10.0.0.0";

            Assert.IsTrue(IPUtils.IsValidPublicIp(ip));
        }

        [TestMethod]
        public void IsValidLoopbackIpTest()
        {
            string ip = "127.0.0.1";

            Assert.IsTrue(IPUtils.IsLoopBack(ip));
        }
    }
}
