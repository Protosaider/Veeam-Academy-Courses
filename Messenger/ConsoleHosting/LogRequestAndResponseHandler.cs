using log4net;
using Other;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleHosting
{
    internal class LogRequestAndResponseHandler : DelegatingHandler
    {
        private static readonly ILog s_log = SLogger.GetLogger();
        private static readonly String s_requestBody = "Incoming request body: ";
        private static readonly String s_responseBody = "Outgoing response body: ";

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // log request body
            String requestBody = await request.Content.ReadAsStringAsync();
            Console.WriteLine(requestBody);
            s_log.LogInfo(s_requestBody + requestBody);

            Console.WriteLine(request.RequestUri.AbsolutePath);
            Console.WriteLine(request.RequestUri.AbsoluteUri);
            Console.WriteLine(request.RequestUri.OriginalString);
            Console.WriteLine(request.GetRequestContext().VirtualPathRoot);

            // let other handlers process the request
            var result = await base.SendAsync(request, cancellationToken);

            if (result.Content != null)
            {
                // once response body is ready, log it
                var responseBody = await result.Content.ReadAsStringAsync();
                Console.WriteLine(responseBody);
                s_log.LogInfo(s_responseBody + responseBody);
            }

            return result;
        }
    }
}