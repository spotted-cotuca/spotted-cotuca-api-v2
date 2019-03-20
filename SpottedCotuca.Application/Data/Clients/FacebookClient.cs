using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SpottedCotuca.Application.Data.Clients
{
    public class FacebookClient : IFacebookClient
    {
        private readonly string _facebookApiUrl = "https://graph.facebook.com/";
        private string _pageId;
        private string _accessToken;

        public FacebookClient(string pageId, string accessToken)
        {
            _pageId = pageId;
            _accessToken = accessToken;
        }

        public async Task<long> CreatePost(string message)
        {
            var data = new Dictionary<string, string> {
                { "message", message }
            };

            var response = await SendPostRequest("feed", data);

            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<FacebookResponse>(response.Content.ToString()).Id.ToFacebookPostId();

            throw new FacebookClientException(response.Content.ToString());
        }

        public async Task<bool> DeletePost(long id)
        {
            var response = await SendDeleteRequest($"{id}");

            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<FacebookResponse>(response.Content.ToString()).Success;

            throw new FacebookClientException(response.Content.ToString());
        }

        private async Task<HttpResponseMessage> SendPostRequest(string url, Dictionary<string, string> data)
        {
            var fullUrl = $"{_facebookApiUrl}{_pageId}/{url}";

            data.Add("access_token", _accessToken);

            var formData = new FormUrlEncodedContent(data);

            using (var http = new HttpClient())
            {
                return await http.PostAsync(fullUrl, formData);
            }
        }

        private async Task<HttpResponseMessage> SendDeleteRequest(string url)
        {
            var fullUrl = $"{_facebookApiUrl}{_pageId}_{url}?access_token={_accessToken}";

            using (var http = new HttpClient())
            {
                return await http.DeleteAsync(fullUrl);
            }
        }
    }

    internal static class FacebookClientExtensions
    {
        public static long ToFacebookPostId(this string pagePostId)
        {
            return Convert.ToInt64(pagePostId.Substring(pagePostId.IndexOf('_') + 1));
        }
    }

    internal class FacebookResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("sucess")]
        public bool Success { get; set; }
    }
}
