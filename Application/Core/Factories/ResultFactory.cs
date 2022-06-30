namespace Application.Core.Factories
{
    public static class ResultFactory
    {
        public static Result<T> CreateSuccessfulResult<T>(T value)
        {
            return new Result<T>{ IsSuccess = true, Value = value};
        }

        public static Result<T> CreateFailedResult<T>(string errorMessage)
        {
            return new Result<T> {IsSuccess = false, Error = errorMessage};
        }

    }
}