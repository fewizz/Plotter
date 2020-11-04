namespace Plotter
{
    partial class ComboBoxAddDelete
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
            this.ComboBox = new System.Windows.Forms.ComboBox();
            this.Remove = new System.Windows.Forms.Button();
            this.Add = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ComboBox
            // 
            this.ComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBox.Font = new System.Drawing.Font("Consolas", 12F);
            this.ComboBox.FormattingEnabled = true;
            this.ComboBox.Location = new System.Drawing.Point(0, 0);
            this.ComboBox.Name = "ComboBox";
            this.ComboBox.Size = new System.Drawing.Size(157, 27);
            this.ComboBox.TabIndex = 0;
            // 
            // Remove
            // 
            this.Remove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Remove.Enabled = false;
            this.Remove.Location = new System.Drawing.Point(234, 0);
            this.Remove.Name = "Remove";
            this.Remove.Size = new System.Drawing.Size(65, 27);
            this.Remove.TabIndex = 1;
            this.Remove.Text = "Удалить";
            this.Remove.UseVisualStyleBackColor = true;
            // 
            // Add
            // 
            this.Add.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Add.Location = new System.Drawing.Point(163, 0);
            this.Add.Name = "Add";
            this.Add.Size = new System.Drawing.Size(65, 27);
            this.Add.TabIndex = 2;
            this.Add.Text = "Добавить";
            this.Add.UseVisualStyleBackColor = true;
            // 
            // ComboBoxAddDelete
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Add);
            this.Controls.Add(this.Remove);
            this.Controls.Add(this.ComboBox);
            this.Name = "ComboBoxAddDelete";
            this.Size = new System.Drawing.Size(299, 29);
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.ComboBox ComboBox;
        public System.Windows.Forms.Button Add;
        public System.Windows.Forms.Button Remove;
    }
}
