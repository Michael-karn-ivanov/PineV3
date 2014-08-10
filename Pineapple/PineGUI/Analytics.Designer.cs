namespace PineGUI
{
    partial class Analytics
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
            this.lbxData = new System.Windows.Forms.ListBox();
            this.lbxStat = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // lbxData
            // 
            this.lbxData.FormattingEnabled = true;
            this.lbxData.ItemHeight = 20;
            this.lbxData.Location = new System.Drawing.Point(12, 12);
            this.lbxData.Name = "lbxData";
            this.lbxData.Size = new System.Drawing.Size(293, 444);
            this.lbxData.TabIndex = 0;
            this.lbxData.SelectedIndexChanged += new System.EventHandler(this.lbxData_SelectedIndexChanged);
            // 
            // lbxStat
            // 
            this.lbxStat.FormattingEnabled = true;
            this.lbxStat.ItemHeight = 20;
            this.lbxStat.Location = new System.Drawing.Point(339, 12);
            this.lbxStat.Name = "lbxStat";
            this.lbxStat.Size = new System.Drawing.Size(457, 444);
            this.lbxStat.TabIndex = 1;
            // 
            // Analytics
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(808, 469);
            this.Controls.Add(this.lbxStat);
            this.Controls.Add(this.lbxData);
            this.Name = "Analytics";
            this.Text = "Analytics";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lbxData;
        private System.Windows.Forms.ListBox lbxStat;
    }
}