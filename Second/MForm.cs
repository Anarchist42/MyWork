using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Tao.DevIl;
using Tao.OpenGl;           // для работы с библиотекой OpenGL 
using Tao.FreeGlut;         // для работы с библиотекой FreeGLUT 
using Tao.Platform.Windows; // для работы с элементом управления SimpleOpenGLControl 


using System.Numerics;


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
        private bool AddPointLayers;
        /// <summary>
        /// Выбрали слои или минералы (1 - слои, 2 - минералы).
        /// </summary>
        private int LayerMinerals;
        /// <summary>
        /// Массив материалов слоя.
        /// </summary>
        private List<Material> MaterialsLayer;
        /// <summary>
        /// Массив материалов минералов.
        /// </summary>
        private List<Material> MaterialsMineral;
        #endregion

        #region Меню
        private void CloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion


        public MainForm()
        {
            /*Инициализация компонент*/
            InitializeComponent();
            MainPaint.InitializeContexts();
            /*Создаем объект класса отрисовки*/
            Draw = new Paint(MainPaint);
            FlagMouseGlControl = false;
            MouseDownLeft = false;
            DrawLayers = false;
            DrawMineral = false;
            CheckControlPoint = new int[3];
            AddPointLayers = false;
            LayerMinerals = 0;
            GlobalConst.Difference = 5;
            GlobalConst.Accuracy = -1;
            /*Создаем и заполняем материалы*/
            MaterialsLayer = new List<Material>();
            MaterialsLayer.Add(new Material("Земля", 0));
            MaterialsLayer.Add(new Material("Грунт", 1));
            MaterialsLayer.Add(new Material("Грязь", 2));
            for (int i = 0; i < MaterialsLayer.Count; i++)
                СomboBoxLayerMaterial.Items.Add(MaterialsLayer[i].NAME);
            СomboBoxLayerMaterial.SelectedIndex = 0;
            MaterialsMineral = new List<Material>();
            MaterialsMineral.Add(new Material("Земля", 0));
            MaterialsMineral.Add(new Material("Грунт", 1));
            MaterialsMineral.Add(new Material("Грязь", 2));
            for (int i = 0; i < MaterialsLayer.Count; i++)
                ComboBoxMineralMaterial.Items.Add(MaterialsMineral[i].NAME);
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
            if (MainPaint_VScroll.LargeChange == MainPaint_VScroll.Maximum + 1)
                MainPaint_VScroll.Visible = false;
            else MainPaint_VScroll.Visible = true;
            if (MainPaint_HScroll.LargeChange == MainPaint_HScroll.Maximum + 1)
                MainPaint_HScroll.Visible = false;
            else MainPaint_HScroll.Visible = true;
            Draw.Draw();
        }

        /*Изменяем ползунки*/
        private void ChangeScrollBars()
        {
            MainPaint_VScroll.LargeChange = Convert.ToInt32(Draw.YAREASIZE + 1);
            MainPaint_VScroll.Maximum = Draw.ScrollMaximum(0);
            MainPaint_VScroll.Value = Draw.ScrollValue(0);
            MainPaint_HScroll.LargeChange = Convert.ToInt32(Draw.XAREASIZE + 1);
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
                Draw.ReturnInformation(0, i, out color, out LayerPosition, out Material);
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
            for (i = 0; i < DataGridViewLayers.Rows.Count - 1; i++)
            {
                Draw.ReturnInformation(1, i, out color, out LayerPosition, out Material);
                DataGridViewMinerals.Rows[i].Cells[0].Value = i + 1;
                DataGridViewMinerals.Rows[i].Cells[1].Style.BackColor = color;
                DataGridViewMinerals.Rows[i].Cells[2].Value = LayerPosition;
                DataGridViewMinerals.Rows[i].Cells[3].Value = Material;
            }
        }

        #region Работа с MainPaint




        #region Контексное меню
        private void AddValueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetValue Set = new SetValue();
            Set.ShowDialog();
            if(GlobalConst.Buffer[0]!="" && GlobalConst.Buffer[1]!="")
                Draw.ChangePoint(CheckControlPoint, new PointSpline(GlobalConst.Buffer[0], GlobalConst.Buffer[1]));
            if (CheckControlPoint[0] == 1)
                ChangeDataGridViewLayers();
            else
                ChangeDataGridViewMinerals();
        }
        private void ChangeColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog.ShowDialog();
            if (!Draw.SetLayerColor(ColorDialog.Color, CheckControlPoint))
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
            AddPointLayers = true;
        }
        private void DeletePointToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Draw.DeletePoint(CheckControlPoint);
        }
        private void DeleteLayersMainPaintToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int i,
            /*Старое значение максимального значения ползунка*/
            MaximumScroll = MainPaint_VScroll.Maximum;
            /*Удаляем слой*/
            Draw.DeleteSplineForm(CheckControlPoint);
            /*Изменяем ползунки*/
            MainPaint_VScroll.LargeChange = Convert.ToInt32(Draw.YAREASIZE + 1);            
            MainPaint_VScroll.Maximum = Draw.ScrollMaximum(0);
            Draw.YOFFSET += (MaximumScroll - MainPaint_VScroll.Maximum ) * Draw.ZOOM;
            MainPaint_VScroll.Value = Draw.ScrollValue(0);
            if (LayerMinerals == 1)
            {
                DataGridViewLayers.Rows.RemoveAt(0);
                ChangeDataGridViewLayers();
            }
            if (LayerMinerals == 2)
            {
                DataGridViewMinerals.Rows.RemoveAt(0);
                ChangeDataGridViewMinerals();
            }
        }
        #endregion
        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState != FormWindowState.Minimized)
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
                    MainPaint_HScroll.Maximum = Convert.ToInt32(Draw.XAREASIZE);
                }
                else
                {
                    Draw.MINZOOM = GlobalConst.MinZoom * Convert.ToDouble(MainPaint.Height - GlobalConst.Difference)
                        / Convert.ToDouble(MainPaint.MinimumSize.Height - GlobalConst.Difference);
                    MainPaint_HScroll.Maximum = Draw.ScrollMaximum(1);
                    MainPaint_VScroll.Maximum = Convert.ToInt32(Draw.YAREASIZE);
                }
                /*Настраиваем вертикальный ползунок*/
                MainPaint_VScroll.Size = new Size(MainPaint_VScroll.Size.Width, MainPaint.Height);
                MainPaint_VScroll.LargeChange = Convert.ToInt32(Draw.YAREASIZE + 1);
                MainPaint_VScroll.Value = (MainPaint_VScroll.Maximum - MainPaint_VScroll.LargeChange) / 2 + 1;
                /*Настраиваем горизонтальный ползунок*/
                MainPaint_HScroll.Size = new Size(MainPaint.Width, MainPaint_HScroll.Size.Height);
                MainPaint_HScroll.LargeChange = Convert.ToInt32(Draw.XAREASIZE + 1);
                MainPaint_HScroll.Value = (MainPaint_HScroll.Maximum - MainPaint_HScroll.LargeChange) / 2 + 1;
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
        private void MainPaint_VScroll_Scroll(object sender, ScrollEventArgs e)
        {
            Draw.YOFFSET += (e.NewValue - e.OldValue) * Draw.ScrollStep(0, MainPaint_VScroll.Maximum - MainPaint_VScroll.LargeChange + 1);
        }
        private void MainPaint_HScroll_Scroll(object sender, ScrollEventArgs e)
        {
            Draw.XOFFSET += (e.OldValue - e.NewValue) * Draw.ScrollStep(1, MainPaint_HScroll.Maximum - MainPaint_HScroll.LargeChange + 1);
        }
        #region СДЕЛАТЬ!!!!

        private void MainPaint_VScroll_ValueChanged(object sender, EventArgs e)
        {

        }
        private void MainPaint_HScroll_ValueChanged(object sender, EventArgs e)
        {
            
        }
        #endregion
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
                MainPaint_VScroll.LargeChange = Convert.ToInt32(Draw.YAREASIZE + 1);
                MainPaint_VScroll.Maximum = Draw.ScrollMaximum(0);
                MainPaint_VScroll.Value = Draw.ScrollValue(0);
                MainPaint_HScroll.LargeChange = Convert.ToInt32(Draw.XAREASIZE + 1);
                MainPaint_HScroll.Maximum = Draw.ScrollMaximum(1);
                MainPaint_HScroll.Value = Draw.ScrollValue(1);
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
                        Draw.ChangePoint(CheckControlPoint,new PointSpline(Draw.GetCoordinate(0),Draw.GetCoordinate(1)));
                        TextBoxYAreaSize.Text = Draw.YAREASIZE.ToString();
                        MainPaint_VScroll.LargeChange = Convert.ToInt32(Draw.YAREASIZE + 1);
                        MainPaint_VScroll.Maximum = Draw.ScrollMaximum(0);
                        MainPaint_VScroll.Value = Draw.ScrollValue(0);
                        MainPaint_HScroll.LargeChange = Convert.ToInt32(Draw.XAREASIZE + 1);
                        MainPaint_HScroll.Maximum = Draw.ScrollMaximum(1);
                        MainPaint_HScroll.Value = Draw.ScrollValue(1);
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
            /*Убираем выделение в таблице Слоев*/
            DataGridViewLayers.ClearSelection();
            /*Если нажата левая кнопка мыши*/
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                MouseDownLeft = true;
                MouseDownPosition = new Point(e.X, e.Y);
                Draw.MOUSEPOSITION = new Point(e.X, e.Y);
                /*Если выбранно "Добавить точку" в контексном меню слоя*/
                if(AddPointLayers)
                {
                    Draw.AddPoint(CheckControlPoint);
                    AddPointLayers = false;
                    MouseDownLeft = false;
                    return;
                }            
                /*Если кнопка "Нарисовать" новый слой "включена"*/
                if (DrawLayers)
                {
                    Draw.AddLayers(Draw.GetCoordinate(1), Convert.ToInt32(TextBoxLayerNumberOfPoints.Text), MaterialsLayer[СomboBoxLayerMaterial.SelectedIndex]);
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
                /*Если кнопка "Нарисовать" минерал "включена"*/
                if(DrawMineral)
                {
                    Draw.AddPointMinerals();
                }
            }
            /*Проверяем попали ли мы на опорную точку*/
            Draw.CheckPoint(new Point(e.X, e.Y), out CheckControlPoint);
            /*Если попали и нажата правая кнопка мыши, то вызываем контексное меню*/
            if (CheckControlPoint[0] != 0 && e.Button == System.Windows.Forms.MouseButtons.Right)
                СontextMenuMainPaint.Show(Cursor.Position);
        }
        #endregion

        #region TabControl


        #region Настройки
        /*Снимаем фокус*/
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
            if (TextBoxAccuracy.TextLength > 0 && Convert.ToInt32(TextBoxAccuracy.Text) < 13)
                e.Cancel = false;
            else
            {
                if (GlobalConst.Accuracy == -1)
                    TextBoxAccuracy.Text = "";
                else
                    TextBoxAccuracy.Text = GlobalConst.Accuracy.ToString();
                MessageBox.Show("Введите число от 0 до 12");
            }
        }
        private void TextBoxAccuracy_Validated(object sender, EventArgs e)
        {
            /*Если ввели данные и старая и новая точность не совпадают, то*/
            if (TextBoxAccuracy.TextLength > 0 && Convert.ToInt32(TextBoxAccuracy.Text) != GlobalConst.Accuracy)
            {
                GlobalConst.Accuracy = Convert.ToInt32(TextBoxAccuracy.Text);
                if (TextBoxXAreaSize.TextLength == 0)
                {
                    /*Заносим данные по дефолту*/
                    TextBoxXAreaSize.Text = "10";
                    Draw.XAREASIZE = Convert.ToDouble(TextBoxXAreaSize.Text);
                    TextBoxXAreaSize.Text = Draw.XAREASIZE.ToString();
                    TextBoxYAreaSize.Text = Draw.YAREASIZE.ToString();
                    TextBoxEarthSize.Text = "0";
                    Draw.EARTHSIZE = Convert.ToDouble(TextBoxEarthSize.Text);
                    TextBoxChangeXMoveSpline.Text = "0";
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
                    /*Разрешаем менять окно*/
                    this.MaximumSize = SystemInformation.PrimaryMonitorSize;
                    this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
                    /*Меняем ползунки*/
                    ChangeScrollBars();
                    /*Добавляем "Землю" 0 сплайн*/
                    Draw.AddLayers(0, 2, new Material("Non", 0));
                    /*Запускаем отрисовку*/
                    RenderTimer.Start();
                }
                else
                {
                    /*Меняем данные(точность этих данных)*/
                    Draw.XAREASIZE = Convert.ToDouble(TextBoxXAreaSize.Text);
                    Draw.EARTHSIZE = Convert.ToDouble(TextBoxEarthSize.Text);
                    /*Изменяем точность слпайнов*/
                    Draw.ChangeAccuracy();
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
                    /*Изменяем ползунки*/
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
            if (TextBoxXAreaSize.TextLength == 0)
                TextBoxXAreaSize.Text = "10";
            if (Convert.ToDouble(TextBoxXAreaSize.Text) > 10000000)
            {
                TextBoxXAreaSize.Text = "10000000";
                MessageBox.Show("Ширина области должна быть меньше 10000км");
            }
            double Min = (582 / Math.Pow(10, GlobalConst.Accuracy) > 10)
                ? 582 / Math.Pow(10, GlobalConst.Accuracy)
                : 10;
            if (Convert.ToDouble(TextBoxXAreaSize.Text) < Min)
            {
                TextBoxXAreaSize.Text = Min.ToString();
                MessageBox.Show("Ширина области должна быть больше чем " + Min + " м");
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
            /*Запускаем отрисовку*/
            RenderTimer.Start();
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
            if (TextBoxEarthSize.TextLength == 0)
                TextBoxEarthSize.Text = "0";
            if (Convert.ToDouble(TextBoxEarthSize.Text) > 10000)
            {
                TextBoxXAreaSize.Text = "10000";
                MessageBox.Show("Высота над уровнем земли должна быть меньше 10км");
            }
        }
        private void TextBoxEarthSize_Validated(object sender, EventArgs e)
        {
            /*Если введенное число не совпадает с заданной точностью, то*/
            if (Draw.EARTHSIZE != Math.Round(Convert.ToDouble(TextBoxEarthSize.Text), GlobalConst.Accuracy))
            {
                /*Записываем данные*/
                Draw.EARTHSIZE = Convert.ToDouble(TextBoxEarthSize.Text);
                TextBoxYAreaSize.Text = (Convert.ToDouble(TextBoxYAreaSize.Text) > Draw.GetMaxPointLayers())
                    ? Convert.ToDouble(TextBoxYAreaSize.Text).ToString()
                    : Draw.GetMaxPointLayers().ToString();
                Draw.YAREASIZE = Convert.ToDouble(TextBoxYAreaSize.Text);
                Draw.XOFFSET = 0.0;
                Draw.YOFFSET = 0.0;               
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
            if (TextBoxYAreaSize.TextLength == 0)
                TextBoxYAreaSize.Text = Draw.DEFYAREASIZE.ToString();
            if (Convert.ToDouble(TextBoxYAreaSize.Text) > 1000000)
            {
                TextBoxYAreaSize.Text = "1000000";
                MessageBox.Show("Высота области должна быть меньше 1000км");
            }
            if (Convert.ToDouble(TextBoxYAreaSize.Text) < Draw.GetMaxPointLayers())
            {
                TextBoxYAreaSize.Text = Draw.GetMaxPointLayers().ToString();
                MessageBox.Show("Высота области должна быть больше чем " + Draw.GetMaxPointLayers().ToString() + "м");
            }
        }
        private void TextBoxYAreaSize_Validated(object sender, EventArgs e)
        {
            if (Draw.YAREASIZE != Math.Round(Convert.ToDouble(TextBoxYAreaSize.Text), GlobalConst.Accuracy))
            {
                /*Записываем данные*/
                Draw.YAREASIZE = Convert.ToDouble(TextBoxYAreaSize.Text);
                Draw.XOFFSET = 0.0;
                Draw.YOFFSET = 0.0;             
                
            }
            /*Записываем данные*/
            TextBoxYAreaSize.Text = Draw.YAREASIZE.ToString();
            /*Изменяем ползунки*/
            ChangeScrollBars();
        }

        private void СheckedListBoxSettings_ItemCheck(object sender, ItemCheckEventArgs e)
        {
                /*Если нажата Сетка*/
                if (e.Index == 0)
                {
                    if (e.NewValue == CheckState.Checked)
                        Draw.GRID = true;
                    else Draw.GRID = false;
                }
                /*Если нажата Разметка*/
                if (e.Index == 1)
                {
                    if (e.NewValue == CheckState.Checked)
                        Draw.MARKING = true;
                    else Draw.MARKING = false;
                }
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
            /*Суммарные изменения должны не выходим за максимальные рамки (1000км)*/
            if(Math.Round(Convert.ToDouble(TextBoxChangeXMoveSpline.Text),GlobalConst.Accuracy)+Draw.XAREASIZE > 1000000)
            {
                TextBoxChangeXMoveSpline.Text = (1000000.0 - Math.Round(Convert.ToDouble(TextBoxChangeXMoveSpline.Text), GlobalConst.Accuracy)).ToString();
                MessageBox.Show("Смещение области должно быть меньше чем " + TextBoxChangeXMoveSpline.Text + "м");
            }
        }
        private void TextBoxChangeXMoveSpline_Validated(object sender, EventArgs e)
        {
            TextBoxChangeXMoveSpline.Text = Math.Round(Convert.ToDouble(TextBoxChangeXMoveSpline.Text), GlobalConst.Accuracy).ToString();
        }

        private void ButtonChangeXMoveSpline_Click(object sender, EventArgs e)
        {
            if (Convert.ToDouble(TextBoxChangeXMoveSpline.Text) > 0)
            {
                double ChangeX = Convert.ToDouble(TextBoxChangeXMoveSpline.Text);
                TextBoxXAreaSize.Text = (Draw.XAREASIZE + ChangeX).ToString();
                Draw.XAREASIZE = Convert.ToDouble(TextBoxXAreaSize.Text);
                TextBoxYAreaSize.Text = Draw.YAREASIZE.ToString();
                ChangeScrollBars();
                Draw.MoveSpline(Convert.ToDouble(TextBoxChangeXMoveSpline.Text));
            }
        }
        #endregion

        #region Почва

        #region Слои и Минералы контекстное меню(общие)
        #region Таблица
        private void DeleteSplineDataGridViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int i;
            /*Старое значение максимального значения ползунка*/
            int MaximumScroll = MainPaint_VScroll.Maximum;
            /*Удаляем слой*/
            Draw.DeleteSplineGrid(LayerMinerals, CheckControlPoint[1]);
            /*Изменяем ползунки*/
            MainPaint_VScroll.LargeChange = Convert.ToInt32(Draw.YAREASIZE + 1);
            MainPaint_VScroll.Maximum = Draw.ScrollMaximum(0);
            Draw.YOFFSET += (MaximumScroll - MainPaint_VScroll.Maximum) * Draw.ZOOM;
            MainPaint_VScroll.Value = Draw.ScrollValue(0);
            /*Удаляем строку в таблице*/
            DataGridViewLayers.Rows.RemoveAt(CheckControlPoint[1]);
            /*Уменьшаем индекс таблице*/
            for (i = CheckControlPoint[1]; i < DataGridViewLayers.Rows.Count - 1; i++)
                DataGridViewLayers.Rows[i].Cells[0].Value = Convert.ToInt32(DataGridViewLayers.Rows[i].Cells[0].Value) - 1;
            /*Убираем выделение в таблице Слоев*/
            DataGridViewLayers.ClearSelection();
        }
        #endregion
        #region Материал
        private void AddMaterialToolStripMenuItem_Click(object sender, EventArgs e)
        {

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
            {
                TextBoxLayerNumberOfPoints.Enabled = false;
                TextBoxLayerNumberOfPoints.Enabled = true;
            }
        }
        private void TextBoxLayerNumberOfPoints_Validating(object sender, CancelEventArgs e)
        {
            if(TextBoxLayerNumberOfPoints.TextLength==0)
            {
                TextBoxLayerNumberOfPoints.Text = "2";
                MessageBox.Show("Количество опорных точек должно быть больше 1");
            }
            double X = Draw.XAREASIZE;
            if(Convert.ToDouble(TextBoxLayerNumberOfPoints.Text) > X * Math.Pow(10, GlobalConst.Accuracy)) 
            {
                TextBoxLayerNumberOfPoints.Text = X.ToString();
                MessageBox.Show("Количество опорных точек должно быть меньше " + X.ToString() + ".");
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
            {
                TextBoxXCoordinate.Enabled = false;
                TextBoxXCoordinate.Enabled = true;
            }
            if (TextBoxXCoordinate.TextLength == 0 && e.KeyChar == 44)
                e.Handled = true;
        }
        private void DrawSplineLayers_Click(object sender, EventArgs e)
        {
            /*Проверка количества опорных точек*/
            if (TextBoxLayerNumberOfPoints.Text.Length == 0 || Convert.ToInt32(TextBoxLayerNumberOfPoints.Text) < 2)
            {
                MessageBox.Show("Должно быть минимум 2 опорных точки.");
                return;
            }
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
            Draw.AddLayers(Convert.ToInt32(TextBoxLayerHeight.Text), Convert.ToInt32(TextBoxLayerNumberOfPoints.Text),new Material());
            /*Изменяем ползунки*/
            MainPaint_VScroll.LargeChange = Convert.ToInt32(Draw.YAREASIZE + 1);
            MainPaint_VScroll.Maximum = Draw.ScrollMaximum(0);
            MainPaint_VScroll.Value = Draw.ScrollValue(0);
            /*Заполняем таблицу*/
            if (DataGridViewLayers.Rows.Count == 1)
            {
                //DataGridViewLayers.Rows.Add(1, "", Convert.ToInt32(TextBoxLayerHeight.Text), Convert.ToInt32(TextBoxLayerNumberOfPoints.Text));
                //DataGridViewLayers.Rows[0].Cells[1].Style.BackColor = Draw.GetLayerColor(Convert.ToInt32(DataGridViewLayers.Rows[0].Cells[0].Value));
            }
            else
            {
                //DataGridViewLayers.Rows.Add(DataGridViewLayers.Rows.Count, "", Convert.ToInt32(TextBoxLayerHeight.Text), Convert.ToInt32(TextBoxLayerNumberOfPoints.Text));
                //DataGridViewLayers.Rows[DataGridViewLayers.Rows.Count - 2].Cells[1].Style.BackColor = Draw.GetLayerColor(Convert.ToInt32(DataGridViewLayers.Rows[DataGridViewLayers.Rows.Count - 2].Cells[0].Value));
            }
        }


        private void СomboBoxLayerMaterial_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
        private void СomboBoxLayerMaterial_MouseClick(object sender, MouseEventArgs e)
        {
            //if (e.Button == MouseButtons.Right)
            //    ContextMenuMaterials.Show();
        }


        private void СomboBoxLayerMaterial_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                ContextMenuMaterials.Show();
        }
        private void DataGridViewLayers_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex != DataGridViewLayers.Rows.Count - 1)
            {
                LayerMinerals = 1;
                DataGridViewLayers.Rows[e.RowIndex].Selected = true;
                CheckControlPoint[1] = e.RowIndex;
                ContextMenuDataGridView.Show(Cursor.Position);
            }
        }
        private void СheckedListBoxSpline_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            /*Если нажата "Опорные линии"*/
            if (e.Index == 0)
            {
                if (e.NewValue == CheckState.Checked)
                    Draw.SUPPORTLINE = true;
                else Draw.SUPPORTLINE = false;
            }
            /*Если нажата "BSpline"*/
            if (e.Index == 1)
            {
                if (e.NewValue == CheckState.Checked)
                {
                    Draw.BSPLINE = true;
                    Draw.CSPLINE = false;
                    СheckedListBoxSpline.SetItemChecked(2, false);
                }
                Draw.BSPLINE = false;
            }
            /*Если нажата "CSpline"*/
            if (e.Index == 2)
            {
                if (e.NewValue == CheckState.Checked)
                {
                    Draw.CSPLINE = true;
                    Draw.BSPLINE = false;
                    СheckedListBoxSpline.SetItemChecked(1, false);
                }
                Draw.CSPLINE = false;
            }
        }
        #endregion

        #region Минералы



        private void DrawSplineMinerals_Click(object sender, EventArgs e)
        {
            /*"Включаем" кнопку, теперь можно рисовать несколько слоев с заданным количеством*/
            /*опорных точек*/
            if (DrawMineral == false)
            {
                DrawMineral = true;
                DrawSplineMinerals.BackColor = System.Drawing.SystemColors.Highlight;
                DrawLayers = false;
                DrawSplineLayers.BackColor = System.Drawing.SystemColors.Control;
                Draw.NewMinerals(MaterialsLayer[ComboBoxMineralMaterial.SelectedIndex]);
                UnFocus.Focus();
            }
            else
            {
                DrawMineral = false;
                DrawSplineMinerals.BackColor = System.Drawing.SystemColors.Control;
                if(!Draw.CheckPointsMinerals())
                    MessageBox.Show("Должно быть минимум 3 точки, точки удалены.");
                UnFocus.Focus();
            }
        }












        #endregion

        #endregion

        #endregion


    }
}
