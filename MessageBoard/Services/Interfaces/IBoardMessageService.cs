using MessageBoard.API.Entities;
using MessageBoard.API.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageBoard.API.Services.Interfaces
{
    public interface IBoardMessageService
    {
        List<BoardMessage> GetBoardMessages(bool latestFirst = true);
        List<BoardMessage> GetBoardMessages(int? clientId, bool latestFirst = true);
        BoardMessageResponse SetBoardMessage(BoardMessage boardMessage);
        BoardMessageResponse UpdateBoardMessage(BoardMessage boardMessage);
        BoardMessageResponse DeleteBoardMessage(int? boardMessageId, int? clientId);
    }
}
