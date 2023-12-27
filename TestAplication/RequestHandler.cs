using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using Restsharp;

namespace TestAplication
{
    internal class RequestsHandler
    {

        //--------------------- COMMON METHODS ---------------------

        static public XmlDocument getResponseAsXMLDocument(string requestURI, RestClient client, string res_type)
        {
            try
            {
                // Creates and Executes a GET request
                RestRequest request = new RestRequest(requestURI, Method.Get);
                RestResponse response = client.Execute(request);

                // Creates the XML document
                var doc = new XmlDocument();

                // Loads the Response XML Content to the XML document
                doc.LoadXml(response.Content);

                // Shows Status Code
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    MessageBox.Show("Resource does not exist");
                    return null;
                }
                MessageBox.Show(response.StatusCode.ToString());


                return doc;
            }
            catch (Exception)
            {
                throw new Exception("Could not get " + res_type);
            }
        }


        static public void delete(string requestURI, RestClient client)
        {
            try
            {
                // Creates and Executes a Delete request
                RestRequest request = new RestRequest(requestURI, Method.Delete);
                RestResponse response = client.Execute(request);
                // Shows Status Code

                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    MessageBox.Show("Resource does not exist");
                }
                else
                {
                    MessageBox.Show("Deleted");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        //--------------------- END OF COMMON METHODS ---------------------

        //--------------------- APPLICATION ---------------------
        static public void createApplication(string requestURI, RestClient client, string applicationName)
        {
            try
            {
                // Creates the Object Application
                Middleware.Models.Application application = new Middleware.Models.Application
                {
                    Name = applicationName,
                    Res_type = "application"
                };


                var request = new RestRequest("/api/somiod", Method.Post);

                // Adds the message body to the response
                request.AddObject(application);

                RestResponse response = client.Execute(request);
                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    MessageBox.Show("Application already exists");
                }
                else
                {
                    MessageBox.Show("Application created");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        static public void updateApplicationName(string requestURI, RestClient client, string newApplicationName)
        {
            try
            {
                // Creates and Executes a PUT request
                RestRequest request = new RestRequest(requestURI, Method.Put);

                // Creates Application Object
                Middleware.Models.Application application = new Middleware.Models.Application
                {
                    Name = newApplicationName,
                    Res_type = "application"
                };

                // Adds the message body to the response
                request.AddJsonBody(application);

                RestResponse response = client.Execute(request);
                // Shows Status Code
                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    MessageBox.Show("Application does not exist");
                }
                else
                {
                    MessageBox.Show("Application updated");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        //--------------------- END OF APPLICATION ---------------------

        //--------------------- ContainerS ---------------------
        static public void createContainer(string requestURI, RestClient client, string applicationName, string ContainerName)
        {
            try
            {
                // Creates the Object Application
                Middleware.Models.Container Container = new Middleware.Models.Container
                {
                    Name = ContainerName,
                    Res_type = "container"
                };


                var request = new RestRequest("/api/somiod/" + applicationName, Method.Post);

                // Adds the message body to the response
                request.AddObject(Container);

                RestResponse response = client.Execute(request);
                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    MessageBox.Show("Container already exists");
                }
                else
                {
                    MessageBox.Show("Container created");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        static public void updateContainer(string requestURI, RestClient client, string newContainerName)
        {
            try
            {
                // Creates and Executes a PUT request
                RestRequest request = new RestRequest(requestURI, Method.Put);

                Middleware.Models.Container Container = new Middleware.Models.Container
                {
                    Name = newContainerName,
                    Res_type = "Container"
                };

                // Adds the message body to the response
                request.AddJsonBody(Container);

                RestResponse response = client.Execute(request);
                // Shows Status Code
                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    MessageBox.Show("Container does not exist");
                }
                else
                {
                    MessageBox.Show("Container updated");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        //--------------------- END OF Container ---------------------

        //--------------------- DATA ---------------------

        static public void createData(string requestURI, RestClient client, string applicationName, string ContainerName, string dataContent)
        {
            try
            {
                // Creates the Object Application
                Middleware.Models.Data data = new Middleware.Models.Data
                {
                    Content = dataContent,
                    Res_type = "data"
                };


                var request = new RestRequest("/api/somiod/" + applicationName + "/" + ContainerName, Method.Post);

                // Adds the message body to the response
                request.AddJsonBody(data);


                RestResponse response = client.Execute(request);
                MessageBox.Show(response.Content);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        //--------------------- END OF DATA ---------------------

        //--------------------- SUBSCRIPTION ---------------------

        static public void createSubscription(string requestURI, RestClient client, string applicationName, string ContainerName, string subscriptionName, string eventName, string endpoint)
        {
            try
            {
                // Creates the Object Application
                Middleware.Models.Subscription subscription = new Middleware.Models.Subscription
                {
                    Name = subscriptionName,
                    Res_type = "subscription",
                    Event = eventName,
                    Endpoint = endpoint
                };


                var request = new RestRequest("/api/somiod/" + applicationName + "/" + ContainerName, Method.Post);

                // Adds the message body to the response
                request.AddJsonBody(subscription);


                RestResponse response = client.Execute(request);
                if (response.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    MessageBox.Show("Subscription Created");
                }
                else
                {
                    MessageBox.Show(response.Content.ToString());
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        //--------------------- END OF SUBSCRIPTION ---------------------

    }
}
