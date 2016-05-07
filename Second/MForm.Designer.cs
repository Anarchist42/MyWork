namespace Second
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            this.MainPaint = new Tao.Platform.Windows.SimpleOpenGlControl();
            this.RenderTimer = new System.Windows.Forms.Timer(this.components);
            this.MainPaint_VScroll = new System.Windows.Forms.VScrollBar();
            this.MainPaint_HScroll = new System.Windows.Forms.HScrollBar();
            this.MenuStrip = new System.Windows.Forms.MenuStrip();
            this.FileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CloseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutProgrammToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainPaint
            // 
            this.MainPaint.AccumBits = ((byte)(0));
            this.MainPaint.AutoCheckErrors = false;
            this.MainPaint.AutoFinish = false;
            this.MainPaint.AutoMakeCurrent = true;
            this.MainPaint.AutoSwapBuffers = true;
            this.MainPaint.BackColor = System.Drawing.Color.Black;
            this.MainPaint.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.MainPaint.ColorBits = ((byte)(32));
            this.MainPaint.DepthBits = ((byte)(16));
            this.MainPaint.Location = new System.Drawing.Point(12, 32);
            this.MainPaint.Name = "MainPaint";
            this.MainPaint.Size = new System.Drawing.Size(587, 526);
            this.MainPaint.StencilBits = ((byte)(0));
            this.MainPaint.TabIndex = 0;
            this.MainPaint.Load += new System.EventHandler(this.MainPaint_Load);
            this.MainPaint.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainPaint_MouseDown);
            this.MainPaint.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainPaint_MouseMove);
            this.MainPaint.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainPaint_MouseUp);
            this.MainPaint.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.MainPaint_MouseWheel);
            // 
            // RenderTimer
            // 
            this.RenderTimer.Interval = 30;
            this.RenderTimer.Tick += new System.EventHandler(this.RenderTimer_Tick);
            // 
            // MainPaint_VScroll
            // 
            this.MainPaint_VScroll.LargeChange = 527;
            this.MainPaint_VScroll.Location = new System.Drawing.Point(602, 32);
            this.MainPaint_VScroll.Maximum = 526;
            this.MainPaint_VScroll.Name = "MainPaint_VScroll";
            this.MainPaint_VScroll.Size = new System.Drawing.Size(12, 526);
            this.MainPaint_VScroll.TabIndex = 1;
            this.MainPaint_VScroll.Visible = false;
            this.MainPaint_VScroll.Scroll += new System.Windows.Forms.ScrollEventHandler(this.MainPaint_VScroll_Scroll);
            // 
            // MainPaint_HScroll
            // 
            this.MainPaint_HScroll.LargeChange = 588;
            this.MainPaint_HScroll.Location = new System.Drawing.Point(12, 561);
            this.MainPaint_HScroll.Maximum = 587;
            this.MainPaint_HScroll.Name = "MainPaint_HScroll";
            this.MainPaint_HScroll.Size = new System.Drawing.Size(587, 12);
            this.MainPaint_HScroll.TabIndex = 2;
            this.MainPaint_HScroll.Visible = false;
            this.MainPaint_HScroll.Scroll += new System.Windows.Forms.ScrollEventHandler(this.MainPaint_HScroll_Scroll);
            // 
            // MenuStrip
            // 
            this.MenuStrip.BackColor = System.Drawing.SystemColors.Control;
            this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileToolStripMenuItem,
            this.AboutProgrammToolStripMenuItem});
            this.MenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MenuStrip.Name = "MenuStrip";
            this.MenuStrip.Size = new System.Drawing.Size(884, 24);
            this.MenuStrip.TabIndex = 9;
            this.MenuStrip.Text = "menuStrip1";
            // 
            // FileToolStripMenuItem
            // 
            this.FileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CloseToolStripMenuItem});
            this.FileToolStripMenuItem.Name = "FileToolStripMenuItem";
            this.FileToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.FileToolStripMenuItem.Text = "Файл";
            // 
            // CloseToolStripMenuItem
            // 
            this.CloseToolStripMenuItem.Name = "CloseToolStripMenuItem";
            this.CloseToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.CloseToolStripMenuItem.Text = "Закрыть";
            this.CloseToolStripMenuItem.Click += new System.EventHandler(this.CloseToolStripMenuItem_Click);
            // 
            // AboutProgrammToolStripMenuItem
            // 
            this.AboutProgrammToolStripMenuItem.Name = "AboutProgrammToolStripMenuItem";
            this.AboutProgrammToolStripMenuItem.Size = new System.Drawing.Size(94, 20);
            this.AboutProgrammToolStripMenuItem.Text = "О программе";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 582);
            this.Controls.Add(this.MainPaint_HScroll);
            this.Controls.Add(this.MainPaint_VScroll);
            this.Controls.Add(this.MainPaint);
            this.Controls.Add(this.MenuStrip);
            this.MainMenuStrip = this.MenuStrip;
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "MainForm";
            this.Text = "Задание геологических структур для программного комплекса решения задач геоэлектр" +
    "ики";
            this.SizeChanged += new System.EventHandler(this.MainForm_SizeChanged);
            this.MenuStrip.ResumeLayout(false);
            this.MenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Tao.Platform.Windows.SimpleOpenGlControl MainPaint;
        private System.Windows.Forms.Timer RenderTimer;
        private System.Windows.Forms.VScrollBar MainPaint_VScroll;
        private System.Windows.Forms.HScrollBar MainPaint_HScroll;
        //private System.Windows.Forms.Button FirstStartButton;
        //private System.Windows.Forms.Label FirstLabelMain;
        //private System.Windows.Forms.TextBox TextBoxWidthArea;
        //private System.Windows.Forms.TextBox TextBoxHeightEarth;
        //private System.Windows.Forms.Label LabelWidthArea;
        //private System.Windows.Forms.Label LabelHeightEarth;
        private System.Windows.Forms.MenuStrip MenuStrip;
        private System.Windows.Forms.ToolStripMenuItem FileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CloseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AboutProgrammToolStripMenuItem;
    }
}

