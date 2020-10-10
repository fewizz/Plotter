namespace Plotter
{
    partial class PointsForm
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
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.pointsControl = new Plotter.PointsControl();
            this.SuspendLayout();
            // 
            // pointsControl1
            // 
            this.pointsControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pointsControl.Location = new System.Drawing.Point(0, 0);
            this.pointsControl.Name = "pointsControl1";
            this.pointsControl.Size = new System.Drawing.Size(283, 255);
            this.pointsControl.TabIndex = 0;
            // 
            // PointsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(283, 255);
            this.ControlBox = false;
            this.Controls.Add(this.pointsControl);
            this.KeyPreview = true;
            this.Name = "PointsForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Точки";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ColorDialog colorDialog1;
        public PointsControl pointsControl;
    }
}