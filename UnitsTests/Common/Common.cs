using System;
using System.Net.Http;
using System.Threading.Tasks;
using NexerTest.Development;
using NexerTest.Development.NexerHttpClient;

namespace NexerTest.Infrastructure.FhirHttpClient
{
    public class NexerHttpClient : INexerHttpClient
    {

        private readonly HttpClient _http;
        //private readonly IAuthenticationProvider _auth;

        public Uri BaseUrl { get { return _http.BaseAddress; } }

        public NexerHttpClient(HttpClient http)
        {

            _http = http;
            //_auth = auth;
        }

        public async Task<HttpResponseMessage> DeleteAsync(string requestUri)
        {
            //_ = await _auth.SetBearerToken(_http);
            return await _http.DeleteAsync(requestUri);
        }

        public async Task<HttpResponseMessage> GetAsync(string requestUri)
        {
            //_ = await _auth.SetBearerToken(_http);
            return await _http.GetAsync(requestUri);
        }

        public async Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content)
        {
            //_ = await _auth.SetBearerToken(_http);
            return await _http.PostAsync(requestUri, content);
        }

        public async Task<HttpResponseMessage> PutAsync(string requestUri, HttpContent content)
        {
            //_ = await _auth.SetBearerToken(_http);
            return await _http.PutAsync(requestUri, content);
        }
    }
}

