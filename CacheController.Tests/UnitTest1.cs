﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CacheController;

namespace CacheController.Tests
{
    [TestClass]
    public class UnitTest1
    {
        
        [TestInitialize]
        public void init()
        {
            
        }

        [TestMethod]
        public void TestMethod1()
        {
            var result = CacheController.Cache("test", 10, new Func<DateTime>(TestFunction));

            var result2 = CacheController.Cache("test", 10, new Func<DateTime>(TestFunction));

            Assert.AreEqual(result, result2);

        }

        [TestMethod]
        public void TestMethod2()
        {
            var result = CacheController.Cache("test", 10, new Func<DateTime>(TestFunction));

            System.Threading.Thread.Sleep(15);

            var result2 = CacheController.Cache("test", 10, new Func<DateTime>(TestFunction));

            Assert.AreNotEqual(result, result2);

        }

        [TestMethod]
        public void TestMethod3()
        {
            var result = CacheController.Cache("test", 10, new Func<DateTime>(TestFunction));

            
            var result2 = CacheController.Cache("test1", 10, new Func<DateTime>(TestFunction));

            Assert.AreNotEqual(result, result2);

        }

        [TestMethod]
        public void TestMethod4()
        {
            var result = CacheController.Cache("test", 10, new Func<DateTime>(TestFunction));


            var result2 = CacheController.Cache("test1", 10, new Func<int>(TestFunction2));

            Assert.AreNotEqual(result, result2);
        }


        public DateTime TestFunction()
        {

            return DateTime.Now;
        }

        public int TestFunction2()
        {
            return 0;
        }
    }
}
