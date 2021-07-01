namespace SharedKernel
{
    public class Result
    {
        protected Result(bool isError, string message)
        {
            IsError = isError;
            Message = message;
        }

        public bool IsSuccess => !IsError;
        public bool IsError { get; }
        public string Message { get; }

        public static Result CreateValidResult() => new Result(false, "OK");
        public static Result CreateInvalidResult(string message) => new Result(true, message);
    }

    public class Result<T> : Result
    {
        private Result(T data) : base(false, "OK")
        {
            Data = data;
        }

        private Result(string message) : base(true, message)
        { }

        public T Data { get; }

        public static Result<T> CreateValidResult(T data) => new Result<T>(data);
        public static Result<T> CreateInvalidResult(string message) => new Result<T>(message);
    }
}
