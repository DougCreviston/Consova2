namespace LynxClient
{
    partial class DlgUpdate
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
            this.DlgTxt = new System.Windows.Forms.Label();
            this.okBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // DlgTxt
            // 
            this.DlgTxt.AutoSize = true;
            this.DlgTxt.Location = new System.Drawing.Point(12, 31);
            this.DlgTxt.Name = "DlgTxt";
            this.DlgTxt.Size = new System.Drawing.Size(57, 13);
            this.DlgTxt.TabIndex = 0;
            this.DlgTxt.Text = "Dialog text";
            // 
            // okBtn
            // 
            this.okBtn.Location = new System.Drawing.Point(109, 88);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new System.Drawing.Size(75, 23);
            this.okBtn.TabIndex = 1;
            this.okBtn.Text = "Okay";
            this.okBtn.UseVisualStyleBackColor = true;
            this.okBtn.Click += new System.EventHandler(this.okBtn_Click);
            // 
            // DlgUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(305, 114);
            this.Controls.Add(this.okBtn);
            this.Controls.Add(this.DlgTxt);
            this.Name = "DlgUpdate";
            this.Text = "Information";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label DlgTxt;
        private System.Windows.Forms.Button okBtn;
    }
}