namespace Plotter
{
    partial class PointsControl
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
            this.PointsList = new Plotter.ComboBoxAddDelete();
            this.pointConstructor = new System.Windows.Forms.Panel();
            this.GridsList = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.z = new Plotter.StatusTextBox();
            this.x = new Plotter.StatusTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.name = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pointConstructor.SuspendLayout();
            this.SuspendLayout();
            // 
            // PointsList
            // 
            this.PointsList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PointsList.DataSource = null;
            this.PointsList.Location = new System.Drawing.Point(3, 3);
            this.PointsList.Name = "PointsList";
            this.PointsList.Size = new System.Drawing.Size(295, 31);
            this.PointsList.TabIndex = 6;
            // 
            // pointConstructor
            // 
            this.pointConstructor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pointConstructor.Controls.Add(this.GridsList);
            this.pointConstructor.Controls.Add(this.label5);
            this.pointConstructor.Controls.Add(this.label4);
            this.pointConstructor.Controls.Add(this.z);
            this.pointConstructor.Controls.Add(this.x);
            this.pointConstructor.Controls.Add(this.label3);
            this.pointConstructor.Controls.Add(this.label2);
            this.pointConstructor.Controls.Add(this.name);
            this.pointConstructor.Controls.Add(this.label1);
            this.pointConstructor.Location = new System.Drawing.Point(0, 40);
            this.pointConstructor.Name = "pointConstructor";
            this.pointConstructor.Size = new System.Drawing.Size(302, 206);
            this.pointConstructor.TabIndex = 5;
            this.pointConstructor.Visible = false;
            // 
            // GridsList
            // 
            this.GridsList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GridsList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.GridsList.Font = new System.Drawing.Font("Consolas", 12F);
            this.GridsList.FormattingEnabled = true;
            this.GridsList.Location = new System.Drawing.Point(3, 65);
            this.GridsList.Name = "GridsList";
            this.GridsList.Size = new System.Drawing.Size(295, 27);
            this.GridsList.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 152);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(14, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Z";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 120);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(14, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "X";
            // 
            // z
            // 
            this.z.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.z.Font = new System.Drawing.Font("Consolas", 14.25F);
            this.z.Location = new System.Drawing.Point(23, 145);
            this.z.Name = "z";
            this.z.Size = new System.Drawing.Size(275, 30);
            this.z.TabIndex = 7;
            // 
            // x
            // 
            this.x.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.x.Font = new System.Drawing.Font("Consolas", 14.25F);
            this.x.Location = new System.Drawing.Point(23, 113);
            this.x.Name = "x";
            this.x.Size = new System.Drawing.Size(275, 30);
            this.x.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Поверхность";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Положение";
            // 
            // name
            // 
            this.name.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.name.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.name.Location = new System.Drawing.Point(3, 19);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(295, 26);
            this.name.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Имя";
            // 
            // PointsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.PointsList);
            this.Controls.Add(this.pointConstructor);
            this.Name = "PointsControl";
            this.Size = new System.Drawing.Size(302, 246);
            this.pointConstructor.ResumeLayout(false);
            this.pointConstructor.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ComboBoxAddDelete PointsList;
        private System.Windows.Forms.Panel pointConstructor;
        private System.Windows.Forms.ComboBox GridsList;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private StatusTextBox z;
        private StatusTextBox x;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox name;
        private System.Windows.Forms.Label label1;
    }
}
