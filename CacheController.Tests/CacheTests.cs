using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;


namespace CacheController.Tests
{
    [TestFixture]
    public class CacheTests
    {
        private ICachingService _cachingService;
        

        [SetUp]
        public void Init()
        {
            _cachingService = new CachingService();
            _cachingService.DeleteAll();
        }

        [Test]
        public void CacheReturnsSameDateTime()
        {
            var result = _cachingService.Cache("test", 100, new Func<DateTime>(TestFunction));

            var result2 = _cachingService.Cache("test", 100, new Func<DateTime>(TestFunction));

            Assert.AreEqual(result, result2);

        }

        [Test]
        public void CacheWithGenerics()
        {
            var result = _cachingService.Cache<DateTime>("test", 1, CacheTimeType.Second, new Func<DateTime>(TestFunction));

            var result2 = _cachingService.Cache<DateTime>("test", 1, CacheTimeType.Second, new Func<DateTime>(TestFunction));

            Assert.AreEqual(result, result2);
        }

        [Test]
        public void CacheWithGenericsCacheTimeType()
        {
            var result = _cachingService.Cache<DateTime>("test", 1, CacheTimeType.Second, new Func<DateTime>(TestFunction));

            System.Threading.Thread.Sleep(2000);
            var result2 = _cachingService.Cache<DateTime>("test", 1, CacheTimeType.Second, new Func<DateTime>(TestFunction));

            Assert.AreNotEqual(result, result2);
        }

        [Test]
        public void CacheWithoutCacheTimeType()
        {
            var result = _cachingService.Cache("test", 1, CacheTimeType.Second, new Func<DateTime>(TestFunction));

            System.Threading.Thread.Sleep(2000);
            var result2 = _cachingService.Cache("test", 1, CacheTimeType.Second, new Func<DateTime>(TestFunction));

            Assert.AreNotEqual(result, result2);
        }

        [Test]
        public void UpdateCacheWithCacheTime()
        {
            var currentDate = TestFunction();
            _cachingService.UpdateCacheForKey("test", 1, CacheTimeType.Second, currentDate);

            //System.Threading.Thread.Sleep(2000);
            var result2 = _cachingService.Cache("test", 1, CacheTimeType.Second, new Func<DateTime>(TestFunction));

            Assert.AreEqual(currentDate, result2);
        }

        [Test]
        public void UpdateCacheWithCacheTimeExpiring()
        {
            var currentDate = TestFunction();
            _cachingService.UpdateCacheForKey("test", 1, CacheTimeType.Second, currentDate);

            System.Threading.Thread.Sleep(2000);
            var result2 = _cachingService.Cache("test", 1, CacheTimeType.Second, new Func<DateTime>(TestFunction));

            Assert.AreNotEqual(currentDate, result2);
        }
        [Test]
        public void CacheDoesntReturnSameDateTimeAfterCachePeriod()
        {
            var result = _cachingService.Cache("test", 10, new Func<DateTime>(TestFunction));

            System.Threading.Thread.Sleep(15);

            var result2 = _cachingService.Cache("test", 10, new Func<DateTime>(TestFunction));

            Assert.AreNotEqual(result, result2);

        }

        [Test]
        public void CachingReturnsDifferentResultForDifferentKeys()
        {
            var result = _cachingService.Cache("test", 10, new Func<DateTime>(TestFunction));

            System.Threading.Thread.Sleep(5);
            var result2 = _cachingService.Cache("test1", 10, new Func<DateTime>(TestFunction));

            Assert.AreNotEqual(result, result2);

        }

        [Test]
        public void CachingReturnsDifferentResultForDifferentKeys2()
        {
            var result = _cachingService.Cache("test", 10, new Func<DateTime>(TestFunction));


            var result2 = _cachingService.Cache("test1", 10, new Func<int>(TestFunction2));

            Assert.AreNotEqual(result, result2);
        }

        [Test]
        public void CacheReturnsDifferentAmountAfterCacheKeyDeleted()
        {
            var result = _cachingService.Cache("test", 100, new Func<DateTime>(TestFunction));

            _cachingService.DeleteCache("test");
            System.Threading.Thread.Sleep(5);
            var result2 = _cachingService.Cache("test", 100, new Func<DateTime>(TestFunction));

            Assert.AreNotEqual(result, result2);

        }

        [Test]
        public void CacheReturnsDifferentAmountAfterCacheCompletelyDeleted()
        {
            var result = _cachingService.Cache("test", 30, new Func<DateTime>(TestFunction));
            _cachingService.DeleteAll();
            System.Threading.Thread.Sleep(5);
            var result2 = _cachingService.Cache("test", 30, new Func<DateTime>(TestFunction));

            Assert.AreNotEqual(result, result2);

        }

        [Test]
        public void CacheResultIsUpdatedWithNewResult()
        {
            var result = _cachingService.Cache("test", 10, new Func<int>(TestFunction2));


            _cachingService.UpdateCacheForKey("test", 20, 1);
            var result2 = _cachingService.Cache("test", 10, new Func<int>(TestFunction2));

            Assert.AreNotEqual(result, result2);
            Assert.AreEqual(1,result2);
        }


        [Test]
        public void CacheReturnNullIfDelegateIsNull()
        {
            var result = _cachingService.Cache("test7", 10, null);


                        
            Assert.AreEqual(null,result);
        }


        [Test]
        [ExpectedException(typeof(CacheDelegateMethodException))]
        public void CacheThrowsExceptionIfDelegateDoes()
        {
             _cachingService.Cache("test", 10, new Func<int>(Test3));            
        }

        [Test]
        public void CacheReturnNullIfDelegateResultIsNull()
        {
            var result = _cachingService.Cache("test7", 10, new Func<List<string>>(TestNull));
            
            Assert.IsNull(result);
        }

        [Test]
        public void GetCacheWithNoValue()
        {
            var result = _cachingService.GetCacheValue("Test");

            Assert.IsNull(result);
        }

        [Test]
        public void GetCacheWithValue()
        {
            _cachingService.UpdateCacheForKey("Test",1000, "CacheTest");
            var result = _cachingService.GetCacheValue("Test") as String;
            Assert.AreEqual("CacheTest",result);
        }

        [Test]
        public void GetCacheWithValueGenerics()
        {
            _cachingService.UpdateCacheForKey("Test", 1000, "CacheTest");
            var result = _cachingService.GetCacheValue<String>("Test");
            Assert.AreEqual("CacheTest", result);
        }


        [Test]
        public void GetAllCacheKeys()
        {
            _cachingService.UpdateCacheForKey("Test", 1000, "CacheTest");
            _cachingService.UpdateCacheForKey("Test2", 1000, "CacheTest");
            var keys = _cachingService.CacheKeys.ToList();

            Assert.IsTrue(keys.Any());
            Assert.AreEqual(keys.Count(), 2);

            Assert.AreEqual(keys.Single(x => x == "Test"), "Test");
            Assert.AreEqual(keys.Single(x => x == "Test2"), "Test2");
        }

        

        public DateTime TestFunction()
        {

            return DateTime.Now;
        }

        public int TestFunction2()
        {
            return 0;
        }

        public int Test3()
        {
            throw new NullReferenceException();
        }

        public List<string> TestNull()
        {
            return null;
        }
    }
}
