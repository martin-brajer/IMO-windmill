namespace IMO_windmill
{
    partial class Form_main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_main));
            this.lblNum = new System.Windows.Forms.Label();
            this.lblRng = new System.Windows.Forms.Label();
            this.lblVelocity = new System.Windows.Forms.Label();
            this.btnRng = new System.Windows.Forms.Button();
            this.btnErace = new System.Windows.Forms.Button();
            this.tBoxRng = new System.Windows.Forms.TextBox();
            this.tBoxPeriod = new System.Windows.Forms.TextBox();
            this.chBoxStart = new System.Windows.Forms.CheckBox();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.linkLabel_IMO = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // lblNum
            // 
            this.lblNum.AutoSize = true;
            this.lblNum.Location = new System.Drawing.Point(552, 36);
            this.lblNum.Name = "lblNum";
            this.lblNum.Size = new System.Drawing.Size(41, 13);
            this.lblNum.TabIndex = 21;
            this.lblNum.Text = "<Num>";
            // 
            // lblRng
            // 
            this.lblRng.AutoSize = true;
            this.lblRng.Location = new System.Drawing.Point(305, 36);
            this.lblRng.Name = "lblRng";
            this.lblRng.Size = new System.Drawing.Size(144, 13);
            this.lblRng.TabIndex = 18;
            this.lblRng.Text = "Number of points to generate";
            // 
            // lblVelocity
            // 
            this.lblVelocity.AutoSize = true;
            this.lblVelocity.Location = new System.Drawing.Point(305, 15);
            this.lblVelocity.Name = "lblVelocity";
            this.lblVelocity.Size = new System.Drawing.Size(81, 13);
            this.lblVelocity.TabIndex = 19;
            this.lblVelocity.Text = "Velocity [deg/s]";
            // 
            // btnRng
            // 
            this.btnRng.Location = new System.Drawing.Point(143, 12);
            this.btnRng.Name = "btnRng";
            this.btnRng.Size = new System.Drawing.Size(50, 41);
            this.btnRng.TabIndex = 17;
            this.btnRng.Text = "Rng";
            this.btnRng.UseVisualStyleBackColor = true;
            this.btnRng.Click += new System.EventHandler(this.BtnRnd_Click);
            // 
            // btnErace
            // 
            this.btnErace.Location = new System.Drawing.Point(87, 12);
            this.btnErace.Name = "btnErace";
            this.btnErace.Size = new System.Drawing.Size(50, 41);
            this.btnErace.TabIndex = 16;
            this.btnErace.Text = "Erace";
            this.btnErace.UseVisualStyleBackColor = true;
            this.btnErace.Click += new System.EventHandler(this.BtnErace_Click);
            // 
            // tBoxRng
            // 
            this.tBoxRng.Location = new System.Drawing.Point(199, 33);
            this.tBoxRng.Name = "tBoxRng";
            this.tBoxRng.Size = new System.Drawing.Size(100, 20);
            this.tBoxRng.TabIndex = 14;
            this.tBoxRng.Text = "10";
            // 
            // tBoxPeriod
            // 
            this.tBoxPeriod.Location = new System.Drawing.Point(199, 12);
            this.tBoxPeriod.Name = "tBoxPeriod";
            this.tBoxPeriod.Size = new System.Drawing.Size(100, 20);
            this.tBoxPeriod.TabIndex = 15;
            this.tBoxPeriod.Text = "10";
            // 
            // chBoxStart
            // 
            this.chBoxStart.Appearance = System.Windows.Forms.Appearance.Button;
            this.chBoxStart.Location = new System.Drawing.Point(12, 12);
            this.chBoxStart.Name = "chBoxStart";
            this.chBoxStart.Size = new System.Drawing.Size(69, 41);
            this.chBoxStart.TabIndex = 13;
            this.chBoxStart.Text = "Start";
            this.chBoxStart.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chBoxStart.UseVisualStyleBackColor = true;
            this.chBoxStart.CheckedChanged += new System.EventHandler(this.ChBoxStart_CheckedChanged);
            // 
            // pictureBox
            // 
            this.pictureBox.Location = new System.Drawing.Point(12, 65);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(800, 612);
            this.pictureBox.TabIndex = 12;
            this.pictureBox.TabStop = false;
            this.pictureBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PictureBox_MouseClick);
            // 
            // linkLabel_IMO
            // 
            this.linkLabel_IMO.AutoSize = true;
            this.linkLabel_IMO.Location = new System.Drawing.Point(552, 15);
            this.linkLabel_IMO.Name = "linkLabel_IMO";
            this.linkLabel_IMO.Size = new System.Drawing.Size(257, 13);
            this.linkLabel_IMO.TabIndex = 22;
            this.linkLabel_IMO.TabStop = true;
            this.linkLabel_IMO.Text = "International Mathematical Olympiad 2011: Problem 2";
            this.linkLabel_IMO.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabel_IMO_LinkClicked);
            // 
            // Form_main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(824, 689);
            this.Controls.Add(this.linkLabel_IMO);
            this.Controls.Add(this.lblNum);
            this.Controls.Add(this.lblRng);
            this.Controls.Add(this.lblVelocity);
            this.Controls.Add(this.btnRng);
            this.Controls.Add(this.btnErace);
            this.Controls.Add(this.tBoxRng);
            this.Controls.Add(this.tBoxPeriod);
            this.Controls.Add(this.chBoxStart);
            this.Controls.Add(this.pictureBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form_main";
            this.Text = "IMO Windmill";
            this.Load += new System.EventHandler(this.Form_main_Load);
            this.SizeChanged += new System.EventHandler(this.Form_main_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblNum;
        private System.Windows.Forms.Label lblRng;
        private System.Windows.Forms.Label lblVelocity;
        private System.Windows.Forms.Button btnRng;
        private System.Windows.Forms.Button btnErace;
        private System.Windows.Forms.TextBox tBoxRng;
        private System.Windows.Forms.TextBox tBoxPeriod;
        private System.Windows.Forms.CheckBox chBoxStart;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.LinkLabel linkLabel_IMO;
    }
}

