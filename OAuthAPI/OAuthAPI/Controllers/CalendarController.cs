using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using OAuthAPI.Models;

namespace OAuthAPI.Controllers
{
    public class CalendarController : ApiController
    {
        // GET api/calendar
        // get calendars
        public IEnumerable<string> Get()
        {
            Authenticator.Init();
            Authenticator.GetCalendars();
            return new string[] { "value1", "value2" };
        }
    }
}
