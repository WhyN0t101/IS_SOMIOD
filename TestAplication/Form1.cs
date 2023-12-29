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
        string baseURI = @"http://localhost:53204";
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

            XDocument xDoc = RequestsHandler.GetObject(requestUri, client,"application");

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
            XDocument xDoc = RequestsHandler.GetObject(requestUri, client,"application");

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
                RequestsHandler.PostApplication(requestUri, client, applicationName);
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
                RequestsHandler.PutAplication(requestUri, client, newApplicationName);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void buttonDELETEApplication_Click(object sender, EventArgs e)
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
                RequestsHandler.DeleteApplication(requestUri, client);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void buttonGetAllContainers_Click(object sender, EventArgs e)
        {
            richTextBoxListContainers.Clear();
            string applicationName = textBoxApplicationNameContainer.Text;
            if (string.IsNullOrEmpty(applicationName))
            {
                MessageBox.Show("Please enter application name");
                return;
            }

            string requestURI = "/api/somiod/" + applicationName + "/containers";
            XDocument xDoc = RequestsHandler.GetObject(requestURI, client, "container");

            if (xDoc == null)
            {
                return;
            }

            richTextBoxListContainers.Text = xDoc.ToString();
        }

        private void buttonGetContainer_Click(object sender, EventArgs e)
        {
            richTextBoxSpecificContainer.Clear();
            string applicationName = textBoxApplicationNameContainer.Text;
            string containerName = textBoxGetContainerName.Text;
            if (string.IsNullOrEmpty(applicationName) || string.IsNullOrEmpty(containerName))
            {
                MessageBox.Show("Please enter both application name and container name");
                return;
            }
            string requestURI = "/api/somiod/" + applicationName + "/" + containerName;
            XDocument xDoc = RequestsHandler.GetObject(requestURI, client, "container");

            if (xDoc == null)
            {
                return;
            }

            richTextBoxSpecificContainer.Text = xDoc.ToString();
        }

        private void buttonPOSTContainer_Click(object sender, EventArgs e)
        {
            // Verifies if Application Name Input is Empty
            string applicationName = textBoxApplicationNameContainer.Text;
            string containerName = textBoxContainerName.Text;

            if (string.IsNullOrEmpty(applicationName) || string.IsNullOrEmpty(containerName))
            {
                MessageBox.Show("Please enter both application name and container name");
                return;
            }
            // Makes the Put Request
            string requestURI = "/api/somiod/" + applicationName;
            try
            {
                RequestsHandler.PostContainer(requestURI, client, applicationName, containerName);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void buttonPUTContainer_Click(object sender, EventArgs e)
        {
            string applicationName = textBoxApplicationNameContainer.Text;
            string newContainerName = textBoxContainerNewName.Text;
            string containerName = textBoxContainerName.Text;
            if (string.IsNullOrEmpty(applicationName) || string.IsNullOrEmpty(containerName) || string.IsNullOrEmpty(newContainerName))
            {
                MessageBox.Show("Please enter both application name and container name");
                return;
            }
            // Makes the Put Request
            string requestURI = "/api/somiod/" + applicationName + "/" + containerName;
            try
            {
                RequestsHandler.PutContainer(requestURI, client, newContainerName);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void buttonDELETEContainer_Click(object sender, EventArgs e)
        {
            string applicationName = textBoxApplicationNameContainer.Text;
            string containerName = textBoxContainerName.Text;

            if (string.IsNullOrEmpty(applicationName) || string.IsNullOrEmpty(containerName))
            {
                MessageBox.Show("Please enter both application name and container name");
                return;
            }
            string requestURI = "/api/somiod/" + applicationName + "/" + containerName;
            try
            {
                RequestsHandler.DeleteContainer(requestURI, client);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void buttonPOSTData_Click(object sender, EventArgs e)
        {
            // Verifies if Application Name Input is Empty
            string applicationName = textBoxApplicationNameData.Text;
            string containerName = textBoxContainerNameData.Text;
            string dataContent = richTextBoxDataContent.Text;
   
            if (string.IsNullOrEmpty(applicationName) || string.IsNullOrEmpty(containerName) || string.IsNullOrEmpty(dataContent))
            {
                MessageBox.Show("Please enter both application name,container name and data content");
                return;
            }
            // Makes the Put Request
            string requestURI = "/api/somiod/" + applicationName + "/" + containerName;
            try
            {
                RequestsHandler.PostData(requestURI, client, applicationName, containerName, dataContent);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void buttonDELETEData_Click(object sender, EventArgs e)
        {
            // Verifies if Application Name Input is Empty
            string applicationName = textBoxApplicationNameData.Text;
            string containerName = textBoxContainerNameData.Text;
            string dataId = textBoxDataID.Text;

            if (string.IsNullOrEmpty(applicationName) || string.IsNullOrEmpty(containerName) || string.IsNullOrEmpty(dataId))
            {
                MessageBox.Show("Please enter both application name,container name and data content");
                return;
            }
            string requestURI = "/api/somiod/" + applicationName + "/" + containerName + "/data" + "/" + dataId;
            try
            {
                RequestsHandler.DeleteData(requestURI, client);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        
    }
}
