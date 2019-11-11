using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecureNative.SDK.Tests
{
    [TestClass]
    public class AgentTest
    {

       // [ClassInitialize]
       // public void Setup()
       // {
         
       // }

        [TestMethod]
        public void TestGetDep(
            )
        {
            Console.WriteLine("here");

            Console.WriteLine(Agent.GetDependencies());

            Assert.AreEqual("", "");

        }

    }
}
