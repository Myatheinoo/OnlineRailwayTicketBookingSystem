namespace Database.DTO
{
    public class ResponseModel
    {
        public ResponseModel(string message, bool isSuccess, object data = null!)
        {
            IsSuccess = isSuccess;
            Message = message;
            Data = data;
        }

        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
