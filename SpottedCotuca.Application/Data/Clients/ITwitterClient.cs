using System.Threading.Tasks;

namespace SpottedCotuca.Application.Data.Clients
{
    public interface ITwitterClient
    {
        Task<long> PublishTweet(string status);
        Task<bool> DestroyTweet(long id);
    }
}
