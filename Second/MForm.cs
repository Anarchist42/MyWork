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
        /*Переменная для отрисовки*/
        Paint draw;
        /*шаг для прокрутки*/
        double wheel = 0.01;
        
        //*******************************************************************//
        //*******************************************************************//
        //*******************************************************************//
        public MainForm()
        {
            InitializeComponent();
            MainPaint.InitializeContexts();
            /*инициализация компонент*/
            draw = new Paint(MainPaint);
            
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
            /*старт таймера для рендера*/
            RenderTimer.Start();
        }


        private void RenderTimer_Tick(object sender, EventArgs e)
        {
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
        }
        
        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
        }

        private void MainPaint_MouseEnter(object sender, EventArgs e)
        {
        }

        private void MainPaint_MouseLeave(object sender, EventArgs e)
        {
        }

        private void MainPaint_MouseClick(object sender, MouseEventArgs e)
        {         
        }

        private void MainPaint_MouseUp(object sender, MouseEventArgs e)
        {
        }

        private void MainPaint_MouseDown(object sender, MouseEventArgs e)
        {
        }

        private void NewLayer_Click(object sender, EventArgs e)
        {
        }
    }
}
