namespace E_Commerce_api.DTO
{
    public class BaseResponse<T>
    {
        public string? devErrorMessage { get; set; }
        public T? data { get; }
        public bool isSuccess { get; set; }
        
        private BaseResponse(bool isSuccess, string? devErrorMessage, T? data)
        {
            this.isSuccess = isSuccess;
            this.devErrorMessage = devErrorMessage;
            this.data = data;
        }

        public static BaseResponse<T> Success(T data)
        {
            return new BaseResponse<T>(true, null, data);
        }

        public static BaseResponse<T> Success(string devErrorMessage)
        {
            return new BaseResponse<T>(false, devErrorMessage, default);
        }

        public static BaseResponse<T> Failure(string errorMessage) => new(false, errorMessage, default);
    }
}
