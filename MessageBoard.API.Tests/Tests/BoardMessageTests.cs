using MessageBoard.API.Entities;
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

        #region IdTests

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

        #endregion

        #region SetBoardMessage

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

        #endregion

        #region UpdateBoardMessage

        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public void UpdateBoardMessageWithInvalidIdThrowsHttpResponseException()
        {
            boardMessageService.UpdateBoardMessage(new BoardMessage()
            {
                Id = -42,
                Message = "some message.",
                ClientId = MockData.ClientMocks.ExistingValidClient.Id
            });
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateBoardMessageWithInvalidClientIdThrowsArgumentException()
        {
            boardMessageService.UpdateBoardMessage(MockData.BoardMessageMocks.BoardMessageWitInvalidClientId);
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public void UpdateBoardMessagetWithEmptyMessageThrowsHttpResponseException()
        {
            boardMessageService.UpdateBoardMessage(MockData.BoardMessageMocks.BoardMessageWithEmptyMessage);
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public void UpdateBoardMessageWithoutMessageThrowsHttpResponseException()
        {
            boardMessageService.UpdateBoardMessage(MockData.BoardMessageMocks.BoardMessageWithoutMessage);
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public void UpdateBoardMessageWithWhiteSpaceMessageThrowsHttpResponseException()
        {
            boardMessageService.UpdateBoardMessage(MockData.BoardMessageMocks.BoardMessageWithWhiteSpaceMessage);
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public void UpdateBoardMessageWithOtherClientsIdThrowsHttpResponseException()
        {
            var existingBoardMessageWithOtherClientId = new BoardMessage()
            {
                Id = MockData.BoardMessageMocks.ExistingValidBoardMessage.Id,
                ClientId = MockData.BoardMessageMocks.ExistingValidBoardMessage.ClientId + 1,
                Created = MockData.BoardMessageMocks.ExistingValidBoardMessage.Created,
                Message = MockData.BoardMessageMocks.ExistingValidBoardMessage.Message
            };

            boardMessageService.UpdateBoardMessage(existingBoardMessageWithOtherClientId);
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UpdateNullBoardMessageThrowsArgumentNullException()
        {
            boardMessageService.UpdateBoardMessage(null);
            Assert.Fail();
        }

        [TestMethod]
        public void UpdateValidBoardMessageReturnsValidBoardMessageResponse()
        {
            var existingBoardMessageWithUpdatedMessage = new BoardMessage()
            {
                Id = MockData.BoardMessageMocks.ExistingValidBoardMessage.Id,
                Message = "This message is modified and should be updated.",
                ClientId = MockData.BoardMessageMocks.ExistingValidBoardMessage.ClientId,
                Created = MockData.BoardMessageMocks.ExistingValidBoardMessage.Created
            };
            var expected = new BoardMessageResponse()
            {
                BoardMessage = existingBoardMessageWithUpdatedMessage,
                OperationSuccess = true,
                StorageOperation = new Entities.StorageOperation()
                {
                    Id = Enums.StorageOperationEnum.Update,
                    Name = Constants.Constants.StorageOperations.Update
                }
            };
            var response = boardMessageService.UpdateBoardMessage(existingBoardMessageWithUpdatedMessage);

            // setting not tested properties
            expected.Status = response.Status;
            expected.BoardMessage.Modified = response.BoardMessage.Modified;

            // convert to json strings for comparison
            var jsonExpected = JsonConvert.SerializeObject(expected);
            var jsonResponse = JsonConvert.SerializeObject(response);
            Assert.AreEqual(jsonExpected, jsonResponse);
        }

        #endregion
    }
}
