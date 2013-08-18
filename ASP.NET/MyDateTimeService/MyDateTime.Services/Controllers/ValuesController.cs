using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Helpers;
using System.Web.Http;
using Newtonsoft.Json;
using MyDateTime.Services.Models;
namespace MyDateTime.Services.Controllers
{
    public class MyDateTimeController : ApiController
    {
       
        // GET api/values/
        public HttpResponseMessage Get()
        {
            MyDateTime.Services.Models.DateTime dateTime = new MyDateTime.Services.Models.DateTime();
            
            return Request.CreateResponse(HttpStatusCode.OK,dateTime);
        }       
    }
}