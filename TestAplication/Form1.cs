using RestSharp;
using System;
using System.Windows.Forms;
using System.Xml.Linq;


namespace TestAplication
{
    public partial class Form1 : Form
    {
        string baseURI = @"http://localhost:52885";
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
            string requestUri = "/api/somiod/" + applicationName;

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
            string requestURI = "/api/somiod/" + applicationName + "/container/" + containerName;
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
                RequestsHandler.PostContainer(requestURI, client, containerName);
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
                RequestsHandler.PostData(requestURI, client, dataContent);
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

 

        private void buttonPOSTSubscription_Click(object sender, EventArgs e)
        {
            string applicationName = textBoxApplicationNameSubscription.Text;
            string containerName = textBoxContainerNameSubscription.Text;
            string subName = textBoxSubscriptionNamePOST.Text;
            string subEvent = comboBoxSubscriptionEvents.SelectedItem.ToString();
            MessageBox.Show(subEvent);
            string subEndpoint = textBoxEndPoint.Text;

            if (string.IsNullOrEmpty(applicationName) || string.IsNullOrEmpty(containerName) || string.IsNullOrEmpty(subName) || string.IsNullOrEmpty(subEvent) || string.IsNullOrEmpty(subEndpoint))
            {
                MessageBox.Show("Please fill all the information");
                return;
            }
            // Makes the Post Request
            string requestURI = "/api/somiod/" + applicationName + "/" + containerName;
            try
            {
                RequestsHandler.PostSubscription(requestURI, client,subName,subEvent, subEndpoint);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void buttonDELETESubscription_Click(object sender, EventArgs e)
        {
            string applicationName = textBoxApplicationNameSubscription.Text;
            string containerName = textBoxContainerNameSubscription.Text;
            string subName = textBoxSubscriptionNamePOST.Text;


            if (string.IsNullOrEmpty(applicationName) || string.IsNullOrEmpty(containerName) || string.IsNullOrEmpty(subName))
            {
                MessageBox.Show("Please fill all the information");
                return;
            }
            // Makes the Delete Request
            string requestURI = "/api/somiod/" + applicationName + "/" + containerName + "/" + subName;
            try
            {
                RequestsHandler.DeleteSubscription(requestURI, client);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
