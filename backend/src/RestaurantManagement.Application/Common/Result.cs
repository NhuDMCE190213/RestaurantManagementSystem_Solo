namespace RestaurantManagement.Application.Common
{
    public sealed class Result<T>
    {
        public bool IsSuccess { get; }
        public T? Value { get; }
        public string? Message { get; }
        public string? ErrorCode { get; }

        private Result(bool isSuccess, T? value, string? errorCode = null, string? message = null)
        {
            IsSuccess = isSuccess;
            Value = value;
            ErrorCode = errorCode;
            Message = message;
        }

        public static Result<T> Success(T value, string? message = null) => new Result<T>(true, value, null, message);
        public static Result<T> Failure(string errorCode, string message) => new Result<T>(false, default, errorCode, message);
    }
}
