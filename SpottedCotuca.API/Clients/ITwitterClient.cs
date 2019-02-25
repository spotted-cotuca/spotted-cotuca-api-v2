using System.Threading.Tasks;

namespace SpottedCotuca.API.Clients
{
    public interface ITwitterClient
    {
        Task<long> PublishTweet(string status);
        Task<bool> DestroyTweet(long id);
    }
}
