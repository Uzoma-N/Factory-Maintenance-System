namespace Maintenance_system
{
    partial class formServer
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
            this.addressBox = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.serverText = new System.Windows.Forms.TextBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.Connection = new System.Windows.Forms.Label();
            this.ServerStatus = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // addressBox
            // 
            this.addressBox.Location = new System.Drawing.Point(12, 242);
            this.addressBox.Name = "addressBox";
            this.addressBox.Size = new System.Drawing.Size(463, 20);
            this.addressBox.TabIndex = 29;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(264, 266);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 28;
            this.button1.Text = "Save In";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // serverText
            // 
            this.serverText.Location = new System.Drawing.Point(12, 84);
            this.serverText.Multiline = true;
            this.serverText.Name = "serverText";
            this.serverText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.serverText.Size = new System.Drawing.Size(463, 155);
            this.serverText.TabIndex = 27;
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(400, 268);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 25;
            this.btnStop.Text = "Stop Server";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click_1);
            // 
            // Connection
            // 
            this.Connection.AutoSize = true;
            this.Connection.Location = new System.Drawing.Point(65, 69);
            this.Connection.Name = "Connection";
            this.Connection.Size = new System.Drawing.Size(0, 13);
            this.Connection.TabIndex = 22;
            // 
            // ServerStatus
            // 
            this.ServerStatus.AutoSize = true;
            this.ServerStatus.Location = new System.Drawing.Point(65, 51);
            this.ServerStatus.Name = "ServerStatus";
            this.ServerStatus.Size = new System.Drawing.Size(0, 13);
            this.ServerStatus.TabIndex = 23;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(13, 13);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 30;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // formServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(496, 297);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.addressBox);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.serverText);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.Connection);
            this.Controls.Add(this.ServerStatus);
            this.Name = "formServer";
            this.Text = "formServer";
            this.Load += new System.EventHandler(this.formServer_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox addressBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox serverText;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Label Connection;
        private System.Windows.Forms.Label ServerStatus;
        private System.Windows.Forms.Button btnStart;
    }
}