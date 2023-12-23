using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Web.Http;
using System.Xml.Linq;
using Middleware.Handler;
using Middleware.Models;
using Newtonsoft.Json.Linq;

namespace Middleware.Controllers
{
    public class SomiodController : ApiController
    {
    

        private readonly string connStr = Properties.Settings.Default.connStr;

        //GET: api/somiod
        [Route("api/somiod")]
        public IHttpActionResult GetAllApplications()
        {
            //Verify if it has a header
            if (!Request.Headers.Contains("somiod-discover"))
            {
                return BadRequest();
            }

            var discoverHeaderValue = Request.Headers.GetValues("somiod-discover")?.FirstOrDefault();

           //Verify if the header is corresponding to application
            if (string.IsNullOrEmpty(discoverHeaderValue) || !discoverHeaderValue.Equals("application", StringComparison.OrdinalIgnoreCase))
            {
          
                return Unauthorized();
            }
            //Creates list of Apps
            List<Application> apps;

            //Gets all Apps
            try
            {
                apps = AppHandler.GetAllApplications();
            }
            catch (System.Exception)
            {
                return BadRequest();
            }

            return Ok(apps);
        }

        // GET: api/Somiod/5
        [Route("api/somiod/{application_name}")]
        [HttpGet]
        public IHttpActionResult GetApplication(string application_name)
        {
            //Istances a App
            Application app;

            //Gets the App and returns it
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

            return Ok(app);
          
        }

        // POST: api/Somiod
        [Route("api/somiod")]
        [HttpPost]
        public IHttpActionResult PostApplication([FromBody] Application newApplication)
        {
            //Checks if the Application is null or if the res_type is application
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
            //Checks if the Application is null or if the res_type is application
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

            return Ok(app);
        }

        [Route("api/somiod/{application_name}")]
        [HttpDelete]
        public IHttpActionResult DeleteApplication(string application_name)
        {
            try
            {
                AppHandler.DeleteFromDatabase(application_name);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }

            return Ok();
        }

        [Route("api/somiod/{application_name}")]
        [HttpPost]
        public IHttpActionResult PostContainer(string application_name, [FromBody] Container container)
        {
            if (container == null || container.Res_type != "container")
            {
                return BadRequest("Not type container");
            }
            Container obj;

            try
            {
                obj = ContainerHandler.PostToDatabase(container, application_name);
            }
            catch (System.Exception ex)
            {
                return BadRequest();
            }

            return Ok();
        }
       
        [Route("api/somiod/{application_name}/containers")]
        [HttpGet]
        public IHttpActionResult GetAllContainersFromDatabase(string application_name)
        {
            //Verify if it has a header
            if (!Request.Headers.Contains("somiod-discover"))
            {
                return BadRequest();
            }

            var discoverHeaderValue = Request.Headers.GetValues("somiod-discover")?.FirstOrDefault();

            //Verify if the header is null or has the "container" 
            if (string.IsNullOrEmpty(discoverHeaderValue) || !discoverHeaderValue.Equals("container", StringComparison.OrdinalIgnoreCase))
            {

                return Unauthorized();
            }

            IEnumerable<Container> containers;
           
            try
            {
                containers = ContainerHandler.GetAllContainers(application_name);
            }
            catch (System.Exception ex)
            {
                if (ex.Message == "There is no application named  " + application_name)
                {
                    return NotFound();
                        }
                return BadRequest();
            }

            if (containers == null || !containers.Any())
            {
                return NotFound();
            }

            return Ok(containers);
        }

        [Route("api/somiod/{application_name}/{container_name}")]
        [HttpDelete]
        public IHttpActionResult DeleteContainer(string application_name,string container_name)
        {
            try
            {
                ContainerHandler.DeleteFromDatabase(application_name, container_name);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [Route("api/somiod/{application_name}/{container_name}")]
        [HttpPut]
        public IHttpActionResult PutContainer(string application_name, string container_name, [FromBody] Container newContainer)
        {
            if (newContainer == null || newContainer.Res_type != "container")
            {
                return BadRequest();
            }
            Container container;
            try
            {
                container = ContainerHandler.PutToDatabase(application_name, container_name, newContainer);

            }
            catch (System.Exception ex)
            {
                return BadRequest();
            }

            return Ok(container);

        }
    }
}
