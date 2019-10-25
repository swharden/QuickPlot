namespace WinFormsDemos
{
    partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.btnMultiY = new System.Windows.Forms.Button();
            this.btnLinkedSubplots = new System.Windows.Forms.Button();
            this.btnQuickstart = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnMultiY
            // 
            this.btnMultiY.Location = new System.Drawing.Point(12, 41);
            this.btnMultiY.Name = "btnMultiY";
            this.btnMultiY.Size = new System.Drawing.Size(108, 23);
            this.btnMultiY.TabIndex = 1;
            this.btnMultiY.Text = "multiple Y axes";
            this.btnMultiY.UseVisualStyleBackColor = true;
            this.btnMultiY.Click += new System.EventHandler(this.btnMultiY_Click);
            // 
            // btnLinkedSubplots
            // 
            this.btnLinkedSubplots.Location = new System.Drawing.Point(12, 70);
            this.btnLinkedSubplots.Name = "btnLinkedSubplots";
            this.btnLinkedSubplots.Size = new System.Drawing.Size(108, 23);
            this.btnLinkedSubplots.TabIndex = 2;
            this.btnLinkedSubplots.Text = "linked subplots";
            this.btnLinkedSubplots.UseVisualStyleBackColor = true;
            this.btnLinkedSubplots.Click += new System.EventHandler(this.btnLinkedSubplots_Click);
            // 
            // btnQuickstart
            // 
            this.btnQuickstart.Location = new System.Drawing.Point(12, 12);
            this.btnQuickstart.Name = "btnQuickstart";
            this.btnQuickstart.Size = new System.Drawing.Size(108, 23);
            this.btnQuickstart.TabIndex = 0;
            this.btnQuickstart.Text = "quickstart";
            this.btnQuickstart.UseVisualStyleBackColor = true;
            this.btnQuickstart.Click += new System.EventHandler(this.btnQuickstart_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackColor = System.Drawing.Color.Fuchsia;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(232, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(66, 66);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(308, 108);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnQuickstart);
            this.Controls.Add(this.btnLinkedSubplots);
            this.Controls.Add(this.btnMultiY);
            this.Name = "FormMain";
            this.Text = "QuickPlot Demos";
            this.Load += new System.EventHandler(this.FormMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnMultiY;
        private System.Windows.Forms.Button btnLinkedSubplots;
        private System.Windows.Forms.Button btnQuickstart;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}