using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace jmeter_server.Controllers
{
    public class BankController : ApiController
    {
        [HttpGet]
        [Route("interest")]
        public HttpResponseMessage Interest()
        {
            return Request.CreateResponse("Hallo");
        }
    }
}
