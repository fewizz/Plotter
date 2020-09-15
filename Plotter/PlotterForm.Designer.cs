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
            this.SuspendLayout();
            // 
            // gl
            // 
            this.gl.Animation = true;
            this.gl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gl.ColorBits = ((uint)(24u));
            this.gl.ContextVersion = new Khronos.KhronosVersion(1, 1, 0, "gl", null);
            this.gl.DepthBits = ((uint)(16u));
            this.gl.Location = new System.Drawing.Point(0, 0);
            this.gl.MultisampleBits = ((uint)(0u));
            this.gl.Name = "gl";
            this.gl.Size = new System.Drawing.Size(795, 401);
            this.gl.StencilBits = ((uint)(0u));
            this.gl.TabIndex = 0;
            this.gl.Render += new System.EventHandler<OpenGL.GlControlEventArgs>(this.glRender);
            this.gl.Load += new System.EventHandler(this.glLoad);
            // 
            // PlotterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.gl);
            this.KeyPreview = true;
            this.Name = "PlotterForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.OnFormLoad);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.PlotterForm_KeyUp);
            this.ResumeLayout(false);

        }

        #endregion
        private OpenGL.GlControl gl;
    }
}

