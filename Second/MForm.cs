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



namespace Second
{

    public partial class MainForm : Form
    {
        #region Поля класса
        /*Переменная для отрисовки*/
        Paint Draw;
        /*Исходный размер окна*/
        Point SizeForm;
        /*Исходный размер GlControl*/
        Point SizeGlControl;
        /*Флаг находился ли мышка на GlControl*/
        bool FlagMouseGlControl;
        /*Флаг нажата ли левая кнопка мышки*/
        bool MouseDownLeft;
        /*Позиция нажатия мышки*/
        Point MouseDownPosition;
        /*Попала ли мышка на опорную точку*/
        int[] CheckControlPoint;
        /*Флаг нажата ли кнопка "Нарисовать" слой почвы*/
        bool DrawLayers;
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
            SizeForm = new Point(this.Width, this.Height);
            SizeGlControl = new Point(MainPaint.Width, MainPaint.Height);
            FlagMouseGlControl = false;
            MouseDownLeft = false;
            CheckControlPoint = new int[3];
            
            //FirstStartButton.Visible = false;
            //TextBoxWidthArea.Visible = false;
            //TextBoxHeightEarth.Visible = false;
            //LabelHeightEarth.Visible = false;
            //LabelWidthArea.Visible = false;
            //FirstLabelMain.Visible = false;




            /*Аля чек*/
            MainPaint_HScroll.LargeChange = Draw.XAREASIZE + 1;
            MainPaint_HScroll.Maximum = Draw.XAREASIZE;
            MainPaint_VScroll.LargeChange = Draw.YAREASIZE + 1;
            MainPaint_VScroll.Maximum = Draw.YAREASIZE;
            Draw.EARTHSIZE = 0;
            Draw.XAREASIZE = 58200;
            TabControl.Visible = true;
            DrawLayers = false;
            


            //DataGridViewLayers.Columns[2].ValueType = typeof(int);
            //DataGridViewLayers.Columns[3].ValueType = typeof(uint);
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



            RenderTimer.Start();
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

        #region Начальное окно

        /*Добавить максимально допустимые значения кол-ва точек*/


        //private void FirstStartButton_Click(object sender, EventArgs e)
        //{
        //    /*Передаем введеные параметры*/
        //    Draw.EARTHSIZE = (TextBoxHeightEarth.TextLength > 0) ? Convert.ToInt32(TextBoxHeightEarth.Text) : 0;
        //    Draw.XAREASIZE = (TextBoxWidthArea.TextLength > 0) ? Convert.ToInt32(TextBoxWidthArea.Text) : 1;
        //    /*Настраиваем значения ползунков*/
        //    MainPaint_HScroll.LargeChange = Draw.XAREASIZE + 1;
        //    MainPaint_HScroll.Maximum = Draw.XAREASIZE;
        //    MainPaint_VScroll.LargeChange = Draw.YAREASIZE + 1;
        //    MainPaint_VScroll.Maximum = Draw.YAREASIZE;
        //    /*Убираем ненужные объекты интерфейса*/
        //    FirstStartButton.Visible = false;
        //    TextBoxWidthArea.Visible = false;
        //    TextBoxHeightEarth.Visible = false;
        //    LabelHeightEarth.Visible = false;
        //    LabelWidthArea.Visible = false;
        //    FirstLabelMain.Visible = false;
        //    TabControl.Visible = true;
        //    /*Старт всей отрисовки*/
        //    RenderTimer.Start();
        //}

        //private void TextBoxWidthArea_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 8)
        //        e.Handled = true;
        //}

        //private void TextBoxHeightEarth_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 8)
        //        e.Handled = true;
        //}

        #endregion

        #region Работа с MainPaint

        #region Контексное меню
        private void AddValueToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void AddPointToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void DeletePointToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void DeleteLayersMainPaint_Click(object sender, EventArgs e)
        {
            int i,
            /*Удаляем слой*/
            IndexNumber = Draw.DeleteLayersNumber(CheckControlPoint[1]),
            /*Старое значение максимального значения ползунка*/
            MaximumScroll = MainPaint_VScroll.Maximum;
            /*Изменяем ползунки*/
            MainPaint_VScroll.LargeChange = Draw.YAREASIZE + 1;            
            MainPaint_VScroll.Maximum = Draw.ScrollMaximum(0);
            Draw.YOFFSET += (MaximumScroll - MainPaint_VScroll.Maximum ) * Draw.ZOOM;
            MainPaint_VScroll.Value = Draw.ScrollValue(0);
            /*Удаляем строку в таблице*/
            DataGridViewLayers.Rows.RemoveAt(IndexNumber);
            /*Уменьшаем индекс в таблице*/
            for (i = IndexNumber; i < DataGridViewLayers.Rows.Count - 1; i++)
                DataGridViewLayers.Rows[i].Cells[0].Value = Convert.ToInt32(DataGridViewLayers.Rows[i].Cells[0].Value) - 1;
        }
        #endregion

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState != FormWindowState.Minimized)
            {
                /*Изменяем размеры GlControl*/
                MainPaint.Width = this.Width - SizeForm.X + SizeGlControl.X;
                MainPaint.Height = this.Height - SizeForm.Y + SizeGlControl.Y;
                /*Настраиваем зум*/
                if (Convert.ToDouble(MainPaint.Width) / Convert.ToDouble(SizeGlControl.X)
                    > Convert.ToDouble(MainPaint.Height) / Convert.ToDouble(SizeGlControl.Y))
                {
                    Draw.MINZOOM = GlobalConst.MinZoom * Convert.ToDouble(MainPaint.Width - GlobalConst.Difference)
                        / Convert.ToDouble(SizeGlControl.X - GlobalConst.Difference);
                    MainPaint_VScroll.Maximum = Draw.ScrollMaximum(0);
                    MainPaint_HScroll.Maximum = Draw.XAREASIZE;
                }
                else
                {
                    Draw.MINZOOM = GlobalConst.MinZoom * Convert.ToDouble(MainPaint.Height - GlobalConst.Difference)
                        / Convert.ToDouble(SizeGlControl.Y - GlobalConst.Difference);
                    MainPaint_HScroll.Maximum = Draw.ScrollMaximum(1);
                    MainPaint_VScroll.Maximum = Draw.YAREASIZE;
                }
                /*Настраиваем вертикальный ползунок*/
                MainPaint_VScroll.Size = new Size(MainPaint_VScroll.Size.Width, MainPaint.Height);
                MainPaint_VScroll.LargeChange = Draw.YAREASIZE + 1;
                MainPaint_VScroll.Value = (MainPaint_VScroll.Maximum - MainPaint_VScroll.LargeChange) / 2 + 1;
                /*Настраиваем горизонтальный ползунок*/
                MainPaint_HScroll.Size = new Size(MainPaint.Width, MainPaint_HScroll.Size.Height);
                MainPaint_HScroll.LargeChange = Draw.XAREASIZE + 1;
                MainPaint_HScroll.Value = (MainPaint_HScroll.Maximum - MainPaint_HScroll.LargeChange) / 2 + 1;
                /*Настраиваем панель с параметрами*/
                TabControl.Size = new Size(TabControl.Size.Width, MainPaint.Height);
                /*Сбрасываем сдвиги по осям*/
                Draw.XOFFSET = 0.0;
                Draw.YOFFSET = 0.0;
                /*Настраиваем отображение "нового" окна для функций Gl*/
                Gl.glViewport(0, 0, MainPaint.Width, MainPaint.Height);
                Gl.glMatrixMode(Gl.GL_PROJECTION);
                Gl.glLoadIdentity();
                Gl.glOrtho(-MainPaint.Width / 2.0, MainPaint.Width / 2.0, -MainPaint.Height / 2.0, MainPaint.Height / 2.0, -1, 1);
                Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
            }
        }

        private void MainPaint_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            /*Проверяем находится ли мышь на рабочей области*/
            if (FlagMouseGlControl)
            {
                /*Масштабирование*/
                if (e.Delta > 0)
                {
                    /*Изменяем зум*/
                    Draw.ZOOM += GlobalConst.ZoomWheel;
                    /*Изменяем смещение по осям с фиксированием точки приближения*/
                    Draw.ChangeOffsetZoomIn();
                }
                else
                {
                    /*Изменяем зум*/
                    Draw.ZOOM -= GlobalConst.ZoomWheel;
                    /*Изменяем смещение по осям с фиксированием точки приближения*/
                    Draw.ChangeOffsetZoomOut();
                }
                /*Изменяем ползунки*/
                MainPaint_VScroll.LargeChange = Draw.YAREASIZE + 1;
                MainPaint_VScroll.Maximum = Draw.ScrollMaximum(0);
                MainPaint_VScroll.Value = Draw.ScrollValue(0);
                MainPaint_HScroll.LargeChange = Draw.XAREASIZE + 1;
                MainPaint_HScroll.Maximum = Draw.ScrollMaximum(1);
                MainPaint_HScroll.Value = Draw.ScrollValue(1);
            }
        }

        private void MainPaint_VScroll_Scroll(object sender, ScrollEventArgs e)
        {
            Draw.YOFFSET += (e.NewValue - e.OldValue) * Draw.ZOOM;
        }

        private void MainPaint_HScroll_Scroll(object sender, ScrollEventArgs e)
        {
            Draw.XOFFSET += (e.OldValue - e.NewValue) * Draw.ZOOM;
        }

        private void MainPaint_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Y < 0 || e.Y > MainPaint.Height - GlobalConst.Difference || e.X < 0 || e.X > MainPaint.Width - GlobalConst.Difference)
                FlagMouseGlControl = false;
            else
                FlagMouseGlControl = true;
            if (FlagMouseGlControl)
            {
                /*Изменяем позицию мышки*/
                Draw.MOUSEPOSITION = new Point(e.X, e.Y);
                /*Если нажата кнопка нарисовать слой (нельзя двигать)*/
                if (DrawLayers == true)
                    return;
                if (MouseDownLeft)
                {
                    if (CheckControlPoint[0] == 1)
                        Draw.ChangePoint(MouseDownPosition, CheckControlPoint);
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

        private void MainPaint_MouseEnter(object sender, EventArgs e)
        {
            MainPaint.Focus();
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
                /*Если кнопка "Нарисовать" новый слой "включена"*/
                if (DrawLayers == true)
                {
                    /*Рисуем новый слой*/
                    Draw.AddLayers(Draw.GetLayerHeight(), Convert.ToInt32(TextBoxLayerNumberOfPoints.Text));
                    /*Заполняем таблицу*/
                    if (DataGridViewLayers.Rows.Count == 1)
                    {
                        DataGridViewLayers.Rows.Add(1, "", Draw.GetLayerHeight(), Convert.ToInt32(TextBoxLayerNumberOfPoints.Text));
                        DataGridViewLayers.Rows[0].Cells[1].Style.BackColor = Draw.GetLayerColor(Convert.ToInt32(DataGridViewLayers.Rows[0].Cells[0].Value));
                    }
                    else
                    {
                        DataGridViewLayers.Rows.Add(DataGridViewLayers.Rows.Count, "", Draw.GetLayerHeight(), Convert.ToInt32(TextBoxLayerNumberOfPoints.Text));
                        DataGridViewLayers.Rows[DataGridViewLayers.Rows.Count - 2].Cells[1].Style.BackColor = Draw.GetLayerColor(Convert.ToInt32(DataGridViewLayers.Rows[DataGridViewLayers.Rows.Count - 2].Cells[0].Value));
                    }
                }
            }
            /*Проверяем попали ли мы на опорную точку*/
            CheckControlPoint = Draw.CheckPoint(new Point(e.X, e.Y));
            /*Если попали и нажата правая кнопка мыши, то вызываем контексное меню*/
            if (CheckControlPoint[0] == 1 && e.Button == System.Windows.Forms.MouseButtons.Right)
                СontextMenuMainPaint.Show(Cursor.Position);
        }

        #endregion

        #region Почва

        #region Контекстное меню
        private void DeleteLayersDataGridViewLayers_Click(object sender, EventArgs e)
        {
            int i,
            /*Старое значение максимального значения ползунка*/
            MaximumScroll = MainPaint_VScroll.Maximum;
            /*Удаляем слой*/
            Draw.DeleteLayersIndex(CheckControlPoint[1]);           
            /*Изменяем ползунки*/
            MainPaint_VScroll.LargeChange = Draw.YAREASIZE + 1;
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
                LabelLayerHeight.Focus();
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
            Draw.AddLayers(Convert.ToInt32(TextBoxLayerHeight.Text), Convert.ToInt32(TextBoxLayerNumberOfPoints.Text));
            /*Изменяем ползунки*/
            MainPaint_VScroll.LargeChange = Draw.YAREASIZE + 1;
            MainPaint_VScroll.Maximum = Draw.ScrollMaximum(0);
            MainPaint_VScroll.Value = Draw.ScrollValue(0);
            /*Заполняем таблицу*/
            if (DataGridViewLayers.Rows.Count == 1)
            {
                DataGridViewLayers.Rows.Add(1, "", Convert.ToInt32(TextBoxLayerHeight.Text), Convert.ToInt32(TextBoxLayerNumberOfPoints.Text));
                DataGridViewLayers.Rows[0].Cells[1].Style.BackColor = Draw.GetLayerColor(Convert.ToInt32(DataGridViewLayers.Rows[0].Cells[0].Value));
            }
            else
            {
                DataGridViewLayers.Rows.Add(DataGridViewLayers.Rows.Count, "", Convert.ToInt32(TextBoxLayerHeight.Text), Convert.ToInt32(TextBoxLayerNumberOfPoints.Text));
                DataGridViewLayers.Rows[DataGridViewLayers.Rows.Count - 2].Cells[1].Style.BackColor = Draw.GetLayerColor(Convert.ToInt32(DataGridViewLayers.Rows[DataGridViewLayers.Rows.Count - 2].Cells[0].Value));
            }
        }

        private void TextBoxLayerNumberOfPoints_KeyPress(object sender, KeyPressEventArgs e)
        {
            /*Можно вводить только числа и бэкспейс*/
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 8)
                e.Handled = true;
            /*При изменение значений делаем кнопку "Выключенной", в целях проверки введенного числа*/
            DrawLayers = false;
            DrawSplineLayers.BackColor = System.Drawing.SystemColors.Control;
        }

        private void TextBoxLayerHeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            /*Можно вводить только числа и бэкспейс*/
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 8)
                e.Handled = true;
        }

        private void DataGridViewLayers_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex != DataGridViewLayers.Rows.Count - 1)
            {
                DataGridViewLayers.Rows[e.RowIndex].Selected = true;
                CheckControlPoint[1] =  e.RowIndex;
                ContextMenuDataGridViewLayers.Show(Cursor.Position);
            }
        }

        private void DataGridViewLayers_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            DataGridViewLayers.Rows[e.RowIndex].ErrorText = "";
            int newInteger;
            if (DataGridViewLayers.Rows[e.RowIndex].IsNewRow)
                return;
            if (DataGridViewLayers.CurrentCell.ColumnIndex == 2)
            {
                if (!int.TryParse(e.FormattedValue.ToString(), out newInteger) || newInteger >= 0)
                {
                    e.Cancel = true;
                    DataGridViewLayers.Rows[e.RowIndex].ErrorText = "Значение должно быть меньше или равно 0";
                }
                return;
            }
            if (DataGridViewLayers.CurrentCell.ColumnIndex == 3)
            {
                if (!int.TryParse(e.FormattedValue.ToString(), out newInteger) || newInteger >= Draw.XAREASIZE)
                {
                    e.Cancel = true;
                    DataGridViewLayers.Rows[e.RowIndex].ErrorText = "Количество опорных точек должно быть меньше или равно размеру области";
                }
                return;
            }
        }

        #endregion

        

        private void СheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            /*Если нажата Сетка*/
            if(e.Index == 0)
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
            /*Если нажата Опорные линии*/
            if (e.Index == 2)
            {
                if (e.NewValue == CheckState.Checked)
                    Draw.SUPPORTLINE = true;
                else Draw.SUPPORTLINE = false;
            }
        }

        #region Минералы

        private void DrawSplineMinerals_Click(object sender, EventArgs e)
        {

        }

        #endregion


    }
}
