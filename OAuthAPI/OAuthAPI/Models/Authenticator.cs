using System;
using System.Collections.Generic;
using System.Diagnostics;

using System.Linq;
using System.Web;

using System.Security.Cryptography.X509Certificates;

using Google.Apis.Authentication.OAuth2;
using Google.Apis.Authentication.OAuth2.DotNetOpenAuth;
using Google.Apis.Services;
using Google.Apis.Util;
using Google.Apis.Calendar;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Tasks.v1;
using Google.Apis.Tasks.v1.Data;


using DotNetOpenAuth.OAuth2;

namespace OAuthAPI.Models
{
    public static class Authenticator
    {
        private const string SERVICE_ACCOUNT_EMAIL = "780082496750-o14q7mohd6i0d1vh0ettave9gu1hsmfq@developer.gserviceaccount.com";
        private const string SERVICE_ACCOUNT_PKCS12_FILE_PATH = @"C:\Users\Aaron\Dropbox\Endao\OAuth\OAuthAPI\OAuthAPI\b0f0649931fc1ddf98b958272430e49af9b4a31e-privatekey.p12";

        private static CalendarService cal;
        private static TasksService tasks;

        public static CalendarService BuildCalendarService()
        {
            X509Certificate2 certificate = new X509Certificate2(SERVICE_ACCOUNT_PKCS12_FILE_PATH, "notasecret", X509KeyStorageFlags.Exportable);
            var provider = new AssertionFlowClient(GoogleAuthenticationServer.Description, certificate)
            {
                ServiceAccountId = SERVICE_ACCOUNT_EMAIL,
                Scope = CalendarService.Scopes.Calendar.GetStringValue(),
            };
            var auth = new OAuth2Authenticator<AssertionFlowClient>(provider, AssertionFlowClient.GetState);

            return new CalendarService(new BaseClientService.Initializer()
            {
                Authenticator = auth,
                ApplicationName = "Calendar API Sample",
            });


        }

        public static void Init()
        {
            // Register the authenticator.
            var provider = new NativeApplicationClient(GoogleAuthenticationServer.Description)
            {
                ClientIdentifier = "780082496750.apps.googleusercontent.com",
                ClientSecret = "EzplRHeeDJOiHsnv8V062Sud"
            };
            var auth = new OAuth2Authenticator<NativeApplicationClient>(provider, GetAuthorization);

            // create the calendar service
            cal = new CalendarService(new BaseClientService.Initializer()
            {
                Authenticator = auth,
                ApplicationName = "Calendar API Sample"
            });

            // Create the service.
            tasks = new TasksService(new BaseClientService.Initializer()
            {
                Authenticator = auth,
                ApplicationName = "Tasks API Sample"
            });

        }

        private static IAuthorizationState GetAuth()
        {
            // Get the auth URL:
            IAuthorizationState state = new AuthorizationState(new[] { CalendarService.Scopes.Calendar.GetStringValue() });
            state.Callback = new Uri(NativeApplicationClient.OutOfBandCallbackUrl);
            Uri authUri = new Uri("https://accounts.google.com/o/oauth2/auth");

            // Request authorization from the user (by opening a browser window):
            Process.Start(authUri.ToString());
            Console.Write("  Authorization Code: ");
            string authCode = Console.ReadLine();
            Console.WriteLine();

            // Retrieve the access token by using the authorization code:
            return null;
        }

        private static IAuthorizationState GetAuthorization(NativeApplicationClient arg)
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


        public static void GetTaskList()
        {
            TaskLists results = tasks.Tasklists.List().Execute();
            int i = 0;
            foreach (TaskList list in results.Items)
            {
                i++;
            }
        }

        public static void GetCalendars()
        {
            CalendarList results = cal.CalendarList.List().Execute();
            int i = 0;
            foreach (CalendarListEntry item in results.Items) {
                i++;
            }
        }
    }
}