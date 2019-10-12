namespace QuickPlot.Controls
{
    partial class FormsPlot
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblInfo = new System.Windows.Forms.Label();
            this.skControl1 = new SkiaSharp.Views.Desktop.SKControl();
            this.SuspendLayout();
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(6, 7);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(130, 13);
            this.lblInfo.TabIndex = 1;
            this.lblInfo.Text = "This is a regular FormsPlot";
            // 
            // skControl1
            // 
            this.skControl1.BackColor = System.Drawing.Color.Red;
            this.skControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skControl1.Location = new System.Drawing.Point(0, 0);
            this.skControl1.Name = "skControl1";
            this.skControl1.Size = new System.Drawing.Size(299, 173);
            this.skControl1.TabIndex = 2;
            this.skControl1.Text = "skControl1";
            this.skControl1.PaintSurface += new System.EventHandler<SkiaSharp.Views.Desktop.SKPaintSurfaceEventArgs>(this.SkControl1_PaintSurface);
            // 
            // FormsPlot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.skControl1);
            this.Name = "FormsPlot";
            this.Size = new System.Drawing.Size(299, 173);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.Label lblInfo;
        public SkiaSharp.Views.Desktop.SKControl skControl1;
    }
}
