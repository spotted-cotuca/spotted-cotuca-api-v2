using System.Threading.Tasks;

namespace SpottedCotuca.API.Clients
{
    public interface IFacebookClient
    {
        Task<long> CreatePost(string message);
        Task<bool> DeletePost(long id);
    }
}
