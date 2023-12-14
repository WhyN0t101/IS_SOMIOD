using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
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
            //Verifica se existe o header 
            if (!Request.Headers.Contains("somiod-discover"))
            {
                return BadRequest();
            }

            var discoverHeaderValue = Request.Headers.GetValues("somiod-discover")?.FirstOrDefault();

           //Verifica o header do pedido
            if (string.IsNullOrEmpty(discoverHeaderValue) || !discoverHeaderValue.Equals("application", StringComparison.OrdinalIgnoreCase))
            {
          
                return Unauthorized();
            }

            //Verificar parametro discovery : application
            List<Application> apps;
       
            try
            {
                apps = AppHandler.GetAllApplications();
            }
            catch (System.Exception)
            {
                return BadRequest();
            }

            return Content(HttpStatusCode.OK, apps, Configuration.Formatters.XmlFormatter);
            //return Ok(apps);
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

            //return Ok(app);
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
        [Route("api/somiod/{application_name}")]
        [HttpPut]
        public IHttpActionResult PutApplication(string application_name, [FromBody] Application newApplication)
        {
          
            if (newApplication == null || newApplication.Res_type != "application")
            {
                return BadRequest();
            }

            Application app;

            try
            {
                app = AppHandler.UpdateToDatabase(application_name,newApplication);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }

            return Content(HttpStatusCode.OK, app, Configuration.Formatters.XmlFormatter);
        }

        // DELETE: api/Somiod/5
        public void Delete(int id)
        {
            Ok();
        }
    }
}
