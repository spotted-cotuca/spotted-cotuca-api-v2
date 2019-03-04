namespace SpottedCotuca.Application.Services.Definitions
{
    public class MetaError
    {
        public int? StatusCode { get; set; }
        public string Error { get; set; }

        public MetaError(int? statusCode, string error)
        {
            StatusCode = statusCode;
            Error = error;
        }
    }
}
