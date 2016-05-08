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
        /// Required method for Designer support  do not modify
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
            this.MenuStrip = new System.Windows.Forms.MenuStrip();
            this.FileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CloseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutProgrammToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TabControl = new System.Windows.Forms.TabControl();
            this.TabPageSpline = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.СontextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.DeleteLayers = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolTipFirstStart = new System.Windows.Forms.ToolTip(this.components);
            this.MenuStrip.SuspendLayout();
            this.TabControl.SuspendLayout();
            this.СontextMenuStrip.SuspendLayout();
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
            this.MainPaint_VScroll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
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
            this.MainPaint_HScroll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.MainPaint_HScroll.LargeChange = 588;
            this.MainPaint_HScroll.Location = new System.Drawing.Point(12, 561);
            this.MainPaint_HScroll.Maximum = 587;
            this.MainPaint_HScroll.Name = "MainPaint_HScroll";
            this.MainPaint_HScroll.Size = new System.Drawing.Size(587, 12);
            this.MainPaint_HScroll.TabIndex = 2;
            this.MainPaint_HScroll.Visible = false;
            this.MainPaint_HScroll.Scroll += new System.Windows.Forms.ScrollEventHandler(this.MainPaint_HScroll_Scroll);
            // 
            // FirstStartButton
            // 
            this.FirstStartButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FirstStartButton.Location = new System.Drawing.Point(694, 135);
            this.FirstStartButton.Name = "FirstStartButton";
            this.FirstStartButton.Size = new System.Drawing.Size(84, 23);
            this.FirstStartButton.TabIndex = 3;
            this.FirstStartButton.Text = "Start";
            this.ToolTipFirstStart.SetToolTip(this.FirstStartButton, "Запуск основной части программы. Изменить введенные\r\nданные после нажатия кнопки " +
        "- НЕЛЬЗЯ.");
            this.FirstStartButton.UseVisualStyleBackColor = true;
            this.FirstStartButton.Click += new System.EventHandler(this.FirstStartButton_Click);
            // 
            // FirstLabelMain
            // 
            this.FirstLabelMain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FirstLabelMain.AutoSize = true;
            this.FirstLabelMain.Location = new System.Drawing.Point(618, 32);
            this.FirstLabelMain.Name = "FirstLabelMain";
            this.FirstLabelMain.Size = new System.Drawing.Size(136, 26);
            this.FirstLabelMain.TabIndex = 4;
            this.FirstLabelMain.Text = "Введите ширину исследуемой \r\nобласти и высоту земли\r\n";
            // 
            // TextBoxWidthArea
            // 
            this.TextBoxWidthArea.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxWidthArea.Location = new System.Drawing.Point(694, 62);
            this.TextBoxWidthArea.MaxLength = 6;
            this.TextBoxWidthArea.Name = "TextBoxWidthArea";
            this.TextBoxWidthArea.Size = new System.Drawing.Size(84, 20);
            this.TextBoxWidthArea.TabIndex = 5;
            this.ToolTipFirstStart.SetToolTip(this.TextBoxWidthArea, "Ширина исследуемой облости 582-999999 м");
            this.TextBoxWidthArea.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxWidthArea_KeyPress);
            // 
            // TextBoxHeightEarth
            // 
            this.TextBoxHeightEarth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxHeightEarth.Location = new System.Drawing.Point(694, 97);
            this.TextBoxHeightEarth.MaxLength = 3;
            this.TextBoxHeightEarth.Name = "TextBoxHeightEarth";
            this.TextBoxHeightEarth.Size = new System.Drawing.Size(84, 20);
            this.TextBoxHeightEarth.TabIndex = 6;
            this.ToolTipFirstStart.SetToolTip(this.TextBoxHeightEarth, "Высота над уровнем земли 0-999 1м");
            this.TextBoxHeightEarth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxHeightEarth_KeyPress);
            // 
            // LabelWidthArea
            // 
            this.LabelWidthArea.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelWidthArea.AutoSize = true;
            this.LabelWidthArea.Location = new System.Drawing.Point(618, 62);
            this.LabelWidthArea.Name = "LabelWidthArea";
            this.LabelWidthArea.Size = new System.Drawing.Size(59, 26);
            this.LabelWidthArea.TabIndex = 7;
            this.LabelWidthArea.Text = "Ширина\r\nобласти,м";
            // 
            // LabelHeightEarth
            // 
            this.LabelHeightEarth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelHeightEarth.AutoSize = true;
            this.LabelHeightEarth.Location = new System.Drawing.Point(618, 97);
            this.LabelHeightEarth.Name = "LabelHeightEarth";
            this.LabelHeightEarth.Size = new System.Drawing.Size(50, 26);
            this.LabelHeightEarth.TabIndex = 8;
            this.LabelHeightEarth.Text = "Высота\r\nземли,м";
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
            // TabControl
            // 
            this.TabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TabControl.Controls.Add(this.TabPageSpline);
            this.TabControl.Controls.Add(this.tabPage2);
            this.TabControl.Location = new System.Drawing.Point(618, 32);
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(254, 526);
            this.TabControl.TabIndex = 10;
            this.TabControl.Visible = false;
            // 
            // TabPageSpline
            // 
            this.TabPageSpline.BackColor = System.Drawing.SystemColors.Control;
            this.TabPageSpline.ForeColor = System.Drawing.SystemColors.ControlText;
            this.TabPageSpline.Location = new System.Drawing.Point(4, 22);
            this.TabPageSpline.Name = "TabPageSpline";
            this.TabPageSpline.Padding = new System.Windows.Forms.Padding(3);
            this.TabPageSpline.Size = new System.Drawing.Size(246, 500);
            this.TabPageSpline.TabIndex = 0;
            this.TabPageSpline.Text = "Почва";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(246, 500);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            // 
            // СontextMenuStrip
            // 
            this.СontextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DeleteLayers});
            this.СontextMenuStrip.Name = "СontextMenuStrip";
            this.СontextMenuStrip.Size = new System.Drawing.Size(119, 26);
            // 
            // DeleteLayers
            // 
            this.DeleteLayers.Name = "DeleteLayers";
            this.DeleteLayers.Size = new System.Drawing.Size(118, 22);
            this.DeleteLayers.Text = "Удалить";
            this.DeleteLayers.Click += new System.EventHandler(this.DeleteLayers_Click);
            // 
            // ToolTipFirstStart
            // 
            this.ToolTipFirstStart.AutoPopDelay = 5000;
            this.ToolTipFirstStart.InitialDelay = 500;
            this.ToolTipFirstStart.ReshowDelay = 100;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 582);
            this.Controls.Add(this.LabelHeightEarth);
            this.Controls.Add(this.LabelWidthArea);
            this.Controls.Add(this.TextBoxHeightEarth);
            this.Controls.Add(this.TextBoxWidthArea);
            this.Controls.Add(this.FirstLabelMain);
            this.Controls.Add(this.FirstStartButton);
            this.Controls.Add(this.TabControl);
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
            this.TabControl.ResumeLayout(false);
            this.СontextMenuStrip.ResumeLayout(false);
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
        private System.Windows.Forms.MenuStrip MenuStrip;
        private System.Windows.Forms.ToolStripMenuItem FileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CloseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AboutProgrammToolStripMenuItem;
        private System.Windows.Forms.TabControl TabControl;
        private System.Windows.Forms.TabPage TabPageSpline;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ContextMenuStrip СontextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem DeleteLayers;
        private System.Windows.Forms.ToolTip ToolTipFirstStart;
    }
}

