namespace Plotter
{
    partial class PlainGridControl
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
            this.step = new Plotter.StatusEditBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // expression
            // 
            this.expression.Size = new System.Drawing.Size(251, 30);
            this.expression.Text = "0";
            // 
            // step
            // 
            this.step.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.step.BackColor = System.Drawing.Color.Red;
            this.step.Font = new System.Drawing.Font("Consolas", 14.25F);
            this.step.Location = new System.Drawing.Point(278, 71);
            this.step.Name = "step";
            this.step.Size = new System.Drawing.Size(47, 30);
            this.step.TabIndex = 25;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(275, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(27, 13);
            this.label4.TabIndex = 27;
            this.label4.Text = "Шаг";
            // 
            // PlainGridControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label4);
            this.Controls.Add(this.step);
            this.Name = "PlainGridControl";
            this.Size = new System.Drawing.Size(328, 300);
            this.Controls.SetChildIndex(this.expression, 0);
            this.Controls.SetChildIndex(this.label9, 0);
            this.Controls.SetChildIndex(this.colorControl1, 0);
            this.Controls.SetChildIndex(this.step, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private StatusEditBox step;
        private System.Windows.Forms.Label label4;
    }
}
