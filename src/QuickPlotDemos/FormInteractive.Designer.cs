namespace QuickPlotDemos
{
    partial class FormInteractive
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
            this.SuspendLayout();
            // 
            // interactivePlot1
            // 
            this.interactivePlot1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.interactivePlot1.BackColor = System.Drawing.Color.Navy;
            this.interactivePlot1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.interactivePlot1.Location = new System.Drawing.Point(12, 12);
            this.interactivePlot1.Name = "interactivePlot1";
            this.interactivePlot1.Size = new System.Drawing.Size(451, 246);
            this.interactivePlot1.TabIndex = 0;
            // 
            // FormInteractive
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(475, 270);
            this.Controls.Add(this.interactivePlot1);
            this.Name = "FormInteractive";
            this.Text = "QuickPlot Interactive Demo";
            this.SizeChanged += new System.EventHandler(this.FormInteractive_SizeChanged);
            this.ResumeLayout(false);

        }

        #endregion

        private QuickPlot.WinForms.InteractivePlot interactivePlot1;
    }
}