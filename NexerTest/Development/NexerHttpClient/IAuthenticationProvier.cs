using System.Net.Http;
using System.Threading.Tasks;

namespace NexerTest.Development.FhirHttpClient {
    public interface IAuthenticationProvider {
        Task<bool> SetBearerToken(HttpClient requestHttpClient);
    }
}
