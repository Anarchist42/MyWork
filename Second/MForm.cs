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
        /*Шаг для прокрутки*/
        double Wheel = 0.01;
        #endregion

        #region Константы
        /*Размер бардюра у GlControl*/
        const int Difference = 5;      
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

            Draw.EARTHSIZE = 100;
            Draw.XAREASIZE = 100000;
            FirstStartButton.Visible = false;
            TextBoxWidthArea.Visible = false;
            TextBoxHeightEarth.Visible = false;
            LabelHeightEarth.Visible = false;
            LabelWidthArea.Visible = false;
            FirstLabelMain.Visible = false;
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
            int ChangeWidth = this.Width - SizeForm.X;
            int ChangeHeight = this.Height - SizeForm.Y;
            /*Если основная часть программы не запущена (Не нажата первая кнопка "Start")*/
            if (FirstStartButton.Visible == true)
            {
                FirstStartButton.Location = new Point(FirstStartButton.Location.X + ChangeWidth, FirstStartButton.Location.Y);
                TextBoxWidthArea.Location = new Point(TextBoxWidthArea.Location.X + ChangeWidth, TextBoxWidthArea.Location.Y);
                TextBoxHeightEarth.Location = new Point(TextBoxHeightEarth.Location.X + ChangeWidth, TextBoxHeightEarth.Location.Y);
                LabelHeightEarth.Location = new Point(LabelHeightEarth.Location.X + ChangeWidth, LabelHeightEarth.Location.Y);
                LabelWidthArea.Location = new Point(LabelWidthArea.Location.X + ChangeWidth, LabelWidthArea.Location.Y);
                FirstLabelMain.Location = new Point(FirstLabelMain.Location.X + ChangeWidth, FirstLabelMain.Location.Y);
            }
            /*Изменяем размеры окна*/
            MainPaint.Width += ChangeWidth;
            MainPaint.Height += ChangeHeight;
            /*Настраиваем зум*/
            if (ChangeWidth > ChangeHeight) Draw.ZOOM = Draw.MINZOOM * Convert.ToDouble(MainPaint.Width - Difference) / Convert.ToDouble(SizeGlControl.X - Difference);
            else Draw.ZOOM = Draw.MINZOOM * Convert.ToDouble(MainPaint.Height - Difference) / Convert.ToDouble(SizeGlControl.Y - Difference);         
            /*Сохраняем размеры текущего окна*/
            SizeForm.X = this.Width;
            SizeForm.Y = this.Height;
            /*Настраиваем горизонтальный ползунок*/
            MainPaint_VScroll.Location = new Point(MainPaint_VScroll.Location.X + ChangeWidth, MainPaint_VScroll.Location.Y);
            MainPaint_VScroll.Size = new Size(MainPaint_VScroll.Size.Width, MainPaint.Height);
            MainPaint_VScroll.LargeChange = MainPaint.Height + 1;
            MainPaint_VScroll.Maximum = Draw.ScrollMaximum(0);
            /*Настраиваем вертикальный ползунок*/
            MainPaint_HScroll.Location = new Point(MainPaint_HScroll.Location.X, MainPaint_HScroll.Location.Y + ChangeHeight);
            MainPaint_HScroll.Size = new Size(MainPaint.Width, MainPaint_HScroll.Size.Height);
            MainPaint_HScroll.LargeChange = MainPaint.Width + 1;
            MainPaint_HScroll.Maximum = Draw.ScrollMaximum(1);
            /*Настраиваем отображение "нового" окна для функций Gl*/
            Gl.glViewport(0, 0, MainPaint.Width, MainPaint.Height);
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();
            Gl.glOrtho(-MainPaint.Width / 2.0, MainPaint.Width / 2.0, -MainPaint.Height / 2.0, MainPaint.Height / 2.0, -1, 1);
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
        }

        private void MainPaint_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
        }

        private void MainPaint_VScroll_Scroll(object sender, ScrollEventArgs e)
        {
        }

        private void MainPaint_HScroll_Scroll(object sender, ScrollEventArgs e)
        {
        }

        private void MainPaint_MouseMove(object sender, MouseEventArgs e)
        {
            if(FlagMouseGlControl)
                Draw.MOUSEPOSITION = new Point(e.X, e.Y);
        }

        private void MainPaint_MouseEnter(object sender, EventArgs e)
        {
            FlagMouseGlControl = true;
        }

        private void MainPaint_MouseLeave(object sender, EventArgs e)
        {
            FlagMouseGlControl = false;
        }

        private void MainPaint_MouseClick(object sender, MouseEventArgs e)
        {
        }

        private void MainPaint_MouseUp(object sender, MouseEventArgs e)
        {
        }

        private void MainPaint_MouseDown(object sender, MouseEventArgs e)
        {
            Draw.MOUSEPOSITION = new Point(e.X, e.Y);
        }

        private void NewLayer_Click(object sender, EventArgs e)
        {
        }

        private void FirstStartButton_Click(object sender, EventArgs e)
        {
            Draw.EARTHSIZE = (TextBoxHeightEarth.TextLength > 0) ? Convert.ToInt32(TextBoxHeightEarth.Text) : 0;
            Draw.XAREASIZE = (TextBoxWidthArea.TextLength > 0) ? Convert.ToInt32(TextBoxWidthArea.Text) : 1;
            FirstStartButton.Visible = false;
            TextBoxWidthArea.Visible = false;
            TextBoxHeightEarth.Visible = false;
            LabelHeightEarth.Visible = false;
            LabelWidthArea.Visible = false;
            FirstLabelMain.Visible = false;


            RenderTimer.Start();
        }

        private void TextBoxWidthArea_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 8)
                e.Handled = true;
        }

        private void TextBoxHeightEarth_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 8 && (e.KeyChar != 45 || TextBoxHeightEarth.TextLength > 0))
                e.Handled = true;
        }
    }
}
