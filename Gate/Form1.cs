﻿using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace Gate
{
    public partial class Form1 : Form
    {
        //Define default variables
        MqttClient mClient = null;
        string endpoint = "127.0.0.1";
        string baseURI = @"http://localhost:52885";
        RestClient client = null;
        string aContainer = "";
        string eventMqt = "";
        Timer httpTimer = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Define default variables
            this.Text = "Gate";
            client = new RestClient(baseURI);
            textBoxApplicationName.Text = "gate";
            textBoxContainerName.Text = "gate_container";
            textBoxSubscriptionName.Text = "sub";
            textBoxSubscriptionEndPoint.Text = "127.0.0.1";
            comboBoxEventType.Text = "creation";

        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            //Check the operation type
            if (typeOfOperation.SelectedItem == null)
            {
                MessageBox.Show("Please select a type of operation.");
                return;
            }
            // checks if all data is filled
            string applicationName = textBoxApplicationName.Text;
            string containerName = textBoxContainerName.Text;
            string subscriptionName = textBoxSubscriptionName.Text;

            if (applicationName.Equals("") && containerName.Equals("") && subscriptionName.Equals(""))
            {
                MessageBox.Show("Please fill out application, container, and subscription name");
                return;
            }

            string subscriptionEventType = comboBoxEventType.GetItemText(comboBoxEventType.SelectedItem);
            if (!subscriptionEventType.Equals("creation") && !subscriptionEventType.Equals("deletion") && !subscriptionEventType.Equals("creation and deletion"))
            {
                MessageBox.Show("Subscription event not properly selected");
                return;
            }

            string subscrptionEndPoint = textBoxSubscriptionEndPoint.Text;
            if (subscrptionEndPoint.Equals(""))
            {
                MessageBox.Show("Subscription endpoint cannot be empty");
                return;
            }

            applicationName = applicationName.Replace(" ", "-");
            containerName = containerName.Replace(" ", "-");

            try
            {
                // Disconnect the timer if it's already running
                if (httpTimer != null && httpTimer.Enabled)
                {
                    httpTimer.Stop();
                    httpTimer.Dispose();
                }

                // Disconnect MQTT if it's already connected
                if (mClient != null && mClient.IsConnected)
                {
                    mClient.Disconnect();
                }

                string requestName = $"/api/somiod/{applicationName}";
                string requestContainer = "/api/somiod/" + applicationName + "/container/" + containerName;
                //Checks to see if application or container already exits
                XDocument applicationExists = GetObject(requestName, client, "application");
                XDocument containerExists = GetObject(requestContainer, client, "container");

                if (applicationExists == null)
                {
                    Middleware.Models.Application appCreated = createApplication(applicationName);
                    if (appCreated != null)
                    {
                        applicationName = appCreated.Name;
                    }
                }

                if (containerExists == null)
                {
                    Middleware.Models.Container containerCreated = createContainer(containerName, applicationName);
                    if (containerCreated != null)
                    {
                        containerName = containerCreated.Name;
                    }
                }
                //checks the type of operation
                if (typeOfOperation.SelectedItem.Equals("HTTP"))
                {
                    // If HTTP is selected, start a timer to periodically check the last data
                    httpTimer = new Timer();
                    httpTimer.Interval = 5000; // 5 seconds interval
                    httpTimer.Tick += (timerSender, timerEvent) =>
                    {
                        HttpOperation(applicationName, containerName);
                    };
                    httpTimer.Start();

                }
                else
                {   //Disables timer
                    if (httpTimer != null && httpTimer.Enabled)
                    {
                        httpTimer.Stop();
                        httpTimer.Dispose();
                    }
                    // If MQTT is selected, connect to Mosquitto
                    string responseSubscription = createSubscription(subscriptionEventType, subscrptionEndPoint, subscriptionName, containerName, applicationName);
                    if (!responseSubscription.Contains("exists"))
                    {
                        string topic = applicationName + "/" + containerName;
                        connectToMosquitto(topic);
                        aContainer = containerName;
                        eventMqt = subscriptionEventType;
                        if (applicationExists != null || containerExists != null)
                        {
                            MessageBox.Show("Application or Container already exists and connected");
                        }
                        else
                        {
                            MessageBox.Show("Created and Connected to Server Successfully");
                        }
                    }
                    else
                    {
                        MessageBox.Show(responseSubscription);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not connect to the server: {ex.Message}");
            }
        }
        private void HttpOperation(string applicationName, string containerName)
        {
      
                // If HTTP is selected, start a timer to periodically check the last data
                Timer timer = new Timer();
                timer.Interval = 5000; // 5 seconds interval
                timer.Tick += (timerSender, timerEvent) =>
                {
                    string lastData = getLastData(applicationName, containerName);
                    if (lastData != null)
                    {
                        // Check the content and display appropriate message box
                        if (lastData.Equals("ON"))
                        {

                            pictureBox1.Image = Properties.Resources.gate_open;
                        }
                        else if (lastData.Equals("OFF"))
                        {
                            pictureBox1.Image = Properties.Resources.gate_close;
                        }
                    }
                    else
                    {
                        MessageBox.Show("No data found");
                    }
                };
                timer.Start();
            
        }
        private string getLastData(string applicationName, string containerName)
        {
            // Creates and Executes a GET request
            RestRequest request = new RestRequest("api/somiod/" + applicationName + "/" + containerName, Method.Get);
            request.AddHeader("somiod-discover", "data");
            RestResponse response = client.Execute(request);

            // Creates the XDocument
            XDocument xDoc = XDocument.Parse(response.Content);

            // Extracts the last Data element
            XElement lastData = xDoc.Descendants("Data").LastOrDefault();

            // Shows Status Code
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                MessageBox.Show("Resource does not exist");
                return null;
            }

            if (lastData != null)
            {
                return lastData.Element("content")?.Value;
            }
            else
            {
                MessageBox.Show("No data found");
                return null;
            }
        }



        void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            string body = Encoding.UTF8.GetString(e.Message);


            string[] vars = body.Split(';');

            if (vars.Length != 2)
            {
                MessageBox.Show("Something went wrong with Mosquitto. Please try again later");
                return;
            }
            //checks the event and changes state 
            string eventMosquitto = vars[0].ToLower();
            string message = vars[1];

            if (!eventMqt.Contains(eventMosquitto))
            {
                //ignores publish because it's not the type of event that this application subscribed to
                return;
            }


            if (message.Equals("ON"))
            {
                pictureBox1.Image = Properties.Resources.gate_open;
            }
            else if (message.Equals("OFF"))
            {
                pictureBox1.Image = Properties.Resources.gate_close;
            }
        }
        private Middleware.Models.Application createApplication(string applicationName)
        {
            try
            {
                // Creates the Object Application
                Middleware.Models.Application application = new Middleware.Models.Application
                {
                    Name = applicationName,
                    Res_type = "application"
                };

                // Sends Application to the server
                var request = new RestRequest("/api/somiod", Method.Post);
                request.AddXmlBody(application);
                RestResponse<Middleware.Models.Application> response = client.Execute<Middleware.Models.Application>(request);

                // RestSharp automatically handles XML deserialization
                if (response.ErrorException != null)
                {
                    throw response.ErrorException;
                }
                return response.Data;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("exists"))
                {
                    return null;
                }
                throw new Exception("Could not create application");
            }
        }

        private Middleware.Models.Container createContainer(string containerName, string applicationName)
        {
            try
            {
                // Creates the Object container
                Middleware.Models.Container container = new Middleware.Models.Container
                {
                    Name = containerName,
                    Res_type = "container"
                };

                // Sends container to the server
                var request = new RestRequest("/api/somiod/" + applicationName, Method.Post);
                request.AddXmlBody(container);
                RestResponse<Middleware.Models.Container> response = client.Execute<Middleware.Models.Container>(request);

                if (response.ErrorException != null)
                {
                    throw response.ErrorException;
                }

                return response.Data;

            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("exists"))
                {
                    return null;
                }
                throw new Exception("Could not create container");
            }
        }

        private string createSubscription(string subscriptionEventType, string subscrptionEndPoint, string subscriptionName, string containerName, string applicationName)
        {
            try
            {
                // Creates the Object Subscription
                Middleware.Models.Subscription subscription = new Middleware.Models.Subscription
                {
                    Name = subscriptionName,
                    Res_type = "subscription",
                    Event = subscriptionEventType,
                    Endpoint = subscrptionEndPoint
                };

                // Sends Subscription to the server
                var request = new RestRequest("/api/somiod/" + applicationName + "/" + containerName, Method.Post);
                request.AddXmlBody(subscription);
                RestResponse response = client.Execute(request);
                return response.Content;
            }
            catch
            {
                throw new Exception("Could not create subscription");
            }
        }

        private void connectToMosquitto(string topic)
        {
            try
            {
                //Create client to endpoint and checks if its connected
                mClient = new MqttClient(endpoint);
                mClient.Connect(Guid.NewGuid().ToString());
                if (!mClient.IsConnected)
                {
                    Console.WriteLine("Error connecting to message broker.");
                    return;
                }
                mClient.MqttMsgPublishReceived += client_MqttMsgPublishReceived;
                byte[] qosLevels = { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE }; //QoS – depends on the topics number
                                                                                                               // mClient.Subscribe(container, qosLevels)
                mClient.Subscribe(new string[] { topic }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
            }
            catch (Exception)
            {
                throw new Exception("Could not connect to the server");
            }
        }
        static public XDocument GetObject(string requestUri, RestClient client, string res_type)
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
                if (response.StatusCode == HttpStatusCode.NotFound)
                {

                    return null;
                }

                return xDoc;
            }
            catch (Exception)
            {

                return null;
            }
        }
    }

}

