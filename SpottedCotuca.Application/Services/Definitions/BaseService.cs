namespace SpottedCotuca.Application.Services.Definitions
{
    public abstract class BaseService
    {
        public Result Success()
        {
            return new Result();
        }

        public Result<T> Success<T>(T obj)
        {
            return new Result<T>() { Obj = obj };
        }

        public Result Error(MetaError metaError)
        {
            return new Result() { MetaError = metaError };
        }

        public Result<T> Error<T>(MetaError metaError)
        {
            return new Result<T>() { MetaError = metaError, Obj = default(T) };
        }

        protected bool IsValid(string value, int minLength, int maxLegth, MetaError errorForNull, MetaError errorForLength, out Result result)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                result = Error(errorForNull);
            }
            else if (value.Length < minLength || value.Length > maxLegth)
            {
                result = Error(errorForLength);
            }
            else
            {
                result = null;
            }

            return result == null;
        }
    }
}
