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
            string requestURI = "/api/somiod/";
            XmlDocument doc = RequestsHandler.getResponseAsXMLDocument(requestURI, client, "applications");

            if (doc == null)
            {
                return;
            }

            // Loads the XML document to the RichTextBox
            TextBoxListAllApplications.Text = doc.InnerXml;

        }
    }
}
