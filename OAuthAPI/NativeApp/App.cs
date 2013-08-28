using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using DotNetOpenAuth.OAuth2;
using Google.Apis.Authentication;
using Google.Apis.Authentication.OAuth2;
using Google.Apis.Authentication.OAuth2.DotNetOpenAuth;
using Google.Apis.Tasks.v1;
using Google.Apis.Tasks.v1.Data;
using Google.Apis.Util;

namespace NativeApp
{
    class App
    {


        public static void Main(string[] args)
        {
            // Display the header and initialize the sample.
            CommandLine.EnableExceptionHandling();
            CommandLine.DisplayGoogleSampleHeader("Tasks API");

            // Register the authenticator.
            var provider = new NativeApplicationClient(GoogleAuthenticationServer.Description)
            {
                ClientIdentifier = "<client_id>",
                ClientSecret = "<client_secret>"
            };
            var auth = new OAuth2Authenticator<NativeApplicationClient>(provider, GetAuthorization);

            // Create the service.
            var service = new TasksService(new BaseClientService.Initializer()
            {
                Authenticator = auth,
                ApplicationName = "Tasks API Sample",

            });
            TaskLists results = service.Tasklists.List().Fetch();
            Console.WriteLine("Lists:");
            foreach (TaskList list in results.Items)
            {
                Console.WriteLine("- " + list.Title);
            }
            Console.ReadKey();
        }

        private static IAuthorizationState GetAuthorization(NativeApplicationClient arg)
        {
            // Get the auth URL:
            IAuthorizationState state = new AuthorizationState(new[] { TasksService.Scopes.Tasks.GetStringValue() });
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

    }
}
