using MessageBoard.API.Entities;
using MessageBoard.API.Enums;

namespace MessageBoard.API.Responses
{
    public class BoardMessageResponse : BaseResponse
    {
        public BoardMessage BoardMessage { get; set; }
    }
}
