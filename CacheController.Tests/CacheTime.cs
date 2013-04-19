using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CacheController.Tests
{
    [TestClass]
    public class CacheTimeTest
    {
        [TestMethod]
        public void TestSeconds()
        {
            var cacheTime = CacheTime.GetCacheTime(10, CacheTimeType.Second);

            Assert.AreEqual(10000, cacheTime);
        }

        [TestMethod]
        public void TestSecondsZeron()
        {
            var cacheTime = CacheTime.GetCacheTime(0, CacheTimeType.Second);

            Assert.AreEqual(0, cacheTime);
        }

        [TestMethod]
        public void TestMilliseconds()
        {
            var cacheTime = CacheTime.GetCacheTime(100, CacheTimeType.Milliseconds);

            Assert.AreEqual(100, cacheTime);
        }

        [TestMethod]
        public void TestMillisecondsZero()
        {
            var cacheTime = CacheTime.GetCacheTime(0, CacheTimeType.Milliseconds);

            Assert.AreEqual(0, cacheTime);
        }

        [TestMethod]
        public void TestMinute()
        {
            var cacheTime = CacheTime.GetCacheTime(1, CacheTimeType.Minute);

            Assert.AreEqual(60000, cacheTime);
        }

        [TestMethod]
        public void TestMinuteZero()
        {
            var cacheTime = CacheTime.GetCacheTime(0, CacheTimeType.Minute);

            Assert.AreEqual(0, cacheTime);
        }

        [TestMethod]
        public void TestHour()
        {
            var cacheTime = CacheTime.GetCacheTime(1, CacheTimeType.Hour);

            Assert.AreEqual(3600000, cacheTime);
        }

        [TestMethod]
        public void TestMHourZero()
        {
            var cacheTime = CacheTime.GetCacheTime(0, CacheTimeType.Hour);

            Assert.AreEqual(0, cacheTime);
        }

        [TestMethod]
        public void TestHourLarge()
        {
            var cacheTime = CacheTime.GetCacheTime(1000, CacheTimeType.Hour);

            Assert.AreEqual(3600000000, cacheTime);
        }

    }
}
