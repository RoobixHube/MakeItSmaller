using MakeItSmaller.DataLayer;
using MakeItSmaller.Services;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace MakeItSmaller.Controllers
{
    public class RedirectController : ApiController
    {
        private readonly IMISURLService _service;
        private const string dbFileName = "MISURLdb.txt";

        public RedirectController()
        {
            string systemPath = AppDomain.CurrentDomain.BaseDirectory;
            var path = Path.Combine(systemPath, dbFileName);

            IMISURLRepository _repository = new MISURLRepository(path);
            _service = new MISURLService(_repository);
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("{MSIURL}")]
        public HttpResponseMessage GetURL()
        {
            var incomingMSIULR = Url.Request.RequestUri.ToString().Split('/').Last();
            var redirectURL = _service.GetURLFROMMISURL(incomingMSIULR);

            var response = Request.CreateResponse(System.Net.HttpStatusCode.Moved);
            response.Headers.Location = new Uri(redirectURL);
            return response;
        }
    }
}
