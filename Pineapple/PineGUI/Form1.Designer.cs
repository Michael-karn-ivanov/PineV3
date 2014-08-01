namespace PineGUI
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
            this.tbxVillain = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbxHeroShort = new System.Windows.Forms.TextBox();
            this.tbxHeroTop = new System.Windows.Forms.TextBox();
            this.tbxHeroMiddle = new System.Windows.Forms.TextBox();
            this.Hero = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxCurrentTriple = new System.Windows.Forms.TextBox();
            this.tbxDeadCards = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnFind = new System.Windows.Forms.Button();
            this.btnRestart = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tbx2ndSampleSize = new System.Windows.Forms.TextBox();
            this.tbx3rdSampleSize = new System.Windows.Forms.TextBox();
            this.lblBestPlace = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.tbxVillain2 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbxVillain
            // 
            this.tbxVillain.Location = new System.Drawing.Point(12, 33);
            this.tbxVillain.Multiline = true;
            this.tbxVillain.Name = "tbxVillain";
            this.tbxVillain.Size = new System.Drawing.Size(254, 124);
            this.tbxVillain.TabIndex = 0;
            this.tbxVillain.DoubleClick += new System.EventHandler(this.tbxVillain_DoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Villain";
            // 
            // tbxHeroShort
            // 
            this.tbxHeroShort.Location = new System.Drawing.Point(12, 367);
            this.tbxHeroShort.Name = "tbxHeroShort";
            this.tbxHeroShort.Size = new System.Drawing.Size(128, 26);
            this.tbxHeroShort.TabIndex = 2;
            this.tbxHeroShort.DoubleClick += new System.EventHandler(this.tbxHeroShort_DoubleClick);
            // 
            // tbxHeroTop
            // 
            this.tbxHeroTop.Location = new System.Drawing.Point(12, 461);
            this.tbxHeroTop.Name = "tbxHeroTop";
            this.tbxHeroTop.Size = new System.Drawing.Size(254, 26);
            this.tbxHeroTop.TabIndex = 3;
            this.tbxHeroTop.DoubleClick += new System.EventHandler(this.tbxHeroTop_DoubleClick);
            // 
            // tbxHeroMiddle
            // 
            this.tbxHeroMiddle.Location = new System.Drawing.Point(12, 414);
            this.tbxHeroMiddle.Name = "tbxHeroMiddle";
            this.tbxHeroMiddle.Size = new System.Drawing.Size(254, 26);
            this.tbxHeroMiddle.TabIndex = 4;
            this.tbxHeroMiddle.DoubleClick += new System.EventHandler(this.tbxHeroMiddle_DoubleClick);
            // 
            // Hero
            // 
            this.Hero.AutoSize = true;
            this.Hero.Location = new System.Drawing.Point(12, 341);
            this.Hero.Name = "Hero";
            this.Hero.Size = new System.Drawing.Size(44, 20);
            this.Hero.TabIndex = 5;
            this.Hero.Text = "Hero";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 207);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 20);
            this.label2.TabIndex = 6;
            this.label2.Text = "Current triple:";
            // 
            // tbxCurrentTriple
            // 
            this.tbxCurrentTriple.Location = new System.Drawing.Point(124, 200);
            this.tbxCurrentTriple.Name = "tbxCurrentTriple";
            this.tbxCurrentTriple.Size = new System.Drawing.Size(143, 26);
            this.tbxCurrentTriple.TabIndex = 7;
            this.tbxCurrentTriple.DoubleClick += new System.EventHandler(this.tbxCurrentTriple_DoubleClick);
            // 
            // tbxDeadCards
            // 
            this.tbxDeadCards.Location = new System.Drawing.Point(535, 273);
            this.tbxDeadCards.Multiline = true;
            this.tbxDeadCards.Name = "tbxDeadCards";
            this.tbxDeadCards.Size = new System.Drawing.Size(277, 76);
            this.tbxDeadCards.TabIndex = 8;
            this.tbxDeadCards.DoubleClick += new System.EventHandler(this.tbxDeadCards_DoubleClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(531, 243);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 20);
            this.label3.TabIndex = 9;
            this.label3.Text = "Dead cards";
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(16, 243);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(101, 41);
            this.btnApply.TabIndex = 10;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnFind
            // 
            this.btnFind.Location = new System.Drawing.Point(143, 243);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(123, 41);
            this.btnFind.TabIndex = 11;
            this.btnFind.Text = "Go!";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // btnRestart
            // 
            this.btnRestart.Location = new System.Drawing.Point(295, 243);
            this.btnRestart.Name = "btnRestart";
            this.btnRestart.Size = new System.Drawing.Size(118, 44);
            this.btnRestart.TabIndex = 12;
            this.btnRestart.Text = "Restart game";
            this.btnRestart.UseVisualStyleBackColor = true;
            this.btnRestart.Click += new System.EventHandler(this.btnRestart_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(406, 367);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(193, 20);
            this.label4.TabIndex = 13;
            this.label4.Text = "Sample size on 2nd street";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(406, 417);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(189, 20);
            this.label5.TabIndex = 14;
            this.label5.Text = "Sample size on 3rd street";
            // 
            // tbx2ndSampleSize
            // 
            this.tbx2ndSampleSize.Location = new System.Drawing.Point(605, 367);
            this.tbx2ndSampleSize.Name = "tbx2ndSampleSize";
            this.tbx2ndSampleSize.Size = new System.Drawing.Size(100, 26);
            this.tbx2ndSampleSize.TabIndex = 15;
            this.tbx2ndSampleSize.Text = "125";
            // 
            // tbx3rdSampleSize
            // 
            this.tbx3rdSampleSize.Location = new System.Drawing.Point(605, 411);
            this.tbx3rdSampleSize.Name = "tbx3rdSampleSize";
            this.tbx3rdSampleSize.Size = new System.Drawing.Size(100, 26);
            this.tbx3rdSampleSize.TabIndex = 16;
            this.tbx3rdSampleSize.Text = "500";
            // 
            // lblBestPlace
            // 
            this.lblBestPlace.AutoSize = true;
            this.lblBestPlace.Location = new System.Drawing.Point(13, 297);
            this.lblBestPlace.Name = "lblBestPlace";
            this.lblBestPlace.Size = new System.Drawing.Size(0, 20);
            this.lblBestPlace.TabIndex = 17;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(297, 184);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(116, 43);
            this.btnClear.TabIndex = 18;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // tbxVillain2
            // 
            this.tbxVillain2.Location = new System.Drawing.Point(491, 33);
            this.tbxVillain2.Multiline = true;
            this.tbxVillain2.Name = "tbxVillain2";
            this.tbxVillain2.Size = new System.Drawing.Size(254, 124);
            this.tbxVillain2.TabIndex = 19;
            this.tbxVillain2.DoubleClick += new System.EventHandler(this.tbxVillain2_DoubleClick);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(487, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(63, 20);
            this.label7.TabIndex = 21;
            this.label7.Text = "Villain 2";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(310, 33);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 53);
            this.btnSave.TabIndex = 22;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(310, 104);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 53);
            this.btnLoad.TabIndex = 23;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(854, 499);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.tbxVillain2);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.lblBestPlace);
            this.Controls.Add(this.tbx3rdSampleSize);
            this.Controls.Add(this.tbx2ndSampleSize);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnRestart);
            this.Controls.Add(this.btnFind);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbxDeadCards);
            this.Controls.Add(this.tbxCurrentTriple);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Hero);
            this.Controls.Add(this.tbxHeroMiddle);
            this.Controls.Add(this.tbxHeroTop);
            this.Controls.Add(this.tbxHeroShort);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbxVillain);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbxVillain;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxHeroShort;
        private System.Windows.Forms.TextBox tbxHeroTop;
        private System.Windows.Forms.TextBox tbxHeroMiddle;
        private System.Windows.Forms.Label Hero;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbxCurrentTriple;
        private System.Windows.Forms.TextBox tbxDeadCards;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.Button btnRestart;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbx2ndSampleSize;
        private System.Windows.Forms.TextBox tbx3rdSampleSize;
        private System.Windows.Forms.Label lblBestPlace;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.TextBox tbxVillain2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnLoad;
    }
}

