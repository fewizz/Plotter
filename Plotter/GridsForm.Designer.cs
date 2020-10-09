namespace Plotter
{
    partial class GridsForm
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
            this.gridsControl = new Plotter.GridsControl();
            this.SuspendLayout();
            // 
            // gridsControl1
            // 
            this.gridsControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridsControl.Location = new System.Drawing.Point(0, 0);
            this.gridsControl.Name = "gridsControl1";
            this.gridsControl.Size = new System.Drawing.Size(337, 334);
            this.gridsControl.TabIndex = 0;
            // 
            // GridsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(337, 334);
            this.ControlBox = false;
            this.Controls.Add(this.gridsControl);
            this.KeyPreview = true;
            this.Name = "GridsForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Сетки";
            this.ResumeLayout(false);

        }

        #endregion

        public GridsControl gridsControl;
    }
}