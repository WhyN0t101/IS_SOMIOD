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
            catch (System.Exception)
            {
                return BadRequest();
            }
            if (app == null)
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
                return BadRequest("New Application is null or not correct res_type");
            }
            //Posts to DB
            try
            {
                AppHandler.PostToDatabase(newApplication);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }

            return Created(new Uri(Request.RequestUri, newApplication.Name), newApplication);

        }

        [Route("api/somiod/{application_name}")]
        [HttpPut]
        public IHttpActionResult PutApplication(string application_name, [FromBody] Application newApplication)
        {
            //Checks if the Application is null or if the res_type is application
            if (newApplication == null || newApplication.Res_type != "application")
            {
                return BadRequest("New Application is null or not correct res_type");
            }
            //Instances an App
            Application app;
            //Updates the application
            try
            {
                app = AppHandler.UpdateToDatabase(application_name, newApplication);
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
            //Deletes Application if it exists
            try
            {
                AppHandler.DeleteFromDatabase(application_name);
            }
            catch (System.Exception)
            {
                return BadRequest("No application named " + application_name);
            }

            return Ok("Deleted");
        }

        [Route("api/somiod/{application_name}")]
        [HttpPost]
        public IHttpActionResult PostContainer(string application_name, [FromBody] Container container)
        {
            //Checks if container is null or if res_type is container
            if (container == null || container.Res_type != "container")
            {
                return BadRequest("Not type container or null");
            }
            //Posts do DB the container
            try
            {
                ContainerHandler.PostToDatabase(container, application_name);
            }
            catch (System.Exception ex)
            {
                return BadRequest("Failed to create container");
            }
            return Created(new Uri(Request.RequestUri, application_name), container);

        }

        [Route("api/somiod/{application_name}/containers")]
        [HttpGet]
        public IHttpActionResult GetAllContainers(string application_name)
        {
            //Verify if it has a header
            if (!Request.Headers.Contains("somiod-discover"))
            {
                return BadRequest("Does not have container in header");
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

                return BadRequest("Couldnt not retrieve containers");
            }

            if (containers == null || !containers.Any())
            {
                return NotFound();
            }

            return Ok(containers);
        }

        [Route("api/somiod/{application_name}/{container_name}")]
        [HttpDelete]
        public IHttpActionResult DeleteContainer(string application_name, string container_name)
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
        public IHttpActionResult PutContainer(string application_name, string container_name, [FromBody] Models.Container newContainer)
        {
            //Checks if the new container is null and res_type container
            if (newContainer == null || newContainer.Res_type != "container")
            {
                return BadRequest("New Application is null or not correct res_type");
            }
            //Checks if the container exists
            if (ContainerHandler.GetContainerInDatabase(application_name, container_name) == null)
            {
                return NotFound();
            }
            //Updates the container
            Models.Container container;
            try
            {
                container = ContainerHandler.PutToDatabase(application_name, container_name, newContainer);

            }
            catch (Exception)
            {
                return BadRequest("Faild to update container");
            }

            return Ok(container);

        }
        [Route("api/somiod/{application_name}/{container_name}")]
        [HttpGet]
        public IHttpActionResult GetContainerFromApplication(string application_name, string container_name)
        {
            Models.Container container = null;

            try
            {
                container = ContainerHandler.GetContainerInDatabase(application_name, container_name);

                if (container == null)
                {
                    return NotFound();
                }

                return Ok(container);
            }
        
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [Route("api/somiod/{application_name}/{container_name}/{data_id}")]
        [HttpGet]
        public IHttpActionResult GetDataFromContainer(string application_name, string container_name, int data_id)
        {
            //Verify if it has a header
            if (!Request.Headers.Contains("somiod-discover"))
            {
                return BadRequest("Does not have container in header");
            }

            var discoverHeaderValue = Request.Headers.GetValues("somiod-discover")?.FirstOrDefault();

            //Verify if the header is null or has the "container" 
            if (string.IsNullOrEmpty(discoverHeaderValue) || !discoverHeaderValue.Equals("data", StringComparison.OrdinalIgnoreCase))
            {

                return Unauthorized();
            }
            Data data = null;

            try
            {
                data = DataHandler.GetDataFromDatabase(application_name, container_name,data_id);

                if (data == null)
                {
                    return NotFound();
                }

                return Ok(data);
            }

            catch (Exception)
            {
                return BadRequest();
            }
        }
        // POST: api/Somiod/lighting/light_bulb
        [Route("api/somiod/{application_name}/{container_name}")]
        [HttpPost]
        public IHttpActionResult PostDataOrSubscription(string application_name, string container_name, [FromBody] JObject newObj)
        {
            //--Data
            if (newObj["res_type"].ToString().Equals("data"))
            {
                Data data = newObj.ToObject<Data>();

                int idInserted = -1;

                try
                {
                    idInserted = DataHandler.PostToDatabase(data, application_name, container_name);
                    //DataHandler.PublishDataToMosquitto(application_name, module_name, data, "creation");
                }
                catch (System.Exception ex)
                {
                    return BadRequest(ex.Message);
                }

              return  Created(new Uri(Request.RequestUri, application_name), idInserted);
            }
            //--Subscription
            else if (newObj["res_type"].ToString().Equals("subscription"))
            {
                Subscription subscription = newObj.ToObject<Subscription>();

                int rowsInserted;

                try
                {
                    rowsInserted = SubHandler.PostToDatabase(application_name, container_name, subscription);
                }
                catch (System.Exception ex)
                {
                    return BadRequest(ex.Message);
                }

               return  Created(new Uri(Request.RequestUri, application_name), rowsInserted);
            }
            //--Neither of them
            else
            {
                return BadRequest("Object is not of 'data' or 'subscription' res_type, is " + newObj["res_type"]);
            }
        }

    }
}