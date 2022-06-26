using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MarketLib.src.MarketSystemNS;

namespace UnitTestProject1
{
   
    /// <summary>
    /// Summary description for ConnectTest
    /// </summary>
    [TestClass]
    public class ConnectTest
    {
        private MarketSystem market = new MarketSystem();
        public ConnectTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;



        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void get_connection_id()
        {
            string id = market.connect();
            Assert.IsNotNull(id);
        }

        [TestMethod]
        public void check_different_connections()
        {
            string id1 = market.connect();
            string id2 = market.connect();
            Assert.AreNotEqual(id1, id2);
        }
    }
}
