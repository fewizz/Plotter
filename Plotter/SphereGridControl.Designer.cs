namespace Plotter
{
    partial class SphereGridControl
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
            this.Frequency = new Plotter.StatusTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // colorControl1
            // 
            this.colorControl1.Size = new System.Drawing.Size(316, 196);
            // 
            // expression
            // 
            this.expression.Size = new System.Drawing.Size(231, 30);
            // 
            // statusEditBox1
            // 
            this.Frequency.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Frequency.BackColor = System.Drawing.Color.Red;
            this.Frequency.Font = new System.Drawing.Font("Consolas", 14.25F);
            this.Frequency.Location = new System.Drawing.Point(259, 71);
            this.Frequency.Name = "statusEditBox1";
            this.Frequency.Size = new System.Drawing.Size(60, 30);
            this.Frequency.TabIndex = 25;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(256, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 26;
            this.label4.Text = "Частота";
            // 
            // SphereGridControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Frequency);
            this.Name = "SphereGridControl";
            this.Size = new System.Drawing.Size(322, 297);
            this.Controls.SetChildIndex(this.expression, 0);
            this.Controls.SetChildIndex(this.label9, 0);
            this.Controls.SetChildIndex(this.colorControl1, 0);
            this.Controls.SetChildIndex(this.Frequency, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private StatusTextBox Frequency;
        private System.Windows.Forms.Label label4;
    }
}
