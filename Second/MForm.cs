using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

using Tao.OpenGl;           // для работы с библиотекой OpenGL 
using Tao.FreeGlut;         // для работы с библиотекой FreeGLUT 



namespace Second
{
    public partial class MainForm : Form
    {
        #region Поля класса
        /// <summary>
        /// Переменная для отисовки.
        /// </summary>
        private Paint Draw;
        /// <summary>
        /// Находится ли мышь на OpenGlControl.
        /// </summary>
        private bool FlagMouseGlControl;
        /// <summary>
        /// Нажата ли левая кнопка мыши.
        /// </summary>
        private bool MouseDownLeft;
        /// <summary>
        /// Позиция нажатия мыши.
        /// </summary>
        private Point MouseDownPosition;
        /// <summary>
        /// Массив [0] - 0(нету),1(слой),2(минерал); 
        ///        [1] - номер слоя\минерала;
        ///        [2] - номер опорной точки.
        /// </summary>
        private int[] CheckControlPoint;
        /// <summary>
        /// Нажата ли кнопка "Нарисовать" слой.
        /// </summary>
        private bool DrawLayers;
        /// <summary>
        /// Нажата ли кнопка "Нарисовать" минерал.
        /// </summary>
        private bool DrawMineral;
        /// <summary>
        /// Нажата ли кнопка "Добавить точку" в контексном меню.
        /// </summary>
        private bool FlagAddPoint;
        /// <summary>
        /// Массив материалов слоя.
        /// </summary>
        private List<Material> MaterialLayer;
        /// <summary>
        /// Массив материалов минералов.
        /// </summary>
        private List<Material> MaterialMineral;
        /// <summary>
        /// Для работы с файлйами.
        /// </summary>
        private WorkingWithFiles WFiles;
        #endregion

        #region Меню
        private void CloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void LoadSceneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog Files = new OpenFileDialog();
            if (Files.ShowDialog() == DialogResult.OK)
            {
                Draw.HardReset();
             
                WFiles.InputScene(Files.FileName, Draw, out MaterialLayer, out MaterialMineral);
                /*Делаем интерфейс активным*/
                ChangeEnabled();
                /*Записываем данные в настройки*/
                TextBoxAccuracy.Text = GlobalConst.Accuracy.ToString();
                TextBoxXAreaSize.Text = Draw.XAREASIZE.ToString();
                TextBoxYAreaSize.Text = Draw.YAREASIZE.ToString();
                TextBoxEarthSize.Text = Draw.EARTHSIZE.ToString();
                /*Изменяем скроллбары*/
                ChangeScrollBars();
                /*Записываем данные в таблицы*/
                ChangeDataGridViewLayers();
                ChangeDataGridViewMinerals();
                /*Записываем данные в выпадающие списки*/
                int i;
                СomboBoxLayerMaterial.Items.Clear();
                ComboBoxMineralMaterial.Items.Clear();
                for (i = 0; i < MaterialLayer.Count; i++)
                    СomboBoxLayerMaterial.Items.Add(MaterialLayer[i].NAME);
                СomboBoxLayerMaterial.SelectedIndex = 0;
                for (i = 0; i < MaterialMineral.Count; i++)
                    ComboBoxMineralMaterial.Items.Add(MaterialMineral[i].NAME);
                ComboBoxMineralMaterial.SelectedIndex = 0;
                /*Запускаем отрисовку*/
                RenderTimer.Start();
            }
        }

        private void SaveSceneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*Открываем диалог*/
            SaveFileDialog Files = new SaveFileDialog();
            /*Если все окей,то*/
            if (Files.ShowDialog() == DialogResult.OK)
            {
                WFiles.OutputScene(Files.FileName, Draw, MaterialLayer, MaterialMineral);
            }
        }

        private void AboutProgrammToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        #endregion

        public MainForm()
        {
            /*Инициализация компонент*/
            InitializeComponent();
            MainPaint.InitializeContexts();
            /*Создаем объект класса отрисовки*/
            Draw = new Paint(MainPaint);
            WFiles = new WorkingWithFiles();
            FlagMouseGlControl = false;
            MouseDownLeft = false;
            DrawLayers = false;
            DrawMineral = false;
            CheckControlPoint = new int[3];
            FlagAddPoint = false;
            GlobalConst.Difference = 5;
            GlobalConst.Accuracy = -1;
            /*Создаем и заполняем материалы*/
            MaterialLayer = new List<Material>();
            MaterialLayer.Add(new Material("Земля", 0));
            MaterialLayer.Add(new Material("Грунт", 1));
            MaterialLayer.Add(new Material("Грязь", 2));
            for (int i = 0; i < MaterialLayer.Count; i++)
                СomboBoxLayerMaterial.Items.Add(MaterialLayer[i].NAME);
            СomboBoxLayerMaterial.SelectedIndex = 0;
            MaterialMineral = new List<Material>();
            MaterialMineral.Add(new Material("Земля", 0));
            MaterialMineral.Add(new Material("Грунт", 1));
            MaterialMineral.Add(new Material("Грязь", 2));
            for (int i = 0; i < MaterialLayer.Count; i++)
                ComboBoxMineralMaterial.Items.Add(MaterialMineral[i].NAME);
            ComboBoxMineralMaterial.SelectedIndex = 0;
        }
        private void MainPaint_Load(object sender, EventArgs e)
        {
            /*устанавливаем положение плоскости отображения*/
            Gl.glViewport(0, 0, MainPaint.Width, MainPaint.Height);
            /*загружаем проекционную матрицу*/
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            /*делаем матрицу трансформации единичной*/
            Gl.glLoadIdentity();
            /*масштабируем*/
            Gl.glOrtho(-MainPaint.Width / 2.0, MainPaint.Width / 2.0, -MainPaint.Height / 2.0, MainPaint.Height / 2.0, -1, 1);
            /*загружаем объектно-видовую матрицу*/
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            /*инициализация openglut*/
            Glut.glutInit();
            /*включаем режими opengl*/
            Glut.glutInitDisplayMode(Glut.GLUT_RGB | Glut.GLUT_DOUBLE | Glut.GLUT_DEPTH);
            /*включаем смешение цветов*/
            Gl.glEnable(Gl.GL_BLEND);
            Gl.glEnable(Gl.GL_ALPHA_TEST);
            Gl.glBlendFunc(Gl.GL_SRC_COLOR, Gl.GL_ONE_MINUS_SRC_ALPHA);
            /*очищаем экран*/
            Gl.glClearColor(1.0f, 1.0f, 1.0f, 0.0f);
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
        }
        private void RenderTimer_Tick(object sender, EventArgs e)
        {
            double a = Math.Pow(10, -GlobalConst.Accuracy);
            if (MainPaint_VScroll.Maximum <= 2 * a)
                MainPaint_VScroll.Visible = false;
            else MainPaint_VScroll.Visible = true;
            
            if (MainPaint_HScroll.Maximum <= 2 * a)
                MainPaint_HScroll.Visible = false;
            else MainPaint_HScroll.Visible = true;
            Draw.Draw();
        }

        #region Изменение объектов интерфейса
        /*Изменяем доступ к меню*/
        private void ChangeEnabled()
        {
            /*Делаем активным окно с сеткой и поля с кнопками*/
            MainPaint.Enabled = true;
            TextBoxXAreaSize.Enabled = true;
            TextBoxEarthSize.Enabled = true;
            TextBoxYAreaSize.Enabled = true;
            DrawSplineLayers.Enabled = true;
            AddSplineLayers.Enabled = true;
            DrawSplineMinerals.Enabled = true;
            СheckedListBoxSpline.Enabled = true;
            СheckedListBoxSettings.Enabled = true;
            TextBoxChangeXMoveSpline.Enabled = true;
            ButtonChangeXMoveSpline.Enabled = true;
            TextBoxLayerNumberOfPoints.Enabled = true;
            TextBoxLayerHeight.Enabled = true;
            TextBoxStepPartition.Enabled = true;
            PictureBoxColorPartition.Enabled = true;
            СheckedListBoxMKE.Enabled = true;
            ButtonMakePartition.Enabled = true;
            ButtonSavePartition.Enabled = true;
            /*Разрешаем менять окно*/
            this.MaximumSize = SystemInformation.PrimaryMonitorSize;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            /*Меняем точность в скроллбарах*/
            MainPaint_VScroll.SmallStep = Math.Pow(10, -GlobalConst.Accuracy);
            MainPaint_HScroll.SmallStep = Math.Pow(10, -GlobalConst.Accuracy);
        }
        /*Изменяем ползунки*/
        private void ChangeScrollBars()
        {
            MainPaint_VScroll.LargeChange = Draw.YAREASIZE;
            MainPaint_VScroll.Maximum = Draw.ScrollMaximum(0);
            MainPaint_VScroll.Value = Draw.ScrollValue(0);
            MainPaint_HScroll.LargeChange = Draw.XAREASIZE;
            MainPaint_HScroll.Maximum = Draw.ScrollMaximum(1);
            MainPaint_HScroll.Value = Draw.ScrollValue(1);
        }
        /*Изменение таблицы слоев*/
        private void ChangeDataGridViewLayers()
        {
            int i;
            Color color = new Color();
            string LayerPosition = "";
            string Material;
            for (i = 0; i < DataGridViewLayers.Rows.Count - 1; i++)
            {
                Draw.GetSplineInformation(0, i, out color, out LayerPosition, out Material);
                DataGridViewLayers.Rows[i].Cells[0].Value = i + 1;
                DataGridViewLayers.Rows[i].Cells[1].Style.BackColor = color;
                DataGridViewLayers.Rows[i].Cells[2].Value = LayerPosition;
                DataGridViewLayers.Rows[i].Cells[3].Value = Material;
            }
        }
        /*Изменение таблицы минералов*/
        private void ChangeDataGridViewMinerals()
        {
            int i;
            Color color = new Color();
            string LayerPosition = "";
            string Material;
            for (i = 0; i < DataGridViewMinerals.Rows.Count - 1; i++)
            {
                Draw.GetSplineInformation(1, i, out color, out LayerPosition, out Material);
                DataGridViewMinerals.Rows[i].Cells[0].Value = i + 1;
                DataGridViewMinerals.Rows[i].Cells[1].Style.BackColor = color;
                DataGridViewMinerals.Rows[i].Cells[2].Value = LayerPosition;
                DataGridViewMinerals.Rows[i].Cells[3].Value = Material;
            }
        }
        /*Изменение контексного меню, вкладки Материалы*/
        private void ChangeMaterialsToolStripMenuItem(ToolStripMenuItem MenuItem)
        {
            int i;
            Material Material;
            ToolStripMenuItem AddItem;
            MenuItem.DropDownItems.Clear();
            if (CheckControlPoint[0]==1)
            {               
                Draw.GetSplineMaterial(CheckControlPoint,out Material);
                for (i=0;i< MaterialLayer.Count;i++)
                {
                    AddItem = new ToolStripMenuItem(MaterialLayer[i].NAME);
                    if (MenuItem == ChangeMaterialToolStripMenuItem)
                        AddItem.Click += new EventHandler(ChangeMaterialToolStripMenuItem_Click);
                    else
                        AddItem.Click += new EventHandler(MaterialSplineDataGridViewToolStripMenuItem_Click);
                    if (Material.NAME == MaterialLayer[i].NAME)
                        AddItem.Checked = true;
                    MenuItem.DropDownItems.Add(AddItem);
                }
            }
            else
            {
                Draw.GetSplineMaterial(CheckControlPoint, out Material);
                for (i = 0; i < MaterialMineral.Count; i++)
                {
                    AddItem = new ToolStripMenuItem(MaterialMineral[i].NAME);
                    AddItem.Name = MenuItem.Name;
                    if (MenuItem == ChangeMaterialToolStripMenuItem)
                        AddItem.Click += new EventHandler(ChangeMaterialToolStripMenuItem_Click);
                    else
                        AddItem.Click += new EventHandler(MaterialSplineDataGridViewToolStripMenuItem_Click);
                    if (Material.NAME == MaterialLayer[i].NAME)
                        AddItem.Checked = true;
                    MenuItem.DropDownItems.Add(AddItem);
                }
            }
        }
        #endregion

        #region Работа с MainPaint
        #region Контексное меню
        private void ChangeValueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetValue Set = new SetValue();
            Set.ShowDialog();
            if (GlobalConst.Buffer[0] != "" && GlobalConst.Buffer[1] != "")
            {
                double X = Math.Round(Convert.ToDouble(GlobalConst.Buffer[0]));
                double Y = Math.Round(Convert.ToDouble(GlobalConst.Buffer[1]));
                if (X > Draw.XAREASIZE)
                    MessageBox.Show("Абсцисса точки должна быть меньше "+X.ToString());
                if (Y < -1000000 + Draw.EARTHSIZE)
                    MessageBox.Show("Ордината точки должна быть меньше " + (-1000000 + Draw.EARTHSIZE).ToString());
                else
                {
                    Draw.ChangePoint(CheckControlPoint, new PointSpline(GlobalConst.Buffer[0], GlobalConst.Buffer[1]));
                    if (CheckControlPoint[0] == 1)
                        ChangeDataGridViewLayers();
                    else
                        ChangeDataGridViewMinerals();
                }
            }
        }
        private void ChangeMaterialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int i = 0;
            foreach (ToolStripMenuItem Item in ChangeMaterialToolStripMenuItem.DropDownItems)
            {
                if ((ToolStripMenuItem)sender == Item)
                {
                    if (CheckControlPoint[0] == 1)
                    {
                        Draw.SetSplineMaterial(CheckControlPoint, MaterialLayer[i]);
                        ChangeDataGridViewLayers();
                    }
                    else
                    {
                        Draw.SetSplineMaterial(CheckControlPoint, MaterialMineral[i]);
                        ChangeDataGridViewMinerals();
                    }
                }
                i++;
            }
        }
        private void ChangeColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog.ShowDialog();
            if (!Draw.SetSplineColor(ColorDialog.Color, CheckControlPoint))
                MessageBox.Show("Цвет уже занят");
            else
            {
                if (CheckControlPoint[0] == 1)
                    ChangeDataGridViewLayers();
                else
                    ChangeDataGridViewMinerals();
            }
        }
        private void AddPointToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FlagAddPoint = true;
        }
        private void DeletePointToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Draw.DeletePoint(CheckControlPoint))
                MessageBox.Show("Нельзя удалить точку.");
        }
        private void DeleteLayersMainPaintToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*Старое значение максимального значения ползунка*/
            double MaximumScroll = MainPaint_VScroll.Maximum;
            /*Удаляем слой*/
            if (Draw.DeleteSpline(CheckControlPoint))
            {
                if (CheckControlPoint[0] == 1)
                {
                    DataGridViewLayers.Rows.RemoveAt(0);
                    ChangeDataGridViewLayers();
                }
                if (CheckControlPoint[0] == 2 && DataGridViewMinerals.Rows.Count > 0)
                {
                    DataGridViewMinerals.Rows.RemoveAt(0);
                    ChangeDataGridViewMinerals();
                }
            }
            /*Изменяем ползунки*/
            MainPaint_VScroll.LargeChange = Convert.ToInt32(Draw.YAREASIZE + 1);            
            MainPaint_VScroll.Maximum = Draw.ScrollMaximum(0);
            Draw.YOFFSET += (MaximumScroll - MainPaint_VScroll.Maximum ) * Draw.ZOOM;
            MainPaint_VScroll.Value = Draw.ScrollValue(0);
        }
        #endregion
        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState != FormWindowState.Minimized && MainPaint.Enabled == true)
            {
                /*Сбрасываем сдвиги по осям*/
                Draw.XOFFSET = 0.0;
                Draw.YOFFSET = 0.0;
                /*Изменяем размеры GlControl*/
                MainPaint.Width = this.Width - this.MinimumSize.Width + MainPaint.MinimumSize.Width;
                MainPaint.Height = this.Height - this.MinimumSize.Height + MainPaint.MinimumSize.Height;
                /*Настраиваем зум*/
                if (Convert.ToDouble(MainPaint.Width) / Convert.ToDouble(MainPaint.MinimumSize.Width)
                    > Convert.ToDouble(MainPaint.Height) / Convert.ToDouble(MainPaint.MinimumSize.Height))
                {
                    Draw.MINZOOM = GlobalConst.MinZoom * Convert.ToDouble(MainPaint.Width - GlobalConst.Difference)
                        / Convert.ToDouble(MainPaint.MinimumSize.Width - GlobalConst.Difference);
                    MainPaint_VScroll.Maximum = Draw.ScrollMaximum(0);
                    MainPaint_HScroll.Maximum = Draw.XAREASIZE;
                }
                else
                {
                    Draw.MINZOOM = GlobalConst.MinZoom * Convert.ToDouble(MainPaint.Height - GlobalConst.Difference)
                        / Convert.ToDouble(MainPaint.MinimumSize.Height - GlobalConst.Difference);
                    MainPaint_HScroll.Maximum = Draw.ScrollMaximum(1);
                    MainPaint_VScroll.Maximum = Draw.YAREASIZE;
                }
                /*Настраиваем вертикальный ползунок*/
                MainPaint_VScroll.Size = new Size(MainPaint_VScroll.Size.Width, MainPaint.Height);
                MainPaint_VScroll.LargeChange = Draw.YAREASIZE;
                MainPaint_VScroll.Value = Draw.ScrollValue(0);
                /*Настраиваем горизонтальный ползунок*/
                MainPaint_HScroll.Size = new Size(MainPaint.Width, MainPaint_HScroll.Size.Height);
                MainPaint_HScroll.LargeChange = Draw.XAREASIZE;
                MainPaint_HScroll.Value = Draw.ScrollValue(1);
                /*Настраиваем панель с параметрами*/
                TabControl.Size = new Size(TabControl.Size.Width, MainPaint.Height);
                /*Настраиваем отображение "нового" окна для функций Gl*/
                Gl.glViewport(0, 0, MainPaint.Width, MainPaint.Height);
                Gl.glMatrixMode(Gl.GL_PROJECTION);
                Gl.glLoadIdentity();
                Gl.glOrtho(-MainPaint.Width / 2.0, MainPaint.Width / 2.0, -MainPaint.Height / 2.0, MainPaint.Height / 2.0, -1, 1);
                Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
            }
        }
        private void MainPaint_MouseEnter(object sender, EventArgs e)
        {
            MainPaint.Focus();
        }
        private void MainPaint_MouseLeave(object sender, EventArgs e)
        {
            UnFocus.Focus();
        }
        private void MainPaint_VScroll_ValueChanged(object sender, EventArgs e)
        {
            var v = (sender as MyScroll).Value;
            Draw.YOFFSET = (v - MainPaint_VScroll.Maximum / 2) * Draw.ZOOM;
        }
        private void MainPaint_HScroll_ValueChanged(object sender, EventArgs e)
        {
            var v = (sender as MyScroll).Value;
            Draw.XOFFSET = (MainPaint_HScroll.Maximum / 2 - v) * Draw.ZOOM;
        }
        private void MainPaint_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            /*Проверяем находится ли мышь на рабочей области*/
            if (FlagMouseGlControl)
            {
                /*Масштабирование*/
                if (e.Delta > 0)
                    /*Изменяем смещение по осям с фиксированием точки приближения*/
                    Draw.ChangeOffsetZoomIn();
                else
                    /*Изменяем смещение по осям с фиксированием точки приближения*/
                    Draw.ChangeOffsetZoomOut();
                /*Выводим координаты*/
                TextBoxXCoordinate.Text = Draw.GetCoordinate(0).ToString();
                TextBoxYCoordinate.Text = Draw.GetCoordinate(1).ToString();
                /*Изменяем ползунки*/
                ChangeScrollBars();
            }
        }
        private void MainPaint_MouseMove(object sender, MouseEventArgs e)
        {
            /*Проверяем находится ли мышь на OpenGlControl*/
            if (e.Y < 0 || e.Y > MainPaint.Height - GlobalConst.Difference || e.X < 0 || e.X > MainPaint.Width - GlobalConst.Difference)
                FlagMouseGlControl = false;
            else
                FlagMouseGlControl = true;
            if (FlagMouseGlControl)
            {
                /*Изменяем позицию мышки*/
                Draw.MOUSEPOSITION = new Point(e.X, e.Y);
                /*Выводим координаты*/
                TextBoxXCoordinate.Text = Draw.GetCoordinate(0).ToString();
                TextBoxYCoordinate.Text = Draw.GetCoordinate(1).ToString();
                /*Если нажата кнопка нарисовать слой или минерал (нельзя двигать)*/
                if (DrawLayers == true || DrawMineral == true)
                    return;
                if (MouseDownLeft)
                {
                    if (CheckControlPoint[0] != 0)
                    {
                        Draw.ChangePoint(CheckControlPoint, new PointSpline(Draw.GetCoordinate(0), Draw.GetCoordinate(1)));
                        TextBoxYAreaSize.Text = Draw.YAREASIZE.ToString();
                        MainPaint_VScroll.LargeChange = Draw.YAREASIZE;
                        MainPaint_VScroll.Maximum = Draw.ScrollMaximum(0);
                        MainPaint_VScroll.Value = Draw.ScrollValue(0);
                        MainPaint_HScroll.LargeChange = Draw.XAREASIZE;
                        MainPaint_HScroll.Maximum = Draw.ScrollMaximum(1);
                        MainPaint_HScroll.Value = Draw.ScrollValue(1);
                        if (CheckControlPoint[0] == 1)
                            ChangeDataGridViewLayers();
                        else
                            ChangeDataGridViewMinerals();
                    }
                    else
                    {
                        /*Изменяем смещение по осям с фиксированием точки*/
                        Draw.ChangeOffsetZoomMouse(MouseDownPosition);
                        /*Изменяем ползунки*/
                        MainPaint_VScroll.Value = Draw.ScrollValue(0);
                        MainPaint_HScroll.Value = Draw.ScrollValue(1);
                        MouseDownPosition = new Point(e.X, e.Y);
                    }
                }
            }
        }
        private void MainPaint_MouseUp(object sender, MouseEventArgs e)
        {
            MouseDownLeft = false;
        }
        private void MainPaint_MouseDown(object sender, MouseEventArgs e)
        {
            /*Убираем выделение в таблицах*/
            DataGridViewLayers.ClearSelection();
            DataGridViewMinerals.ClearSelection();
            /*Если нажата левая кнопка мыши*/
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                MouseDownLeft = true;
                MouseDownPosition = new Point(e.X, e.Y);
                Draw.MOUSEPOSITION = new Point(e.X, e.Y);
                /*Если выбранно "Добавить точку" в контексном меню слоя*/
                if (FlagAddPoint)
                {
                    if(!Draw.AddPoint(CheckControlPoint))
                        MessageBox.Show("Нельзя добавить точку.");
                    FlagAddPoint = false;
                    MouseDownLeft = false;
                    return;
                }
                /*Если кнопка "Нарисовать" новый слой "включена"*/
                if (DrawLayers)
                {
                    if (Draw.AddLayers(Draw.GetCoordinate(1), Convert.ToInt32(TextBoxLayerNumberOfPoints.Text), MaterialLayer[СomboBoxLayerMaterial.SelectedIndex]))
                    {
                        /*Изменяем высоту в настройках*/
                        TextBoxYAreaSize.Text = Draw.GetMaxPointLayers().ToString();
                        /*Изменяем ползунки*/
                        MainPaint_VScroll.LargeChange = Convert.ToInt32(Draw.YAREASIZE + 1);
                        MainPaint_VScroll.Maximum = Draw.ScrollMaximum(0);
                        MainPaint_VScroll.Value = Draw.ScrollValue(0);
                        /*Заполняем таблицу*/
                        DataGridViewLayers.Rows.Add(0, "", 0, 0);
                        ChangeDataGridViewLayers();
                        MouseDownLeft = false;
                        return;
                    }
                    else
                        MessageBox.Show("Нельзя добавить слой.");
                }
                /*Если кнопка "Нарисовать" минерал "включена"*/
                if (DrawMineral)
                {
                    if (!Draw.AddPointMinerals())
                        MessageBox.Show("Точка должна находится на слое");
                }
            }
            /*Проверяем попали ли мы на опорную точку*/
            if (!Draw.CheckPoint(new Point(e.X, e.Y), out CheckControlPoint))
                CheckControlPoint[0] = 0;
            /*Если попали и нажата правая кнопка мыши, то вызываем контексное меню*/
            if (CheckControlPoint[0] != 0 && e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                FlagAddPoint = false;
                ChangeMaterialsToolStripMenuItem(ChangeMaterialToolStripMenuItem);
                СontextMenuMainPaint.Show(Cursor.Position);
            }
        }
        #endregion

        #region TabControl
        #region Настройки
        private void TabPageSettings_MouseClick(object sender, MouseEventArgs e)
        {
            UnFocus.Focus();
        }

        private void TextBoxAccuracy_KeyPress(object sender, KeyPressEventArgs e)
        {
            /*Можно вводить только числа и бэкспейс*/
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 8)
                e.Handled = true;
            /*Если нажат энтер*/
            if (e.KeyChar == 13)
                UnFocus.Focus();
        }
        private void TextBoxAccuracy_Validating(object sender, CancelEventArgs e)
        {
            /*Если число меньше 16*/
            if (TextBoxAccuracy.TextLength > 0 && Convert.ToInt32(TextBoxAccuracy.Text) < 16)
                e.Cancel = false;
            else
            {
                /*Если до этого не было введена точность, то пустое ничего не вписываем*/
                if (GlobalConst.Accuracy == -1)
                    TextBoxAccuracy.Text = "";
                /*Если до этого была точность, то выводим ее*/
                else
                    TextBoxAccuracy.Text = GlobalConst.Accuracy.ToString();
                MessageBox.Show("Введите число от 0 до 15");
                return;
            }
        }
        private void TextBoxAccuracy_Validated(object sender, EventArgs e)
        {
            /*Если ввели данные и старая и новая точность не совпадают, то*/
            if (TextBoxAccuracy.TextLength > 0 && Convert.ToInt32(TextBoxAccuracy.Text) != GlobalConst.Accuracy)
            {
                /*Сохраняем точность*/
                GlobalConst.Accuracy = Convert.ToInt32(TextBoxAccuracy.Text);
                /*Если это самое начало программы, то выводим данные по умолчанию*/
                if (TextBoxXAreaSize.TextLength == 0)
                {
                    /*Минимальная ширина области - это 582*10^(-точность)*/
                    TextBoxXAreaSize.Text = (582.0 * Math.Pow(10,-GlobalConst.Accuracy)).ToString();
                    Draw.XAREASIZE = Convert.ToDouble(TextBoxXAreaSize.Text);
                    TextBoxYAreaSize.Text = Draw.YAREASIZE.ToString();
                    TextBoxEarthSize.Text = "0,0";
                    Draw.EARTHSIZE = Convert.ToDouble(TextBoxEarthSize.Text);
                    TextBoxChangeXMoveSpline.Text = "0,0";
                    /*Делаем интерфейс активным*/
                    ChangeEnabled();
                    /*Добавляем "Землю" 0 сплайн*/
                    Draw.AddLayers(0, 2, MaterialLayer[0]);
                    /*Изменяем ползунки*/
                    ChangeScrollBars();
                    /*Запускаем отрисовку*/
                    RenderTimer.Start();
                }
                else
                {
                    /*Меняем данные(точность этих данных)*/
                    Draw.XAREASIZE = Convert.ToDouble(TextBoxXAreaSize.Text);
                    Draw.EARTHSIZE = Convert.ToDouble(TextBoxEarthSize.Text);
                    /*Изменяем точность сплайнов*/
                    Draw.SetSplineAccuracy();
                    /*Меняем точность в скроллбарах*/
                    MainPaint_VScroll.SmallStep = Math.Pow(10, -GlobalConst.Accuracy);
                    MainPaint_HScroll.SmallStep = Math.Pow(10, -GlobalConst.Accuracy);
                    /*Если новое значение больше предыдущего то добавляем точку*/
                    if (Draw.XAREASIZE > Convert.ToDouble(TextBoxXAreaSize.Text))
                    {
                        Draw.XAREASIZE = Convert.ToDouble(TextBoxXAreaSize.Text);
                        Draw.AddLastPoint();
                    }
                    /*Если новое значение меньше предыдущего то убираем точки и добавляем 1 в конец*/
                    if (Draw.XAREASIZE < Convert.ToDouble(TextBoxXAreaSize.Text))
                    {
                        Draw.XAREASIZE = Convert.ToDouble(TextBoxXAreaSize.Text);
                        Draw.DeletePoints();
                    }
                    /*Выводим на форму данные*/
                    TextBoxXAreaSize.Text = Draw.XAREASIZE.ToString();
                    TextBoxYAreaSize.Text = Draw.YAREASIZE.ToString();
                    TextBoxEarthSize.Text = Draw.EARTHSIZE.ToString();
                    /*Сброс смещения и зума*/
                    Draw.ZOOM = Draw.MINZOOM;
                    Draw.XOFFSET = 0.0;
                    Draw.YOFFSET = 0.0;
                    /*Сброс смещений и зума*/
                    ChangeScrollBars();
                }
            }
        }

        private void TextBoxXAreaSize_KeyPress(object sender, KeyPressEventArgs e)
        {
            /*Можно вводить только числа, бэкспейс и запятая */
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 8
                && (e.KeyChar != 44 || TextBoxXAreaSize.Text.IndexOf(",") > -1))
                e.Handled = true;
            /*Если нажат энтер*/
            if (e.KeyChar == 13)
                UnFocus.Focus();
            /*Запятую первой ставить нельзя*/
            if (TextBoxXAreaSize.TextLength == 0 && e.KeyChar == 44)
                e.Handled = true;
        }
        private void TextBoxXAreaSize_Validating(object sender, CancelEventArgs e)
        {
            double Min = 582 * Math.Pow(10, -GlobalConst.Accuracy);
            /*Если число не введенно, ставим минимум*/
            if (TextBoxXAreaSize.TextLength == 0)
                TextBoxXAreaSize.Text = Min.ToString();
            /*Если введено число больше максимума, ставиммаксимум*/
            if (Convert.ToDouble(TextBoxXAreaSize.Text) > 1000000)
            {
                TextBoxXAreaSize.Text = "1000000";
                MessageBox.Show("Ширина области должна быть меньше 1000км");
                return;
            }
            /*Если число меньше минимума, то ставим минимум*/
            if (Convert.ToDouble(TextBoxXAreaSize.Text) < Min)
            {
                TextBoxXAreaSize.Text = Min.ToString();
                MessageBox.Show("Ширина области должна быть больше чем " + Min + " м");
                return;
            }
        }
        private void TextBoxXAreaSize_Validated(object sender, EventArgs e)
        {
            /*Если введенное число не совпадает с заданной точностью, то*/
            if (Draw.XAREASIZE != Math.Round(Convert.ToDouble(TextBoxXAreaSize.Text),GlobalConst.Accuracy))
            {
                /*Если старое число меньше введенного. добавляем точку в конец*/
                if (Draw.XAREASIZE < Convert.ToDouble(TextBoxXAreaSize.Text))
                {
                    Draw.XAREASIZE = Convert.ToDouble(TextBoxXAreaSize.Text);
                    Draw.AddLastPoint();
                }
                /*Если старое число больше введенного, удаляем точки и добавляем 1 в конец*/
                else
                {
                    Draw.XAREASIZE = Convert.ToDouble(TextBoxXAreaSize.Text);
                    Draw.DeletePoints();
                }
            }
            /*Записываем данные*/
            TextBoxXAreaSize.Text = Draw.XAREASIZE.ToString();
            TextBoxYAreaSize.Text = Draw.YAREASIZE.ToString();
            /*Изменяем ползунки*/
            ChangeScrollBars();
        }

        private void TextBoxEarthSize_KeyPress(object sender, KeyPressEventArgs e)
        {
            /*Можно вводить только числа, бэкспейс и запятая */
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 8
                && (e.KeyChar != 44 || TextBoxEarthSize.Text.IndexOf(",") > -1))
                e.Handled = true;
            /*Если нажат энтер*/
            if (e.KeyChar == 13)
                UnFocus.Focus();
            /*Запятую первой ставить нельзя*/
            if (TextBoxEarthSize.TextLength == 0 && e.KeyChar == 44)
                e.Handled = true;
        }
        private void TextBoxEarthSize_Validating(object sender, CancelEventArgs e)
        {
            /*Если число не введенно, то по умолчанию 0*/
            if (TextBoxEarthSize.TextLength == 0)
            {
                TextBoxEarthSize.Text = "0,0";
                return;
            }
            /*Если высота больше максимума, ставим максимум*/
            if (Convert.ToDouble(TextBoxEarthSize.Text) > 10000)
            {
                TextBoxXAreaSize.Text = "10000";
                MessageBox.Show("Высота над уровнем земли должна быть меньше 10км");
                return;
            }
        }
        private void TextBoxEarthSize_Validated(object sender, EventArgs e)
        {
            /*Если введенное число не совпадает с заданной точностью, то*/
            if (Draw.EARTHSIZE != Math.Round(Convert.ToDouble(TextBoxEarthSize.Text), GlobalConst.Accuracy))
            {
                /*Записываем данные*/
                Draw.EARTHSIZE = Convert.ToDouble(TextBoxEarthSize.Text);
                /*Проверяем меняется ли максимальная высота области(все ли слоя входят в область)*/
                TextBoxYAreaSize.Text = (Convert.ToDouble(TextBoxYAreaSize.Text) > Draw.GetMaxPointLayers())
                    ? Convert.ToDouble(TextBoxYAreaSize.Text).ToString()
                    : Draw.GetMaxPointLayers().ToString();
                Draw.YAREASIZE = Convert.ToDouble(TextBoxYAreaSize.Text);           
            }
            /*Записываем данные*/
            TextBoxEarthSize.Text = Draw.EARTHSIZE.ToString();
            /*Изменяем ползунки*/
            ChangeScrollBars();
        }

        private void TextBoxYAreaSize_KeyPress(object sender, KeyPressEventArgs e)
        {
            /*Можно вводить только числа, бэкспейс и запятая */
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 8
                && (e.KeyChar != 44 || TextBoxYAreaSize.Text.IndexOf(",") > -1))
                e.Handled = true;
            /*Если нажат энтер*/
            if (e.KeyChar == 13)
                UnFocus.Focus();
            /*Запятую первой ставить нельзя*/
            if (TextBoxYAreaSize.TextLength == 0 && e.KeyChar == 44)
                e.Handled = true;
        }
        private void TextBoxYAreaSize_Validating(object sender, CancelEventArgs e)
        {
            /*Если ничего не ввели то выводим самый минимум доступного*/
            if (TextBoxYAreaSize.TextLength == 0)
            {
                TextBoxYAreaSize.Text = Draw.GetMaxPointLayers().ToString();
                return;
            }
            /*Если ввели область больше максимального, то ставим максимум*/
            if (Convert.ToDouble(TextBoxYAreaSize.Text) > 1000000)
            {
                TextBoxYAreaSize.Text = "1000000";
                MessageBox.Show("Высота области должна быть меньше 1000км");
                return;
            }
            /*Если ввели число меньше минимума, то ставим минимум*/
            if (Convert.ToDouble(TextBoxYAreaSize.Text) < Draw.GetMaxPointLayers())
            {
                TextBoxYAreaSize.Text = Draw.GetMaxPointLayers().ToString();
                MessageBox.Show("Высота области должна быть больше чем " + TextBoxYAreaSize.Text + "м");
                return;
            }
        }
        private void TextBoxYAreaSize_Validated(object sender, EventArgs e)
        {
            /*Если вводимые данные с заданной точностью не равны рание введенным*/
            if (Draw.YAREASIZE != Math.Round(Convert.ToDouble(TextBoxYAreaSize.Text), GlobalConst.Accuracy))
            {
                /*Записываем данные*/
                Draw.YAREASIZE = Convert.ToDouble(TextBoxYAreaSize.Text);
                /*Переписываем данные с заданной точностью*/
                TextBoxYAreaSize.Text = Draw.YAREASIZE.ToString();                                      
            }
            /*Изменяем ползунки*/
            ChangeScrollBars();
        }

        private void СheckedListBoxSettings_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            switch(e.Index)
            {
                /*Если нажата Сетка*/
                case 0:
                    {
                        if (e.NewValue == CheckState.Checked)
                            Draw.GRID = true;
                        else Draw.GRID = false;
                    }
                    break;
                /*Если нажата Разметка*/
                case 1:
                    {
                        if (e.NewValue == CheckState.Checked)
                            Draw.MARKING = true;
                        else Draw.MARKING = false;
                    }
                    break;
            }
            UnFocus.Focus();
        }
        private void СheckedListBoxSettings_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*Вывод подсказок*/
            switch(СheckedListBoxSettings.SelectedIndex)
            {
                case 0: ToolTip.Show("Отображение сетки.", СheckedListBoxSettings); break;
                case 1: ToolTip.Show("Отображение разметки.", СheckedListBoxSettings); break;
            }
        }
        private void СheckedListBoxSettings_MouseLeave(object sender, EventArgs e)
        {
            /*Если вышли за границы, то убираем выделение*/
            СheckedListBoxSettings.SelectedIndex = -1;
            /*Скидываем фокус*/
            UnFocus.Focus();
        }

        private void TextBoxChangeXMoveSpline_KeyPress(object sender, KeyPressEventArgs e)
        {
            /*Можно вводить только числа, бэкспейс и запятая */
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 8
                && (e.KeyChar != 44 || TextBoxChangeXMoveSpline.Text.IndexOf(",") > -1))
                e.Handled = true;
            /*Если нажат энтер*/
            if (e.KeyChar == 13)
                UnFocus.Focus();
            /*Запятую первой ставить нельзя*/
            if (TextBoxChangeXMoveSpline.TextLength == 0 && e.KeyChar == 44)
                e.Handled = true;
        }
        private void TextBoxChangeXMoveSpline_Validating(object sender, CancelEventArgs e)
        {
            /*число должно быть введенно суммарные изменения должны не выходить за максимальные рамки (1000км)*/
            if(TextBoxChangeXMoveSpline.TextLength > 0 && Math.Round(Convert.ToDouble(TextBoxChangeXMoveSpline.Text),GlobalConst.Accuracy)+Draw.XAREASIZE > 1000000)
            {
                double Move = 1000000.0 - Draw.XAREASIZE;
                TextBoxChangeXMoveSpline.Text = (Move > 0) ? Move.ToString() : "0";
                MessageBox.Show("Смещение области должно быть меньше чем " + TextBoxChangeXMoveSpline.Text + "м");
                return;
            }
        }
        private void TextBoxChangeXMoveSpline_Validated(object sender, EventArgs e)
        {
            /*Если число введенно округляем введенные данные*/
            if (TextBoxChangeXMoveSpline.TextLength > 0)
                TextBoxChangeXMoveSpline.Text = Math.Round(Convert.ToDouble(TextBoxChangeXMoveSpline.Text), GlobalConst.Accuracy).ToString();
        }

        private void ButtonChangeXMoveSpline_Click(object sender, EventArgs e)
        {
            /*Если число введенно и смещение больше 0 с заданной точностью, то смещаем точки*/
            if (Convert.ToDouble(TextBoxChangeXMoveSpline.Text) > 0 && Convert.ToDouble(TextBoxChangeXMoveSpline.Text) > 0)
            {
                /*Смещение*/
                double ChangeX = Convert.ToDouble(TextBoxChangeXMoveSpline.Text);
                TextBoxXAreaSize.Text = (Draw.XAREASIZE + ChangeX).ToString();
                Draw.XAREASIZE = Convert.ToDouble(TextBoxXAreaSize.Text);
                TextBoxYAreaSize.Text = Draw.YAREASIZE.ToString();
                /*Изменяем скроллбары*/
                ChangeScrollBars();
                /*Двигаем сплайны*/
                Draw.MoveSpline(ChangeX);
            }
        }
        #endregion

        #region Почва
        private void TabPageSpline_Click(object sender, EventArgs e)
        {
            UnFocus.Focus();
        }

        #region Контекстное меню
        #region Таблица
        private void DeleteSplineDataGridViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Draw.DeleteSpline(CheckControlPoint);
            /*Изменяем ползунки*/
            MainPaint_VScroll.LargeChange = Draw.YAREASIZE;
            MainPaint_VScroll.Maximum = Draw.ScrollMaximum(0);
            MainPaint_VScroll.Value = Draw.ScrollValue(0);
            /*Выбраты слои*/
            if (CheckControlPoint[0] == 1)
            {
                DataGridViewLayers.Rows.RemoveAt(0);
                ChangeDataGridViewLayers();
                DataGridViewLayers.ClearSelection();
            }
            /*Выбраны минералы*/
            if (CheckControlPoint[0] == 2)
            {
                DataGridViewMinerals.Rows.RemoveAt(0);
                ChangeDataGridViewMinerals();
                DataGridViewMinerals.ClearSelection();
            }
        }
        private void MaterialSplineDataGridViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int i = 0;
            foreach (ToolStripMenuItem Item in MaterialSplineDataGridViewToolStripMenuItem.DropDownItems)
            {
                if ((ToolStripMenuItem)sender == Item)
                {
                    if (CheckControlPoint[0] == 1)
                    {
                        Draw.SetSplineMaterial(CheckControlPoint, MaterialLayer[i]);
                        ChangeDataGridViewLayers();
                    }
                    else
                    {
                        Draw.SetSplineMaterial(CheckControlPoint, MaterialMineral[i]);
                        ChangeDataGridViewMinerals();
                    }
                }
                i++;
            }
        }
        private void ChangeColorDataGridViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog.ShowDialog();
            if (!Draw.SetSplineColor(ColorDialog.Color, CheckControlPoint))
                MessageBox.Show("Цвет уже занят");
            else
            {
                if (CheckControlPoint[0] == 1)
                    ChangeDataGridViewLayers();
                else
                    ChangeDataGridViewMinerals();
            }
        }
        #endregion
        #region Материал
        /*Добавление материала*/
        private void AddMaterial(string Name,string Resistance)
        {
            int i;
            ToolStripMenuItem AddItem;
            double Resistanc;
            if (!double.TryParse(Resistance, out Resistanc))
            {
                MessageBox.Show(Name + " имеет сопротивление в другом формате");
                return;
            }
            /*Если выбран материал слоя*/
            if (CheckControlPoint[0] == 1)
            {
                /*Если данное название уже используется, то ничего не делаем*/
                for (i = 0; i < MaterialLayer.Count; i++)
                    if (MaterialLayer[i].NAME == Name)
                    {
                        MessageBox.Show(Name + " уже используется.");
                        return;
                    }
                /*Добавляем данные*/
                MaterialLayer.Add(new Material(Name, Convert.ToDouble(Resistance)));
                AddItem = new ToolStripMenuItem(MaterialLayer[MaterialLayer.Count - 1].NAME);
                AddItem.Click += new EventHandler(ChangeMaterialToolStripMenuItem_Click);
                ChangeMaterialToolStripMenuItem.DropDownItems.Add(AddItem);
                СomboBoxLayerMaterial.Items.Add(MaterialLayer[MaterialLayer.Count - 1].NAME);
            }
            /*Если выбрат материал минерала*/
            else
            {
                /*Если данное название уже используется, то ничего не делаем*/
                for (i = 0; i < MaterialMineral.Count; i++)
                    if (MaterialMineral[i].NAME == GlobalConst.Buffer[0])
                    {
                        MessageBox.Show(Name + " уже используется.");
                        return;
                    }
                /*Добавляем данные*/
                MaterialMineral.Add(new Material(Name, Convert.ToDouble(Resistance)));
                AddItem = new ToolStripMenuItem(MaterialMineral[MaterialMineral.Count - 1].NAME);
                AddItem.Click += new EventHandler(ChangeMaterialToolStripMenuItem_Click);
                ChangeMaterialToolStripMenuItem.DropDownItems.Add(AddItem);
                ComboBoxMineralMaterial.Items.Add(MaterialMineral[MaterialMineral.Count - 1].NAME);
            }
        }
        private void AddFromFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*Открываем диалог*/
            OpenFileDialog Files = new OpenFileDialog();
            /*Если все окей,то*/
            if (Files.ShowDialog() == DialogResult.OK)
            {
                int i;
                List<string> StringIn = new List<string>();
                WFiles.AddMaterial(Files.FileName, out StringIn);
                for (i = 0; i < StringIn.Count - 1; i += 2)
                    AddMaterial(StringIn[i], StringIn[i + 1]);
            }
            
        }
        private void AddMaterialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*Вызываем диалоговое окно*/
            AddMaterial Set = new AddMaterial();
            Set.ShowDialog();
            /*Если входные данные не пусты*/
            if (GlobalConst.Buffer[0] != "" && GlobalConst.Buffer[1] != "")
                AddMaterial(GlobalConst.Buffer[0], GlobalConst.Buffer[1]);
        }
        private void ChangeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*Вызываем диалоговое окно*/
            AddMaterial Set = new AddMaterial(MaterialLayer[СomboBoxLayerMaterial.SelectedIndex].NAME, MaterialLayer[СomboBoxLayerMaterial.SelectedIndex].RESISTANCE);
            Set.ShowDialog();
            List<int> Number;
            int i;
            string Out = "";
            /*Если данные не пусты и не равны предыдущему*/
            if (GlobalConst.Buffer[0] != "" && GlobalConst.Buffer[1] != "" && (GlobalConst.Buffer[0] != MaterialLayer[СomboBoxLayerMaterial.SelectedIndex].NAME
                || GlobalConst.Buffer[1] != MaterialLayer[СomboBoxLayerMaterial.SelectedIndex].RESISTANCE.ToString()))
            {
                /*Создаем новый материал*/
                Material Material = new Material(GlobalConst.Buffer[0], Convert.ToDouble(GlobalConst.Buffer[1]));
                /*Если это слои*/
                if (CheckControlPoint[0] == 1)
                {
                    /*Проверяем встречается ли старый тип и меняем их*/
                    if (!Draw.CheckMaterial(CheckControlPoint[0], MaterialLayer[СomboBoxLayerMaterial.SelectedIndex], out Number))
                    {
                        for (i = 0; i < Number.Count; i++)
                        {
                            Draw.SetSplineMaterial(new int[2] { 1, Number[i] }, Material);
                            Out += " " + Number[i];
                        }
                        MessageBox.Show("Изменены типы в " + Out + " слоях.");
                    }
                    /*Меняем в массиве и в списке*/
                    MaterialLayer[СomboBoxLayerMaterial.SelectedIndex] = Material;
                    СomboBoxLayerMaterial.SelectedText = Material.NAME;
                }
                /*Если это минералы*/
                else
                {
                    /*Проверяем встречается ли старый тип и меняем их*/
                    if (!Draw.CheckMaterial(CheckControlPoint[0], MaterialMineral[ComboBoxMineralMaterial.SelectedIndex], out Number))
                    {
                        for (i = 0; i < Number.Count; i++)
                        {
                            Draw.SetSplineMaterial(new int[2] { 1, Number[i] }, Material);
                            Out += " " + Number[i];
                        }
                        MessageBox.Show("Изменены типы в " + Out + " слоях.");
                    }
                    /*Меняем в массиве и в списке*/
                    MaterialMineral[ComboBoxMineralMaterial.SelectedIndex] = Material;
                    ComboBoxMineralMaterial.SelectedText = Material.NAME;
                }
            }
        }
        private void DeleteMaterialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<int> Number;
            int i;
            string Out = "";
            /*Если слои*/
            if (CheckControlPoint[0] == 1)
                /*Поиск такого материала в слоях*/
                if (!Draw.CheckMaterial(CheckControlPoint[0], MaterialLayer[СomboBoxLayerMaterial.SelectedIndex], out Number))
                {
                    for (i = 0; i < Number.Count; i++)
                        Out += " " + Number[i];
                    MessageBox.Show("Нельзя удалить материал, так как он использутся в" + Out + " слоях.");
                }
                else
                {
                    if (MaterialLayer.Count > 1)
                    {
                        /*Удаление из массива и выпадающего списка*/
                        MaterialLayer.RemoveAt(СomboBoxLayerMaterial.SelectedIndex);
                        СomboBoxLayerMaterial.Items.RemoveAt(СomboBoxLayerMaterial.SelectedIndex);
                        СomboBoxLayerMaterial.SelectedIndex = 0;
                    }
                    else
                        MessageBox.Show("Должен остаться хотя бы один материал.");
                }
            else
            {
                /*Поиск такого материала в минералах*/
                if (!Draw.CheckMaterial(CheckControlPoint[0], MaterialMineral[ComboBoxMineralMaterial.SelectedIndex], out Number))
                {
                    for (i = 0; i < Number.Count; i++)
                        Out += " " + Number[i];
                    MessageBox.Show("Нельзя удалить материал, так как он использутся в" + Out + " объектах.");
                }
                else
                {
                    if (MaterialMineral.Count > 1)
                    {
                        /*Удаление из массива и выпадающего списка*/
                        MaterialMineral.RemoveAt(ComboBoxMineralMaterial.SelectedIndex);
                        ComboBoxMineralMaterial.Items.RemoveAt(ComboBoxMineralMaterial.SelectedIndex);
                        ComboBoxMineralMaterial.SelectedIndex = 0;
                    }
                    else
                        MessageBox.Show("Должен остаться хотя бы один материал.");
                }
            }
        }
        #endregion
        #endregion

        #region Слои

        private void TextBoxLayerNumberOfPoints_KeyPress(object sender, KeyPressEventArgs e)
        {
            /*Можно вводить только числа и бэкспейс*/
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 8)
                e.Handled = true;
            /*Если нажат энтер*/
            if (e.KeyChar == 13)
                UnFocus.Focus();
        }
        private void TextBoxLayerNumberOfPoints_Validating(object sender, CancelEventArgs e)
        {
            if(TextBoxLayerNumberOfPoints.TextLength==0 || Convert.ToInt32(TextBoxLayerNumberOfPoints.Text) < 2)
            {
                TextBoxLayerNumberOfPoints.Text = "2";
                MessageBox.Show("Количество опорных точек должно быть больше 1");
                return;
            }
            double X = Draw.XAREASIZE;
            if(Convert.ToDouble(TextBoxLayerNumberOfPoints.Text) > X * Math.Pow(10, GlobalConst.Accuracy)) 
            {
                TextBoxLayerNumberOfPoints.Text = X.ToString();
                MessageBox.Show("Количество опорных точек должно быть меньше " + X.ToString() + ".");
                return;
            }
        }
        private void TextBoxLayerNumberOfPoints_Validated(object sender, EventArgs e)
        {
            /*При изменение значений делаем кнопку "Выключенной", в целях проверки введенного числа*/
            DrawLayers = false;
            DrawSplineLayers.BackColor = System.Drawing.SystemColors.Control;
        }

        private void TextBoxLayerHeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            /*Можно вводить только числа, бэкспейс, запятая и минус*/
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 8
                && (e.KeyChar != 44 || TextBoxXCoordinate.Text.IndexOf(",") > -1))
                e.Handled = true;
            /*Если нажат энтер*/
            if (e.KeyChar == 13)
                UnFocus.Focus();
            if (TextBoxXCoordinate.TextLength == 0 && e.KeyChar == 44)
                e.Handled = true;
        }
        private void TextBoxLayerHeight_Validating(object sender, CancelEventArgs e)
        {
            if (Convert.ToDouble(TextBoxLayerHeight.Text) + Draw.EARTHSIZE > 1000000)
            {
                TextBoxLayerHeight.Text = (100000000.0-Draw.EARTHSIZE).ToString();
                MessageBox.Show("Высота слоя должна быть меньше "+TextBoxYAreaSize.Text);
                return;
            }
        }

        private void DrawSplineLayers_Click(object sender, EventArgs e)
        { 
            /*"Включаем" кнопку, теперь можно рисовать несколько слоев с заданным количеством*/
            /*опорных точек*/
            if (DrawLayers == false)
            {
                DrawLayers = true;
                DrawSplineLayers.BackColor = System.Drawing.SystemColors.Highlight;
                DrawMineral = false;
                DrawSplineMinerals.BackColor = System.Drawing.SystemColors.Control;
                UnFocus.Focus();
            }
            else
            {
                DrawLayers = false;
                DrawSplineLayers.BackColor = System.Drawing.SystemColors.Control;
                LabelLayerHeight.Focus();
            }
        }
        private void AddSplineLayers_Click(object sender, EventArgs e)
        {
            /*Рисуем новый слой*/
            if (Draw.AddLayers(-Convert.ToInt32(TextBoxLayerHeight.Text), Convert.ToInt32(TextBoxLayerNumberOfPoints.Text), MaterialLayer[СomboBoxLayerMaterial.SelectedIndex]))
            {
                /*Изменяем ползунки*/
                MainPaint_VScroll.LargeChange = Convert.ToInt32(Draw.YAREASIZE + 1);
                MainPaint_VScroll.Maximum = Draw.ScrollMaximum(0);
                MainPaint_VScroll.Value = Draw.ScrollValue(0);
                /*Заполняем таблицу*/
                DataGridViewLayers.Rows.Add(0, "", 0, 0);
                ChangeDataGridViewLayers();
            }
            else
            MessageBox.Show("Нельзя добавить слой.");
        }

        private void СomboBoxLayerMaterial_MouseDown(object sender, MouseEventArgs e)
        {
            /*Если нажата правая кнопка, то выводим контексное меню*/
            if (e.Button == MouseButtons.Right)
            {
                CheckControlPoint[0] = 1;
                ContextMenuMaterials.Show(Cursor.Position);
            }
        }
        private void СomboBoxLayerMaterial_DrawItem(object sender, DrawItemEventArgs e)
        {
            /*Добовляем тултип*/
            if (e.Index < 0) { return; }
            string text = "Сопротивление:" + MaterialLayer[e.Index].RESISTANCE.ToString();
            e.DrawBackground();
            using (SolidBrush br = new SolidBrush(e.ForeColor))
            { e.Graphics.DrawString(СomboBoxLayerMaterial.GetItemText(СomboBoxLayerMaterial.Items[e.Index]), e.Font, br, e.Bounds); }
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                ToolTip.Show(text, СomboBoxLayerMaterial, e.Bounds.Right, e.Bounds.Bottom);
            e.DrawFocusRectangle();
        }
        private void СomboBoxLayerMaterial_DropDownClosed(object sender, EventArgs e)
        {
            /*Скрываем тултип, убираем фокус*/
            ToolTip.Hide(СomboBoxLayerMaterial);
            UnFocus.Focus();
        }

        private void DataGridViewLayers_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex != DataGridViewLayers.Rows.Count - 1)
            {
                DataGridViewLayers.Rows[e.RowIndex].Selected = true;
                CheckControlPoint[0] = 1;
                CheckControlPoint[1] = e.RowIndex;
                ChangeMaterialsToolStripMenuItem(MaterialSplineDataGridViewToolStripMenuItem);
                ContextMenuDataGridView.Show(Cursor.Position);
            }
        }      

        #endregion

        #region Минералы
        private void DrawSplineMinerals_Click(object sender, EventArgs e)
        {
            /*"Включаем" кнопку, теперь можно рисовать минерал*/
            if (DrawMineral == false)
            {
                DrawMineral = true;
                DrawSplineMinerals.BackColor = System.Drawing.SystemColors.Highlight;
                DrawLayers = false;
                DrawSplineLayers.BackColor = System.Drawing.SystemColors.Control;
                Draw.NewMinerals(MaterialLayer[ComboBoxMineralMaterial.SelectedIndex]);
                UnFocus.Focus();
            }
            else
            {
                DrawMineral = false;
                DrawSplineMinerals.BackColor = System.Drawing.SystemColors.Control;
                if (!Draw.CheckPointsMinerals())
                    MessageBox.Show("Должно быть минимум 3 точки, точки удалены.");
                else
                {
                    DataGridViewMinerals.Rows.Add(0, "", 0, 0);
                    ChangeDataGridViewMinerals();
                }
                UnFocus.Focus();
            }
        }

        private void ComboBoxMineralMaterial_MouseDown(object sender, MouseEventArgs e)
        {
            /*Если нажата правая кнопка, то выводим контексное меню*/
            if (e.Button == MouseButtons.Right)
            {
                CheckControlPoint[0] = 2;
                ContextMenuMaterials.Show(Cursor.Position);
            }
        }
        private void ComboBoxMineralMaterial_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) { return; }
            string text = "Сопротивление:" + MaterialMineral[e.Index].RESISTANCE.ToString();
            e.DrawBackground();
            using (SolidBrush br = new SolidBrush(e.ForeColor))
            { e.Graphics.DrawString(ComboBoxMineralMaterial.GetItemText(ComboBoxMineralMaterial.Items[e.Index]), e.Font, br, e.Bounds); }
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                ToolTip.Show(text, ComboBoxMineralMaterial, e.Bounds.Right, e.Bounds.Bottom);
            }
            e.DrawFocusRectangle();
        }
        private void ComboBoxMineralMaterial_DropDownClosed(object sender, EventArgs e)
        {
            /*Скрываем тултип, убираем фокус*/
            ToolTip.Hide(СomboBoxLayerMaterial);
            UnFocus.Focus();
        }

        private void DataGridViewMinerals_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex != DataGridViewMinerals.Rows.Count - 1)
            {
                DataGridViewMinerals.Rows[e.RowIndex].Selected = true;
                CheckControlPoint[0] = 2;
                CheckControlPoint[1] = e.RowIndex;
                ChangeMaterialsToolStripMenuItem(MaterialSplineDataGridViewToolStripMenuItem);
                ContextMenuDataGridView.Show(Cursor.Position);
            }
        }

        #endregion

        private void СheckedListBoxSpline_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            switch (e.Index)
            {
                /*Нажата Опорные линии.*/
                case 0:
                    {
                        if (e.NewValue == CheckState.Checked)
                            Draw.SUPPORTLINE = true;
                        else Draw.SUPPORTLINE = false;
                    }
                    break;
                /*Нажата Bspline.*/
                case 1:
                    {
                        if (e.NewValue == CheckState.Checked)
                        {
                            Draw.BSPLINE = true;
                            Draw.CSPLINE = false;
                            СheckedListBoxSpline.SetItemChecked(2, false);
                        }
                        Draw.BSPLINE = false;
                    }
                    break;
                /*Нажата CSpline.*/
                case 2:
                    {
                        if (e.NewValue == CheckState.Checked)
                        {
                            Draw.CSPLINE = true;
                            Draw.BSPLINE = false;
                            СheckedListBoxSpline.SetItemChecked(1, false);
                        }
                        Draw.CSPLINE = false;
                    }
                    break;
            }
        }
        private void СheckedListBoxSpline_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*Вывод подсказок*/
            switch (СheckedListBoxSpline.SelectedIndex)
            {
                case 0: ToolTip.Show("Отображение опорных линий.", СheckedListBoxSpline); break;
                case 1: ToolTip.Show("Отображение BSpline.", СheckedListBoxSpline); break;
                case 2: ToolTip.Show("Отображение CSpline.", СheckedListBoxSpline); break;
            }
        }
        private void СheckedListBoxSpline_MouseLeave(object sender, EventArgs e)
        {
            /*Если вышли за границы, то убираем выделение*/
            СheckedListBoxSpline.SelectedIndex = -1;
            /*Скидываем фокус*/
            UnFocus.Focus();
        }
        #endregion

        #region МКЭ
        private void TabPageMKE_Click(object sender, EventArgs e)
        {
            UnFocus.Focus();
        }

        private void TextBoxStepPartition_KeyPress(object sender, KeyPressEventArgs e)
        {
            /*Можно вводить только числа, бэкспейс и запятая */
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 8
                && (e.KeyChar != 44 || TextBoxXAreaSize.Text.IndexOf(",") > -1))
                e.Handled = true;
            /*Если нажат энтер*/
            if (e.KeyChar == 13)
                UnFocus.Focus();
            /*Запятую первой ставить нельзя*/
            if (TextBoxXAreaSize.TextLength == 0 && e.KeyChar == 44)
                e.Handled = true;
        }
        private void TextBoxStepPartition_Validating(object sender, CancelEventArgs e)
        {
            double Min = 2 * Math.Pow(10, -GlobalConst.Accuracy);
            /*Если пустое поле, то запоняем минимумом*/
            if (TextBoxStepPartition.TextLength == 0)
            {
                TextBoxStepPartition.Text = Min.ToString();
                MessageBox.Show("Минимальный шаг разбиения " + Min);
                Min = 2 * Math.Pow(10, -GlobalConst.Accuracy);
                return;
            }
            /*Обрезаем с нужной точностью*/
            TextBoxStepPartition.Text = Math.Round(Convert.ToDouble(TextBoxStepPartition.Text), GlobalConst.Accuracy).ToString();
            /*Если меньше минимума, то ставим минимум*/
            if (Convert.ToDouble(TextBoxStepPartition.Text) < Min)
            {
                TextBoxStepPartition.Text = Min.ToString();
                MessageBox.Show("Минимальный шаг разбиения " + Min);
                Min = 2 * Math.Pow(10, -GlobalConst.Accuracy);
                return;
            }
            /*Если больше максимума, то ставим максимум*/
            if (Convert.ToDouble(TextBoxStepPartition.Text) > Draw.XAREASIZE)
            {
                TextBoxStepPartition.Text = Draw.XAREASIZE.ToString();
                MessageBox.Show("Максимальный шаг разбиения " + Draw.XAREASIZE);
                return;
            }
            if (Draw.XAREASIZE % Convert.ToDouble(TextBoxStepPartition.Text) != 0)
            {
                double a = Math.Round(Draw.XAREASIZE % Convert.ToDouble(TextBoxStepPartition.Text), GlobalConst.Accuracy);
                double b = Math.Round(Convert.ToDouble(TextBoxStepPartition.Text), GlobalConst.Accuracy);
                TextBoxStepPartition.Text = (b - a).ToString();
                MessageBox.Show("Область не делится нацело с данным шагом. Ближайшие число " + (b - a).ToString());
                return;
            }
            if (СheckedListBoxMKE.GetItemChecked(0) == true)
            {
                СheckedListBoxMKE.SetItemCheckState(0, CheckState.Unchecked);
                Draw.MAKEPARTITION = false;
            }
        }

        private void PictureBoxColorPartition_MouseDown(object sender, MouseEventArgs e)
        {
            /*Меняем цвет линии разбиения*/
            if(e.Button == MouseButtons.Right)
            {
                ColorDialog.ShowDialog();
                PictureBoxColorPartition.BackColor = ColorDialog.Color;
                Draw.COLORPARTITION = ColorDialog.Color;
            }
        }

        private void СheckedListBoxMKE_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            switch (e.Index)
            {
                /*Нажата Опорные линии.*/
                case 0:
                    {
                        if (e.NewValue == CheckState.Checked && TextBoxStepPartition.Text!="")
                        {
                            Draw.PARTITIONX = Convert.ToDouble(TextBoxStepPartition.Text);
                            Draw.MAKEPARTITION = true;
                        }
                        else Draw.MAKEPARTITION = false;
                    }
                    break;
            }
        }
        private void СheckedListBoxMKE_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*Вывод подсказок*/
            switch (СheckedListBoxMKE.SelectedIndex)
            {
                case 0: ToolTip.Show("Отображение опорных линий разбиения.", СheckedListBoxMKE); break;
            }
        }
        private void СheckedListBoxMKE_MouseLeave(object sender, EventArgs e)
        {
            /*Если вышли за границы, то убираем выделение*/
            СheckedListBoxMKE.SelectedIndex = -1;
            /*Скидываем фокус*/
            UnFocus.Focus();
        }


        private void ButtonMakePartition_Click(object sender, EventArgs e)
        {

        }

        private void ButtonSavePartition_Click(object sender, EventArgs e)
        {
            /*Открываем диалог*/
            SaveFileDialog Files = new SaveFileDialog();
            if (Files.ShowDialog() == DialogResult.OK)
            {
                FileStream fs = new FileStream(Files.FileName, FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                int i;
                /*Выводим материал слоя*/
                for (i=0;i<MaterialLayer.Count;i++)
                    sw.WriteLine((i+1).ToString()+"l " + MaterialLayer[i].NAME +" " + MaterialLayer[i].RESISTANCE.ToString());
                sw.WriteLine();
                /*Выводим материалы минерала*/
                for (i = 0; i < MaterialMineral.Count; i++)
                    sw.WriteLine((i + 1).ToString() + "m " + MaterialMineral[i].NAME + " " + MaterialMineral[i].RESISTANCE.ToString());
                sw.Close();

            }
        }



        #endregion

        #endregion
    }
}