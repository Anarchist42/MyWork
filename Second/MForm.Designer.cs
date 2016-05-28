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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.MainPaint = new Tao.Platform.Windows.SimpleOpenGlControl();
            this.RenderTimer = new System.Windows.Forms.Timer(this.components);
            this.MenuStrip = new System.Windows.Forms.MenuStrip();
            this.FileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CloseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutProgrammToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TabControl = new System.Windows.Forms.TabControl();
            this.TabPageSettings = new System.Windows.Forms.TabPage();
            this.ButtonChangeXMoveSpline = new System.Windows.Forms.Button();
            this.TextBoxChangeXMoveSpline = new System.Windows.Forms.TextBox();
            this.LabelChangeXMoveSpline = new System.Windows.Forms.Label();
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
            this.СomboBoxLayerMaterial = new System.Windows.Forms.ComboBox();
            this.LabelLayerMaterial = new System.Windows.Forms.Label();
            this.ComboBoxMineralMaterial = new System.Windows.Forms.ComboBox();
            this.СheckedListBoxSpline = new System.Windows.Forms.CheckedListBox();
            this.LabelMineralMaterial = new System.Windows.Forms.Label();
            this.DrawSplineMinerals = new System.Windows.Forms.Button();
            this.DataGridViewMinerals = new System.Windows.Forms.DataGridView();
            this.TextBoxLayerHeight = new System.Windows.Forms.TextBox();
            this.LabelLayerHeight = new System.Windows.Forms.Label();
            this.AddSplineLayers = new System.Windows.Forms.Button();
            this.DataGridViewLayers = new System.Windows.Forms.DataGridView();
            this.TextBoxLayerNumberOfPoints = new System.Windows.Forms.TextBox();
            this.LabelLayerNumberOfPoints = new System.Windows.Forms.Label();
            this.DrawSplineLayers = new System.Windows.Forms.Button();
            this.TabPageMKE = new System.Windows.Forms.TabPage();
            this.ButtonMakePartition = new System.Windows.Forms.Button();
            this.СheckedListBoxMKE = new System.Windows.Forms.CheckedListBox();
            this.PictureBoxColorPartition = new System.Windows.Forms.PictureBox();
            this.LabelColorPartition = new System.Windows.Forms.Label();
            this.ButtonSavePartition = new System.Windows.Forms.Button();
            this.TextBoxStepPartition = new System.Windows.Forms.TextBox();
            this.LabelStepPartition = new System.Windows.Forms.Label();
            this.ContextMenuMaterials = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.AddFromFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AddMaterialToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ChangeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteMaterialToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.СontextMenuMainPaint = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ChangeValueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ChangeMaterialToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ChangeColorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AddPointToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeletePointToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteLayersMainPaintToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.ContextMenuDataGridView = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MaterialSplineDataGridViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ChangeColorDataGridViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteSplineDataGridViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GroupBox = new System.Windows.Forms.GroupBox();
            this.TextBoxYCoordinate = new System.Windows.Forms.TextBox();
            this.TextBoxXCoordinate = new System.Windows.Forms.TextBox();
            this.LabelYCoordiante = new System.Windows.Forms.Label();
            this.LabelXCoordinate = new System.Windows.Forms.Label();
            this.ColorDialog = new System.Windows.Forms.ColorDialog();
            this.UnFocus = new System.Windows.Forms.Button();
            this.OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.SaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.SaveSceneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LoadSceneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NumberLayer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColorLayer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HeightLayer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Material = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MainPaint_HScroll = new Second.MyScroll();
            this.MainPaint_VScroll = new Second.MyScroll();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MenuStrip.SuspendLayout();
            this.TabControl.SuspendLayout();
            this.TabPageSettings.SuspendLayout();
            this.TabPageSpline.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewMinerals)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewLayers)).BeginInit();
            this.TabPageMKE.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxColorPartition)).BeginInit();
            this.ContextMenuMaterials.SuspendLayout();
            this.СontextMenuMainPaint.SuspendLayout();
            this.ContextMenuDataGridView.SuspendLayout();
            this.GroupBox.SuspendLayout();
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
            this.MainPaint.MinimumSize = new System.Drawing.Size(587, 526);
            this.MainPaint.Name = "MainPaint";
            this.MainPaint.Size = new System.Drawing.Size(587, 526);
            this.MainPaint.StencilBits = ((byte)(0));
            this.MainPaint.TabIndex = 0;
            this.MainPaint.Load += new System.EventHandler(this.MainPaint_Load);
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
            this.LoadSceneToolStripMenuItem,
            this.SaveSceneToolStripMenuItem,
            this.CloseToolStripMenuItem});
            this.FileToolStripMenuItem.Name = "FileToolStripMenuItem";
            this.FileToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.FileToolStripMenuItem.Text = "Файл";
            // 
            // CloseToolStripMenuItem
            // 
            this.CloseToolStripMenuItem.Name = "CloseToolStripMenuItem";
            this.CloseToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.CloseToolStripMenuItem.Text = "Закрыть";
            this.CloseToolStripMenuItem.Click += new System.EventHandler(this.CloseToolStripMenuItem_Click);
            // 
            // AboutProgrammToolStripMenuItem
            // 
            this.AboutProgrammToolStripMenuItem.Name = "AboutProgrammToolStripMenuItem";
            this.AboutProgrammToolStripMenuItem.Size = new System.Drawing.Size(94, 20);
            this.AboutProgrammToolStripMenuItem.Text = "О программе";
            this.AboutProgrammToolStripMenuItem.Click += new System.EventHandler(this.AboutProgrammToolStripMenuItem_Click);
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
            this.TabPageSettings.Controls.Add(this.ButtonChangeXMoveSpline);
            this.TabPageSettings.Controls.Add(this.TextBoxChangeXMoveSpline);
            this.TabPageSettings.Controls.Add(this.LabelChangeXMoveSpline);
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
            this.TabPageSettings.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TabPageSettings_MouseClick);
            // 
            // ButtonChangeXMoveSpline
            // 
            this.ButtonChangeXMoveSpline.Enabled = false;
            this.ButtonChangeXMoveSpline.Location = new System.Drawing.Point(126, 234);
            this.ButtonChangeXMoveSpline.Name = "ButtonChangeXMoveSpline";
            this.ButtonChangeXMoveSpline.Size = new System.Drawing.Size(75, 23);
            this.ButtonChangeXMoveSpline.TabIndex = 11;
            this.ButtonChangeXMoveSpline.Text = "Сместить";
            this.ButtonChangeXMoveSpline.UseVisualStyleBackColor = true;
            this.ButtonChangeXMoveSpline.Click += new System.EventHandler(this.ButtonChangeXMoveSpline_Click);
            // 
            // TextBoxChangeXMoveSpline
            // 
            this.TextBoxChangeXMoveSpline.Enabled = false;
            this.TextBoxChangeXMoveSpline.Location = new System.Drawing.Point(128, 199);
            this.TextBoxChangeXMoveSpline.Name = "TextBoxChangeXMoveSpline";
            this.TextBoxChangeXMoveSpline.Size = new System.Drawing.Size(100, 20);
            this.TextBoxChangeXMoveSpline.TabIndex = 10;
            this.TextBoxChangeXMoveSpline.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxChangeXMoveSpline_KeyPress);
            this.TextBoxChangeXMoveSpline.Validating += new System.ComponentModel.CancelEventHandler(this.TextBoxChangeXMoveSpline_Validating);
            this.TextBoxChangeXMoveSpline.Validated += new System.EventHandler(this.TextBoxChangeXMoveSpline_Validated);
            // 
            // LabelChangeXMoveSpline
            // 
            this.LabelChangeXMoveSpline.AutoSize = true;
            this.LabelChangeXMoveSpline.Location = new System.Drawing.Point(5, 196);
            this.LabelChangeXMoveSpline.Name = "LabelChangeXMoveSpline";
            this.LabelChangeXMoveSpline.Size = new System.Drawing.Size(105, 26);
            this.LabelChangeXMoveSpline.TabIndex = 9;
            this.LabelChangeXMoveSpline.Text = "Смещение области\r\nвправо, м\r\n";
            // 
            // СheckedListBoxSettings
            // 
            this.СheckedListBoxSettings.BackColor = System.Drawing.SystemColors.Control;
            this.СheckedListBoxSettings.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.СheckedListBoxSettings.Enabled = false;
            this.СheckedListBoxSettings.FormattingEnabled = true;
            this.СheckedListBoxSettings.Items.AddRange(new object[] {
            "Сетка",
            "Разметка"});
            this.СheckedListBoxSettings.Location = new System.Drawing.Point(4, 148);
            this.СheckedListBoxSettings.Name = "СheckedListBoxSettings";
            this.СheckedListBoxSettings.Size = new System.Drawing.Size(78, 30);
            this.СheckedListBoxSettings.TabIndex = 8;
            this.СheckedListBoxSettings.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.СheckedListBoxSettings_ItemCheck);
            this.СheckedListBoxSettings.SelectedIndexChanged += new System.EventHandler(this.СheckedListBoxSettings_SelectedIndexChanged);
            this.СheckedListBoxSettings.MouseLeave += new System.EventHandler(this.СheckedListBoxSettings_MouseLeave);
            // 
            // TextBoxAccuracy
            // 
            this.TextBoxAccuracy.Location = new System.Drawing.Point(126, 6);
            this.TextBoxAccuracy.MaxLength = 2;
            this.TextBoxAccuracy.Name = "TextBoxAccuracy";
            this.TextBoxAccuracy.Size = new System.Drawing.Size(100, 20);
            this.TextBoxAccuracy.TabIndex = 1;
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
            this.TextBoxXAreaSize.TabIndex = 2;
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
            this.TabPageSpline.Controls.Add(this.СomboBoxLayerMaterial);
            this.TabPageSpline.Controls.Add(this.LabelLayerMaterial);
            this.TabPageSpline.Controls.Add(this.ComboBoxMineralMaterial);
            this.TabPageSpline.Controls.Add(this.СheckedListBoxSpline);
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
            this.TabPageSpline.Click += new System.EventHandler(this.TabPageSpline_Click);
            // 
            // СomboBoxLayerMaterial
            // 
            this.СomboBoxLayerMaterial.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.СomboBoxLayerMaterial.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.СomboBoxLayerMaterial.FormattingEnabled = true;
            this.СomboBoxLayerMaterial.Location = new System.Drawing.Point(71, 35);
            this.СomboBoxLayerMaterial.Name = "СomboBoxLayerMaterial";
            this.СomboBoxLayerMaterial.Size = new System.Drawing.Size(89, 21);
            this.СomboBoxLayerMaterial.TabIndex = 13;
            this.СomboBoxLayerMaterial.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.СomboBoxLayerMaterial_DrawItem);
            this.СomboBoxLayerMaterial.DropDownClosed += new System.EventHandler(this.СomboBoxLayerMaterial_DropDownClosed);
            this.СomboBoxLayerMaterial.MouseDown += new System.Windows.Forms.MouseEventHandler(this.СomboBoxLayerMaterial_MouseDown);
            // 
            // LabelLayerMaterial
            // 
            this.LabelLayerMaterial.AutoSize = true;
            this.LabelLayerMaterial.Location = new System.Drawing.Point(4, 35);
            this.LabelLayerMaterial.Name = "LabelLayerMaterial";
            this.LabelLayerMaterial.Size = new System.Drawing.Size(60, 26);
            this.LabelLayerMaterial.TabIndex = 12;
            this.LabelLayerMaterial.Text = "Выберите \r\nматериал";
            // 
            // ComboBoxMineralMaterial
            // 
            this.ComboBoxMineralMaterial.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ComboBoxMineralMaterial.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxMineralMaterial.FormattingEnabled = true;
            this.ComboBoxMineralMaterial.Location = new System.Drawing.Point(71, 223);
            this.ComboBoxMineralMaterial.Name = "ComboBoxMineralMaterial";
            this.ComboBoxMineralMaterial.Size = new System.Drawing.Size(89, 21);
            this.ComboBoxMineralMaterial.TabIndex = 11;
            this.ComboBoxMineralMaterial.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.ComboBoxMineralMaterial_DrawItem);
            this.ComboBoxMineralMaterial.DropDownClosed += new System.EventHandler(this.ComboBoxMineralMaterial_DropDownClosed);
            this.ComboBoxMineralMaterial.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ComboBoxMineralMaterial_MouseDown);
            // 
            // СheckedListBoxSpline
            // 
            this.СheckedListBoxSpline.BackColor = System.Drawing.SystemColors.Control;
            this.СheckedListBoxSpline.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.СheckedListBoxSpline.Enabled = false;
            this.СheckedListBoxSpline.FormattingEnabled = true;
            this.СheckedListBoxSpline.Items.AddRange(new object[] {
            "Опорные линии.",
            "BSpline.",
            "CSpline"});
            this.СheckedListBoxSpline.Location = new System.Drawing.Point(4, 374);
            this.СheckedListBoxSpline.Name = "СheckedListBoxSpline";
            this.СheckedListBoxSpline.Size = new System.Drawing.Size(109, 45);
            this.СheckedListBoxSpline.TabIndex = 10;
            this.СheckedListBoxSpline.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.СheckedListBoxSpline_ItemCheck);
            this.СheckedListBoxSpline.SelectedIndexChanged += new System.EventHandler(this.СheckedListBoxSpline_SelectedIndexChanged);
            this.СheckedListBoxSpline.MouseLeave += new System.EventHandler(this.СheckedListBoxSpline_MouseLeave);
            // 
            // LabelMineralMaterial
            // 
            this.LabelMineralMaterial.AutoSize = true;
            this.LabelMineralMaterial.Location = new System.Drawing.Point(4, 223);
            this.LabelMineralMaterial.Name = "LabelMineralMaterial";
            this.LabelMineralMaterial.Size = new System.Drawing.Size(60, 26);
            this.LabelMineralMaterial.TabIndex = 9;
            this.LabelMineralMaterial.Text = "Выберите \r\nматериал";
            // 
            // DrawSplineMinerals
            // 
            this.DrawSplineMinerals.BackColor = System.Drawing.SystemColors.Control;
            this.DrawSplineMinerals.Enabled = false;
            this.DrawSplineMinerals.Location = new System.Drawing.Point(163, 223);
            this.DrawSplineMinerals.Name = "DrawSplineMinerals";
            this.DrawSplineMinerals.Size = new System.Drawing.Size(78, 23);
            this.DrawSplineMinerals.TabIndex = 8;
            this.DrawSplineMinerals.Text = "Нарисовать";
            this.ToolTip.SetToolTip(this.DrawSplineMinerals, "Начать рисовать отложения.");
            this.DrawSplineMinerals.UseVisualStyleBackColor = false;
            this.DrawSplineMinerals.Click += new System.EventHandler(this.DrawSplineMinerals_Click);
            // 
            // DataGridViewMinerals
            // 
            this.DataGridViewMinerals.AllowUserToResizeColumns = false;
            this.DataGridViewMinerals.AllowUserToResizeRows = false;
            this.DataGridViewMinerals.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.DataGridViewMinerals.BackgroundColor = System.Drawing.SystemColors.Control;
            this.DataGridViewMinerals.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridViewMinerals.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataGridViewMinerals.DefaultCellStyle = dataGridViewCellStyle3;
            this.DataGridViewMinerals.GridColor = System.Drawing.SystemColors.Control;
            this.DataGridViewMinerals.Location = new System.Drawing.Point(4, 252);
            this.DataGridViewMinerals.Name = "DataGridViewMinerals";
            this.DataGridViewMinerals.RowHeadersVisible = false;
            this.DataGridViewMinerals.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.DataGridViewMinerals.Size = new System.Drawing.Size(239, 120);
            this.DataGridViewMinerals.TabIndex = 7;
            this.DataGridViewMinerals.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DataGridViewMinerals_CellMouseDown);
            // 
            // TextBoxLayerHeight
            // 
            this.TextBoxLayerHeight.Location = new System.Drawing.Point(117, 64);
            this.TextBoxLayerHeight.Name = "TextBoxLayerHeight";
            this.TextBoxLayerHeight.Size = new System.Drawing.Size(43, 20);
            this.TextBoxLayerHeight.TabIndex = 6;
            this.TextBoxLayerHeight.Text = "0";
            this.TextBoxLayerHeight.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxLayerHeight_KeyPress);
            this.TextBoxLayerHeight.Validating += new System.ComponentModel.CancelEventHandler(this.TextBoxLayerHeight_Validating);
            // 
            // LabelLayerHeight
            // 
            this.LabelLayerHeight.AutoSize = true;
            this.LabelLayerHeight.Location = new System.Drawing.Point(4, 64);
            this.LabelLayerHeight.Name = "LabelLayerHeight";
            this.LabelLayerHeight.Size = new System.Drawing.Size(94, 26);
            this.LabelLayerHeight.TabIndex = 5;
            this.LabelLayerHeight.Text = "Введите глубину \r\nслоя почвы\r\n";
            // 
            // AddSplineLayers
            // 
            this.AddSplineLayers.BackColor = System.Drawing.SystemColors.Control;
            this.AddSplineLayers.Enabled = false;
            this.AddSplineLayers.Location = new System.Drawing.Point(166, 64);
            this.AddSplineLayers.Name = "AddSplineLayers";
            this.AddSplineLayers.Size = new System.Drawing.Size(78, 23);
            this.AddSplineLayers.TabIndex = 4;
            this.AddSplineLayers.Text = "Добавить";
            this.ToolTip.SetToolTip(this.AddSplineLayers, "Добавить границу слоя почвы.");
            this.AddSplineLayers.UseVisualStyleBackColor = false;
            this.AddSplineLayers.Click += new System.EventHandler(this.AddSplineLayers_Click);
            // 
            // DataGridViewLayers
            // 
            this.DataGridViewLayers.AllowUserToResizeColumns = false;
            this.DataGridViewLayers.AllowUserToResizeRows = false;
            this.DataGridViewLayers.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.DataGridViewLayers.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataGridViewLayers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.DataGridViewLayers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridViewLayers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NumberLayer,
            this.ColorLayer,
            this.HeightLayer,
            this.Material});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataGridViewLayers.DefaultCellStyle = dataGridViewCellStyle5;
            this.DataGridViewLayers.GridColor = System.Drawing.SystemColors.Control;
            this.DataGridViewLayers.Location = new System.Drawing.Point(5, 93);
            this.DataGridViewLayers.MultiSelect = false;
            this.DataGridViewLayers.Name = "DataGridViewLayers";
            this.DataGridViewLayers.RowHeadersVisible = false;
            this.DataGridViewLayers.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.DataGridViewLayers.Size = new System.Drawing.Size(239, 120);
            this.DataGridViewLayers.TabIndex = 3;
            this.DataGridViewLayers.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DataGridViewLayers_CellMouseDown);
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
            this.TextBoxLayerNumberOfPoints.Validating += new System.ComponentModel.CancelEventHandler(this.TextBoxLayerNumberOfPoints_Validating);
            this.TextBoxLayerNumberOfPoints.Validated += new System.EventHandler(this.TextBoxLayerNumberOfPoints_Validated);
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
            this.DrawSplineLayers.Enabled = false;
            this.DrawSplineLayers.Location = new System.Drawing.Point(165, 9);
            this.DrawSplineLayers.Name = "DrawSplineLayers";
            this.DrawSplineLayers.Size = new System.Drawing.Size(78, 23);
            this.DrawSplineLayers.TabIndex = 0;
            this.DrawSplineLayers.Text = "Нарисовать";
            this.ToolTip.SetToolTip(this.DrawSplineLayers, "Нарисовать границу слоя почвы.");
            this.DrawSplineLayers.UseVisualStyleBackColor = false;
            this.DrawSplineLayers.Click += new System.EventHandler(this.DrawSplineLayers_Click);
            // 
            // TabPageMKE
            // 
            this.TabPageMKE.BackColor = System.Drawing.SystemColors.Control;
            this.TabPageMKE.Controls.Add(this.ButtonMakePartition);
            this.TabPageMKE.Controls.Add(this.СheckedListBoxMKE);
            this.TabPageMKE.Controls.Add(this.PictureBoxColorPartition);
            this.TabPageMKE.Controls.Add(this.LabelColorPartition);
            this.TabPageMKE.Controls.Add(this.ButtonSavePartition);
            this.TabPageMKE.Controls.Add(this.TextBoxStepPartition);
            this.TabPageMKE.Controls.Add(this.LabelStepPartition);
            this.TabPageMKE.ForeColor = System.Drawing.SystemColors.ControlText;
            this.TabPageMKE.Location = new System.Drawing.Point(4, 22);
            this.TabPageMKE.Name = "TabPageMKE";
            this.TabPageMKE.Padding = new System.Windows.Forms.Padding(3);
            this.TabPageMKE.Size = new System.Drawing.Size(246, 425);
            this.TabPageMKE.TabIndex = 1;
            this.TabPageMKE.Text = "МКЭ";
            this.TabPageMKE.Click += new System.EventHandler(this.TabPageMKE_Click);
            // 
            // ButtonMakePartition
            // 
            this.ButtonMakePartition.Location = new System.Drawing.Point(140, 67);
            this.ButtonMakePartition.Name = "ButtonMakePartition";
            this.ButtonMakePartition.Size = new System.Drawing.Size(75, 23);
            this.ButtonMakePartition.TabIndex = 8;
            this.ButtonMakePartition.Text = "Разбиение";
            this.ButtonMakePartition.UseVisualStyleBackColor = true;
            this.ButtonMakePartition.Click += new System.EventHandler(this.ButtonMakePartition_Click);
            // 
            // СheckedListBoxMKE
            // 
            this.СheckedListBoxMKE.BackColor = System.Drawing.SystemColors.Control;
            this.СheckedListBoxMKE.FormattingEnabled = true;
            this.СheckedListBoxMKE.Items.AddRange(new object[] {
            "Макет"});
            this.СheckedListBoxMKE.Location = new System.Drawing.Point(3, 67);
            this.СheckedListBoxMKE.Name = "СheckedListBoxMKE";
            this.СheckedListBoxMKE.Size = new System.Drawing.Size(62, 19);
            this.СheckedListBoxMKE.TabIndex = 7;
            this.СheckedListBoxMKE.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.СheckedListBoxMKE_ItemCheck);
            this.СheckedListBoxMKE.SelectedIndexChanged += new System.EventHandler(this.СheckedListBoxMKE_SelectedIndexChanged);
            this.СheckedListBoxMKE.MouseLeave += new System.EventHandler(this.СheckedListBoxMKE_MouseLeave);
            // 
            // PictureBoxColorPartition
            // 
            this.PictureBoxColorPartition.BackColor = System.Drawing.Color.White;
            this.PictureBoxColorPartition.Location = new System.Drawing.Point(140, 36);
            this.PictureBoxColorPartition.Name = "PictureBoxColorPartition";
            this.PictureBoxColorPartition.Size = new System.Drawing.Size(100, 25);
            this.PictureBoxColorPartition.TabIndex = 6;
            this.PictureBoxColorPartition.TabStop = false;
            this.PictureBoxColorPartition.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PictureBoxColorPartition_MouseDown);
            // 
            // LabelColorPartition
            // 
            this.LabelColorPartition.AutoSize = true;
            this.LabelColorPartition.Location = new System.Drawing.Point(3, 35);
            this.LabelColorPartition.Name = "LabelColorPartition";
            this.LabelColorPartition.Size = new System.Drawing.Size(32, 13);
            this.LabelColorPartition.TabIndex = 5;
            this.LabelColorPartition.Text = "Цвет";
            // 
            // ButtonSavePartition
            // 
            this.ButtonSavePartition.Location = new System.Drawing.Point(140, 108);
            this.ButtonSavePartition.Name = "ButtonSavePartition";
            this.ButtonSavePartition.Size = new System.Drawing.Size(75, 23);
            this.ButtonSavePartition.TabIndex = 4;
            this.ButtonSavePartition.Text = "Сохранение";
            this.ButtonSavePartition.UseVisualStyleBackColor = true;
            this.ButtonSavePartition.Click += new System.EventHandler(this.ButtonSavePartition_Click);
            // 
            // TextBoxStepPartition
            // 
            this.TextBoxStepPartition.Location = new System.Drawing.Point(140, 9);
            this.TextBoxStepPartition.Name = "TextBoxStepPartition";
            this.TextBoxStepPartition.Size = new System.Drawing.Size(100, 20);
            this.TextBoxStepPartition.TabIndex = 1;
            this.TextBoxStepPartition.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxStepPartition_KeyPress);
            this.TextBoxStepPartition.Validating += new System.ComponentModel.CancelEventHandler(this.TextBoxStepPartition_Validating);
            // 
            // LabelStepPartition
            // 
            this.LabelStepPartition.AutoSize = true;
            this.LabelStepPartition.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LabelStepPartition.Location = new System.Drawing.Point(3, 12);
            this.LabelStepPartition.Name = "LabelStepPartition";
            this.LabelStepPartition.Size = new System.Drawing.Size(123, 13);
            this.LabelStepPartition.TabIndex = 0;
            this.LabelStepPartition.Text = "Шаг разбиения по Х, м";
            // 
            // ContextMenuMaterials
            // 
            this.ContextMenuMaterials.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddFromFileToolStripMenuItem,
            this.AddMaterialToolStripMenuItem,
            this.ChangeToolStripMenuItem,
            this.DeleteMaterialToolStripMenuItem});
            this.ContextMenuMaterials.Name = "ContextMenuMaterials";
            this.ContextMenuMaterials.Size = new System.Drawing.Size(180, 92);
            // 
            // AddFromFileToolStripMenuItem
            // 
            this.AddFromFileToolStripMenuItem.Name = "AddFromFileToolStripMenuItem";
            this.AddFromFileToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.AddFromFileToolStripMenuItem.Text = "Добавить из файла";
            this.AddFromFileToolStripMenuItem.Click += new System.EventHandler(this.AddFromFileToolStripMenuItem_Click);
            // 
            // AddMaterialToolStripMenuItem
            // 
            this.AddMaterialToolStripMenuItem.Name = "AddMaterialToolStripMenuItem";
            this.AddMaterialToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.AddMaterialToolStripMenuItem.Text = "Добавить";
            this.AddMaterialToolStripMenuItem.Click += new System.EventHandler(this.AddMaterialToolStripMenuItem_Click);
            // 
            // ChangeToolStripMenuItem
            // 
            this.ChangeToolStripMenuItem.Name = "ChangeToolStripMenuItem";
            this.ChangeToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.ChangeToolStripMenuItem.Text = "Изменить";
            this.ChangeToolStripMenuItem.Click += new System.EventHandler(this.ChangeToolStripMenuItem_Click);
            // 
            // DeleteMaterialToolStripMenuItem
            // 
            this.DeleteMaterialToolStripMenuItem.Name = "DeleteMaterialToolStripMenuItem";
            this.DeleteMaterialToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.DeleteMaterialToolStripMenuItem.Text = "Удалить";
            this.DeleteMaterialToolStripMenuItem.Click += new System.EventHandler(this.DeleteMaterialToolStripMenuItem_Click);
            // 
            // СontextMenuMainPaint
            // 
            this.СontextMenuMainPaint.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ChangeValueToolStripMenuItem,
            this.ChangeMaterialToolStripMenuItem,
            this.ChangeColorToolStripMenuItem,
            this.AddPointToolStripMenuItem,
            this.DeletePointToolStripMenuItem,
            this.DeleteLayersMainPaintToolStripMenuItem});
            this.СontextMenuMainPaint.Name = "СontextMenuStrip";
            this.СontextMenuMainPaint.Size = new System.Drawing.Size(167, 136);
            // 
            // ChangeValueToolStripMenuItem
            // 
            this.ChangeValueToolStripMenuItem.Name = "ChangeValueToolStripMenuItem";
            this.ChangeValueToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.ChangeValueToolStripMenuItem.Text = "Задать значение";
            this.ChangeValueToolStripMenuItem.Click += new System.EventHandler(this.ChangeValueToolStripMenuItem_Click);
            // 
            // ChangeMaterialToolStripMenuItem
            // 
            this.ChangeMaterialToolStripMenuItem.Name = "ChangeMaterialToolStripMenuItem";
            this.ChangeMaterialToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.ChangeMaterialToolStripMenuItem.Text = "Задать материал";
            // 
            // ChangeColorToolStripMenuItem
            // 
            this.ChangeColorToolStripMenuItem.Name = "ChangeColorToolStripMenuItem";
            this.ChangeColorToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.ChangeColorToolStripMenuItem.Text = "Поменять цвет";
            this.ChangeColorToolStripMenuItem.Click += new System.EventHandler(this.ChangeColorToolStripMenuItem_Click);
            // 
            // AddPointToolStripMenuItem
            // 
            this.AddPointToolStripMenuItem.Name = "AddPointToolStripMenuItem";
            this.AddPointToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.AddPointToolStripMenuItem.Text = "Добавить точку";
            this.AddPointToolStripMenuItem.Click += new System.EventHandler(this.AddPointToolStripMenuItem_Click);
            // 
            // DeletePointToolStripMenuItem
            // 
            this.DeletePointToolStripMenuItem.Name = "DeletePointToolStripMenuItem";
            this.DeletePointToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.DeletePointToolStripMenuItem.Text = "Удалить точку";
            this.DeletePointToolStripMenuItem.Click += new System.EventHandler(this.DeletePointToolStripMenuItem_Click);
            // 
            // DeleteLayersMainPaintToolStripMenuItem
            // 
            this.DeleteLayersMainPaintToolStripMenuItem.Name = "DeleteLayersMainPaintToolStripMenuItem";
            this.DeleteLayersMainPaintToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.DeleteLayersMainPaintToolStripMenuItem.Text = "Удалить сплайн";
            this.DeleteLayersMainPaintToolStripMenuItem.Click += new System.EventHandler(this.DeleteLayersMainPaintToolStripMenuItem_Click);
            // 
            // ToolTip
            // 
            this.ToolTip.AutoPopDelay = 5000;
            this.ToolTip.InitialDelay = 500;
            this.ToolTip.ReshowDelay = 100;
            // 
            // ContextMenuDataGridView
            // 
            this.ContextMenuDataGridView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MaterialSplineDataGridViewToolStripMenuItem,
            this.ChangeColorDataGridViewToolStripMenuItem,
            this.DeleteSplineDataGridViewToolStripMenuItem});
            this.ContextMenuDataGridView.Name = "СontextMenuStrip";
            this.ContextMenuDataGridView.Size = new System.Drawing.Size(167, 70);
            // 
            // MaterialSplineDataGridViewToolStripMenuItem
            // 
            this.MaterialSplineDataGridViewToolStripMenuItem.Name = "MaterialSplineDataGridViewToolStripMenuItem";
            this.MaterialSplineDataGridViewToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.MaterialSplineDataGridViewToolStripMenuItem.Text = "Задать материал";
            this.MaterialSplineDataGridViewToolStripMenuItem.Click += new System.EventHandler(this.MaterialSplineDataGridViewToolStripMenuItem_Click);
            // 
            // ChangeColorDataGridViewToolStripMenuItem
            // 
            this.ChangeColorDataGridViewToolStripMenuItem.Name = "ChangeColorDataGridViewToolStripMenuItem";
            this.ChangeColorDataGridViewToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.ChangeColorDataGridViewToolStripMenuItem.Text = "Изменить цвет";
            this.ChangeColorDataGridViewToolStripMenuItem.Click += new System.EventHandler(this.ChangeColorDataGridViewToolStripMenuItem_Click);
            // 
            // DeleteSplineDataGridViewToolStripMenuItem
            // 
            this.DeleteSplineDataGridViewToolStripMenuItem.Name = "DeleteSplineDataGridViewToolStripMenuItem";
            this.DeleteSplineDataGridViewToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.DeleteSplineDataGridViewToolStripMenuItem.Text = "Удалить";
            this.DeleteSplineDataGridViewToolStripMenuItem.Click += new System.EventHandler(this.DeleteSplineDataGridViewToolStripMenuItem_Click);
            // 
            // GroupBox
            // 
            this.GroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox.Controls.Add(this.TextBoxYCoordinate);
            this.GroupBox.Controls.Add(this.TextBoxXCoordinate);
            this.GroupBox.Controls.Add(this.LabelYCoordiante);
            this.GroupBox.Controls.Add(this.LabelXCoordinate);
            this.GroupBox.Location = new System.Drawing.Point(618, 32);
            this.GroupBox.Name = "GroupBox";
            this.GroupBox.Size = new System.Drawing.Size(254, 69);
            this.GroupBox.TabIndex = 11;
            this.GroupBox.TabStop = false;
            this.GroupBox.Text = "Координаты";
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
            // UnFocus
            // 
            this.UnFocus.Location = new System.Drawing.Point(792, 13);
            this.UnFocus.Name = "UnFocus";
            this.UnFocus.Size = new System.Drawing.Size(0, 0);
            this.UnFocus.TabIndex = 12;
            this.UnFocus.Text = "UnFocus";
            this.UnFocus.UseVisualStyleBackColor = true;
            // 
            // OpenFileDialog
            // 
            this.OpenFileDialog.FileName = "OpenFileDialog";
            // 
            // SaveSceneToolStripMenuItem
            // 
            this.SaveSceneToolStripMenuItem.Name = "SaveSceneToolStripMenuItem";
            this.SaveSceneToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.SaveSceneToolStripMenuItem.Text = "Сохранить";
            this.SaveSceneToolStripMenuItem.Click += new System.EventHandler(this.SaveSceneToolStripMenuItem_Click);
            // 
            // LoadSceneToolStripMenuItem
            // 
            this.LoadSceneToolStripMenuItem.Name = "LoadSceneToolStripMenuItem";
            this.LoadSceneToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.LoadSceneToolStripMenuItem.Text = "Загрузить";
            this.LoadSceneToolStripMenuItem.Click += new System.EventHandler(this.LoadSceneToolStripMenuItem_Click);
            // 
            // NumberLayer
            // 
            this.NumberLayer.HeaderText = "№";
            this.NumberLayer.Name = "NumberLayer";
            this.NumberLayer.ReadOnly = true;
            this.NumberLayer.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.NumberLayer.ToolTipText = "Порядковый номер слоя.";
            this.NumberLayer.Width = 24;
            // 
            // ColorLayer
            // 
            this.ColorLayer.HeaderText = "Цвет";
            this.ColorLayer.Name = "ColorLayer";
            this.ColorLayer.ReadOnly = true;
            this.ColorLayer.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ColorLayer.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColorLayer.ToolTipText = "Цвет слоя.";
            this.ColorLayer.Width = 32;
            // 
            // HeightLayer
            // 
            this.HeightLayer.HeaderText = "Глубина";
            this.HeightLayer.MaxInputLength = 6;
            this.HeightLayer.Name = "HeightLayer";
            this.HeightLayer.ReadOnly = true;
            this.HeightLayer.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.HeightLayer.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.HeightLayer.ToolTipText = "Глубина на которой находится слой.";
            this.HeightLayer.Width = 104;
            // 
            // Material
            // 
            this.Material.HeaderText = "Материал";
            this.Material.MaxInputLength = 6;
            this.Material.Name = "Material";
            this.Material.ReadOnly = true;
            this.Material.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Material.ToolTipText = "Материал слоя.";
            this.Material.Width = 84;
            // 
            // MainPaint_HScroll
            // 
            this.MainPaint_HScroll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.MainPaint_HScroll.BorderColor = System.Drawing.Color.Silver;
            this.MainPaint_HScroll.LargeChange = 588D;
            this.MainPaint_HScroll.Location = new System.Drawing.Point(12, 561);
            this.MainPaint_HScroll.Maximum = 587D;
            this.MainPaint_HScroll.Name = "MainPaint_HScroll";
            this.MainPaint_HScroll.Orientation = System.Windows.Forms.ScrollOrientation.HorizontalScroll;
            this.MainPaint_HScroll.Size = new System.Drawing.Size(587, 12);
            this.MainPaint_HScroll.SmallStep = 1D;
            this.MainPaint_HScroll.TabIndex = 2;
            this.MainPaint_HScroll.ThumbColor = System.Drawing.Color.Gray;
            this.MainPaint_HScroll.ThumbSize = 10;
            this.MainPaint_HScroll.Value = 0D;
            this.MainPaint_HScroll.Visible = false;
            this.MainPaint_HScroll.ValueChanged += new System.EventHandler(this.MainPaint_HScroll_ValueChanged);
            // 
            // MainPaint_VScroll
            // 
            this.MainPaint_VScroll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MainPaint_VScroll.BorderColor = System.Drawing.Color.Silver;
            this.MainPaint_VScroll.LargeChange = 527D;
            this.MainPaint_VScroll.Location = new System.Drawing.Point(602, 32);
            this.MainPaint_VScroll.Maximum = 526D;
            this.MainPaint_VScroll.Name = "MainPaint_VScroll";
            this.MainPaint_VScroll.Orientation = System.Windows.Forms.ScrollOrientation.VerticalScroll;
            this.MainPaint_VScroll.Size = new System.Drawing.Size(12, 526);
            this.MainPaint_VScroll.SmallStep = 1D;
            this.MainPaint_VScroll.TabIndex = 1;
            this.MainPaint_VScroll.ThumbColor = System.Drawing.Color.Gray;
            this.MainPaint_VScroll.ThumbSize = 10;
            this.MainPaint_VScroll.Value = 0D;
            this.MainPaint_VScroll.Visible = false;
            this.MainPaint_VScroll.ValueChanged += new System.EventHandler(this.MainPaint_VScroll_ValueChanged);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "№";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn1.ToolTipText = "Порядковый номер слоя.";
            this.dataGridViewTextBoxColumn1.Width = 24;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewTextBoxColumn2.HeaderText = "Цвет";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn2.ToolTipText = "Цвет минерала.";
            this.dataGridViewTextBoxColumn2.Width = 32;
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewTextBoxColumn3.HeaderText = "Глубина";
            this.dataGridViewTextBoxColumn3.MaxInputLength = 6;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn3.ToolTipText = "Глубина на которой находятся отложения.";
            this.dataGridViewTextBoxColumn3.Width = 104;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Материал";
            this.dataGridViewTextBoxColumn4.MaxInputLength = 6;
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn4.ToolTipText = "Материал отложений.";
            this.dataGridViewTextBoxColumn4.Width = 84;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 582);
            this.Controls.Add(this.UnFocus);
            this.Controls.Add(this.GroupBox);
            this.Controls.Add(this.TabControl);
            this.Controls.Add(this.MainPaint_HScroll);
            this.Controls.Add(this.MainPaint_VScroll);
            this.Controls.Add(this.MainPaint);
            this.Controls.Add(this.MenuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
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
            this.TabPageMKE.ResumeLayout(false);
            this.TabPageMKE.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxColorPartition)).EndInit();
            this.ContextMenuMaterials.ResumeLayout(false);
            this.СontextMenuMainPaint.ResumeLayout(false);
            this.ContextMenuDataGridView.ResumeLayout(false);
            this.GroupBox.ResumeLayout(false);
            this.GroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Tao.Platform.Windows.SimpleOpenGlControl MainPaint;
        private System.Windows.Forms.Timer RenderTimer;
        private MyScroll MainPaint_VScroll;
        private MyScroll MainPaint_HScroll;
        private System.Windows.Forms.MenuStrip MenuStrip;
        private System.Windows.Forms.ToolStripMenuItem FileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CloseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AboutProgrammToolStripMenuItem;
        private System.Windows.Forms.TabControl TabControl;
        private System.Windows.Forms.TabPage TabPageSpline;
        private System.Windows.Forms.TabPage TabPageMKE;
        private System.Windows.Forms.ContextMenuStrip СontextMenuMainPaint;
        private System.Windows.Forms.ToolStripMenuItem DeleteLayersMainPaintToolStripMenuItem;
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
        private System.Windows.Forms.CheckedListBox СheckedListBoxSpline;
        private System.Windows.Forms.ComboBox ComboBoxMineralMaterial;
        private System.Windows.Forms.ContextMenuStrip ContextMenuDataGridView;
        private System.Windows.Forms.ToolStripMenuItem DeleteSplineDataGridViewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ChangeValueToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AddPointToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DeletePointToolStripMenuItem;
        private System.Windows.Forms.TabPage TabPageSettings;
        private System.Windows.Forms.GroupBox GroupBox;
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
        private System.Windows.Forms.TextBox TextBoxChangeXMoveSpline;
        private System.Windows.Forms.Label LabelChangeXMoveSpline;
        private System.Windows.Forms.Button ButtonChangeXMoveSpline;
        private System.Windows.Forms.ComboBox СomboBoxLayerMaterial;
        private System.Windows.Forms.Label LabelLayerMaterial;
        private System.Windows.Forms.ContextMenuStrip ContextMenuMaterials;
        private System.Windows.Forms.ToolStripMenuItem AddMaterialToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DeleteMaterialToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ChangeColorToolStripMenuItem;
        private System.Windows.Forms.ColorDialog ColorDialog;
        private System.Windows.Forms.Button UnFocus;
        private System.Windows.Forms.ToolStripMenuItem ChangeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ChangeMaterialToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MaterialSplineDataGridViewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ChangeColorDataGridViewToolStripMenuItem;
        private System.Windows.Forms.Label LabelStepPartition;
        private System.Windows.Forms.TextBox TextBoxStepPartition;
        private System.Windows.Forms.Label LabelColorPartition;
        private System.Windows.Forms.Button ButtonSavePartition;
        private System.Windows.Forms.PictureBox PictureBoxColorPartition;
        private System.Windows.Forms.ToolStripMenuItem AddFromFileToolStripMenuItem;
        private System.Windows.Forms.CheckedListBox СheckedListBoxMKE;
        private System.Windows.Forms.Button ButtonMakePartition;
        private System.Windows.Forms.OpenFileDialog OpenFileDialog;
        private System.Windows.Forms.SaveFileDialog SaveFileDialog;
        private System.Windows.Forms.ToolStripMenuItem LoadSceneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SaveSceneToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn NumberLayer;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColorLayer;
        private System.Windows.Forms.DataGridViewTextBoxColumn HeightLayer;
        private System.Windows.Forms.DataGridViewTextBoxColumn Material;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
    }
}

