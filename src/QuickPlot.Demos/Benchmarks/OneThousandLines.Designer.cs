namespace QuickPlot.Demos.Benchmarks
{
    partial class OneThousandLines
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
            this.interactivePlot1 = new QuickPlot.Forms.InteractivePlot();
            this.btnRender = new System.Windows.Forms.Button();
            this.btnBenchmark = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblUsingGL = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // interactivePlot1
            // 
            this.interactivePlot1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.interactivePlot1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.interactivePlot1.Location = new System.Drawing.Point(12, 41);
            this.interactivePlot1.Name = "interactivePlot1";
            this.interactivePlot1.Size = new System.Drawing.Size(625, 288);
            this.interactivePlot1.TabIndex = 0;
            // 
            // btnRender
            // 
            this.btnRender.Location = new System.Drawing.Point(12, 12);
            this.btnRender.Name = "btnRender";
            this.btnRender.Size = new System.Drawing.Size(75, 23);
            this.btnRender.TabIndex = 1;
            this.btnRender.Text = "Render";
            this.btnRender.UseVisualStyleBackColor = true;
            this.btnRender.Click += new System.EventHandler(this.BtnRender_Click);
            // 
            // btnBenchmark
            // 
            this.btnBenchmark.Location = new System.Drawing.Point(93, 12);
            this.btnBenchmark.Name = "btnBenchmark";
            this.btnBenchmark.Size = new System.Drawing.Size(75, 23);
            this.btnBenchmark.TabIndex = 2;
            this.btnBenchmark.Text = "Benchmark";
            this.btnBenchmark.UseVisualStyleBackColor = true;
            this.btnBenchmark.Click += new System.EventHandler(this.BtnBenchmark_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(284, 17);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(113, 13);
            this.lblStatus.TabIndex = 3;
            this.lblStatus.Text = "benchmark not yet run";
            // 
            // lblUsingGL
            // 
            this.lblUsingGL.AutoSize = true;
            this.lblUsingGL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblUsingGL.Location = new System.Drawing.Point(174, 17);
            this.lblUsingGL.Name = "lblUsingGL";
            this.lblUsingGL.Size = new System.Drawing.Size(106, 15);
            this.lblUsingGL.TabIndex = 4;
            this.lblUsingGL.Text = "Using OpenGL: YES";
            // 
            // OneThousandLines
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(649, 341);
            this.Controls.Add(this.lblUsingGL);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnBenchmark);
            this.Controls.Add(this.btnRender);
            this.Controls.Add(this.interactivePlot1);
            this.Name = "OneThousandLines";
            this.Text = "OneThousandLines";
            this.Load += new System.EventHandler(this.OneThousandLines_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Forms.InteractivePlot interactivePlot1;
        private System.Windows.Forms.Button btnRender;
        private System.Windows.Forms.Button btnBenchmark;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblUsingGL;
    }
}