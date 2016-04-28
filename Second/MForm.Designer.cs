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
            this.NewLayer = new System.Windows.Forms.Button();
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
            this.MainPaint.Location = new System.Drawing.Point(12, 12);
            this.MainPaint.Name = "MainPaint";
            this.MainPaint.Size = new System.Drawing.Size(587, 526);
            this.MainPaint.StencilBits = ((byte)(0));
            this.MainPaint.TabIndex = 0;
            this.MainPaint.Load += new System.EventHandler(this.MainPaint_Load);
            this.MainPaint.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MainPaint_MouseClick);
            this.MainPaint.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainPaint_MouseDown);
            this.MainPaint.MouseEnter += new System.EventHandler(this.MainPaint_MouseEnter);
            this.MainPaint.MouseLeave += new System.EventHandler(this.MainPaint_MouseLeave);
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
            this.MainPaint_VScroll.Location = new System.Drawing.Point(602, 12);
            this.MainPaint_VScroll.Maximum = 526;
            this.MainPaint_VScroll.Name = "MainPaint_VScroll";
            this.MainPaint_VScroll.Size = new System.Drawing.Size(12, 526);
            this.MainPaint_VScroll.TabIndex = 1;
            this.MainPaint_VScroll.Scroll += new System.Windows.Forms.ScrollEventHandler(this.MainPaint_VScroll_Scroll);
            // 
            // MainPaint_HScroll
            // 
            this.MainPaint_HScroll.LargeChange = 588;
            this.MainPaint_HScroll.Location = new System.Drawing.Point(12, 541);
            this.MainPaint_HScroll.Maximum = 587;
            this.MainPaint_HScroll.Name = "MainPaint_HScroll";
            this.MainPaint_HScroll.Size = new System.Drawing.Size(587, 12);
            this.MainPaint_HScroll.TabIndex = 2;
            this.MainPaint_HScroll.Scroll += new System.Windows.Forms.ScrollEventHandler(this.MainPaint_HScroll_Scroll);
            // 
            // NewLayer
            // 
            this.NewLayer.Location = new System.Drawing.Point(618, 13);
            this.NewLayer.Name = "NewLayer";
            this.NewLayer.Size = new System.Drawing.Size(80, 23);
            this.NewLayer.TabIndex = 3;
            this.NewLayer.Text = "Новый слой";
            this.NewLayer.UseVisualStyleBackColor = true;
            this.NewLayer.Click += new System.EventHandler(this.NewLayer_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.NewLayer);
            this.Controls.Add(this.MainPaint_HScroll);
            this.Controls.Add(this.MainPaint_VScroll);
            this.Controls.Add(this.MainPaint);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "MainForm";
            this.Text = "Приложение версия b0.0000000001";
            this.SizeChanged += new System.EventHandler(this.MainForm_SizeChanged);
            this.ResumeLayout(false);

        }

        #endregion

        private Tao.Platform.Windows.SimpleOpenGlControl MainPaint;
        private System.Windows.Forms.Timer RenderTimer;
        private System.Windows.Forms.VScrollBar MainPaint_VScroll;
        private System.Windows.Forms.HScrollBar MainPaint_HScroll;
        private System.Windows.Forms.Button NewLayer;
    }
}

