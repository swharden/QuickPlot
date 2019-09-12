namespace QuickPlot.Demos
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
            this.btnBenchOneKLines = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnBenchOneKLines
            // 
            this.btnBenchOneKLines.Location = new System.Drawing.Point(12, 12);
            this.btnBenchOneKLines.Name = "btnBenchOneKLines";
            this.btnBenchOneKLines.Size = new System.Drawing.Size(87, 23);
            this.btnBenchOneKLines.TabIndex = 0;
            this.btnBenchOneKLines.Text = "SKControl";
            this.btnBenchOneKLines.UseVisualStyleBackColor = true;
            this.btnBenchOneKLines.Click += new System.EventHandler(this.BtnBenchOneKLines_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 41);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(87, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "OpenGL";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(285, 207);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnBenchOneKLines);
            this.Name = "FormMain";
            this.Text = "QuickPlot Demos";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnBenchOneKLines;
        private System.Windows.Forms.Button button1;
    }
}