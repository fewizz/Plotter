namespace Plotter
{
    partial class GridsControl
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
            this.GridsList = new Plotter.ComboBoxAddDelete();
            this.Panel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // gridsList
            // 
            this.GridsList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GridsList.Location = new System.Drawing.Point(3, 4);
            this.GridsList.Name = "gridsList";
            this.GridsList.Size = new System.Drawing.Size(314, 29);
            this.GridsList.TabIndex = 6;
            // 
            // panel1
            // 
            this.Panel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel.Location = new System.Drawing.Point(3, 39);
            this.Panel.Name = "panel1";
            this.Panel.Size = new System.Drawing.Size(314, 299);
            this.Panel.TabIndex = 7;
            // 
            // GridsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Panel);
            this.Controls.Add(this.GridsList);
            this.Name = "GridsControl";
            this.Size = new System.Drawing.Size(320, 341);
            this.ResumeLayout(false);

        }

        #endregion

        public ComboBoxAddDelete GridsList;
        private System.Windows.Forms.Panel Panel;
    }
}
