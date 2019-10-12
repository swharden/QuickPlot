namespace QuickPlotDemos
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
            this.btnSaveFig = new System.Windows.Forms.Button();
            this.btnInteractive = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSaveFig
            // 
            this.btnSaveFig.Location = new System.Drawing.Point(12, 12);
            this.btnSaveFig.Name = "btnSaveFig";
            this.btnSaveFig.Size = new System.Drawing.Size(102, 23);
            this.btnSaveFig.TabIndex = 0;
            this.btnSaveFig.Text = "Save Figure";
            this.btnSaveFig.UseVisualStyleBackColor = true;
            this.btnSaveFig.Click += new System.EventHandler(this.btnSaveFig_Click);
            // 
            // btnInteractive
            // 
            this.btnInteractive.Location = new System.Drawing.Point(12, 41);
            this.btnInteractive.Name = "btnInteractive";
            this.btnInteractive.Size = new System.Drawing.Size(102, 23);
            this.btnInteractive.TabIndex = 1;
            this.btnInteractive.Text = "Interactive Plot";
            this.btnInteractive.UseVisualStyleBackColor = true;
            this.btnInteractive.Click += new System.EventHandler(this.btnInteractive_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(311, 144);
            this.Controls.Add(this.btnInteractive);
            this.Controls.Add(this.btnSaveFig);
            this.Name = "Form1";
            this.Text = "QuickPlot Demos";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSaveFig;
        private System.Windows.Forms.Button btnInteractive;
    }
}

