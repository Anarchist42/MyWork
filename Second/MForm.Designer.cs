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
            this.MenuStrip = new System.Windows.Forms.MenuStrip();
            this.FileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CloseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutProgrammToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TabControl = new System.Windows.Forms.TabControl();
            this.TabPageSettings = new System.Windows.Forms.TabPage();
            this.СheckedListBoxSettings = new System.Windows.Forms.CheckedListBox();
            this.TextBoxAccuracy = new System.Windows.Forms.TextBox();
            this.LabelAccuracy = new System.Windows.Forms.Label();
            this.TextBoxXAreaSize = new System.Windows.Forms.TextBox();
            this.TextBoxYAreaSize = new System.Windows.Forms.TextBox();
            this.TextBoxEarthSize = new System.Windows.Forms.TextBox();
            this.LabelEarthSize = new System.Windows.Forms.Label();
            this.LabelYAreaSize = new System.Windows.Forms.Label();
            this.LabelXAreaSize = new System.Windows.Forms.Label();
            this.TabPageSpline = new System.Windows.Forms.TabPage();
            this.ComboBoxMaterial = new System.Windows.Forms.ComboBox();
            this.СheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.LabelMineralMaterial = new System.Windows.Forms.Label();
            this.DrawSplineMinerals = new System.Windows.Forms.Button();
            this.DataGridViewMinerals = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TextBoxLayerHeight = new System.Windows.Forms.TextBox();
            this.LabelLayerHeight = new System.Windows.Forms.Label();
            this.AddSplineLayers = new System.Windows.Forms.Button();
            this.DataGridViewLayers = new System.Windows.Forms.DataGridView();
            this.NumberLayer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColorLayer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HeightLayer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NumberOfPoints = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TextBoxLayerNumberOfPoints = new System.Windows.Forms.TextBox();
            this.LabelLayerNumberOfPoints = new System.Windows.Forms.Label();
            this.DrawSplineLayers = new System.Windows.Forms.Button();
            this.TabPageMKE = new System.Windows.Forms.TabPage();
            this.СontextMenuMainPaint = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.AddValueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AddPointToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeletePointToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteLayersMainPaint = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.ContextMenuDataGridViewLayers = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.DeleteLayersDataGridViewLayers = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TextBoxYCoordinate = new System.Windows.Forms.TextBox();
            this.TextBoxXCoordinate = new System.Windows.Forms.TextBox();
            this.LabelYCoordiante = new System.Windows.Forms.Label();
            this.LabelXCoordinate = new System.Windows.Forms.Label();
            this.MenuStrip.SuspendLayout();
            this.TabControl.SuspendLayout();
            this.TabPageSettings.SuspendLayout();
            this.TabPageSpline.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewMinerals)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewLayers)).BeginInit();
            this.СontextMenuMainPaint.SuspendLayout();
            this.ContextMenuDataGridViewLayers.SuspendLayout();
            this.groupBox1.SuspendLayout();
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
            this.MainPaint.Enabled = false;
            this.MainPaint.Location = new System.Drawing.Point(12, 32);
            this.MainPaint.Name = "MainPaint";
            this.MainPaint.Size = new System.Drawing.Size(587, 526);
            this.MainPaint.StencilBits = ((byte)(0));
            this.MainPaint.TabIndex = 0;
            this.MainPaint.Load += new System.EventHandler(this.MainPaint_Load);
            this.MainPaint.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainPaint_MouseDown);
            this.MainPaint.MouseEnter += new System.EventHandler(this.MainPaint_MouseEnter);
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
            this.TabControl.Controls.Add(this.TabPageSettings);
            this.TabControl.Controls.Add(this.TabPageSpline);
            this.TabControl.Controls.Add(this.TabPageMKE);
            this.TabControl.Location = new System.Drawing.Point(618, 107);
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(254, 451);
            this.TabControl.TabIndex = 10;
            // 
            // TabPageSettings
            // 
            this.TabPageSettings.BackColor = System.Drawing.SystemColors.Control;
            this.TabPageSettings.Controls.Add(this.СheckedListBoxSettings);
            this.TabPageSettings.Controls.Add(this.TextBoxAccuracy);
            this.TabPageSettings.Controls.Add(this.LabelAccuracy);
            this.TabPageSettings.Controls.Add(this.TextBoxXAreaSize);
            this.TabPageSettings.Controls.Add(this.TextBoxYAreaSize);
            this.TabPageSettings.Controls.Add(this.TextBoxEarthSize);
            this.TabPageSettings.Controls.Add(this.LabelEarthSize);
            this.TabPageSettings.Controls.Add(this.LabelYAreaSize);
            this.TabPageSettings.Controls.Add(this.LabelXAreaSize);
            this.TabPageSettings.Location = new System.Drawing.Point(4, 22);
            this.TabPageSettings.Name = "TabPageSettings";
            this.TabPageSettings.Size = new System.Drawing.Size(246, 425);
            this.TabPageSettings.TabIndex = 2;
            this.TabPageSettings.Text = "Настройки";
            // 
            // СheckedListBoxSettings
            // 
            this.СheckedListBoxSettings.BackColor = System.Drawing.SystemColors.Control;
            this.СheckedListBoxSettings.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.СheckedListBoxSettings.FormattingEnabled = true;
            this.СheckedListBoxSettings.Items.AddRange(new object[] {
            "Сетка",
            "Разметка"});
            this.СheckedListBoxSettings.Location = new System.Drawing.Point(4, 148);
            this.СheckedListBoxSettings.Name = "СheckedListBoxSettings";
            this.СheckedListBoxSettings.Size = new System.Drawing.Size(78, 30);
            this.СheckedListBoxSettings.TabIndex = 8;
            this.СheckedListBoxSettings.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.СheckedListBoxSettings_ItemCheck);
            // 
            // TextBoxAccuracy
            // 
            this.TextBoxAccuracy.Location = new System.Drawing.Point(126, 6);
            this.TextBoxAccuracy.MaxLength = 2;
            this.TextBoxAccuracy.Name = "TextBoxAccuracy";
            this.TextBoxAccuracy.Size = new System.Drawing.Size(100, 20);
            this.TextBoxAccuracy.TabIndex = 7;
            this.TextBoxAccuracy.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxAccuracy_KeyPress);
            this.TextBoxAccuracy.Validating += new System.ComponentModel.CancelEventHandler(this.TextBoxAccuracy_Validating);
            this.TextBoxAccuracy.Validated += new System.EventHandler(this.TextBoxAccuracy_Validated);
            // 
            // LabelAccuracy
            // 
            this.LabelAccuracy.AutoSize = true;
            this.LabelAccuracy.Location = new System.Drawing.Point(4, 6);
            this.LabelAccuracy.Name = "LabelAccuracy";
            this.LabelAccuracy.Size = new System.Drawing.Size(84, 13);
            this.LabelAccuracy.TabIndex = 6;
            this.LabelAccuracy.Text = "Точность (0-15)";
            // 
            // TextBoxXAreaSize
            // 
            this.TextBoxXAreaSize.Enabled = false;
            this.TextBoxXAreaSize.Location = new System.Drawing.Point(126, 42);
            this.TextBoxXAreaSize.Name = "TextBoxXAreaSize";
            this.TextBoxXAreaSize.Size = new System.Drawing.Size(100, 20);
            this.TextBoxXAreaSize.TabIndex = 5;
            this.TextBoxXAreaSize.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxXAreaSize_KeyPress);
            this.TextBoxXAreaSize.Validating += new System.ComponentModel.CancelEventHandler(this.TextBoxXAreaSize_Validating);
            this.TextBoxXAreaSize.Validated += new System.EventHandler(this.TextBoxXAreaSize_Validated);
            // 
            // TextBoxYAreaSize
            // 
            this.TextBoxYAreaSize.Enabled = false;
            this.TextBoxYAreaSize.Location = new System.Drawing.Point(127, 100);
            this.TextBoxYAreaSize.Name = "TextBoxYAreaSize";
            this.TextBoxYAreaSize.Size = new System.Drawing.Size(100, 20);
            this.TextBoxYAreaSize.TabIndex = 4;
            this.TextBoxYAreaSize.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxYAreaSize_KeyPress);
            this.TextBoxYAreaSize.Validating += new System.ComponentModel.CancelEventHandler(this.TextBoxYAreaSize_Validating);
            this.TextBoxYAreaSize.Validated += new System.EventHandler(this.TextBoxYAreaSize_Validated);
            // 
            // TextBoxEarthSize
            // 
            this.TextBoxEarthSize.Enabled = false;
            this.TextBoxEarthSize.Location = new System.Drawing.Point(126, 71);
            this.TextBoxEarthSize.Name = "TextBoxEarthSize";
            this.TextBoxEarthSize.Size = new System.Drawing.Size(100, 20);
            this.TextBoxEarthSize.TabIndex = 3;
            this.TextBoxEarthSize.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxEarthSize_KeyPress);
            this.TextBoxEarthSize.Validating += new System.ComponentModel.CancelEventHandler(this.TextBoxEarthSize_Validating);
            this.TextBoxEarthSize.Validated += new System.EventHandler(this.TextBoxEarthSize_Validated);
            // 
            // LabelEarthSize
            // 
            this.LabelEarthSize.AutoSize = true;
            this.LabelEarthSize.Location = new System.Drawing.Point(4, 71);
            this.LabelEarthSize.Name = "LabelEarthSize";
            this.LabelEarthSize.Size = new System.Drawing.Size(115, 26);
            this.LabelEarthSize.TabIndex = 2;
            this.LabelEarthSize.Text = "Высота над уровнем \r\nземли, м\r\n";
            // 
            // LabelYAreaSize
            // 
            this.LabelYAreaSize.AutoSize = true;
            this.LabelYAreaSize.Location = new System.Drawing.Point(4, 100);
            this.LabelYAreaSize.Name = "LabelYAreaSize";
            this.LabelYAreaSize.Size = new System.Drawing.Size(92, 26);
            this.LabelYAreaSize.TabIndex = 1;
            this.LabelYAreaSize.Text = "Высота рабочей \r\nобласти, м\r\n";
            // 
            // LabelXAreaSize
            // 
            this.LabelXAreaSize.AutoSize = true;
            this.LabelXAreaSize.Location = new System.Drawing.Point(4, 42);
            this.LabelXAreaSize.Name = "LabelXAreaSize";
            this.LabelXAreaSize.Size = new System.Drawing.Size(93, 26);
            this.LabelXAreaSize.TabIndex = 0;
            this.LabelXAreaSize.Text = "Ширина рабочей \r\nобласти, м\r\n";
            // 
            // TabPageSpline
            // 
            this.TabPageSpline.BackColor = System.Drawing.SystemColors.Control;
            this.TabPageSpline.Controls.Add(this.ComboBoxMaterial);
            this.TabPageSpline.Controls.Add(this.СheckedListBox);
            this.TabPageSpline.Controls.Add(this.LabelMineralMaterial);
            this.TabPageSpline.Controls.Add(this.DrawSplineMinerals);
            this.TabPageSpline.Controls.Add(this.DataGridViewMinerals);
            this.TabPageSpline.Controls.Add(this.TextBoxLayerHeight);
            this.TabPageSpline.Controls.Add(this.LabelLayerHeight);
            this.TabPageSpline.Controls.Add(this.AddSplineLayers);
            this.TabPageSpline.Controls.Add(this.DataGridViewLayers);
            this.TabPageSpline.Controls.Add(this.TextBoxLayerNumberOfPoints);
            this.TabPageSpline.Controls.Add(this.LabelLayerNumberOfPoints);
            this.TabPageSpline.Controls.Add(this.DrawSplineLayers);
            this.TabPageSpline.ForeColor = System.Drawing.SystemColors.ControlText;
            this.TabPageSpline.Location = new System.Drawing.Point(4, 22);
            this.TabPageSpline.Name = "TabPageSpline";
            this.TabPageSpline.Padding = new System.Windows.Forms.Padding(3);
            this.TabPageSpline.Size = new System.Drawing.Size(246, 425);
            this.TabPageSpline.TabIndex = 0;
            this.TabPageSpline.Text = "Почва";
            // 
            // ComboBoxMaterial
            // 
            this.ComboBoxMaterial.FormattingEnabled = true;
            this.ComboBoxMaterial.Location = new System.Drawing.Point(70, 201);
            this.ComboBoxMaterial.Name = "ComboBoxMaterial";
            this.ComboBoxMaterial.Size = new System.Drawing.Size(89, 21);
            this.ComboBoxMaterial.TabIndex = 11;
            // 
            // СheckedListBox
            // 
            this.СheckedListBox.BackColor = System.Drawing.SystemColors.Control;
            this.СheckedListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.СheckedListBox.FormattingEnabled = true;
            this.СheckedListBox.Items.AddRange(new object[] {
            "Опорные линии.",
            "BSpline."});
            this.СheckedListBox.Location = new System.Drawing.Point(4, 367);
            this.СheckedListBox.Name = "СheckedListBox";
            this.СheckedListBox.Size = new System.Drawing.Size(109, 45);
            this.СheckedListBox.TabIndex = 10;
            this.СheckedListBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.СheckedListBox_ItemCheck);
            // 
            // LabelMineralMaterial
            // 
            this.LabelMineralMaterial.AutoSize = true;
            this.LabelMineralMaterial.Location = new System.Drawing.Point(3, 201);
            this.LabelMineralMaterial.Name = "LabelMineralMaterial";
            this.LabelMineralMaterial.Size = new System.Drawing.Size(60, 26);
            this.LabelMineralMaterial.TabIndex = 9;
            this.LabelMineralMaterial.Text = "Выберите \r\nматериал";
            // 
            // DrawSplineMinerals
            // 
            this.DrawSplineMinerals.BackColor = System.Drawing.SystemColors.Control;
            this.DrawSplineMinerals.Location = new System.Drawing.Point(162, 201);
            this.DrawSplineMinerals.Name = "DrawSplineMinerals";
            this.DrawSplineMinerals.Size = new System.Drawing.Size(78, 23);
            this.DrawSplineMinerals.TabIndex = 8;
            this.DrawSplineMinerals.Text = "Нарисовать";
            this.DrawSplineMinerals.UseVisualStyleBackColor = false;
            this.DrawSplineMinerals.Click += new System.EventHandler(this.DrawSplineMinerals_Click);
            // 
            // DataGridViewMinerals
            // 
            this.DataGridViewMinerals.AllowUserToResizeColumns = false;
            this.DataGridViewMinerals.AllowUserToResizeRows = false;
            this.DataGridViewMinerals.BackgroundColor = System.Drawing.SystemColors.Control;
            this.DataGridViewMinerals.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridViewMinerals.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4});
            this.DataGridViewMinerals.GridColor = System.Drawing.SystemColors.Control;
            this.DataGridViewMinerals.Location = new System.Drawing.Point(3, 230);
            this.DataGridViewMinerals.Name = "DataGridViewMinerals";
            this.DataGridViewMinerals.RowHeadersVisible = false;
            this.DataGridViewMinerals.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.DataGridViewMinerals.Size = new System.Drawing.Size(239, 131);
            this.DataGridViewMinerals.TabIndex = 7;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "№";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn1.ToolTipText = "Порядковый номер слоя.";
            this.dataGridViewTextBoxColumn1.Width = 32;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Цвет";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn2.ToolTipText = "Цвет минерала.";
            this.dataGridViewTextBoxColumn2.Width = 68;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Материал";
            this.dataGridViewTextBoxColumn3.MaxInputLength = 6;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn3.ToolTipText = "Материал минерала.";
            this.dataGridViewTextBoxColumn3.Width = 68;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Точек";
            this.dataGridViewTextBoxColumn4.MaxInputLength = 6;
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn4.ToolTipText = "Количество опорных точек минерала.";
            this.dataGridViewTextBoxColumn4.Width = 68;
            // 
            // TextBoxLayerHeight
            // 
            this.TextBoxLayerHeight.Location = new System.Drawing.Point(116, 35);
            this.TextBoxLayerHeight.MaxLength = 6;
            this.TextBoxLayerHeight.Name = "TextBoxLayerHeight";
            this.TextBoxLayerHeight.Size = new System.Drawing.Size(43, 20);
            this.TextBoxLayerHeight.TabIndex = 6;
            this.TextBoxLayerHeight.Text = "0";
            this.TextBoxLayerHeight.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxLayerHeight_KeyPress);
            // 
            // LabelLayerHeight
            // 
            this.LabelLayerHeight.AutoSize = true;
            this.LabelLayerHeight.Location = new System.Drawing.Point(3, 35);
            this.LabelLayerHeight.Name = "LabelLayerHeight";
            this.LabelLayerHeight.Size = new System.Drawing.Size(94, 26);
            this.LabelLayerHeight.TabIndex = 5;
            this.LabelLayerHeight.Text = "Введите глубину \r\nслоя почвы\r\n";
            // 
            // AddSplineLayers
            // 
            this.AddSplineLayers.BackColor = System.Drawing.SystemColors.Control;
            this.AddSplineLayers.Location = new System.Drawing.Point(165, 35);
            this.AddSplineLayers.Name = "AddSplineLayers";
            this.AddSplineLayers.Size = new System.Drawing.Size(78, 23);
            this.AddSplineLayers.TabIndex = 4;
            this.AddSplineLayers.Text = "Добавить";
            this.AddSplineLayers.UseVisualStyleBackColor = false;
            this.AddSplineLayers.Click += new System.EventHandler(this.AddSplineLayers_Click);
            // 
            // DataGridViewLayers
            // 
            this.DataGridViewLayers.AllowUserToResizeColumns = false;
            this.DataGridViewLayers.AllowUserToResizeRows = false;
            this.DataGridViewLayers.BackgroundColor = System.Drawing.SystemColors.Control;
            this.DataGridViewLayers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridViewLayers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NumberLayer,
            this.ColorLayer,
            this.HeightLayer,
            this.NumberOfPoints});
            this.DataGridViewLayers.GridColor = System.Drawing.SystemColors.Control;
            this.DataGridViewLayers.Location = new System.Drawing.Point(4, 64);
            this.DataGridViewLayers.MultiSelect = false;
            this.DataGridViewLayers.Name = "DataGridViewLayers";
            this.DataGridViewLayers.RowHeadersVisible = false;
            this.DataGridViewLayers.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.DataGridViewLayers.Size = new System.Drawing.Size(239, 131);
            this.DataGridViewLayers.TabIndex = 3;
            this.DataGridViewLayers.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DataGridViewLayers_CellMouseDown);
            this.DataGridViewLayers.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.DataGridViewLayers_CellValidating);
            // 
            // NumberLayer
            // 
            this.NumberLayer.HeaderText = "№";
            this.NumberLayer.Name = "NumberLayer";
            this.NumberLayer.ReadOnly = true;
            this.NumberLayer.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.NumberLayer.ToolTipText = "Порядковый номер слоя.";
            this.NumberLayer.Width = 32;
            // 
            // ColorLayer
            // 
            this.ColorLayer.HeaderText = "Цвет";
            this.ColorLayer.Name = "ColorLayer";
            this.ColorLayer.ReadOnly = true;
            this.ColorLayer.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ColorLayer.ToolTipText = "Цвет слоя.";
            this.ColorLayer.Width = 68;
            // 
            // HeightLayer
            // 
            this.HeightLayer.HeaderText = "Глубина";
            this.HeightLayer.MaxInputLength = 6;
            this.HeightLayer.Name = "HeightLayer";
            this.HeightLayer.ReadOnly = true;
            this.HeightLayer.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.HeightLayer.ToolTipText = "Глубина на которой находится слой.";
            this.HeightLayer.Width = 68;
            // 
            // NumberOfPoints
            // 
            this.NumberOfPoints.HeaderText = "Точек";
            this.NumberOfPoints.MaxInputLength = 6;
            this.NumberOfPoints.Name = "NumberOfPoints";
            this.NumberOfPoints.ReadOnly = true;
            this.NumberOfPoints.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.NumberOfPoints.ToolTipText = "Количество опорных точек слоя.";
            this.NumberOfPoints.Width = 68;
            // 
            // TextBoxLayerNumberOfPoints
            // 
            this.TextBoxLayerNumberOfPoints.Location = new System.Drawing.Point(116, 9);
            this.TextBoxLayerNumberOfPoints.MaxLength = 6;
            this.TextBoxLayerNumberOfPoints.Name = "TextBoxLayerNumberOfPoints";
            this.TextBoxLayerNumberOfPoints.Size = new System.Drawing.Size(43, 20);
            this.TextBoxLayerNumberOfPoints.TabIndex = 2;
            this.TextBoxLayerNumberOfPoints.Text = "3";
            this.TextBoxLayerNumberOfPoints.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxLayerNumberOfPoints_KeyPress);
            // 
            // LabelLayerNumberOfPoints
            // 
            this.LabelLayerNumberOfPoints.AutoSize = true;
            this.LabelLayerNumberOfPoints.Location = new System.Drawing.Point(3, 6);
            this.LabelLayerNumberOfPoints.Name = "LabelLayerNumberOfPoints";
            this.LabelLayerNumberOfPoints.Size = new System.Drawing.Size(110, 26);
            this.LabelLayerNumberOfPoints.TabIndex = 1;
            this.LabelLayerNumberOfPoints.Text = "Введите количество\r\nопорных точек слоя";
            // 
            // DrawSplineLayers
            // 
            this.DrawSplineLayers.BackColor = System.Drawing.SystemColors.Control;
            this.DrawSplineLayers.Location = new System.Drawing.Point(165, 9);
            this.DrawSplineLayers.Name = "DrawSplineLayers";
            this.DrawSplineLayers.Size = new System.Drawing.Size(78, 23);
            this.DrawSplineLayers.TabIndex = 0;
            this.DrawSplineLayers.Text = "Нарисовать";
            this.DrawSplineLayers.UseVisualStyleBackColor = false;
            this.DrawSplineLayers.Click += new System.EventHandler(this.DrawSplineLayers_Click);
            // 
            // TabPageMKE
            // 
            this.TabPageMKE.BackColor = System.Drawing.SystemColors.Control;
            this.TabPageMKE.ForeColor = System.Drawing.SystemColors.ControlText;
            this.TabPageMKE.Location = new System.Drawing.Point(4, 22);
            this.TabPageMKE.Name = "TabPageMKE";
            this.TabPageMKE.Padding = new System.Windows.Forms.Padding(3);
            this.TabPageMKE.Size = new System.Drawing.Size(246, 425);
            this.TabPageMKE.TabIndex = 1;
            this.TabPageMKE.Text = "МКЭ";
            // 
            // СontextMenuMainPaint
            // 
            this.СontextMenuMainPaint.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddValueToolStripMenuItem,
            this.AddPointToolStripMenuItem,
            this.DeletePointToolStripMenuItem,
            this.DeleteLayersMainPaint});
            this.СontextMenuMainPaint.Name = "СontextMenuStrip";
            this.СontextMenuMainPaint.Size = new System.Drawing.Size(165, 92);
            // 
            // AddValueToolStripMenuItem
            // 
            this.AddValueToolStripMenuItem.Name = "AddValueToolStripMenuItem";
            this.AddValueToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.AddValueToolStripMenuItem.Text = "Задать значение";
            this.AddValueToolStripMenuItem.Click += new System.EventHandler(this.AddValueToolStripMenuItem_Click);
            // 
            // AddPointToolStripMenuItem
            // 
            this.AddPointToolStripMenuItem.Name = "AddPointToolStripMenuItem";
            this.AddPointToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.AddPointToolStripMenuItem.Text = "Добавить точку";
            this.AddPointToolStripMenuItem.Click += new System.EventHandler(this.AddPointToolStripMenuItem_Click);
            // 
            // DeletePointToolStripMenuItem
            // 
            this.DeletePointToolStripMenuItem.Name = "DeletePointToolStripMenuItem";
            this.DeletePointToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.DeletePointToolStripMenuItem.Text = "Удалить точку";
            this.DeletePointToolStripMenuItem.Click += new System.EventHandler(this.DeletePointToolStripMenuItem_Click);
            // 
            // DeleteLayersMainPaint
            // 
            this.DeleteLayersMainPaint.Name = "DeleteLayersMainPaint";
            this.DeleteLayersMainPaint.Size = new System.Drawing.Size(164, 22);
            this.DeleteLayersMainPaint.Text = "Удалить сплайн";
            this.DeleteLayersMainPaint.Click += new System.EventHandler(this.DeleteLayersMainPaint_Click);
            // 
            // ToolTip
            // 
            this.ToolTip.AutoPopDelay = 5000;
            this.ToolTip.InitialDelay = 500;
            this.ToolTip.ReshowDelay = 100;
            // 
            // ContextMenuDataGridViewLayers
            // 
            this.ContextMenuDataGridViewLayers.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DeleteLayersDataGridViewLayers});
            this.ContextMenuDataGridViewLayers.Name = "СontextMenuStrip";
            this.ContextMenuDataGridViewLayers.Size = new System.Drawing.Size(119, 26);
            // 
            // DeleteLayersDataGridViewLayers
            // 
            this.DeleteLayersDataGridViewLayers.Name = "DeleteLayersDataGridViewLayers";
            this.DeleteLayersDataGridViewLayers.Size = new System.Drawing.Size(118, 22);
            this.DeleteLayersDataGridViewLayers.Text = "Удалить";
            this.DeleteLayersDataGridViewLayers.Click += new System.EventHandler(this.DeleteLayersDataGridViewLayers_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.TextBoxYCoordinate);
            this.groupBox1.Controls.Add(this.TextBoxXCoordinate);
            this.groupBox1.Controls.Add(this.LabelYCoordiante);
            this.groupBox1.Controls.Add(this.LabelXCoordinate);
            this.groupBox1.Location = new System.Drawing.Point(618, 32);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(254, 69);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Координаты";
            // 
            // TextBoxYCoordinate
            // 
            this.TextBoxYCoordinate.Location = new System.Drawing.Point(30, 44);
            this.TextBoxYCoordinate.Name = "TextBoxYCoordinate";
            this.TextBoxYCoordinate.ReadOnly = true;
            this.TextBoxYCoordinate.Size = new System.Drawing.Size(122, 20);
            this.TextBoxYCoordinate.TabIndex = 3;
            // 
            // TextBoxXCoordinate
            // 
            this.TextBoxXCoordinate.Location = new System.Drawing.Point(30, 19);
            this.TextBoxXCoordinate.Name = "TextBoxXCoordinate";
            this.TextBoxXCoordinate.ReadOnly = true;
            this.TextBoxXCoordinate.Size = new System.Drawing.Size(122, 20);
            this.TextBoxXCoordinate.TabIndex = 2;
            // 
            // LabelYCoordiante
            // 
            this.LabelYCoordiante.AutoSize = true;
            this.LabelYCoordiante.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LabelYCoordiante.Location = new System.Drawing.Point(7, 44);
            this.LabelYCoordiante.Name = "LabelYCoordiante";
            this.LabelYCoordiante.Size = new System.Drawing.Size(24, 20);
            this.LabelYCoordiante.TabIndex = 1;
            this.LabelYCoordiante.Text = "Y:";
            // 
            // LabelXCoordinate
            // 
            this.LabelXCoordinate.AutoSize = true;
            this.LabelXCoordinate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LabelXCoordinate.Location = new System.Drawing.Point(7, 19);
            this.LabelXCoordinate.Name = "LabelXCoordinate";
            this.LabelXCoordinate.Size = new System.Drawing.Size(24, 20);
            this.LabelXCoordinate.TabIndex = 0;
            this.LabelXCoordinate.Text = "X:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 582);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.TabControl);
            this.Controls.Add(this.MainPaint_HScroll);
            this.Controls.Add(this.MainPaint_VScroll);
            this.Controls.Add(this.MainPaint);
            this.Controls.Add(this.MenuStrip);
            this.MainMenuStrip = this.MenuStrip;
            this.MinimumSize = new System.Drawing.Size(900, 620);
            this.Name = "MainForm";
            this.Text = "Задание геологических структур для программного комплекса решения задач геоэлектр" +
    "ики";
            this.SizeChanged += new System.EventHandler(this.MainForm_SizeChanged);
            this.MenuStrip.ResumeLayout(false);
            this.MenuStrip.PerformLayout();
            this.TabControl.ResumeLayout(false);
            this.TabPageSettings.ResumeLayout(false);
            this.TabPageSettings.PerformLayout();
            this.TabPageSpline.ResumeLayout(false);
            this.TabPageSpline.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewMinerals)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewLayers)).EndInit();
            this.СontextMenuMainPaint.ResumeLayout(false);
            this.ContextMenuDataGridViewLayers.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Tao.Platform.Windows.SimpleOpenGlControl MainPaint;
        private System.Windows.Forms.Timer RenderTimer;
        private System.Windows.Forms.VScrollBar MainPaint_VScroll;
        private System.Windows.Forms.HScrollBar MainPaint_HScroll;
        private System.Windows.Forms.MenuStrip MenuStrip;
        private System.Windows.Forms.ToolStripMenuItem FileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CloseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AboutProgrammToolStripMenuItem;
        private System.Windows.Forms.TabControl TabControl;
        private System.Windows.Forms.TabPage TabPageSpline;
        private System.Windows.Forms.TabPage TabPageMKE;
        private System.Windows.Forms.ContextMenuStrip СontextMenuMainPaint;
        private System.Windows.Forms.ToolStripMenuItem DeleteLayersMainPaint;
        private System.Windows.Forms.ToolTip ToolTip;
        private System.Windows.Forms.Button DrawSplineLayers;
        private System.Windows.Forms.TextBox TextBoxLayerNumberOfPoints;
        private System.Windows.Forms.Label LabelLayerNumberOfPoints;
        private System.Windows.Forms.DataGridView DataGridViewLayers;
        private System.Windows.Forms.TextBox TextBoxLayerHeight;
        private System.Windows.Forms.Label LabelLayerHeight;
        private System.Windows.Forms.Button AddSplineLayers;
        private System.Windows.Forms.Label LabelMineralMaterial;
        private System.Windows.Forms.Button DrawSplineMinerals;
        private System.Windows.Forms.DataGridView DataGridViewMinerals;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.CheckedListBox СheckedListBox;
        private System.Windows.Forms.ComboBox ComboBoxMaterial;
        private System.Windows.Forms.DataGridViewTextBoxColumn NumberLayer;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColorLayer;
        private System.Windows.Forms.DataGridViewTextBoxColumn HeightLayer;
        private System.Windows.Forms.DataGridViewTextBoxColumn NumberOfPoints;
        private System.Windows.Forms.ContextMenuStrip ContextMenuDataGridViewLayers;
        private System.Windows.Forms.ToolStripMenuItem DeleteLayersDataGridViewLayers;
        private System.Windows.Forms.ToolStripMenuItem AddValueToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AddPointToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DeletePointToolStripMenuItem;
        private System.Windows.Forms.TabPage TabPageSettings;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label LabelYCoordiante;
        private System.Windows.Forms.Label LabelXCoordinate;
        private System.Windows.Forms.TextBox TextBoxYCoordinate;
        private System.Windows.Forms.TextBox TextBoxXCoordinate;
        private System.Windows.Forms.Label LabelXAreaSize;
        private System.Windows.Forms.TextBox TextBoxXAreaSize;
        private System.Windows.Forms.TextBox TextBoxYAreaSize;
        private System.Windows.Forms.TextBox TextBoxEarthSize;
        private System.Windows.Forms.Label LabelEarthSize;
        private System.Windows.Forms.Label LabelYAreaSize;
        private System.Windows.Forms.Label LabelAccuracy;
        private System.Windows.Forms.TextBox TextBoxAccuracy;
        private System.Windows.Forms.CheckedListBox СheckedListBoxSettings;
    }
}

