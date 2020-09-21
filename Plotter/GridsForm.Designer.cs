namespace Plotter
{
    partial class GridsForm
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
            this.gridsList = new System.Windows.Forms.ListBox();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.gridConstructor = new System.Windows.Forms.Panel();
            this.expr = new System.Windows.Forms.TextBox();
            this.gridConstructor.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridsList
            // 
            this.gridsList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.gridsList.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridsList.FormattingEnabled = true;
            this.gridsList.ItemHeight = 19;
            this.gridsList.Location = new System.Drawing.Point(13, 32);
            this.gridsList.Name = "gridsList";
            this.gridsList.Size = new System.Drawing.Size(157, 118);
            this.gridsList.TabIndex = 0;
            this.gridsList.SelectedIndexChanged += new System.EventHandler(this.OnGridSelectChanged);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(13, 3);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(75, 23);
            this.buttonAdd.TabIndex = 1;
            this.buttonAdd.Text = "Add";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.onAdd);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Enabled = false;
            this.buttonDelete.Location = new System.Drawing.Point(95, 3);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(75, 23);
            this.buttonDelete.TabIndex = 2;
            this.buttonDelete.Text = "Delete";
            this.buttonDelete.UseVisualStyleBackColor = true;
            // 
            // gridConstructor
            // 
            this.gridConstructor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridConstructor.Controls.Add(this.expr);
            this.gridConstructor.Location = new System.Drawing.Point(177, 3);
            this.gridConstructor.Name = "gridConstructor";
            this.gridConstructor.Size = new System.Drawing.Size(295, 147);
            this.gridConstructor.TabIndex = 3;
            this.gridConstructor.Visible = false;
            // 
            // expr
            // 
            this.expr.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.expr.Location = new System.Drawing.Point(7, 9);
            this.expr.Name = "expr";
            this.expr.Size = new System.Drawing.Size(288, 20);
            this.expr.TabIndex = 0;
            // 
            // GridsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 161);
            this.ControlBox = false;
            this.Controls.Add(this.gridConstructor);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.gridsList);
            this.KeyPreview = true;
            this.Name = "GridsForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Grids";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
            this.gridConstructor.ResumeLayout(false);
            this.gridConstructor.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox gridsList;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Panel gridConstructor;
        private System.Windows.Forms.TextBox expr;
    }
}