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
            this.FirstStartButton = new System.Windows.Forms.Button();
            this.FirstLabelMain = new System.Windows.Forms.Label();
            this.TextBoxWidthArea = new System.Windows.Forms.TextBox();
            this.TextBoxHeightEarth = new System.Windows.Forms.TextBox();
            this.LabelWidthArea = new System.Windows.Forms.Label();
            this.LabelHeightEarth = new System.Windows.Forms.Label();
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
            this.MainPaint_VScroll.Visible = false;
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
            this.MainPaint_HScroll.Visible = false;
            this.MainPaint_HScroll.Scroll += new System.Windows.Forms.ScrollEventHandler(this.MainPaint_HScroll_Scroll);
            // 
            // FirstStartButton
            // 
            this.FirstStartButton.Location = new System.Drawing.Point(694, 116);
            this.FirstStartButton.Name = "FirstStartButton";
            this.FirstStartButton.Size = new System.Drawing.Size(84, 23);
            this.FirstStartButton.TabIndex = 3;
            this.FirstStartButton.Text = "Start";
            this.FirstStartButton.UseVisualStyleBackColor = true;
            this.FirstStartButton.Click += new System.EventHandler(this.FirstStartButton_Click);
            // 
            // FirstLabelMain
            // 
            this.FirstLabelMain.AutoSize = true;
            this.FirstLabelMain.Location = new System.Drawing.Point(618, 13);
            this.FirstLabelMain.Name = "FirstLabelMain";
            this.FirstLabelMain.Size = new System.Drawing.Size(136, 26);
            this.FirstLabelMain.TabIndex = 4;
            this.FirstLabelMain.Text = "Введите ширину рабочей \r\nобласти и высоту земли\r\n";
            // 
            // TextBoxWidthArea
            // 
            this.TextBoxWidthArea.Location = new System.Drawing.Point(694, 43);
            this.TextBoxWidthArea.MaxLength = 6;
            this.TextBoxWidthArea.Name = "TextBoxWidthArea";
            this.TextBoxWidthArea.Size = new System.Drawing.Size(84, 20);
            this.TextBoxWidthArea.TabIndex = 5;
            this.TextBoxWidthArea.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxWidthArea_KeyPress);
            // 
            // TextBoxHeightEarth
            // 
            this.TextBoxHeightEarth.Location = new System.Drawing.Point(694, 78);
            this.TextBoxHeightEarth.MaxLength = 3;
            this.TextBoxHeightEarth.Name = "TextBoxHeightEarth";
            this.TextBoxHeightEarth.Size = new System.Drawing.Size(84, 20);
            this.TextBoxHeightEarth.TabIndex = 6;
            this.TextBoxHeightEarth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxHeightEarth_KeyPress);
            // 
            // LabelWidthArea
            // 
            this.LabelWidthArea.AutoSize = true;
            this.LabelWidthArea.Location = new System.Drawing.Point(618, 43);
            this.LabelWidthArea.Name = "LabelWidthArea";
            this.LabelWidthArea.Size = new System.Drawing.Size(59, 26);
            this.LabelWidthArea.TabIndex = 7;
            this.LabelWidthArea.Text = "Ширина\r\nобласти,м";
            // 
            // LabelHeightEarth
            // 
            this.LabelHeightEarth.AutoSize = true;
            this.LabelHeightEarth.Location = new System.Drawing.Point(618, 78);
            this.LabelHeightEarth.Name = "LabelHeightEarth";
            this.LabelHeightEarth.Size = new System.Drawing.Size(50, 26);
            this.LabelHeightEarth.TabIndex = 8;
            this.LabelHeightEarth.Text = "Высота\r\nземли,м";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.LabelHeightEarth);
            this.Controls.Add(this.LabelWidthArea);
            this.Controls.Add(this.TextBoxHeightEarth);
            this.Controls.Add(this.TextBoxWidthArea);
            this.Controls.Add(this.FirstLabelMain);
            this.Controls.Add(this.FirstStartButton);
            this.Controls.Add(this.MainPaint_HScroll);
            this.Controls.Add(this.MainPaint_VScroll);
            this.Controls.Add(this.MainPaint);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "MainForm";
            this.Text = "Приложение версия b0.0000000001";
            this.SizeChanged += new System.EventHandler(this.MainForm_SizeChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Tao.Platform.Windows.SimpleOpenGlControl MainPaint;
        private System.Windows.Forms.Timer RenderTimer;
        private System.Windows.Forms.VScrollBar MainPaint_VScroll;
        private System.Windows.Forms.HScrollBar MainPaint_HScroll;
        private System.Windows.Forms.Button FirstStartButton;
        private System.Windows.Forms.Label FirstLabelMain;
        private System.Windows.Forms.TextBox TextBoxWidthArea;
        private System.Windows.Forms.TextBox TextBoxHeightEarth;
        private System.Windows.Forms.Label LabelWidthArea;
        private System.Windows.Forms.Label LabelHeightEarth;
    }
}

