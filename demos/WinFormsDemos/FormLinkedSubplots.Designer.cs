namespace WinFormsDemos
{
    partial class FormLinkedSubplots
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
            this.interactivePlot1 = new QuickPlot.WinForms.InteractivePlot();
            this.btnShare = new System.Windows.Forms.Button();
            this.btnUnshare = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // interactivePlot1
            // 
            this.interactivePlot1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.interactivePlot1.BackColor = System.Drawing.Color.Navy;
            this.interactivePlot1.Location = new System.Drawing.Point(12, 41);
            this.interactivePlot1.Name = "interactivePlot1";
            this.interactivePlot1.Size = new System.Drawing.Size(927, 579);
            this.interactivePlot1.TabIndex = 0;
            // 
            // btnShare
            // 
            this.btnShare.Location = new System.Drawing.Point(12, 12);
            this.btnShare.Name = "btnShare";
            this.btnShare.Size = new System.Drawing.Size(93, 23);
            this.btnShare.TabIndex = 1;
            this.btnShare.Text = "Share Axes";
            this.btnShare.UseVisualStyleBackColor = true;
            this.btnShare.Click += new System.EventHandler(this.btnShare_Click);
            // 
            // btnUnshare
            // 
            this.btnUnshare.Location = new System.Drawing.Point(111, 12);
            this.btnUnshare.Name = "btnUnshare";
            this.btnUnshare.Size = new System.Drawing.Size(93, 23);
            this.btnUnshare.TabIndex = 2;
            this.btnUnshare.Text = "Unshare Axes";
            this.btnUnshare.UseVisualStyleBackColor = true;
            this.btnUnshare.Click += new System.EventHandler(this.btnUnshare_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 200;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // FormLinkedSubplots
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(951, 632);
            this.Controls.Add(this.btnUnshare);
            this.Controls.Add(this.btnShare);
            this.Controls.Add(this.interactivePlot1);
            this.Name = "FormLinkedSubplots";
            this.Text = "QuickPlot: Linked Subplot Demo";
            this.Load += new System.EventHandler(this.FormLinkedSubplots_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private QuickPlot.WinForms.InteractivePlot interactivePlot1;
        private System.Windows.Forms.Button btnShare;
        private System.Windows.Forms.Button btnUnshare;
        private System.Windows.Forms.Timer timer1;
    }
}

