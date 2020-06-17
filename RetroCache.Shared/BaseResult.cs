    namespace RetroCache.Shared
{
    public class BaseResult
    {
        public bool HasError { get; set; }
        public string ErrorMessage { get; set; }

        public BaseResult()
        {
            HasError = false;
            ErrorMessage = string.Empty;
        }

        public BaseResult(string errorMessage)
        {
            HasError = true;
            ErrorMessage = errorMessage;
        }
    }

    public class BaseResult<T>
    {
        public bool HasError { get; set; }
        public string ErrorMessage { get; set; }
        public T Data { get; set; }

        public BaseResult(T data)
        {
            HasError = false;
            ErrorMessage = string.Empty;
            Data = data;
        }

        public BaseResult(string errorMessage)
        {
            HasError = true;
            ErrorMessage = errorMessage;
        }
    }
}
