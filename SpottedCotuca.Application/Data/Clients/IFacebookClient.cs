using System;
using System.Threading.Tasks;

namespace SpottedCotuca.Application.Data.Clients
{
    public interface IFacebookClient
    {
        Task<long> CreatePost(string message);
        Task<bool> DeletePost(long id);
    }

    public class FacebookClientException : Exception
    {
        public FacebookClientException() { }
        public FacebookClientException(string message) : base(message) { }
    }
}
