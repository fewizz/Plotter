namespace Plotter
{
    partial class SkyControl
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
            this.colorControl1 = new Plotter.ColorControl();
            this.SuspendLayout();
            // 
            // colorControl1
            // 
            this.colorControl1.Constructor = null;
            this.colorControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.colorControl1.Location = new System.Drawing.Point(0, 0);
            this.colorControl1.Name = "colorControl1";
            this.colorControl1.Size = new System.Drawing.Size(313, 190);
            this.colorControl1.TabIndex = 0;
            // 
            // SkyControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.colorControl1);
            this.Name = "SkyControl";
            this.Size = new System.Drawing.Size(313, 190);
            this.ResumeLayout(false);

        }

        #endregion

        private ColorControl colorControl1;
    }
}
