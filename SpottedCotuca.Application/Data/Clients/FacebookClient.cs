using System;
using System.Threading.Tasks;

namespace SpottedCotuca.Application.Data.Clients
{
    public class FacebookPageClient : IFacebookClient
    {
        private string _pageId;
        private string _accessToken;

        public FacebookPageClient(string pageId, string accessToken)
        {
            _pageId = pageId;
            _accessToken = accessToken;
        }

        public async Task<long> CreatePost(string message)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeletePost(long id)
        {
            throw new NotImplementedException();
        }
    }
}
