using System;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using RestSharp;

namespace TestAplication
{
    internal class RequestsHandler
    {

        

        static public XDocument GetObject(string requestUri, RestClient client,string res_type)
        {
            try
            {
                // Creates and Executes a GET request
                RestRequest request = new RestRequest(requestUri, Method.Get);
                request.AddHeader("somiod-discover", res_type);
                RestResponse response = client.Execute(request);

                // Creates the XDocument
                XDocument xDoc = XDocument.Parse(response.Content);

                // Shows Status Code
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    MessageBox.Show("Resource does not exist");
                    return null;
                }

                MessageBox.Show(response.StatusCode.ToString());

                return xDoc;
            }
            catch (Exception)
            {
                MessageBox.Show("Application does not exist");
                return null;
            }
        }

        static public void DeleteApplication(string requestUri, RestClient client)
        {
            try
            {
                // Creates and Executes a Delete request
                RestRequest request = new RestRequest(requestUri, Method.Delete);
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

        static public void PostApplication(string requestUri, RestClient client, string applicationName)
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

        static public void PutAplication(string requestUri, RestClient client, string newApplicationName)
        {
            try
            {
                // Creates and Executes a PUT request
                RestRequest request = new RestRequest(requestUri, Method.Put);

                // Creates Application Object
                Middleware.Models.Application application = new Middleware.Models.Application
                {
                    Name = newApplicationName,
                    Res_type = "application"
                };

                // Adds the message body to the response
                request.AddObject(application);

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


  
        static public void PostContainer(string requestURI, RestClient client, string applicationName, string ContainerName)
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
                request.AddXmlBody(Container);

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

        static public void PutContainer(string requestURI, RestClient client, string newContainerName)
        {
            try
            {
                // Creates and Executes a PUT request
                RestRequest request = new RestRequest(requestURI, Method.Put);

                Middleware.Models.Container Container = new Middleware.Models.Container
                {
                    Name = newContainerName,
                    Res_type = "container"
                };

                // Adds the message body to the response
                request.AddXmlBody(Container);

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
        static public void DeleteContainer(string requestUri, RestClient client)
        {
            try
            {
                // Creates and Executes a Delete request
                RestRequest request = new RestRequest(requestUri, Method.Delete);
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


        static public void PostData(string requestURI, RestClient client, string applicationName, string ContainerName, string dataContent)
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
                request.AddXmlBody(data);


                RestResponse response = client.Execute(request);
                MessageBox.Show(response.Content);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

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

        static public void DeleteData(string requestUri, RestClient client)
        {
            try
            {
                // Creates and Executes a Delete request
                RestRequest request = new RestRequest(requestUri, Method.Delete);
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

        //--------------------- END OF SUBSCRIPTION ---------------------

    }
}
