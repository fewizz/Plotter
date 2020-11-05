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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlotterForm));
            this.gl = new OpenGL.GlControl();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabs = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.GridsControl = new Plotter.GridsControl();
            this.pointsControl1 = new Plotter.PointsControl();
            this.skyControl1 = new Plotter.SkyControl();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabs.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // gl
            // 
            this.gl.Animation = true;
            this.gl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gl.ColorBits = ((uint)(24u));
            this.gl.ContextVersion = new Khronos.KhronosVersion(1, 1, 0, "gl", null);
            this.gl.Cursor = System.Windows.Forms.Cursors.Cross;
            this.gl.DepthBits = ((uint)(16u));
            this.gl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gl.Location = new System.Drawing.Point(0, 0);
            this.gl.MultisampleBits = ((uint)(0u));
            this.gl.Name = "gl";
            this.gl.Size = new System.Drawing.Size(769, 601);
            this.gl.StencilBits = ((uint)(0u));
            this.gl.TabIndex = 0;
            this.gl.Render += new System.EventHandler<OpenGL.GlControlEventArgs>(this.glRender);
            this.gl.Load += new System.EventHandler(this.glLoad);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.gl);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabs);
            this.splitContainer1.Size = new System.Drawing.Size(1010, 601);
            this.splitContainer1.SplitterDistance = 769;
            this.splitContainer1.TabIndex = 1;
            // 
            // tabs
            // 
            this.tabs.Controls.Add(this.tabPage1);
            this.tabs.Controls.Add(this.tabPage2);
            this.tabs.Controls.Add(this.tabPage3);
            this.tabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabs.Location = new System.Drawing.Point(0, 0);
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(237, 601);
            this.tabs.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.GridsControl);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(229, 575);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Плоскости";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.pointsControl1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(229, 575);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Точки";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.skyControl1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(229, 575);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Небо";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // GridsControl
            // 
            this.GridsControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridsControl.Location = new System.Drawing.Point(3, 3);
            this.GridsControl.Name = "GridsControl";
            this.GridsControl.Size = new System.Drawing.Size(223, 569);
            this.GridsControl.TabIndex = 0;
            // 
            // pointsControl1
            // 
            this.pointsControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pointsControl1.Location = new System.Drawing.Point(3, 3);
            this.pointsControl1.Name = "pointsControl1";
            this.pointsControl1.Size = new System.Drawing.Size(223, 569);
            this.pointsControl1.TabIndex = 0;
            // 
            // skyControl1
            // 
            this.skyControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skyControl1.Location = new System.Drawing.Point(3, 3);
            this.skyControl1.Name = "skyControl1";
            this.skyControl1.Size = new System.Drawing.Size(223, 569);
            this.skyControl1.TabIndex = 0;
            // 
            // PlotterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1010, 601);
            this.Controls.Add(this.splitContainer1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "PlotterForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Графопостроитель";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabs.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        public OpenGL.GlControl gl;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabControl tabs;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private PointsControl pointsControl1;
        private System.Windows.Forms.TabPage tabPage3;
        private SkyControl skyControl1;
        public GridsControl GridsControl;
    }
}

