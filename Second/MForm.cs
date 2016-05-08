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
        /*Флаг нажата ли правая кнопка мышки*/
        bool MouseDownRight;
        /*Позиция нажатия мышки*/
        Point MouseDownPosition;
        /*Попала ли мышка на опорную точку*/
        int[] CheckControlPoint;
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
            MouseDownRight = false;
            CheckControlPoint = new int[3];
            


            /*Аля чек*/
            MainPaint_HScroll.LargeChange = Draw.XAREASIZE + 1;
            MainPaint_HScroll.Maximum = Draw.XAREASIZE;
            MainPaint_VScroll.LargeChange = Draw.YAREASIZE + 1;
            MainPaint_VScroll.Maximum = Draw.YAREASIZE;
            Draw.EARTHSIZE = 0;
            Draw.XAREASIZE = 58200;
            //TabControl.Visible = true;

            //FirstStartButton.Visible = false;
            //TextBoxWidthArea.Visible = false;
            //TextBoxHeightEarth.Visible = false;
            //LabelHeightEarth.Visible = false;
            //LabelWidthArea.Visible = false;
            //FirstLabelMain.Visible = false;


            Draw.AddLayers(-30000, 13);
            Draw.AddLayers(-39900, 5);
            Draw.AddLayers(-100, 4);
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
            if (e.Y < 0 || e.Y > MainPaint.Height-GlobalConst.Difference || e.X < 0 || e.X > MainPaint.Width - GlobalConst.Difference)
                FlagMouseGlControl = false;
            else
                FlagMouseGlControl = true;
            if (FlagMouseGlControl)
            {
                /*Изменяем позицию мышки*/
                Draw.MOUSEPOSITION = new Point(e.X, e.Y);
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

        private void MainPaint_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                MouseDownLeft = true;
            }
            MouseDownPosition = new Point(e.X, e.Y);
            Draw.MOUSEPOSITION = new Point(e.X, e.Y);
            CheckControlPoint = Draw.CheckPoint(new Point(e.X, e.Y));
            if (CheckControlPoint[0] == 1 && e.Button == System.Windows.Forms.MouseButtons.Right)
                СontextMenuStrip.Show(Cursor.Position);
        }


        #region Начальное окно

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
        //    if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 8 && (e.KeyChar != 45 || TextBoxHeightEarth.TextLength > 0))
        //        e.Handled = true;
        //}

        #endregion

        #region Меню
        private void CloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion

        private void DeleteLayers_Click(object sender, EventArgs e)
        {
            Draw.DeleteLayers(CheckControlPoint[1]);
        }
    }
}
