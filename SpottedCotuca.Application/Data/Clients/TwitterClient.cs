using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SpottedCotuca.Application.Data.Clients
{
    public class TwitterClient : ITwitterClient
    {
        private readonly string _twitterApiUrl = "https://api.twitter.com/1.1/";
        private readonly TwitterAuthCredentials _authCredentials;
        private readonly HMACSHA1 _sigHasher;
        private readonly DateTime _epochUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public TwitterClient(TwitterAuthCredentials authCredentials)
        {
            _authCredentials = authCredentials;

            _sigHasher = new HMACSHA1(
                new ASCIIEncoding().GetBytes($"{_authCredentials.ConsumerSecret}&{_authCredentials.AccessTokenSecret}")
            );
        }

        // TODO: Handle exceptions and return Tweet ID
        public async Task<long> PublishTweet(string status)
        {
            throw new NotImplementedException();

            var data = new Dictionary<string, string> {
                { "status", status },
                { "trim_user", "0" }
            };

            var response = await SendRequest("statuses/update.json", data);

            if (response.IsSuccessStatusCode)
                // return response.Content.ToString();

            throw new Exception(response.Content.ToString());
        }

        // TODO: Handle exceptions
        public async Task<bool> DestroyTweet(string id)
        {
            var data = new Dictionary<string, string>
            {
                { "trim_user", "0" }
            };

            var response = await SendRequest($"statuses/destroy/{id}.json", data);

            if (response.IsSuccessStatusCode)
                return true;

            throw new Exception(response.Content.ToString());
        }

        async Task<HttpResponseMessage> SendRequest(string url, Dictionary<string, string> data)
        {
            var fullUrl = _twitterApiUrl + url;

            var timestamp = (int)((DateTime.UtcNow - _epochUtc).TotalSeconds);

            data.Add("oauth_consumer_key", _authCredentials.ConsumerKey);
            data.Add("oauth_signature_method", "HMAC-SHA1");
            data.Add("oauth_timestamp", timestamp.ToString());
            data.Add("oauth_nonce", Guid.NewGuid().ToString());
            data.Add("oauth_token", _authCredentials.AccessToken);
            data.Add("oauth_version", "1.0");

            data.Add("oauth_signature", GenerateSignature(fullUrl, data));

            string oAuthHeader = GenerateOAuthHeader(data);

            var formData = new FormUrlEncodedContent(data.Where(kvp => !kvp.Key.StartsWith("oauth_")));

            return await SendRequest(fullUrl, oAuthHeader, formData);
        }

        string GenerateSignature(string url, Dictionary<string, string> data)
        {
            var sigString = string.Join(
                "&",
                data
                    .Union(data)
                    .Select(kvp => string.Format("{0}={1}", Uri.EscapeDataString(kvp.Key), Uri.EscapeDataString(kvp.Value)))
                    .OrderBy(s => s)
            );

            var fullSigData = string.Format(
                "{0}&{1}&{2}",
                "POST",
                Uri.EscapeDataString(url),
                Uri.EscapeDataString(sigString.ToString())
            );

            return Convert.ToBase64String(_sigHasher.ComputeHash(new ASCIIEncoding().GetBytes(fullSigData.ToString())));
        }

        string GenerateOAuthHeader(Dictionary<string, string> data)
        {
            return "OAuth " + string.Join(
                ", ",
                data
                    .Where(kvp => kvp.Key.StartsWith("oauth_"))
                    .Select(kvp => string.Format("{0}=\"{1}\"", Uri.EscapeDataString(kvp.Key), Uri.EscapeDataString(kvp.Value)))
                    .OrderBy(s => s)
            );
        }

        async Task<HttpResponseMessage> SendRequest(string fullUrl, string oAuthHeader, FormUrlEncodedContent formData)
        {
            using (var http = new HttpClient())
            {
                http.DefaultRequestHeaders.Add("Authorization", oAuthHeader);

                var response = await http.PostAsync(fullUrl, formData);

                if (response.IsSuccessStatusCode)
                {
                    return response;
                }

                throw new Exception(response.Content.ToString());
            }
        }

        public Task<bool> DestroyTweet(long id)
        {
            throw new NotImplementedException();
        }
    }

    public class TwitterAuthCredentials
    {
        public string ConsumerKey { get; set; }
        public string ConsumerSecret { get; set; }
        public string AccessToken { get; set; }
        public string AccessTokenSecret { get; set; }
    }
}
