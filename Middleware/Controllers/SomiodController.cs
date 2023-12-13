using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using Middleware.Handler;
using Middleware.Models;
namespace Middleware.Controllers
{
    public class SomiodController : ApiController
    {
    

        private string connStr = Properties.Settings.Default.connStr;

        //GET: api/somiod
        [Route("api/somiod")]
        public HttpResponseMessage GetAllApplications()
        {
            List<Application> objs;
            var formatter = new XmlMediaTypeFormatter();

            try
            {
                objs = AppHandler.GetAllAppications();
            }
            catch (System.Exception)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new Application(), formatter);
            }

            return Request.CreateResponse(HttpStatusCode.OK, objs, formatter);
        }

        // GET: api/Somiod/5
        [Route("api/somiod/{application_name}")]
        [HttpGet]
        public HttpResponseMessage GetApplication(string application_name)
        {
            Application app;
            var formatter = new XmlMediaTypeFormatter();
            try
            {
                app = AppHandler.GetApplicationFromDatabase(application_name);
            }
            catch(System.Exception)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new Application(),formatter);
            }
            if(app == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound,new Application(), formatter);    
            }
            return Request.CreateResponse(HttpStatusCode.OK,app, formatter);
        }

        // POST: api/Somiod
        [Route("api/somiod")]
        [HttpPost]
        public HttpResponseMessage PostApplication([FromBody] Application newApplication)
        {
            if (newApplication == null || newApplication.Res_type != "application")
            {
                return Request.CreateResponse<Application>(HttpStatusCode.BadRequest, null);
            }

            Application app;

            try
            {
                app = AppHandler.PostToDatabase(newApplication);
            }
            catch (System.Exception)
            {
                return Request.CreateResponse<Application>(HttpStatusCode.BadRequest, null);
            }

            return Request.CreateResponse<Application>(HttpStatusCode.Created, app);
        }

        // PUT: api/Somiod/5
        public void Put(int id, [FromBody]string value)
        {
            Ok();
        }

        // DELETE: api/Somiod/5
        public void Delete(int id)
        {
            Ok();
        }
    }
}
