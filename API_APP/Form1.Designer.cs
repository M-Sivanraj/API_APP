namespace API_APP
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
            this.urlListCollections = new System.Windows.Forms.ListBox();
            this.ApiAppslistBox = new System.Windows.Forms.ListBox();
            this.btnApiApps = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // urlListCollections
            // 
            this.urlListCollections.FormattingEnabled = true;
            this.urlListCollections.Location = new System.Drawing.Point(420, 12);
            this.urlListCollections.Name = "urlListCollections";
            this.urlListCollections.Size = new System.Drawing.Size(227, 277);
            this.urlListCollections.TabIndex = 5;
            // 
            // ApiAppslistBox
            // 
            this.ApiAppslistBox.FormattingEnabled = true;
            this.ApiAppslistBox.Location = new System.Drawing.Point(171, 12);
            this.ApiAppslistBox.Name = "ApiAppslistBox";
            this.ApiAppslistBox.Size = new System.Drawing.Size(227, 277);
            this.ApiAppslistBox.TabIndex = 4;
            // 
            // btnApiApps
            // 
            this.btnApiApps.Location = new System.Drawing.Point(11, 119);
            this.btnApiApps.Name = "btnApiApps";
            this.btnApiApps.Size = new System.Drawing.Size(129, 23);
            this.btnApiApps.TabIndex = 3;
            this.btnApiApps.Text = "Get Api Apps";
            this.btnApiApps.UseVisualStyleBackColor = true;
            this.btnApiApps.Click += new System.EventHandler(this.btnApiApps_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(656, 299);
            this.Controls.Add(this.urlListCollections);
            this.Controls.Add(this.ApiAppslistBox);
            this.Controls.Add(this.btnApiApps);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox urlListCollections;
        private System.Windows.Forms.ListBox ApiAppslistBox;
        private System.Windows.Forms.Button btnApiApps;
    }
}

