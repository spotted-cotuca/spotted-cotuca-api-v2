using System.Threading.Tasks;

namespace SpottedCotuca.Application.Data.Clients
{
    public interface IFacebookClient
    {
        Task<long> CreatePost(string message);
        Task<bool> DeletePost(long id);
    }
}
