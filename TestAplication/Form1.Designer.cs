using System;

namespace TestAplication
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.buttonDELETEApplication = new System.Windows.Forms.Button();
            this.buttonPUTApplication = new System.Windows.Forms.Button();
            this.buttonPOSTApplication = new System.Windows.Forms.Button();
            this.textBoxApplicationNewName = new System.Windows.Forms.TextBox();
            this.textBoxApplicationName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.buttonGetApplication = new System.Windows.Forms.Button();
            this.TextBoxListAllApplications = new System.Windows.Forms.RichTextBox();
            this.buttonGetAllApplications = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.buttonDELETEContainer = new System.Windows.Forms.Button();
            this.buttonPUTContainer = new System.Windows.Forms.Button();
            this.buttonPOSTContainer = new System.Windows.Forms.Button();
            this.richTextBoxListContainers = new System.Windows.Forms.RichTextBox();
            this.textBoxContainerNewName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxContainerName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxApplicationNameContainer = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxGetContainerName = new System.Windows.Forms.TextBox();
            this.richTextBoxSpecificContainer = new System.Windows.Forms.RichTextBox();
            this.buttonGetContainer = new System.Windows.Forms.Button();
            this.buttonGetAllContainers = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.buttonDELETEData = new System.Windows.Forms.Button();
            this.textBoxDataID = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.buttonPOSTData = new System.Windows.Forms.Button();
            this.richTextBoxDataContent = new System.Windows.Forms.RichTextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textBoxContainerNameData = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxApplicationNameData = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.buttonPOSTSubscription = new System.Windows.Forms.Button();
            this.textBoxEndPoint = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.comboBoxSubscriptionEvents = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.textBoxSubscriptionNamePOST = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.buttonDELETESubscription = new System.Windows.Forms.Button();
            this.textBoxContainerNameSubscription = new System.Windows.Forms.TextBox();
            this.textBoxApplicationNameSubscription = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(1, 2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(799, 448);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.buttonDELETEApplication);
            this.tabPage1.Controls.Add(this.buttonPUTApplication);
            this.tabPage1.Controls.Add(this.buttonPOSTApplication);
            this.tabPage1.Controls.Add(this.textBoxApplicationNewName);
            this.tabPage1.Controls.Add(this.textBoxApplicationName);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.richTextBox1);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.textBox1);
            this.tabPage1.Controls.Add(this.buttonGetApplication);
            this.tabPage1.Controls.Add(this.TextBoxListAllApplications);
            this.tabPage1.Controls.Add(this.buttonGetAllApplications);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(791, 422);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Application";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // buttonDELETEApplication
            // 
            this.buttonDELETEApplication.Location = new System.Drawing.Point(298, 378);
            this.buttonDELETEApplication.Name = "buttonDELETEApplication";
            this.buttonDELETEApplication.Size = new System.Drawing.Size(124, 22);
            this.buttonDELETEApplication.TabIndex = 12;
            this.buttonDELETEApplication.Text = "DEL (Delete)";
            this.buttonDELETEApplication.UseVisualStyleBackColor = true;
            // 
            // buttonPUTApplication
            // 
            this.buttonPUTApplication.Location = new System.Drawing.Point(298, 350);
            this.buttonPUTApplication.Name = "buttonPUTApplication";
            this.buttonPUTApplication.Size = new System.Drawing.Size(124, 22);
            this.buttonPUTApplication.TabIndex = 11;
            this.buttonPUTApplication.Text = "PUT (Update)";
            this.buttonPUTApplication.UseVisualStyleBackColor = true;
            // 
            // buttonPOSTApplication
            // 
            this.buttonPOSTApplication.Location = new System.Drawing.Point(298, 322);
            this.buttonPOSTApplication.Name = "buttonPOSTApplication";
            this.buttonPOSTApplication.Size = new System.Drawing.Size(124, 22);
            this.buttonPOSTApplication.TabIndex = 10;
            this.buttonPOSTApplication.Text = "POST (Create)";
            this.buttonPOSTApplication.UseVisualStyleBackColor = true;
            // 
            // textBoxApplicationNewName
            // 
            this.textBoxApplicationNewName.Location = new System.Drawing.Point(86, 370);
            this.textBoxApplicationNewName.Name = "textBoxApplicationNewName";
            this.textBoxApplicationNewName.Size = new System.Drawing.Size(162, 20);
            this.textBoxApplicationNewName.TabIndex = 9;
            // 
            // textBoxApplicationName
            // 
            this.textBoxApplicationName.Location = new System.Drawing.Point(86, 328);
            this.textBoxApplicationName.Name = "textBoxApplicationName";
            this.textBoxApplicationName.Size = new System.Drawing.Size(162, 20);
            this.textBoxApplicationName.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 370);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "New Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 331);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Name:";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(432, 66);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(340, 68);
            this.richTextBox1.TabIndex = 5;
            this.richTextBox1.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(603, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Name:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(647, 28);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(125, 20);
            this.textBox1.TabIndex = 3;
            // 
            // buttonGetApplication
            // 
            this.buttonGetApplication.Location = new System.Drawing.Point(432, 26);
            this.buttonGetApplication.Name = "buttonGetApplication";
            this.buttonGetApplication.Size = new System.Drawing.Size(107, 23);
            this.buttonGetApplication.TabIndex = 2;
            this.buttonGetApplication.Text = "Get Application";
            this.buttonGetApplication.UseVisualStyleBackColor = true;
            // 
            // TextBoxListAllApplications
            // 
            this.TextBoxListAllApplications.Location = new System.Drawing.Point(20, 66);
            this.TextBoxListAllApplications.Name = "TextBoxListAllApplications";
            this.TextBoxListAllApplications.Size = new System.Drawing.Size(377, 234);
            this.TextBoxListAllApplications.TabIndex = 1;
            this.TextBoxListAllApplications.Text = "";
            // 
            // buttonGetAllApplications
            // 
            this.buttonGetAllApplications.Location = new System.Drawing.Point(20, 27);
            this.buttonGetAllApplications.Name = "buttonGetAllApplications";
            this.buttonGetAllApplications.Size = new System.Drawing.Size(144, 23);
            this.buttonGetAllApplications.TabIndex = 0;
            this.buttonGetAllApplications.Text = "Get All Applications";
            this.buttonGetAllApplications.UseVisualStyleBackColor = true;
            this.buttonGetAllApplications.Click += new System.EventHandler(this.buttonGetAllApplications_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.buttonDELETEContainer);
            this.tabPage2.Controls.Add(this.buttonPUTContainer);
            this.tabPage2.Controls.Add(this.buttonPOSTContainer);
            this.tabPage2.Controls.Add(this.richTextBoxListContainers);
            this.tabPage2.Controls.Add(this.textBoxContainerNewName);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.textBoxContainerName);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.textBoxApplicationNameContainer);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.textBoxGetContainerName);
            this.tabPage2.Controls.Add(this.richTextBoxSpecificContainer);
            this.tabPage2.Controls.Add(this.buttonGetContainer);
            this.tabPage2.Controls.Add(this.buttonGetAllContainers);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(791, 422);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Container";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // buttonDELETEContainer
            // 
            this.buttonDELETEContainer.Location = new System.Drawing.Point(320, 363);
            this.buttonDELETEContainer.Name = "buttonDELETEContainer";
            this.buttonDELETEContainer.Size = new System.Drawing.Size(124, 22);
            this.buttonDELETEContainer.TabIndex = 32;
            this.buttonDELETEContainer.Text = "DEL (Delete)";
            this.buttonDELETEContainer.UseVisualStyleBackColor = true;
            // 
            // buttonPUTContainer
            // 
            this.buttonPUTContainer.Location = new System.Drawing.Point(320, 335);
            this.buttonPUTContainer.Name = "buttonPUTContainer";
            this.buttonPUTContainer.Size = new System.Drawing.Size(124, 22);
            this.buttonPUTContainer.TabIndex = 31;
            this.buttonPUTContainer.Text = "PUT (Update)";
            this.buttonPUTContainer.UseVisualStyleBackColor = true;
            // 
            // buttonPOSTContainer
            // 
            this.buttonPOSTContainer.Location = new System.Drawing.Point(320, 307);
            this.buttonPOSTContainer.Name = "buttonPOSTContainer";
            this.buttonPOSTContainer.Size = new System.Drawing.Size(124, 22);
            this.buttonPOSTContainer.TabIndex = 30;
            this.buttonPOSTContainer.Text = "POST (Create)";
            this.buttonPOSTContainer.UseVisualStyleBackColor = true;
            // 
            // richTextBoxListContainers
            // 
            this.richTextBoxListContainers.BackColor = System.Drawing.SystemColors.MenuBar;
            this.richTextBoxListContainers.Location = new System.Drawing.Point(36, 96);
            this.richTextBoxListContainers.Name = "richTextBoxListContainers";
            this.richTextBoxListContainers.Size = new System.Drawing.Size(375, 187);
            this.richTextBoxListContainers.TabIndex = 29;
            this.richTextBoxListContainers.Text = "";
            // 
            // textBoxContainerNewName
            // 
            this.textBoxContainerNewName.Location = new System.Drawing.Point(140, 352);
            this.textBoxContainerNewName.Name = "textBoxContainerNewName";
            this.textBoxContainerNewName.Size = new System.Drawing.Size(110, 20);
            this.textBoxContainerNewName.TabIndex = 28;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(23, 355);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(111, 13);
            this.label7.TabIndex = 27;
            this.label7.Text = "New Container Name:";
            this.label7.UseMnemonic = false;
            // 
            // textBoxContainerName
            // 
            this.textBoxContainerName.Location = new System.Drawing.Point(140, 319);
            this.textBoxContainerName.Name = "textBoxContainerName";
            this.textBoxContainerName.Size = new System.Drawing.Size(110, 20);
            this.textBoxContainerName.TabIndex = 26;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(33, 322);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 13);
            this.label5.TabIndex = 25;
            this.label5.Text = "Container Name:";
            this.label5.UseMnemonic = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(33, 28);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(93, 13);
            this.label6.TabIndex = 24;
            this.label6.Text = "Application Name:";
            this.label6.UseMnemonic = false;
            // 
            // textBoxApplicationNameContainer
            // 
            this.textBoxApplicationNameContainer.Location = new System.Drawing.Point(132, 25);
            this.textBoxApplicationNameContainer.Name = "textBoxApplicationNameContainer";
            this.textBoxApplicationNameContainer.Size = new System.Drawing.Size(100, 20);
            this.textBoxApplicationNameContainer.TabIndex = 23;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(568, 73);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 13);
            this.label4.TabIndex = 22;
            this.label4.Text = "Container Name:";
            this.label4.UseMnemonic = false;
            // 
            // textBoxGetContainerName
            // 
            this.textBoxGetContainerName.Location = new System.Drawing.Point(650, 70);
            this.textBoxGetContainerName.Name = "textBoxGetContainerName";
            this.textBoxGetContainerName.Size = new System.Drawing.Size(103, 20);
            this.textBoxGetContainerName.TabIndex = 21;
            // 
            // richTextBoxSpecificContainer
            // 
            this.richTextBoxSpecificContainer.BackColor = System.Drawing.SystemColors.MenuBar;
            this.richTextBoxSpecificContainer.Location = new System.Drawing.Point(417, 96);
            this.richTextBoxSpecificContainer.Name = "richTextBoxSpecificContainer";
            this.richTextBoxSpecificContainer.Size = new System.Drawing.Size(352, 187);
            this.richTextBoxSpecificContainer.TabIndex = 20;
            this.richTextBoxSpecificContainer.Text = "";
            // 
            // buttonGetContainer
            // 
            this.buttonGetContainer.Location = new System.Drawing.Point(438, 68);
            this.buttonGetContainer.Name = "buttonGetContainer";
            this.buttonGetContainer.Size = new System.Drawing.Size(111, 22);
            this.buttonGetContainer.TabIndex = 19;
            this.buttonGetContainer.Text = "Get Container";
            this.buttonGetContainer.UseVisualStyleBackColor = true;
            // 
            // buttonGetAllContainers
            // 
            this.buttonGetAllContainers.Location = new System.Drawing.Point(33, 68);
            this.buttonGetAllContainers.Name = "buttonGetAllContainers";
            this.buttonGetAllContainers.Size = new System.Drawing.Size(124, 22);
            this.buttonGetAllContainers.TabIndex = 18;
            this.buttonGetAllContainers.Text = "Get All Containers\r\n";
            this.buttonGetAllContainers.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.buttonDELETEData);
            this.tabPage3.Controls.Add(this.textBoxDataID);
            this.tabPage3.Controls.Add(this.label11);
            this.tabPage3.Controls.Add(this.buttonPOSTData);
            this.tabPage3.Controls.Add(this.richTextBoxDataContent);
            this.tabPage3.Controls.Add(this.label10);
            this.tabPage3.Controls.Add(this.textBoxContainerNameData);
            this.tabPage3.Controls.Add(this.label9);
            this.tabPage3.Controls.Add(this.textBoxApplicationNameData);
            this.tabPage3.Controls.Add(this.label8);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(791, 422);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Data";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // buttonDELETEData
            // 
            this.buttonDELETEData.Location = new System.Drawing.Point(518, 17);
            this.buttonDELETEData.Name = "buttonDELETEData";
            this.buttonDELETEData.Size = new System.Drawing.Size(124, 22);
            this.buttonDELETEData.TabIndex = 29;
            this.buttonDELETEData.Text = "DEL (Delete)";
            this.buttonDELETEData.UseVisualStyleBackColor = true;
            // 
            // textBoxDataID
            // 
            this.textBoxDataID.Location = new System.Drawing.Point(401, 17);
            this.textBoxDataID.Name = "textBoxDataID";
            this.textBoxDataID.Size = new System.Drawing.Size(100, 20);
            this.textBoxDataID.TabIndex = 28;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(348, 20);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(47, 13);
            this.label11.TabIndex = 27;
            this.label11.Text = "Data ID:";
            this.label11.UseMnemonic = false;
            // 
            // buttonPOSTData
            // 
            this.buttonPOSTData.Location = new System.Drawing.Point(17, 185);
            this.buttonPOSTData.Name = "buttonPOSTData";
            this.buttonPOSTData.Size = new System.Drawing.Size(124, 22);
            this.buttonPOSTData.TabIndex = 26;
            this.buttonPOSTData.Text = "POST (Create)";
            this.buttonPOSTData.UseVisualStyleBackColor = true;
            // 
            // richTextBoxDataContent
            // 
            this.richTextBoxDataContent.BackColor = System.Drawing.SystemColors.MenuBar;
            this.richTextBoxDataContent.Location = new System.Drawing.Point(17, 111);
            this.richTextBoxDataContent.Name = "richTextBoxDataContent";
            this.richTextBoxDataContent.Size = new System.Drawing.Size(282, 68);
            this.richTextBoxDataContent.TabIndex = 25;
            this.richTextBoxDataContent.Text = "";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(14, 95);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(47, 13);
            this.label10.TabIndex = 24;
            this.label10.Text = "Content:";
            this.label10.UseMnemonic = false;
            // 
            // textBoxContainerNameData
            // 
            this.textBoxContainerNameData.Location = new System.Drawing.Point(113, 47);
            this.textBoxContainerNameData.Name = "textBoxContainerNameData";
            this.textBoxContainerNameData.Size = new System.Drawing.Size(100, 20);
            this.textBoxContainerNameData.TabIndex = 23;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(14, 47);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(86, 13);
            this.label9.TabIndex = 22;
            this.label9.Text = "Container Name:";
            this.label9.UseMnemonic = false;
            // 
            // textBoxApplicationNameData
            // 
            this.textBoxApplicationNameData.Location = new System.Drawing.Point(113, 17);
            this.textBoxApplicationNameData.Name = "textBoxApplicationNameData";
            this.textBoxApplicationNameData.Size = new System.Drawing.Size(100, 20);
            this.textBoxApplicationNameData.TabIndex = 21;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(14, 20);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(93, 13);
            this.label8.TabIndex = 20;
            this.label8.Text = "Application Name:";
            this.label8.UseMnemonic = false;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.buttonPOSTSubscription);
            this.tabPage4.Controls.Add(this.textBoxEndPoint);
            this.tabPage4.Controls.Add(this.label17);
            this.tabPage4.Controls.Add(this.comboBoxSubscriptionEvents);
            this.tabPage4.Controls.Add(this.label15);
            this.tabPage4.Controls.Add(this.textBoxSubscriptionNamePOST);
            this.tabPage4.Controls.Add(this.label14);
            this.tabPage4.Controls.Add(this.buttonDELETESubscription);
            this.tabPage4.Controls.Add(this.textBoxContainerNameSubscription);
            this.tabPage4.Controls.Add(this.textBoxApplicationNameSubscription);
            this.tabPage4.Controls.Add(this.label13);
            this.tabPage4.Controls.Add(this.label12);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(791, 422);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Subscriptions";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // buttonPOSTSubscription
            // 
            this.buttonPOSTSubscription.Location = new System.Drawing.Point(153, 188);
            this.buttonPOSTSubscription.Name = "buttonPOSTSubscription";
            this.buttonPOSTSubscription.Size = new System.Drawing.Size(124, 22);
            this.buttonPOSTSubscription.TabIndex = 41;
            this.buttonPOSTSubscription.Text = "POST (Create)";
            this.buttonPOSTSubscription.UseVisualStyleBackColor = true;
            // 
            // textBoxEndPoint
            // 
            this.textBoxEndPoint.Location = new System.Drawing.Point(138, 148);
            this.textBoxEndPoint.Name = "textBoxEndPoint";
            this.textBoxEndPoint.Size = new System.Drawing.Size(121, 20);
            this.textBoxEndPoint.TabIndex = 40;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(21, 151);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(114, 13);
            this.label17.TabIndex = 39;
            this.label17.Text = "Subscription EndPoint:";
            this.label17.UseMnemonic = false;
            // 
            // comboBoxSubscriptionEvents
            // 
            this.comboBoxSubscriptionEvents.FormattingEnabled = true;
            this.comboBoxSubscriptionEvents.Items.AddRange(new object[] {
            "creation",
            "deletion",
            "creation and deletion"});
            this.comboBoxSubscriptionEvents.Location = new System.Drawing.Point(138, 121);
            this.comboBoxSubscriptionEvents.Name = "comboBoxSubscriptionEvents";
            this.comboBoxSubscriptionEvents.Size = new System.Drawing.Size(121, 21);
            this.comboBoxSubscriptionEvents.TabIndex = 38;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(21, 124);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(99, 13);
            this.label15.TabIndex = 37;
            this.label15.Text = "Subscription Event:";
            this.label15.UseMnemonic = false;
            // 
            // textBoxSubscriptionNamePOST
            // 
            this.textBoxSubscriptionNamePOST.Location = new System.Drawing.Point(138, 91);
            this.textBoxSubscriptionNamePOST.Name = "textBoxSubscriptionNamePOST";
            this.textBoxSubscriptionNamePOST.Size = new System.Drawing.Size(121, 20);
            this.textBoxSubscriptionNamePOST.TabIndex = 36;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(21, 94);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(99, 13);
            this.label14.TabIndex = 35;
            this.label14.Text = "Subscription Name:";
            this.label14.UseMnemonic = false;
            // 
            // buttonDELETESubscription
            // 
            this.buttonDELETESubscription.Location = new System.Drawing.Point(23, 188);
            this.buttonDELETESubscription.Name = "buttonDELETESubscription";
            this.buttonDELETESubscription.Size = new System.Drawing.Size(124, 22);
            this.buttonDELETESubscription.TabIndex = 34;
            this.buttonDELETESubscription.Text = "DEL (Delete)";
            this.buttonDELETESubscription.UseVisualStyleBackColor = true;
            // 
            // textBoxContainerNameSubscription
            // 
            this.textBoxContainerNameSubscription.Location = new System.Drawing.Point(138, 48);
            this.textBoxContainerNameSubscription.Name = "textBoxContainerNameSubscription";
            this.textBoxContainerNameSubscription.Size = new System.Drawing.Size(121, 20);
            this.textBoxContainerNameSubscription.TabIndex = 33;
            // 
            // textBoxApplicationNameSubscription
            // 
            this.textBoxApplicationNameSubscription.Location = new System.Drawing.Point(138, 22);
            this.textBoxApplicationNameSubscription.Name = "textBoxApplicationNameSubscription";
            this.textBoxApplicationNameSubscription.Size = new System.Drawing.Size(121, 20);
            this.textBoxApplicationNameSubscription.TabIndex = 32;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(21, 51);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(86, 13);
            this.label13.TabIndex = 31;
            this.label13.Text = "Container Name:";
            this.label13.UseMnemonic = false;
            this.label13.Click += new System.EventHandler(this.label13_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(21, 23);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(93, 13);
            this.label12.TabIndex = 30;
            this.label12.Text = "Application Name:";
            this.label12.UseMnemonic = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Test Application";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.ResumeLayout(false);

        }

        private void label13_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button buttonGetApplication;
        private System.Windows.Forms.RichTextBox TextBoxListAllApplications;
        private System.Windows.Forms.Button buttonGetAllApplications;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button buttonDELETEApplication;
        private System.Windows.Forms.Button buttonPUTApplication;
        private System.Windows.Forms.Button buttonPOSTApplication;
        private System.Windows.Forms.TextBox textBoxApplicationNewName;
        private System.Windows.Forms.TextBox textBoxApplicationName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button buttonDELETEContainer;
        private System.Windows.Forms.Button buttonPUTContainer;
        private System.Windows.Forms.Button buttonPOSTContainer;
        private System.Windows.Forms.RichTextBox richTextBoxListContainers;
        private System.Windows.Forms.TextBox textBoxContainerNewName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxContainerName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxApplicationNameContainer;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxGetContainerName;
        private System.Windows.Forms.RichTextBox richTextBoxSpecificContainer;
        private System.Windows.Forms.Button buttonGetContainer;
        private System.Windows.Forms.Button buttonGetAllContainers;
        private System.Windows.Forms.Button buttonDELETEData;
        private System.Windows.Forms.TextBox textBoxDataID;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button buttonPOSTData;
        private System.Windows.Forms.RichTextBox richTextBoxDataContent;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBoxContainerNameData;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBoxApplicationNameData;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button buttonPOSTSubscription;
        private System.Windows.Forms.TextBox textBoxEndPoint;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ComboBox comboBoxSubscriptionEvents;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox textBoxSubscriptionNamePOST;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button buttonDELETESubscription;
        private System.Windows.Forms.TextBox textBoxContainerNameSubscription;
        private System.Windows.Forms.TextBox textBoxApplicationNameSubscription;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
    }
}

