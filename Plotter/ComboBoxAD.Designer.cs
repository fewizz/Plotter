namespace Plotter
{
    partial class ComboBoxAD
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
            this.comboBox = new CComboBox();
            this.delete = new System.Windows.Forms.Button();
            this.add = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // comboBox
            // 
            this.comboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox.Font = new System.Drawing.Font("Consolas", 12F);
            this.comboBox.FormattingEnabled = true;
            this.comboBox.Location = new System.Drawing.Point(0, 0);
            this.comboBox.Name = "comboBox";
            this.comboBox.Size = new System.Drawing.Size(157, 27);
            this.comboBox.TabIndex = 0;
            // 
            // delete
            // 
            this.delete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.delete.Enabled = false;
            this.delete.Location = new System.Drawing.Point(234, 0);
            this.delete.Name = "delete";
            this.delete.Size = new System.Drawing.Size(65, 23);
            this.delete.TabIndex = 1;
            this.delete.Text = "Удалить";
            this.delete.UseVisualStyleBackColor = true;
            // 
            // add
            // 
            this.add.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.add.Location = new System.Drawing.Point(163, 0);
            this.add.Name = "add";
            this.add.Size = new System.Drawing.Size(65, 23);
            this.add.TabIndex = 2;
            this.add.Text = "Добавить";
            this.add.UseVisualStyleBackColor = true;
            // 
            // ComboBoxAD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.add);
            this.Controls.Add(this.delete);
            this.Controls.Add(this.comboBox);
            this.Name = "ComboBoxAD";
            this.Size = new System.Drawing.Size(299, 29);
            this.ResumeLayout(false);

        }

        #endregion
        public CComboBox comboBox;
        public System.Windows.Forms.Button add;
        public System.Windows.Forms.Button delete;
    }
}
