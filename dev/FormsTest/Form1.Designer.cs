namespace FormsTest
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
            this.components = new System.ComponentModel.Container();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.cbRun = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.button2 = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbY2Label = new System.Windows.Forms.CheckBox();
            this.cbYLabel = new System.Windows.Forms.CheckBox();
            this.cbXLabel = new System.Windows.Forms.CheckBox();
            this.cbTitle = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cbXScale = new System.Windows.Forms.CheckBox();
            this.cbYScale = new System.Windows.Forms.CheckBox();
            this.cbY2Scale = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.cbShowLayout = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(12, 80);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(810, 547);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.SizeChanged += new System.EventHandler(this.PictureBox1_SizeChanged);
            // 
            // cbRun
            // 
            this.cbRun.AutoSize = true;
            this.cbRun.Location = new System.Drawing.Point(87, 23);
            this.cbRun.Name = "cbRun";
            this.cbRun.Size = new System.Drawing.Size(46, 17);
            this.cbRun.TabIndex = 1;
            this.cbRun.Text = "Run";
            this.cbRun.UseVisualStyleBackColor = true;
            this.cbRun.CheckedChanged += new System.EventHandler(this.CheckBox1_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 19);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Render";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(139, 19);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Benchmark";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus,
            this.toolStripProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 630);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(834, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(118, 17);
            this.lblStatus.Text = "toolStripStatusLabel1";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.MarqueeAnimationSpeed = 10;
            this.toolStripProgressBar1.Maximum = 1000;
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(300, 16);
            this.toolStripProgressBar1.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.cbRun);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(220, 49);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Benchmarking";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbY2Label);
            this.groupBox2.Controls.Add(this.cbYLabel);
            this.groupBox2.Controls.Add(this.cbXLabel);
            this.groupBox2.Controls.Add(this.cbTitle);
            this.groupBox2.Location = new System.Drawing.Point(238, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(152, 62);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Border Labels";
            // 
            // cbY2Label
            // 
            this.cbY2Label.AutoSize = true;
            this.cbY2Label.Location = new System.Drawing.Point(74, 42);
            this.cbY2Label.Name = "cbY2Label";
            this.cbY2Label.Size = new System.Drawing.Size(68, 17);
            this.cbY2Label.TabIndex = 3;
            this.cbY2Label.Text = "Y2 Label";
            this.cbY2Label.UseVisualStyleBackColor = true;
            this.cbY2Label.CheckedChanged += new System.EventHandler(this.CbY2Label_CheckedChanged);
            // 
            // cbYLabel
            // 
            this.cbYLabel.AutoSize = true;
            this.cbYLabel.Checked = true;
            this.cbYLabel.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbYLabel.Location = new System.Drawing.Point(74, 19);
            this.cbYLabel.Name = "cbYLabel";
            this.cbYLabel.Size = new System.Drawing.Size(62, 17);
            this.cbYLabel.TabIndex = 2;
            this.cbYLabel.Text = "Y Label";
            this.cbYLabel.UseVisualStyleBackColor = true;
            this.cbYLabel.CheckedChanged += new System.EventHandler(this.CbYLabel_CheckedChanged);
            // 
            // cbXLabel
            // 
            this.cbXLabel.AutoSize = true;
            this.cbXLabel.Checked = true;
            this.cbXLabel.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbXLabel.Location = new System.Drawing.Point(6, 42);
            this.cbXLabel.Name = "cbXLabel";
            this.cbXLabel.Size = new System.Drawing.Size(62, 17);
            this.cbXLabel.TabIndex = 1;
            this.cbXLabel.Text = "X Label";
            this.cbXLabel.UseVisualStyleBackColor = true;
            this.cbXLabel.CheckedChanged += new System.EventHandler(this.CbXLabel_CheckedChanged);
            // 
            // cbTitle
            // 
            this.cbTitle.AutoSize = true;
            this.cbTitle.Checked = true;
            this.cbTitle.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbTitle.Location = new System.Drawing.Point(6, 19);
            this.cbTitle.Name = "cbTitle";
            this.cbTitle.Size = new System.Drawing.Size(46, 17);
            this.cbTitle.TabIndex = 0;
            this.cbTitle.Text = "Title";
            this.cbTitle.UseVisualStyleBackColor = true;
            this.cbTitle.CheckedChanged += new System.EventHandler(this.CbTitle_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cbXScale);
            this.groupBox3.Controls.Add(this.cbYScale);
            this.groupBox3.Controls.Add(this.cbY2Scale);
            this.groupBox3.Location = new System.Drawing.Point(396, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(89, 62);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Scales";
            // 
            // cbXScale
            // 
            this.cbXScale.AutoSize = true;
            this.cbXScale.Checked = true;
            this.cbXScale.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbXScale.Location = new System.Drawing.Point(6, 19);
            this.cbXScale.Name = "cbXScale";
            this.cbXScale.Size = new System.Drawing.Size(33, 17);
            this.cbXScale.TabIndex = 2;
            this.cbXScale.Text = "X";
            this.cbXScale.UseVisualStyleBackColor = true;
            this.cbXScale.CheckedChanged += new System.EventHandler(this.CbXScale_CheckedChanged);
            // 
            // cbYScale
            // 
            this.cbYScale.AutoSize = true;
            this.cbYScale.Checked = true;
            this.cbYScale.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbYScale.Location = new System.Drawing.Point(45, 19);
            this.cbYScale.Name = "cbYScale";
            this.cbYScale.Size = new System.Drawing.Size(33, 17);
            this.cbYScale.TabIndex = 1;
            this.cbYScale.Text = "Y";
            this.cbYScale.UseVisualStyleBackColor = true;
            this.cbYScale.CheckedChanged += new System.EventHandler(this.CbYScale_CheckedChanged);
            // 
            // cbY2Scale
            // 
            this.cbY2Scale.AutoSize = true;
            this.cbY2Scale.Location = new System.Drawing.Point(45, 42);
            this.cbY2Scale.Name = "cbY2Scale";
            this.cbY2Scale.Size = new System.Drawing.Size(39, 17);
            this.cbY2Scale.TabIndex = 3;
            this.cbY2Scale.Text = "Y2";
            this.cbY2Scale.UseVisualStyleBackColor = true;
            this.cbY2Scale.CheckedChanged += new System.EventHandler(this.CbY2Scale_CheckedChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.cbShowLayout);
            this.groupBox4.Location = new System.Drawing.Point(491, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(200, 62);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Developer Settings";
            // 
            // cbShowLayout
            // 
            this.cbShowLayout.AutoSize = true;
            this.cbShowLayout.Location = new System.Drawing.Point(6, 19);
            this.cbShowLayout.Name = "cbShowLayout";
            this.cbShowLayout.Size = new System.Drawing.Size(88, 17);
            this.cbShowLayout.TabIndex = 0;
            this.cbShowLayout.Text = "Show Layout";
            this.cbShowLayout.UseVisualStyleBackColor = true;
            this.cbShowLayout.CheckedChanged += new System.EventHandler(this.CbShowLayout_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 652);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.CheckBox cbRun;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox cbY2Label;
        private System.Windows.Forms.CheckBox cbYLabel;
        private System.Windows.Forms.CheckBox cbXLabel;
        private System.Windows.Forms.CheckBox cbTitle;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox cbXScale;
        private System.Windows.Forms.CheckBox cbYScale;
        private System.Windows.Forms.CheckBox cbY2Scale;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox cbShowLayout;
    }
}

