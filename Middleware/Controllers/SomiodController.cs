using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Reflection;
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
        public IHttpActionResult GetAllApplications()
        {
            List<Application> objs;
            var formatter = new XmlMediaTypeFormatter();

            try
            {
                objs = AppHandler.GetAllApplications();
            }
            catch (System.Exception)
            {
                return BadRequest();
            }

            return Content(HttpStatusCode.OK, objs, Configuration.Formatters.XmlFormatter);
        }

        // GET: api/Somiod/5
        [Route("api/somiod/{application_name}")]
        [HttpGet]
        public IHttpActionResult GetApplication(string application_name)
        {
            Application app;
            try
            {
                app = AppHandler.GetApplicationFromDatabase(application_name);
            }
            catch(System.Exception)
            {
                return BadRequest();
            }
            if(app == null)
            {
                return NotFound();    
            }
           
            return Content(HttpStatusCode.OK, app, Configuration.Formatters.XmlFormatter);
        }

        // POST: api/Somiod
        [Route("api/somiod")]
        [HttpPost]
        public IHttpActionResult PostApplication([FromBody] Application newApplication)
        {
            if (newApplication == null || newApplication.Res_type != "application")
            {
                return BadRequest();
            }

            Application app;

            try
            {
                app = AppHandler.PostToDatabase(newApplication);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }

            return Ok();
        }

        // PUT: api/Somiod/5
        public IHttpActionResult Put(int id, [FromBody]string value)
        {
            return Ok();
        }

        // DELETE: api/Somiod/5
        public void Delete(int id)
        {
            Ok();
        }
    }
}
