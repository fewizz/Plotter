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
            this.gridsConstructor = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.type = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.name = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.expression = new System.Windows.Forms.TextBox();
            this.colorControl = new Plotter.ColorControl();
            this.gridsList = new Plotter.ComboBoxAddDelete();
            this.gridsConstructor.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridsConstructor
            // 
            this.gridsConstructor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridsConstructor.Controls.Add(this.label10);
            this.gridsConstructor.Controls.Add(this.type);
            this.gridsConstructor.Controls.Add(this.label9);
            this.gridsConstructor.Controls.Add(this.label3);
            this.gridsConstructor.Controls.Add(this.label2);
            this.gridsConstructor.Controls.Add(this.name);
            this.gridsConstructor.Controls.Add(this.label1);
            this.gridsConstructor.Controls.Add(this.expression);
            this.gridsConstructor.Location = new System.Drawing.Point(0, 39);
            this.gridsConstructor.Name = "gridsConstructor";
            this.gridsConstructor.Size = new System.Drawing.Size(328, 116);
            this.gridsConstructor.TabIndex = 5;
            this.gridsConstructor.Visible = false;
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(200, 10);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(26, 13);
            this.label10.TabIndex = 17;
            this.label10.Text = "Тип";
            // 
            // type
            // 
            this.type.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.type.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.type.FormattingEnabled = true;
            this.type.Location = new System.Drawing.Point(203, 28);
            this.type.Name = "type";
            this.type.Size = new System.Drawing.Size(121, 28);
            this.type.TabIndex = 16;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(3, 88);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(15, 13);
            this.label9.TabIndex = 15;
            this.label9.Text = "Y";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Выражение";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Имя сетки";
            // 
            // name
            // 
            this.name.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.name.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.name.Location = new System.Drawing.Point(6, 28);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(191, 30);
            this.name.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 1;
            // 
            // expression
            // 
            this.expression.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.expression.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.expression.Location = new System.Drawing.Point(24, 79);
            this.expression.Name = "expression";
            this.expression.Size = new System.Drawing.Size(301, 30);
            this.expression.TabIndex = 0;
            // 
            // colorControl
            // 
            this.colorControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.colorControl.Constructor = null;
            this.colorControl.Location = new System.Drawing.Point(0, 154);
            this.colorControl.Name = "colorControl";
            this.colorControl.Size = new System.Drawing.Size(328, 198);
            this.colorControl.TabIndex = 7;
            this.colorControl.Visible = false;
            // 
            // gridsList
            // 
            this.gridsList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridsList.Location = new System.Drawing.Point(3, 4);
            this.gridsList.Name = "gridsList";
            this.gridsList.Size = new System.Drawing.Size(322, 29);
            this.gridsList.TabIndex = 6;
            // 
            // GridsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.colorControl);
            this.Controls.Add(this.gridsConstructor);
            this.Controls.Add(this.gridsList);
            this.Name = "GridsControl";
            this.Size = new System.Drawing.Size(328, 352);
            this.gridsConstructor.ResumeLayout(false);
            this.gridsConstructor.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public ComboBoxAddDelete gridsList;
        private System.Windows.Forms.Panel gridsConstructor;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox type;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox name;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox expression;
        private ColorControl colorControl;
    }
}
