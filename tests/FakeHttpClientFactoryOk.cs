using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace tests
{

    public class FakeHttpClientHandlerOk : HttpClientHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.FromResult(SendAsync(request.Method, request.RequestUri.PathAndQuery));
        }

        private HttpResponseMessage SendAsync(HttpMethod requestMethod, string requestUriPathAndQuery)
        {
            var response = File.ReadAllText("examples/ok.json");
            return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(response) };
        }
    }
    public class FakeHttpClientHandlerFail : HttpClientHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.FromResult(SendAsync(request.Method, request.RequestUri.PathAndQuery));
        }

        private HttpResponseMessage SendAsync(HttpMethod requestMethod, string requestUriPathAndQuery)
        {
            var response = File.ReadAllText("examples/failed.json");
            return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(response) };
        }
    }


    public class FakeHttpClientFactoryOk: IHttpClientFactory
    {
        public HttpClient CreateClient(string name)
        {
            var client = new HttpClient(new FakeHttpClientHandlerOk());
            return client;
        }
    }
    public class FakeHttpClientFactoryFail: IHttpClientFactory
    {
        public HttpClient CreateClient(string name)
        {
            var client = new HttpClient(new FakeHttpClientHandlerFail());
            return client;
        }
    }
}
