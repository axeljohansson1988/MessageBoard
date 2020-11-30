using MessageBoard.API.Entities;
using MessageBoard.API.ErrorHandling;
using MessageBoard.API.Extensions;
using MessageBoard.API.Providers.Interfaces;
using MessageBoard.API.Responses;
using MessageBoard.API.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageBoard.API.Services
{
    public class BoardMessageService : IBoardMessageService
    {
        private readonly ICacheService cacheService;
        private readonly IClientService clientService;
        private readonly IIdProvider idProvider;
        public BoardMessageService(ICacheService cacheService, IClientService clientService, IIdProvider idProvider)
        {
            this.cacheService = cacheService;
            this.clientService = clientService;
            this.idProvider = idProvider;
        }

        public BoardMessageResponse DeleteBoardMessage(int? boardMessageId, int? clientId)
        {
            if (!boardMessageId.IsValidId())
            {
                throw new ArgumentException($"{boardMessageId} is not a valid {nameof(boardMessageId)}.");
            }

            if (!clientId.IsValidId())
            {
                throw new ArgumentException($"{clientId} is not a valid {nameof(clientId)}.");
            }

            var response = new BoardMessageResponse()
            {
                BoardMessage = null,
                StorageOperation = new StorageOperation()
                {
                    Id = Enums.StorageOperationEnum.Delete,
                    Name = Constants.Constants.StorageOperations.Delete
                }
            };

            var messageList = this.GetBoardMessages();
            var messageToDelete = messageList.FirstOrDefault(message => message.Id == boardMessageId);
            if (messageToDelete == null)
            {
                throw new Exception($"Cannot delete board message with id: {boardMessageId} => message does not exist.");
            }

            if (messageToDelete.ClientId != clientId)
            {
                throw new Exception($"Cannot delete board message with id: {boardMessageId} => message is not created by client with id: {clientId}.");
            }

            messageList = messageList.Where(message => message.Id != boardMessageId).ToList();
            this.cacheService.Store<List<BoardMessage>>(Constants.Constants.CacheKeys.AllBoardMessages, messageList);
            response.Status = $"Successfully deleted board message with id: {boardMessageId}";
            return response;
        }

        public List<BoardMessage> GetBoardMessages(int? clientId, bool latestFirst = true)
        {
            if (!clientId.IsValidId())
            {
                throw new ArgumentException($"{clientId} is not a valid {nameof(clientId)}.");
            }

            if (!this.clientService.ClientExists(clientId))
            {
                throw new ArgumentException($"{nameof(clientId)} {clientId} does not exist. Failed to get messages.");
            }

            var messages = this.GetBoardMessages(latestFirst);
            foreach (var message in messages)
            {
                message.IsMine = message.ClientId == clientId;
            }

            return messages;
        }

        public List<BoardMessage> GetBoardMessages(bool latestFirst = true)
        {
            var messages = this.cacheService.Get<List<BoardMessage>>(Constants.Constants.CacheKeys.AllBoardMessages);
            if (messages == null)
            {
                return new List<BoardMessage>();
            }

            if (latestFirst)
            {
                return messages.OrderByDescending(message => message.Created).ToList();
            }

            return messages.OrderBy(message => message.Created).ToList();
        }

        public BoardMessageResponse SetBoardMessage(BoardMessage boardMessage)
        {
            if (boardMessage == null)
            {
                throw new ArgumentNullException(nameof(boardMessage));
            }

            var response = this.ValidateBoardMessage(boardMessage);
            response.StorageOperation = new StorageOperation()
            {
                Id = Enums.StorageOperationEnum.Create,
                Name = Constants.Constants.StorageOperations.Create
            };

            if (!response.OperationSuccess)
            {
                response.BoardMessage = boardMessage;
                throw new HttpResponseException(response);
            }

            var messages = this.GetBoardMessages();
            boardMessage.Id = this.idProvider.GetNextId(messages);
            boardMessage.Created = DateTime.UtcNow;
            boardMessage.Modified = null;
            boardMessage.IsMine = null;
            messages.Add(boardMessage);
            this.cacheService.Store(Constants.Constants.CacheKeys.AllBoardMessages, messages);
            response.BoardMessage = boardMessage;
            response.Status = $"Successfully stored board message with id: {boardMessage.Id}";
            return response;
        }

        public BoardMessageResponse UpdateBoardMessage(BoardMessage boardMessage)
        {
            if (boardMessage == null)
            {
                throw new ArgumentNullException(nameof(boardMessage));
            }

            var response = this.ValidateBoardMessage(boardMessage);
            response.StorageOperation = new StorageOperation()
            {
                Id = Enums.StorageOperationEnum.Update,
                Name = Constants.Constants.StorageOperations.Update
            };
            if (!response.OperationSuccess)
            {
                response.BoardMessage = boardMessage;
                throw new HttpResponseException(response);
            }

            var messageList = this.GetBoardMessages();
            var messageToUpdate = messageList.FirstOrDefault(message => message.Id == boardMessage.Id);
            if (messageToUpdate == null)
            {
                throw new HttpResponseException(response, $"Cannot update board message with id: {boardMessage.Id} => message does not exist.");
            }

            if (messageToUpdate.ClientId != boardMessage.ClientId)
            {
                throw new HttpResponseException(response, $"Cannot update board message with id: {messageToUpdate.Id} => message is not created by client with id: {boardMessage.ClientId}.");
            }

            messageList = messageList.Where(message => message.Id != messageToUpdate.Id).ToList();
            messageToUpdate.Modified = DateTime.UtcNow;
            messageToUpdate.Message = boardMessage.Message;
            messageList.Add(messageToUpdate);
            this.cacheService.Store<List<BoardMessage>>(Constants.Constants.CacheKeys.AllBoardMessages, messageList);
            response.BoardMessage = messageToUpdate;
            response.Status = $"Successfully updated board message with id: {messageToUpdate.Id}";
            return response;
        }

        private BoardMessageResponse ValidateBoardMessage(BoardMessage boardMessage)
        {
            var response = new BoardMessageResponse();
            if (boardMessage == null)
            {
                response.OperationSuccess = false;
                response.Status = "Boardmessage is null.";
                return response;
            }

            if (string.IsNullOrWhiteSpace(boardMessage.Message))
            {
                response.OperationSuccess = false;
                response.Status = "Boardmessage.Message cannot be empty";
                return response;
            }

            if (!this.clientService.ClientExists(boardMessage.ClientId))
            {
                response.OperationSuccess = false;
                response.Status = "BoardMessage.ClientId is not a valid clientId, the client does not exist.";
                return response;
            }

            response.OperationSuccess = true;
            response.Status = "BoardMessage is valid.";
            return response;
        }
    }
}
