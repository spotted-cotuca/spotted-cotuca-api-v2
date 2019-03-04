namespace SpottedCotuca.Application.Services.Definitions
{
    public class Result
    {
        public bool Success {
            get { return MetaError == null; }
        }

        public MetaError MetaError { get; set; }
    }

    public class Result<T> : Result
    {
        private T _obj;

        public T Obj {
            get { return _obj; }
            set {
                if (value != null)
                {
                    MetaError = null;
                }

                _obj = value;
            }
        }
    }
}
