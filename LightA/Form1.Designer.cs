﻿namespace LightA
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
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxSubscriptionEndPoint = new System.Windows.Forms.TextBox();
            this.comboBoxEventType = new System.Windows.Forms.ComboBox();
            this.textBoxSubscriptionName = new System.Windows.Forms.TextBox();
            this.buttonCreate = new System.Windows.Forms.Button();
            this.textBoxContainerName = new System.Windows.Forms.TextBox();
            this.richTextBoxLightBulb = new System.Windows.Forms.RichTextBox();
            this.textBoxApplicationName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 122);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(111, 13);
            this.label5.TabIndex = 24;
            this.label5.Text = "Subscription EndPoint";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 94);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(123, 13);
            this.label4.TabIndex = 23;
            this.label4.Text = "Subscription Event Type";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 13);
            this.label3.TabIndex = 22;
            this.label3.Text = "Subscription Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 21;
            this.label2.Text = "Container Name";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "Application Name";
            // 
            // textBoxSubscriptionEndPoint
            // 
            this.textBoxSubscriptionEndPoint.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxSubscriptionEndPoint.Location = new System.Drawing.Point(142, 119);
            this.textBoxSubscriptionEndPoint.Name = "textBoxSubscriptionEndPoint";
            this.textBoxSubscriptionEndPoint.Size = new System.Drawing.Size(132, 20);
            this.textBoxSubscriptionEndPoint.TabIndex = 19;
            // 
            // comboBoxEventType
            // 
            this.comboBoxEventType.FormattingEnabled = true;
            this.comboBoxEventType.Items.AddRange(new object[] {
            "creation",
            "deletion",
            "creation and deletion"});
            this.comboBoxEventType.Location = new System.Drawing.Point(142, 91);
            this.comboBoxEventType.Name = "comboBoxEventType";
            this.comboBoxEventType.Size = new System.Drawing.Size(132, 21);
            this.comboBoxEventType.TabIndex = 18;
            // 
            // textBoxSubscriptionName
            // 
            this.textBoxSubscriptionName.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxSubscriptionName.Location = new System.Drawing.Point(142, 66);
            this.textBoxSubscriptionName.Name = "textBoxSubscriptionName";
            this.textBoxSubscriptionName.Size = new System.Drawing.Size(132, 20);
            this.textBoxSubscriptionName.TabIndex = 17;
            // 
            // buttonCreate
            // 
            this.buttonCreate.Location = new System.Drawing.Point(205, 146);
            this.buttonCreate.Name = "buttonCreate";
            this.buttonCreate.Size = new System.Drawing.Size(69, 23);
            this.buttonCreate.TabIndex = 16;
            this.buttonCreate.Text = "Create";
            this.buttonCreate.UseVisualStyleBackColor = true;
            this.buttonCreate.Click += new System.EventHandler(this.buttonCreate_Click);
            // 
            // textBoxContainerName
            // 
            this.textBoxContainerName.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxContainerName.Location = new System.Drawing.Point(142, 40);
            this.textBoxContainerName.Name = "textBoxContainerName";
            this.textBoxContainerName.Size = new System.Drawing.Size(132, 20);
            this.textBoxContainerName.TabIndex = 15;
            // 
            // richTextBoxLightBulb
            // 
            this.richTextBoxLightBulb.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.richTextBoxLightBulb.Location = new System.Drawing.Point(16, 175);
            this.richTextBoxLightBulb.Name = "richTextBoxLightBulb";
            this.richTextBoxLightBulb.Size = new System.Drawing.Size(141, 124);
            this.richTextBoxLightBulb.TabIndex = 14;
            this.richTextBoxLightBulb.Text = "";
            // 
            // textBoxApplicationName
            // 
            this.textBoxApplicationName.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxApplicationName.Location = new System.Drawing.Point(142, 14);
            this.textBoxApplicationName.Name = "textBoxApplicationName";
            this.textBoxApplicationName.Size = new System.Drawing.Size(132, 20);
            this.textBoxApplicationName.TabIndex = 13;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.ClientSize = new System.Drawing.Size(292, 326);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxSubscriptionEndPoint);
            this.Controls.Add(this.comboBoxEventType);
            this.Controls.Add(this.textBoxSubscriptionName);
            this.Controls.Add(this.buttonCreate);
            this.Controls.Add(this.textBoxContainerName);
            this.Controls.Add(this.richTextBoxLightBulb);
            this.Controls.Add(this.textBoxApplicationName);
            this.Name = "Form1";
            this.Text = "Light A";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxSubscriptionEndPoint;
        private System.Windows.Forms.ComboBox comboBoxEventType;
        private System.Windows.Forms.TextBox textBoxSubscriptionName;
        private System.Windows.Forms.Button buttonCreate;
        private System.Windows.Forms.TextBox textBoxContainerName;
        private System.Windows.Forms.RichTextBox richTextBoxLightBulb;
        private System.Windows.Forms.TextBox textBoxApplicationName;
    }
}

