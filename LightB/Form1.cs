using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using uPLibrary.Networking.M2Mqtt;

namespace LightB
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

        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            string applicationName = textBoxApplicationName.Text;
            if (applicationName.Equals(""))
            {
                MessageBox.Show("Application name cannot be empty");
                return;
            }

            string containerName = textBoxContainerName.Text;
            if (containerName.Equals(""))
            {
                MessageBox.Show("Container name cannot be empty");
                return;
            }

            string subscriptionName = textBoxSubscriptionName.Text;
            if (subscriptionName.Equals(""))
            {
                MessageBox.Show("Subscription name cannot be empty");
                return;
            }
        }
    }
}
