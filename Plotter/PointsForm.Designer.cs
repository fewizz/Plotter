﻿namespace Plotter
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
            this.name = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.x = new System.Windows.Forms.TextBox();
            this.z = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.gridsList = new Plotter.ComboBoxAddDelete.ComboBoxExtended();
            this.button3 = new System.Windows.Forms.Button();
            this.pointConstructor = new System.Windows.Forms.Panel();
            this.pointsList = new Plotter.ComboBoxAddDelete();
            this.pointConstructor.SuspendLayout();
            this.SuspendLayout();
            // 
            // name
            // 
            this.name.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.name.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.name.Location = new System.Drawing.Point(3, 19);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(251, 26);
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
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Сетка";
            // 
            // x
            // 
            this.x.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.x.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.x.Location = new System.Drawing.Point(23, 113);
            this.x.Name = "x";
            this.x.Size = new System.Drawing.Size(231, 26);
            this.x.TabIndex = 6;
            // 
            // z
            // 
            this.z.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.z.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.z.Location = new System.Drawing.Point(23, 145);
            this.z.Name = "z";
            this.z.Size = new System.Drawing.Size(231, 26);
            this.z.TabIndex = 7;
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
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 152);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(14, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Z";
            // 
            // gridsList
            // 
            this.gridsList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridsList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.gridsList.Font = new System.Drawing.Font("Consolas", 12F);
            this.gridsList.FormattingEnabled = true;
            this.gridsList.Location = new System.Drawing.Point(3, 65);
            this.gridsList.Name = "gridsList";
            this.gridsList.Size = new System.Drawing.Size(251, 27);
            this.gridsList.TabIndex = 11;
            // 
            // button3
            // 
            this.button3.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(79, 177);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(134, 23);
            this.button3.TabIndex = 4;
            this.button3.Text = "Цвет";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // pointConstructor
            // 
            this.pointConstructor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pointConstructor.Controls.Add(this.gridsList);
            this.pointConstructor.Controls.Add(this.label5);
            this.pointConstructor.Controls.Add(this.label4);
            this.pointConstructor.Controls.Add(this.z);
            this.pointConstructor.Controls.Add(this.x);
            this.pointConstructor.Controls.Add(this.label3);
            this.pointConstructor.Controls.Add(this.button3);
            this.pointConstructor.Controls.Add(this.label2);
            this.pointConstructor.Controls.Add(this.name);
            this.pointConstructor.Controls.Add(this.label1);
            this.pointConstructor.Location = new System.Drawing.Point(13, 48);
            this.pointConstructor.Name = "pointConstructor";
            this.pointConstructor.Size = new System.Drawing.Size(258, 202);
            this.pointConstructor.TabIndex = 3;
            this.pointConstructor.Visible = false;
            // 
            // pointsList
            // 
            this.pointsList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pointsList.Location = new System.Drawing.Point(13, 13);
            this.pointsList.Name = "pointsList";
            this.pointsList.Size = new System.Drawing.Size(258, 29);
            this.pointsList.TabIndex = 4;
            // 
            // PointsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(283, 255);
            this.ControlBox = false;
            this.Controls.Add(this.pointsList);
            this.Controls.Add(this.pointConstructor);
            this.KeyPreview = true;
            this.Name = "PointsForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Точки";
            this.pointConstructor.ResumeLayout(false);
            this.pointConstructor.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox name;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private ComboBoxAddDelete pointsList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox x;
        private System.Windows.Forms.TextBox z;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private ComboBoxAddDelete.ComboBoxExtended gridsList;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Panel pointConstructor;
    }
}