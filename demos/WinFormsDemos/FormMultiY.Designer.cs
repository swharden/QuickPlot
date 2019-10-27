namespace WinFormsDemos
{
    partial class FormMultiY
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
            this.interactivePlot1 = new QuickPlot.WinForms.InteractivePlot();
            this.btnZoomIn = new System.Windows.Forms.Button();
            this.btnZoomOut = new System.Windows.Forms.Button();
            this.btnAuto2 = new System.Windows.Forms.Button();
            this.btnAuto1 = new System.Windows.Forms.Button();
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
            this.interactivePlot1.Size = new System.Drawing.Size(776, 397);
            this.interactivePlot1.TabIndex = 0;
            // 
            // btnZoomIn
            // 
            this.btnZoomIn.Location = new System.Drawing.Point(12, 12);
            this.btnZoomIn.Name = "btnZoomIn";
            this.btnZoomIn.Size = new System.Drawing.Size(89, 23);
            this.btnZoomIn.TabIndex = 1;
            this.btnZoomIn.Text = "y2 zoom in";
            this.btnZoomIn.UseVisualStyleBackColor = true;
            this.btnZoomIn.Click += new System.EventHandler(this.btnZoomIn_Click);
            // 
            // btnZoomOut
            // 
            this.btnZoomOut.Location = new System.Drawing.Point(107, 12);
            this.btnZoomOut.Name = "btnZoomOut";
            this.btnZoomOut.Size = new System.Drawing.Size(89, 23);
            this.btnZoomOut.TabIndex = 2;
            this.btnZoomOut.Text = "y2 zoom out";
            this.btnZoomOut.UseVisualStyleBackColor = true;
            this.btnZoomOut.Click += new System.EventHandler(this.btnZoomOut_Click);
            // 
            // btnAuto2
            // 
            this.btnAuto2.Location = new System.Drawing.Point(297, 12);
            this.btnAuto2.Name = "btnAuto2";
            this.btnAuto2.Size = new System.Drawing.Size(89, 23);
            this.btnAuto2.TabIndex = 3;
            this.btnAuto2.Text = "y2 auto";
            this.btnAuto2.UseVisualStyleBackColor = true;
            this.btnAuto2.Click += new System.EventHandler(this.btnAuto2_Click);
            // 
            // btnAuto1
            // 
            this.btnAuto1.Location = new System.Drawing.Point(202, 12);
            this.btnAuto1.Name = "btnAuto1";
            this.btnAuto1.Size = new System.Drawing.Size(89, 23);
            this.btnAuto1.TabIndex = 4;
            this.btnAuto1.Text = "y1 auto";
            this.btnAuto1.UseVisualStyleBackColor = true;
            this.btnAuto1.Click += new System.EventHandler(this.btnAuto1_Click);
            // 
            // FormMultiY
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnAuto1);
            this.Controls.Add(this.btnAuto2);
            this.Controls.Add(this.btnZoomOut);
            this.Controls.Add(this.btnZoomIn);
            this.Controls.Add(this.interactivePlot1);
            this.Name = "FormMultiY";
            this.Text = "QuickPlot: Multi-Y Axis Demo";
            this.Load += new System.EventHandler(this.FormMultiY_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private QuickPlot.WinForms.InteractivePlot interactivePlot1;
        private System.Windows.Forms.Button btnZoomIn;
        private System.Windows.Forms.Button btnZoomOut;
        private System.Windows.Forms.Button btnAuto2;
        private System.Windows.Forms.Button btnAuto1;
    }
}