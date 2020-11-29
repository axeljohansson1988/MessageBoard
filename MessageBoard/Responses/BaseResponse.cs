using MessageBoard.API.Entities;

namespace MessageBoard.API.Responses
{
    public class BaseResponse
    {
        public BaseResponse()
        {
            this.StorageOperation = new StorageOperation();
        }

        public string Status { get; set; }
        public bool OperationSuccess { get; set; }
        public StorageOperation StorageOperation { get; set; }
    }
}
