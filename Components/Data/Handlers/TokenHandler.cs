using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AttendanceManagement.Handlers
{
    public class TokenHandler : DelegatingHandler
    {
        
        public TokenHandler()
        {

            var uwpClientHandler = new CustomWindowsHandler();

            AttendanceManagement.Services.HttpClientService.HttpClientHandler = uwpClientHandler;
        }


        public class CustomWindowsHandler : DelegatingHandler
        {
            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {


                System.Diagnostics.Debug.WriteLine("Windows Handler has been requested");


                var token = await SecureStorage.GetAsync("Token");
                request.Headers.Add("Authorization", "Bearer " + token);

                return await base.SendAsync(request, cancellationToken);
            }

        }

    }
}
