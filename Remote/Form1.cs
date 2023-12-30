using RestSharp;
using System;
using System.Net;
using System.Windows.Forms;
using System.Xml;

namespace Remote
{
    public partial class Remote : Form
    {
        string baseURI = @"http://localhost:52885";
        RestClient client = null;

        public Remote()
        {
            InitializeComponent();
            client = new RestClient(baseURI);
        }

        private void GetApplications()
        {
            appComboBox.Items.Clear();

            // Creates and Executes a GET request
            RestRequest request = new RestRequest("/api/somiod/", Method.Get);
            request.AddHeader("somiod-discover", "application");
            RestResponse response = client.Execute(request);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                MessageBox.Show("Could not retrieve applications due to bad request");
                return;
            }

            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(response.Content);

                XmlNamespaceManager ns = new XmlNamespaceManager(xmlDoc.NameTable);
                ns.AddNamespace("ns", "urn:ipl:somiod:schemas");

                XmlNodeList list = xmlDoc.SelectNodes("/ArrayOfApplication/Application/name");

                if (list == null)
                {
                    throw new Exception("Could not retrieve the applications");
                }
                foreach (XmlNode item in list)
                {
                    appComboBox.Items.Add(item.InnerText);
                }
            }
            catch (Exception ex)
            {
                // Handle the XML parsing exception or any other exception if needed
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        private void GetContainersFromApplication()
        {
            containerComboBox.Items.Clear();
            string  applicationSelected = appComboBox.SelectedItem.ToString();
            // Creates and Executes a GET request
            RestRequest request = new RestRequest("/api/somiod/" + applicationSelected + "/containers", Method.Get);
            request.AddHeader("somiod-discover", "container");
            RestResponse response = client.Execute(request);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                MessageBox.Show("Could not retrieve containers due to bad request");
                return;
            }

            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(response.Content);

                XmlNamespaceManager ns = new XmlNamespaceManager(xmlDoc.NameTable);
                ns.AddNamespace("ns", "urn:ipl:somiod:schemas");

                XmlNodeList list = xmlDoc.SelectNodes("/ArrayOfContainer/Container/name");

                if (list == null)
                {
                    MessageBox.Show("No containers in this application");
                }
                foreach (XmlNode item in list)
                {
                    containerComboBox.Items.Add(item.InnerText);
                }
            }
            catch (Exception ex)
            {
                // Handle the XML parsing exception or any other exception if needed
                Console.WriteLine($"Error: {ex.Message}");
            }
        }


        private void appComboBox_MouseClick(object sender, MouseEventArgs e)
        {
            GetApplications();
        }

        private void appComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetContainersFromApplication();
        }
    }
}
