/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AttendanceManagement.Services
{
    public partial class Client
    {
         partial void PrepareRequest(HttpClient client, HttpRequestMessage request,string url)
        {
            if (request.RequestUri.AbsoluteUri != "https://localhost:7218/User/Authenticate")
            {
                var token =  Preferences.Get("Token", "");
                request.Headers.Add("Authorization", "Bearer " + token);

            }
            else
            {
            }

        }
    }
}
*/