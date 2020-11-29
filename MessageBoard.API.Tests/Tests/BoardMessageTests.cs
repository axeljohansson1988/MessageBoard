using MessageBoard.API.Providers;
using MessageBoard.API.Providers.Interfaces;
using MessageBoard.API.Services;
using MessageBoard.API.Services.Interfaces;
using MessageBoard.API.Tests.MockServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBoard.API.Tests.Tests
{
    [TestClass]
    public class BoardMessageTests
    {
        private static readonly IIdProvider idProvider = new IdProvider();
        private static readonly IClientService clientService = new ClientService(cacheService, idProvider);
        private static readonly ICacheService cacheService = new CacheService(new MemoryCacheMock());
        private static readonly IBoardMessageService boardMessageService = new BoardMessageService(cacheService, clientService, idProvider);

        [TestMethod]
        public void FirstMessageIdIs1()
        {
            var expected = 1;
            var firstId = idProvider.GetNextId(MockData.BoardMessageMocks.EmptyBoardMessagesList);
            Assert.AreEqual(expected, firstId);
        }

        [TestMethod]
        public void NextClientIdIs4()
        {
            var expected = 4;
            var nextId = idProvider.GetNextId(MockData.BoardMessageMocks.ThreeFirstBoardMessagesList);
            Assert.AreEqual(expected, nextId);
        }
    }
}
