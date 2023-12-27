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
using System.Xml;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TestAplication
{
    public partial class Form1 : Form
    {
        string baseURI = @"http://localhost:52885/";
        RestClient client = null;
        public Form1()
        {
            InitializeComponent();
            client = new RestClient(baseURI);
        }
        private void buttonGetAllApplications_Click(object sender, EventArgs e)
        {
            TextBoxListAllApplications.Clear();
            string requestUri = "/api/somiod/";

            XDocument xDoc = RequestsHandler.getResponseAsXMLDocument(requestUri, client, "applications");

            if (xDoc != null)
            {
                TextBoxListAllApplications.Text = xDoc.ToString();
            }
        }

        private void buttonGetApplication_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            string applicationName = textBox1.Text.Trim();

            if (string.IsNullOrEmpty(applicationName))
            {
                MessageBox.Show("Please enter the application name");
                return;
            }

            string requestUri = $"/api/somiod/{applicationName}";
            XDocument xDoc = RequestsHandler.getResponseAsXMLDocument(requestUri, client, "Application");

            if (xDoc != null)
            {
                richTextBox1.Text = xDoc.ToString();
            }
        }

        private void buttonPOSTApplication_Click(object sender, EventArgs e)
        {
            string applicationName = textBoxApplicationName.Text.Trim();

            if (string.IsNullOrEmpty(applicationName))
            {
                MessageBox.Show("Please enter the application name");
                return;
            }

            string requestUri = "/api/somiod/";

            try
            {
                RequestsHandler.createApplication(requestUri, client, applicationName);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void buttonPUTApplication_Click(object sender, EventArgs e)
        {
            string applicationName = textBoxApplicationName.Text.Trim();
            string newApplicationName = textBoxApplicationNewName.Text.Trim();

            if (string.IsNullOrEmpty(applicationName) || string.IsNullOrEmpty(newApplicationName))
            {
                MessageBox.Show("Please enter both application names");
                return;
            }

            string requestUri = $"/api/somiod/{applicationName}";

            try
            {
                RequestsHandler.updateApplicationName(requestUri, client, newApplicationName);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void buttonDELApplication_Click(object sender, EventArgs e)
        {
            // Verifies if Application Name Input is Empty
            string applicationName = textBoxApplicationName.Text.Trim();

            if (string.IsNullOrEmpty(applicationName))
            {
                MessageBox.Show("Please enter the application name");
                return;
            }

            // Makes the Delete Request
            string requestUri = $"/api/somiod/{applicationName}";

            try
            {
                RequestsHandler.deleteApplication(requestUri, client);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
    }
}
