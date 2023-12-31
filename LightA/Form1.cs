using System;
using RestSharp;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace LightA
{
    public partial class Form1 : Form
    {
        MqttClient mClient = null;
        string endpoint = "127.0.0.1";
        string baseURI = @"http://localhost:53204";
        RestClient client = null;
        string activeModule = "";
        string eventMqt = "";

        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "LightA";
            client = new RestClient(baseURI);

        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            string applicationName = textBoxApplicationName.Text;
            string containerName = textBoxContainerName.Text;
            string subscriptionName = textBoxSubscriptionName.Text;

            if (applicationName.Equals("") && containerName.Equals("") && subscriptionName.Equals(""))
            {
                MessageBox.Show("Please fill out application, container and subcription name");
                return;
            }

        }

    }
}
