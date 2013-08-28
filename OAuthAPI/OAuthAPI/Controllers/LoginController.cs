using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using OAuthAPI.Models;
using System.IO;
using System.Threading.Tasks;

namespace OAuthAPI.Controllers
{
    public class LoginController : ApiController
    {
        // GET api/login
        // get authorization code
        public string Get(HttpRequestMessage request)
        {
            byte[] content = new byte[0];
            // create a request for an authorization token
            var authRequest = (HttpWebRequest)WebRequest.Create("https://accounts.google.com/o/oauth2/auth?" +
                                                                 "response_type=code&" +
                                                                 "client_id=780082496750.apps.googleusercontent.com&" +
                                                                 "redirect_uri=http://localhost:51073/results.aspx&" + 
                                                                 "scope=https://www.googleapis.com/auth/userinfo.profile");
            authRequest.Method = "GET";

            HttpWebResponse response = (HttpWebResponse)authRequest.GetResponse();
            int i = 0;
            i++;
            return response.StatusCode.ToString();
        }

        // GET api/login?code=authorization_code
        public string GetCode(string code)
        {
            return code;
        }
    }
}
