using RestSharp;
using System;
using System.Windows.Forms;

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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBoxGetModuleName_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonGetModule_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void buttonGetAllApplications_Click(object sender, EventArgs e) //GET all Applications
        {


        }
    }
}
