using System.Net;
using System.Runtime.CompilerServices;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using NexerTest.Development;
using Moq;
using Newtonsoft.Json;
using System.IO;
using Microsoft.Extensions.Logging;
using NexerTest.Development.NexerHttpClient;

namespace NexerTest.Application.UnitTests.Common {
    public abstract class QueryTestBase {

        protected readonly IDateTimeService DateTimeService;

        public QueryTestBase() {
        }

        protected Mock<ILogger<T>> makeLogger<T>() {
            return new Mock<ILogger<T>>();
        }

        protected static string ReadJsonFile(string filePath) {
            using var sr = new StreamReader(filePath); return sr.ReadToEnd();
        }

        protected static Mock<INexerHttpClient> MockHttpClientGet(string uri, string stringContent) {
            var mock = new Mock<INexerHttpClient>();
            mock.SetupGet(e => e.BaseUrl).Returns(new Uri("https://baseurl.com"));
            var resp = new HttpResponseMessage {
                Content = new StringContent(stringContent)
            };
            _ = mock.Setup(m => m.GetAsync(It.Is<string>(x => x == uri)))
                .Returns(Task.FromResult(resp));
            return mock;
        }

        protected static Mock<INexerHttpClient> MockHttpClientPost(string uri, string bodyIn, string responseIn, HttpStatusCode codein) {
            var mock = new Mock<INexerHttpClient>();
            mock.SetupGet(e => e.BaseUrl).Returns(new Uri("https://baseurl.com"));
            var resp = new HttpResponseMessage {
                Content = new StringContent(responseIn),
                StatusCode = codein
            };
            HttpContent body = new StringContent(bodyIn);
            _ = mock.Setup(m => m.PostAsync(It.Is<string>(x => x == uri), It.IsAny<HttpContent>()))
                .ReturnsAsync(resp);
            return mock;
        }

        protected static Mock<INexerHttpClient> MockHttpClientPut(string uri, string bodyIn, string responseIn, HttpStatusCode codein) {
            var mock = new Mock<INexerHttpClient>();
            mock.SetupGet(e => e.BaseUrl).Returns(new Uri("https://baseurl.com"));
            var resp = new HttpResponseMessage {
                Content = new StringContent(responseIn),
                StatusCode = codein
            };
            var body = new StringContent(bodyIn);
            _ = mock.Setup(m => m.PutAsync(It.Is<string>(x => x == uri), It.IsAny<HttpContent>()))
                .ReturnsAsync(resp);
            return mock;
        }

        protected static Mock<INexerHttpClient> MockHttpClientDelete(string uri, string responseIn) {
            var mock = new Mock<INexerHttpClient>();
            var resp = new HttpResponseMessage {
                Content = new StringContent(responseIn)
            };
            _ = mock.Setup(m => m.DeleteAsync(It.Is<string>(x => x == uri)))
                .Returns(Task.FromResult(resp));
            return mock;
        }

        protected static Mock<INexerHttpClient> MockHttpClientGetAndPut(string uri, string stringContent, string bodyIn, string responseIn, HttpStatusCode codein, string putUri) {
            var mock = new Mock<INexerHttpClient>();
            mock.SetupGet(e => e.BaseUrl).Returns(new Uri("https://baseurl.com"));
            var respGet = new HttpResponseMessage {
                Content = new StringContent(stringContent)
            };
            mock.Setup(m => m.GetAsync(It.Is<string>(x => x == uri)))
            .Returns(Task.FromResult(respGet));

            var respPut = new HttpResponseMessage {
                Content = new StringContent(responseIn),
                StatusCode = codein
            };
            HttpContent body = new StringContent(bodyIn);
            mock.Setup(m => m.PutAsync(It.Is<string>(x => x == putUri), It.IsAny<HttpContent>()))
            .ReturnsAsync(respPut);

            return mock;
        }

    }
}
