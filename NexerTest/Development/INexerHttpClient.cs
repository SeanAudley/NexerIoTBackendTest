using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace NexerTest.Development.NexerHttpClient {
    public interface INexerHttpClient {
        Uri BaseUrl { get; }
        Task<HttpResponseMessage> GetAsync(string requestUri);
        Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content);
        Task<HttpResponseMessage> PutAsync(string requestUri, HttpContent content);
        Task<HttpResponseMessage> DeleteAsync(string requestUri);
    }
}
