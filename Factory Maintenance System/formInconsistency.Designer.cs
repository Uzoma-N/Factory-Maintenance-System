namespace Maintenance_system
{
    partial class formInconsistency
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
            this.btnClearCheck = new System.Windows.Forms.Button();
            this.icheckDetail = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnClearCheck
            // 
            this.btnClearCheck.Location = new System.Drawing.Point(254, 306);
            this.btnClearCheck.Name = "btnClearCheck";
            this.btnClearCheck.Size = new System.Drawing.Size(75, 23);
            this.btnClearCheck.TabIndex = 6;
            this.btnClearCheck.Text = "Clear";
            this.btnClearCheck.UseVisualStyleBackColor = true;
            this.btnClearCheck.Click += new System.EventHandler(this.btnClearCheck_Click_1);
            // 
            // icheckDetail
            // 
            this.icheckDetail.Location = new System.Drawing.Point(48, 12);
            this.icheckDetail.Multiline = true;
            this.icheckDetail.Name = "icheckDetail";
            this.icheckDetail.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.icheckDetail.Size = new System.Drawing.Size(281, 288);
            this.icheckDetail.TabIndex = 5;
            // 
            // formInconsistency
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(371, 340);
            this.Controls.Add(this.btnClearCheck);
            this.Controls.Add(this.icheckDetail);
            this.Name = "formInconsistency";
            this.Text = "formInconsistency";
            this.Load += new System.EventHandler(this.formInconsistency_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClearCheck;
        private System.Windows.Forms.TextBox icheckDetail;
    }
}