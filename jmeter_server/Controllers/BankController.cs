using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace jmeter_server.Controllers
{
    public class BankController : ApiController
    {
        [System.Web.Http.HttpPost]
        public HttpResponseMessage Interest()
        {
            return Request.CreateResponse("Hallo");
        }
    }
}
