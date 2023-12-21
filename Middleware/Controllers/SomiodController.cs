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
            //Verifica se existe o header 
            if (!Request.Headers.Contains("somiod-discover"))
            {
                return BadRequest();
            }

            var discoverHeaderValue = Request.Headers.GetValues("somiod-discover")?.FirstOrDefault();

            //Verifica o header do pedido
            if (string.IsNullOrEmpty(discoverHeaderValue) || !discoverHeaderValue.Equals("container", StringComparison.OrdinalIgnoreCase))
            {

                return Unauthorized();
            }

            IEnumerable<Container> containers;
            var formatter = new XmlMediaTypeFormatter();
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

            return Content(HttpStatusCode.OK, containers, Configuration.Formatters.XmlFormatter);
        }

        [Route("api/somiod/{application_name}/{module_name}")]
        [HttpPost]
        public HttpResponseMessage PostDataOrSubscription(string application_name, string module_name, [FromBody] Data xmlData)
        {
            //--Data
            if (xmlData.Res_type.Equals("res_type"))
            {
             

                int idInserted = -1;

                try
                {
                    idInserted = DataHandler.SaveToDatabaseData(xmlData, application_name, module_name);
                    // DataHandler.PublishDataToMosquitto(application_name, module_name, data, "creation");
                }
                catch (System.Exception ex)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                }

                return Request.CreateResponse(HttpStatusCode.Created, "Inserted a row with id " + idInserted);
            }
            return null;
           /* //--Subscription
            else if (newObj["res_type"].ToString().Equals("subscription"))
            {
                Subscription subscription = newObj.ToObject<Subscription>();

                int rowsInserted;

                try
                {
                    rowsInserted = SubscriptionHandler.SaveToDatabase(application_name, module_name, subscription);
                }
                catch (System.Exception ex)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                }

                return Request.CreateResponse(HttpStatusCode.Created, "Inserted " + rowsInserted + " row");
            }
            //--Neither of them
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Object is not of 'data' or 'subscription' res_type, is " + newObj["res_type"]);
            }*/
        }


    }
}
