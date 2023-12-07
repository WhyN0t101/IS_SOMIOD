using System;
using System.Collections.Generic;
using System.Web.Http;
using Middleware.Models;

namespace Middleware.Controllers
{
    [RoutePrefix("api/somiod")]
    public class SomiodController : ApiController
    {
        private  List<Application> applications = new List<Application>
        {
            new Application { Id = 1, Name = "App1", Creation_dt = DateTime.Now },
            new Application { Id = 2, Name = "App2", Creation_dt = DateTime.UtcNow },
            new Application { Id = 3, Name = "App3", Creation_dt = DateTime.Now }
        };

        // GET: api/somiod
        [HttpGet, Route("")]
        public IHttpActionResult GetAllApplications()
        {
            return Ok(applications);
        }

        // GET: api/somiod/applications/1
        [HttpGet, Route("applications/{id}")]
        public IHttpActionResult GetApplicationById(int id)
        {
            var application = applications.Find(a => a.Id == id);
            if (application == null)
            {
                return NotFound();
            }
            return Ok(application);
        }


        // POST: api/somiod/applications
        [HttpPost, Route("applications")]
        public IHttpActionResult CreateApplication([FromBody] Application newApplication)
        {
            if (newApplication == null)
            {
                return BadRequest("Invalid data provided for creating an application.");
            }

            // Logic for creating a new application
            newApplication.Id = applications.Count + 1; // Assign a new ID (Replace with appropriate logic)

            applications.Add(newApplication);

            return Created(new Uri(Request.RequestUri + "/" + newApplication.Id), newApplication);
        }

        // PUT: api/somiod/applications/1
        [HttpPut, Route("applications/{id}")]
        public IHttpActionResult UpdateApplication(int id, [FromBody] Application updatedApplication)
        {
            if (updatedApplication == null)
            {
                return BadRequest("Invalid data provided for updating the application.");
            }

            var existingApplication = applications.Find(a => a.Id == id);

            if (existingApplication == null)
            {
                return NotFound();
            }

            // Logic for updating the application
            existingApplication.Name = updatedApplication.Name; // Update other properties as needed

            return Ok(existingApplication);
        }

        // DELETE: api/somiod/applications/1
        [HttpDelete, Route("applications/{id}")]
        public IHttpActionResult DeleteApplication(int id)
        {
            var applicationToRemove = applications.Find(a => a.Id == id);

            if (applicationToRemove == null)
            {
                return NotFound();
            }

            // Logic for deleting the application
            applications.Remove(applicationToRemove);

            return Ok();
        }
    }
}
