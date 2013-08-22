using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Diagnostics;
using DotNetOpenAuth.OAuth2;
using Google.Apis.Authentication;
using Google.Apis.Authentication.OAuth2;
using Google.Apis.Authentication.OAuth2.DotNetOpenAuth;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Util;

namespace Calendar
{
    public partial class _Default : Page
    {
        public static void calendar(string[] args)
  {
      // Register the authenticator. The Client ID and secret have to be copied from the API Access
      // tab on the Google APIs Console.
      var provider = new NativeApplicationClient(GoogleAuthenticationServer.Description);
      provider.ClientIdentifier = "YOUR_CLIENT_ID";
      provider.ClientSecret = "YOUR_CLIENT_SECRET";
      AuthenticatorFactory.GetInstance().RegisterAuthenticator(
              () => new OAuth2Authenticator(provider, GetAuthentication));

      // Create the service. This will automatically call the previously registered authenticator.
      var service = new CalendarService();

  }

        private static IAuthorizationState GetAuthentication(NativeApplicationClient arg)
        {
            // Get the auth URL:
            IAuthorizationState state = new AuthorizationState(new[] { CalendarService.Scopes.Calendar.GetStringValue() });
            state.Callback = new Uri(NativeApplicationClient.OutOfBandCallbackUrl);
            Uri authUri = arg.RequestUserAuthorization(state);

            // Request authorization from the user (by opening a browser window):
            Process.Start(authUri.ToString());
            Console.Write("  Authorization Code: ");
            string authCode = Console.ReadLine();
            Console.WriteLine();

            // Retrieve the access token by using the authorization code:
            return arg.ProcessUserAuthorization(authCode, state);
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}