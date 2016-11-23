using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace OExam.Server
{
    public class HttpResponseHandlerHelp:DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var requestHeader = request.Headers;

            return base.SendAsync(request, cancellationToken);
        }
    }
}