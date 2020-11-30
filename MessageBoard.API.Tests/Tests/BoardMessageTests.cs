using MessageBoard.API.ErrorHandling;
using MessageBoard.API.Providers;
using MessageBoard.API.Providers.Interfaces;
using MessageBoard.API.Responses;
using MessageBoard.API.Services;
using MessageBoard.API.Services.Interfaces;
using MessageBoard.API.Tests.MockServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
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
        private static readonly ICacheService cacheService = new CacheService(new MemoryCacheMock());
        private static readonly IClientService clientService = new ClientService(cacheService, idProvider);
        private static readonly IBoardMessageService boardMessageService = new BoardMessageService(cacheService, clientService, idProvider);

        [TestMethod]
        public void FirstMessageIdIs1()
        {
            var expected = 1;
            var firstId = idProvider.GetNextId(MockData.BoardMessageMocks.EmptyBoardMessagesList);
            Assert.AreEqual(expected, firstId);
        }

        [TestMethod]
        public void NextMessageIdIs4()
        {
            var expected = 4;
            var nextId = idProvider.GetNextId(MockData.BoardMessageMocks.ThreeFirstBoardMessagesList);
            Assert.AreEqual(expected, nextId);
        }

        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public void SetBoardMessagetWithEmptyMessageThrowsHttpResponseException()
        {
            boardMessageService.SetBoardMessage(MockData.BoardMessageMocks.BoardMessageWithEmptyMessage);
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public void SetBoardMessageWithoutMessageThrowsHttpResponseException()
        {
            boardMessageService.SetBoardMessage(MockData.BoardMessageMocks.BoardMessageWithoutMessage);
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public void SetBoardMessageWithWhiteSpaceMessageThrowsHttpResponseException()
        {
            boardMessageService.SetBoardMessage(MockData.BoardMessageMocks.BoardMessageWithWhiteSpaceMessage);
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SetNullBoardMessageThrowsArgumentNullException()
        {
            boardMessageService.SetBoardMessage(null);
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public void SetBoardMessageWithNonExistantClientThrowsHttpResponseException()
        {
            boardMessageService.SetBoardMessage(MockData.BoardMessageMocks.BoardMessageWitNonExistantClient);
            Assert.Fail();
        }

        [TestMethod]
        public void SetValidBoardMessageReturnsValidBoardMessageResponse()
        {
            var validMessage = MockData.BoardMessageMocks.NewValidBoardMessage;
            var expected = new BoardMessageResponse()
            {
                BoardMessage = validMessage,
                OperationSuccess = true,
                StorageOperation = new Entities.StorageOperation()
                {
                    Id = Enums.StorageOperationEnum.Create,
                    Name = Constants.Constants.StorageOperations.Create
                }
            };
            var response = boardMessageService.SetBoardMessage(validMessage);

            // setting not tested properties
            expected.Status = response.Status;
            expected.BoardMessage.Created = response.BoardMessage.Created;

            // convert to json strings for comparison
            var jsonExpected = JsonConvert.SerializeObject(expected);
            var jsonResponse = JsonConvert.SerializeObject(response);
            Assert.AreEqual(jsonExpected, jsonResponse);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SetBoardMessageWithInvalidClientIdThrowsArgumentException()
        {
            boardMessageService.SetBoardMessage(MockData.BoardMessageMocks.BoardMessageWitInvalidClientId);
            Assert.Fail();
        }

        [TestMethod]
        public void UpdateValidBoardMessageReturnsValidBoardMessageResponse()
        {
            var existingMessage = MockData.BoardMessageMocks.ExistingValidBoardMessage;
            existingMessage.Message = "This message is modified and should be updated.";
            var expected = new BoardMessageResponse()
            {
                BoardMessage = existingMessage,
                OperationSuccess = true,
                StorageOperation = new Entities.StorageOperation()
                {
                    Id = Enums.StorageOperationEnum.Update,
                    Name = Constants.Constants.StorageOperations.Update
                }
            };
            var response = boardMessageService.UpdateBoardMessage(existingMessage);

            // setting not tested properties
            expected.Status = response.Status;
            expected.BoardMessage.Modified = response.BoardMessage.Modified;

            // convert to json strings for comparison
            var jsonExpected = JsonConvert.SerializeObject(expected);
            var jsonResponse = JsonConvert.SerializeObject(response);
            Assert.AreEqual(jsonExpected, jsonResponse);
        }
    }
}
