namespace Remote
{
    partial class Remote
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
            this.onButton = new System.Windows.Forms.Button();
            this.offButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.appComboBox = new System.Windows.Forms.ComboBox();
            this.containerComboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // onButton
            // 
            this.onButton.Location = new System.Drawing.Point(81, 188);
            this.onButton.Name = "onButton";
            this.onButton.Size = new System.Drawing.Size(344, 133);
            this.onButton.TabIndex = 0;
            this.onButton.Text = "ON";
            this.onButton.UseVisualStyleBackColor = true;
            // 
            // offButton
            // 
            this.offButton.Location = new System.Drawing.Point(81, 346);
            this.offButton.Name = "offButton";
            this.offButton.Size = new System.Drawing.Size(344, 133);
            this.offButton.TabIndex = 1;
            this.offButton.Text = "OFF";
            this.offButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(81, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "Application name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(81, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(127, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "Container name";
            // 
            // appComboBox
            // 
            this.appComboBox.FormattingEnabled = true;
            this.appComboBox.Location = new System.Drawing.Point(247, 44);
            this.appComboBox.Name = "appComboBox";
            this.appComboBox.Size = new System.Drawing.Size(178, 24);
            this.appComboBox.TabIndex = 6;
            this.appComboBox.SelectedIndexChanged += new System.EventHandler(this.appComboBox_SelectedIndexChanged);
            this.appComboBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.appComboBox_MouseClick);
            // 
            // containerComboBox
            // 
            this.containerComboBox.FormattingEnabled = true;
            this.containerComboBox.Location = new System.Drawing.Point(247, 91);
            this.containerComboBox.Name = "containerComboBox";
            this.containerComboBox.Size = new System.Drawing.Size(178, 24);
            this.containerComboBox.TabIndex = 7;
            // 
            // Remote
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SlateGray;
            this.ClientSize = new System.Drawing.Size(506, 514);
            this.Controls.Add(this.containerComboBox);
            this.Controls.Add(this.appComboBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.offButton);
            this.Controls.Add(this.onButton);
            this.Name = "Remote";
            this.Text = "Remote ";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button onButton;
        private System.Windows.Forms.Button offButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox appComboBox;
        private System.Windows.Forms.ComboBox containerComboBox;
    }
}

