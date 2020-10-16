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
            this.gridsList = new Plotter.ComboBoxAddDelete();
            this.panel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // gridsList
            // 
            this.gridsList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridsList.Location = new System.Drawing.Point(3, 4);
            this.gridsList.Name = "gridsList";
            this.gridsList.Size = new System.Drawing.Size(314, 29);
            this.gridsList.TabIndex = 6;
            // 
            // panel1
            // 
            this.panel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel.Location = new System.Drawing.Point(3, 39);
            this.panel.Name = "panel1";
            this.panel.Size = new System.Drawing.Size(314, 299);
            this.panel.TabIndex = 7;
            // 
            // GridsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel);
            this.Controls.Add(this.gridsList);
            this.Name = "GridsControl";
            this.Size = new System.Drawing.Size(320, 341);
            this.ResumeLayout(false);

        }

        #endregion

        public ComboBoxAddDelete gridsList;
        private System.Windows.Forms.Panel panel;
    }
}
