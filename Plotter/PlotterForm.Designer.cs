namespace Plotter
{
    partial class PlotterForm
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
            this.gl = new OpenGL.GlControl();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // gl
            // 
            this.gl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gl.Animation = true;
            this.gl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gl.ColorBits = ((uint)(24u));
            this.gl.ContextVersion = new Khronos.KhronosVersion(1, 1, 0, "gl", null);
            this.gl.Cursor = System.Windows.Forms.Cursors.Cross;
            this.gl.DepthBits = ((uint)(16u));
            this.gl.Location = new System.Drawing.Point(0, 50);
            this.gl.MultisampleBits = ((uint)(0u));
            this.gl.Name = "gl";
            this.gl.Size = new System.Drawing.Size(572, 185);
            this.gl.StencilBits = ((uint)(0u));
            this.gl.TabIndex = 0;
            this.gl.Render += new System.EventHandler<OpenGL.GlControlEventArgs>(this.glRender);
            this.gl.Load += new System.EventHandler(this.glLoad);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.Font = new System.Drawing.Font("Lucida Console", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(13, 12);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(546, 26);
            this.textBox1.TabIndex = 1;
            // 
            // PlotterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(571, 234);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.gl);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.KeyPreview = true;
            this.Name = "PlotterForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ShowIcon = false;
            this.Text = "Plotter";
            this.Load += new System.EventHandler(this.OnFormLoad);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.PlotterForm_KeyUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private OpenGL.GlControl gl;
        private System.Windows.Forms.TextBox textBox1;
    }
}

