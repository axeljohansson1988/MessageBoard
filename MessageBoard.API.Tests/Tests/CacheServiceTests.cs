using Microsoft.Extensions.Caching.Memory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MessageBoard.API.Entities;
using MessageBoard.API.Services;
using MessageBoard.API.Services.Interfaces;
using MessageBoard.API.Tests.MockServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBoard.API.Tests.Tests
{
    [TestClass]
    public class CacheServiceTests
    {
        private static readonly ICacheService cacheService = new CacheService(new MemoryCacheMock());

        [TestMethod]
        public void GetFromCacheWithNoCacheKeyReturnsDefaultValue()
        {
            object expected = default;
            var valueFromCache = cacheService.Get<object>(null);
            Assert.AreEqual(expected, valueFromCache);
        }

        [TestMethod]
        public void GetFromCacheWithEmptyCacheKeyReturnsDefaultValue()
        {
            object expected = default;
            var valueFromCache = cacheService.Get<object>(string.Empty);
            Assert.AreEqual(expected, valueFromCache);
        }

        [TestMethod]
        public void GetFromCacheWithWhiteSpaceCacheKeyReturnsDefaultValue()
        {
            object expected = default;
            var valueFromCache = cacheService.Get<object>("   ");
            Assert.AreEqual(expected, valueFromCache);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void StoreObjectWithNoCacheKeyThrowsArgumentNullException()
        {
            cacheService.Store<object>(null, new object());
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void StoreObjectWithEmptyCacheKeyThrowsArgumentNullException()
        {
            cacheService.Store<object>(string.Empty, new object());
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void StoreObjectWithWhiteSpaceCacheKeyThrowsArgumentNullException()
        {
            cacheService.Store<object>("   ", new object());
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void StoreNullObjectThrowsArgumentNullException()
        {
            cacheService.Store<object>("anyKey", null);
            Assert.Fail();
        }
    }
}
