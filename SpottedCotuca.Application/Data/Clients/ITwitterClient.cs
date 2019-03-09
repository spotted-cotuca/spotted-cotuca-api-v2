using System;
using System.Threading.Tasks;

namespace SpottedCotuca.Application.Data.Clients
{
    public interface ITwitterClient
    {
        Task<long> PublishTweet(string status);
        Task<bool> DestroyTweet(long id);
    }

    public class TwitterClientException : Exception
    {
        public TwitterClientException() { }
        public TwitterClientException(string message) : base(message) { }
    }
}
