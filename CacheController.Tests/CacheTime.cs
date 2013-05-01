using NUnit.Framework;


namespace CacheController.Tests
{
    [TestFixture]
    public class CacheTimeTest
    {
        [Test]
        public void TestSeconds()
        {
            var cacheTime = CacheTime.GetCacheTime(10, CacheTimeType.Second);

            Assert.AreEqual(10000, cacheTime);
        }

        [Test]
        public void TestSecondsZeron()
        {
            var cacheTime = CacheTime.GetCacheTime(0, CacheTimeType.Second);

            Assert.AreEqual(0, cacheTime);
        }

        [Test]
        public void TestMilliseconds()
        {
            var cacheTime = CacheTime.GetCacheTime(100, CacheTimeType.Milliseconds);

            Assert.AreEqual(100, cacheTime);
        }

        [Test]
        public void TestMillisecondsZero()
        {
            var cacheTime = CacheTime.GetCacheTime(0, CacheTimeType.Milliseconds);

            Assert.AreEqual(0, cacheTime);
        }

        [Test]
        public void TestMinute()
        {
            var cacheTime = CacheTime.GetCacheTime(1, CacheTimeType.Minute);

            Assert.AreEqual(60000, cacheTime);
        }

        [Test]
        public void TestMinuteZero()
        {
            var cacheTime = CacheTime.GetCacheTime(0, CacheTimeType.Minute);

            Assert.AreEqual(0, cacheTime);
        }

        [Test]
        public void TestHour()
        {
            var cacheTime = CacheTime.GetCacheTime(1, CacheTimeType.Hour);

            Assert.AreEqual(3600000, cacheTime);
        }

        [Test]
        public void TestMHourZero()
        {
            var cacheTime = CacheTime.GetCacheTime(0, CacheTimeType.Hour);

            Assert.AreEqual(0, cacheTime);
        }

        [Test]
        public void TestHourLarge()
        {
            var cacheTime = CacheTime.GetCacheTime(1000, CacheTimeType.Hour);

            Assert.AreEqual(3600000000, cacheTime);
        }

    }
}
