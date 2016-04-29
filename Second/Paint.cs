using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

using Tao.DevIl;
using Tao.OpenGl;
using Tao.FreeGlut;
using Tao.Platform.Windows;

namespace Second
{
    class Paint
    {
        #region Поля класса
        #region Нужны Get\Set
        /*Зум*/
        double Zoom;
        /*Количество клеток на линии*/
        int CellsNumber;
        /*Смещение по оси X*/
        double XOffset;
        /*Смещение по оси Y*/
        double YOffset;
        /*Позиция мышки*/
        Point MousePosition;
        /*Размер исследуемой области по оси X*/
        int XAreaSize;
        /*Размер исследуемой области по оси Y*/
        int YAreaSize;
        /*Высота уровня земли*/
        int EarthSize;
        #endregion
        #region Не нужны Get\Set
        /*Указатель на окно где рисуем*/
        SimpleOpenGlControl GLPaint;
        /*Минимальный зум*/
        double MinZoom;
        /*Cлои почвы*/
        List<Layer> Layers = new List<Layer>();
        #endregion
        #endregion

        #region Константы класса
        /*Размер бардюра у GlControl*/
        const int Difference = 5;
        #endregion

        #region Конструктор
        public Paint(SimpleOpenGlControl Paint)
        {
            this.GLPaint = Paint;
            this.MousePosition = new Point(0, 0);
            this.EarthSize = 100;
            this.CellsNumber = 10;
            this.XOffset = 0.0;
            this.YOffset = 0.0;
            this.XAreaSize = 0;
            this.YAreaSize = 0;
        }
        #endregion

        #region SETs and GETs
        public double ZOOM
        {
            get { return this.Zoom; }
            set { this.Zoom = (value > MinZoom) ? value : MinZoom; }
        }
        public int CELLSNUMBER
        {
            get { return this.CellsNumber; }
            set
            {
                this.CellsNumber = (value > 0 && value < 20)
                  ? value : (value < 20) ? 0 : 20;
            }
        }
        public double XOFFSET
        {
            get { return this.XOffset; }
            set { this.XOffset = value; }
        }
        public double YOFFSET
        {
            get { return this.YOffset; }
            set { this.YOffset = value; }
        }
        public Point MOUSEPOSITION
        {
            get { return this.MousePosition; }
            set { this.MousePosition = value; }
        }
        public int XAREASIZE
        {
            get { return this.XAreaSize; }
            set 
            {
                this.XAreaSize = value;
                this.MinZoom = Convert.ToDouble((GLPaint.Width - Difference)) / Convert.ToDouble(XAreaSize);
                this.Zoom = this.MinZoom;
                this.YAreaSize = Convert.ToInt32((GLPaint.Height - Difference) / Zoom);
            }
        }
        public int YAREASIZE
        {
            get { return this.YAreaSize; }
            set
            {
                this.YAreaSize = value;
            }
        }
        public int EARTHSIZE
        {
            get { return this.EarthSize; }
            set { this.EarthSize = (value > 0) ? value : 0; }
        }
        #endregion

        #region Методы
        /*Метод отрсиовки сетки*/
        private void DrawingGrid()
        {
            int i,k;
            #region Сетка
            Gl.glPushMatrix();          
            /*так как максимум на линии - cells_number клеток, 
            то надо выбрать по Х или Y будем выбирать шаг*/
            int GridStep = (Convert.ToInt32(GLPaint.Width / CellsNumber / Zoom)
                > Convert.ToInt32(GLPaint.Height / CellsNumber / Zoom))
                ? Convert.ToInt32(GLPaint.Width / CellsNumber / Zoom)
                : Convert.ToInt32(GLPaint.Height / CellsNumber / Zoom);
            if (GridStep == 0) GridStep = 1;
            Gl.glLineWidth(1);
            Gl.glColor3d(0.8, 0.8, 0.8);
            double GridHalfDown = (-GLPaint.Height / 2 - YOffset) / Zoom;
            double GridHalfUp = (GLPaint.Height / 2 - YOffset) / Zoom;
            double GridHalfLeft = (-GLPaint.Width / 2 - XOffset) / Zoom;
            double GridHalfRight = (GLPaint.Width / 2 - XOffset) / Zoom;
            for (i = 0; i > GridHalfDown; i -= GridStep)
            {
                Gl.glBegin(Gl.GL_LINES);
                Gl.glVertex2d(GridHalfLeft, i);
                Gl.glVertex2d(GridHalfRight, i);
                Gl.glEnd();
            }
            for (i = 0; i <= GridHalfUp; i += GridStep)
            {
                Gl.glBegin(Gl.GL_LINES);
                Gl.glVertex2d(GridHalfLeft, i);
                Gl.glVertex2d(GridHalfRight, i);
                Gl.glEnd();
            }
            for (i = 0; i > GridHalfLeft; i -= GridStep)
            {
                Gl.glBegin(Gl.GL_LINES);
                Gl.glVertex2d(i, GridHalfDown);
                Gl.glVertex2d(i, GridHalfUp);
                Gl.glEnd();
            }
            for (i = 0; i <= GridHalfRight; i += GridStep)
            {
                Gl.glBegin(Gl.GL_LINES);
                Gl.glVertex2d(i, GridHalfDown);
                Gl.glVertex2d(i, GridHalfUp);
                Gl.glEnd();
            }
            Gl.glPopMatrix();
            #endregion

            #region Значение на координатных прямых
            Gl.glPushMatrix();
            Gl.glColor3d(0.3, 0.3, 0.3);
            int LabelStart;
            int x = XAreaSize / 2;
            int y = -YAreaSize / 2 + EarthSize;
            LabelStart = GridStep;
            Gl.glPointSize(3);
            for (i = -LabelStart, k = -1; i > GridHalfDown; i -= GridStep, k--)
            {
                Gl.glPushMatrix();
                Gl.glScaled(1 / Zoom, 1 / Zoom, 1 / Zoom);
                Gl.glBegin(Gl.GL_POINTS);
                Gl.glVertex2d(0.0, i * Zoom);
                Gl.glEnd();
                Gl.glTranslated(5.0, i * Zoom + 5.0, 0.0);
                Gl.glScaled(0.08, 0.08, 0.08);
                string number = (i + y).ToString();
                for (int j = 0; j < number.Length; j++)
                    Glut.glutStrokeCharacter(Glut.GLUT_STROKE_ROMAN, number[j]);
                Gl.glPopMatrix();
            }
            for (i = -LabelStart, k = -1; i > GridHalfLeft; i -= GridStep, k--)
            {
                Gl.glPushMatrix();
                Gl.glScaled(1 / Zoom, 1 / Zoom, 1 / Zoom);
                Gl.glBegin(Gl.GL_POINTS);
                Gl.glVertex2d(i * Zoom, 0.0);
                Gl.glEnd();
                if (i % (GridStep * 2) == 0)
                    Gl.glTranslated(i * Zoom + 5.0, 5.0, 0.0);
                else
                    Gl.glTranslated(i * Zoom + 5.0, -15.0, 0.0);
                Gl.glScaled(0.08, 0.08, 0.08);
                string number = (i + x).ToString();
                for (int j = 0; j < number.Length; j++)
                    Glut.glutStrokeCharacter(Glut.GLUT_STROKE_ROMAN, number[j]);
                Gl.glPopMatrix();
            }
            for (i = LabelStart, k = 1; i <= GridHalfUp; i += GridStep, k++)
            {
                Gl.glPushMatrix();
                Gl.glScaled(1 / Zoom, 1 / Zoom, 1 / Zoom);
                Gl.glBegin(Gl.GL_POINTS);
                Gl.glVertex2d(0.0, i * Zoom);
                Gl.glEnd();
                Gl.glTranslated(5.0, i * Zoom + 5.0, 0.0);
                Gl.glScaled(0.08, 0.08, 0.08);
                string number = (i + y).ToString();
                for (int j = 0; j < number.Length; j++)
                    Glut.glutStrokeCharacter(Glut.GLUT_STROKE_ROMAN, number[j]);
                Gl.glPopMatrix();
            }
            for (i = 0, k = 0; i <= GridHalfRight; i += GridStep, k++)
            {
                Gl.glPushMatrix();
                Gl.glScaled(1 / Zoom, 1 / Zoom, 1 / Zoom);
                Gl.glBegin(Gl.GL_POINTS);
                Gl.glVertex2d(i * Zoom, 0.0);
                Gl.glEnd();
                if (i % (GridStep * 2) == 0)
                    Gl.glTranslated(i * Zoom + 5.0, 5.0, 0.0);
                else
                    Gl.glTranslated(i * Zoom + 5.0, -15.0, 0.0);
                Gl.glScaled(0.08, 0.08, 0.08);
                string number = (i + x).ToString();
                for (int j = 0; j < number.Length; j++)
                    Glut.glutStrokeCharacter(Glut.GLUT_STROKE_ROMAN, number[j]);
                Gl.glPopMatrix();
            }
            Gl.glPopMatrix();
            #endregion

            #region Координаты
            /*Фон для координат*/
            Gl.glPushMatrix();
            Gl.glColor3d(1.0, 1.0, 1.0);
            Gl.glPushMatrix();
            Gl.glBegin(Gl.GL_QUADS);
            Gl.glVertex2d(GLPaint.Width / 2 - 100, GLPaint.Height / 2);
            Gl.glVertex2d(GLPaint.Width / 2 + 100, GLPaint.Height / 2);
            Gl.glVertex2d(GLPaint.Width / 2 + 100, GLPaint.Height / 2 - 30);
            Gl.glVertex2d(GLPaint.Width / 2 - 100, GLPaint.Height / 2 - 30);
            Gl.glEnd();
            Gl.glPopMatrix();
            /*Границы*/
            Gl.glPushMatrix();
            Gl.glColor3d(0.0, 0.0, 0.0);           
            Gl.glBegin(Gl.GL_LINES);
            Gl.glVertex2d(GLPaint.Width / 2 - 100, GLPaint.Height / 2);
            Gl.glVertex2d(GLPaint.Width / 2, GLPaint.Height / 2);
            Gl.glVertex2d(GLPaint.Width / 2, GLPaint.Height / 2);
            Gl.glVertex2d(GLPaint.Width / 2, GLPaint.Height / 2 - 30);
            Gl.glVertex2d(GLPaint.Width / 2, GLPaint.Height / 2 - 30);
            Gl.glVertex2d(GLPaint.Width / 2 - 100, GLPaint.Height / 2 - 30);
            Gl.glVertex2d(GLPaint.Width / 2 - 100, GLPaint.Height / 2 - 30);
            Gl.glVertex2d(GLPaint.Width / 2 - 100, GLPaint.Height / 2);
            Gl.glEnd();
            Gl.glPopMatrix();
            /*Координаты*/
            Gl.glColor3d(0.0, 0.0, 0.0);
            Gl.glPushMatrix();
            Gl.glTranslated(GLPaint.Width / 2 - 95, GLPaint.Height / 2 - 16, 0.0);
            Gl.glScaled(0.10, 0.10, 0.10);
            string coord = "X:"+ Math.Round(((XAreaSize * Zoom - GLPaint.Width + Difference) / 2 - XOffset + MousePosition.X) / Zoom, 2).ToString();
            for (int j = 0; j < coord.Length; j++)
                Glut.glutStrokeCharacter(Glut.GLUT_STROKE_ROMAN, coord[j]);
            Gl.glPopMatrix();
            Gl.glPushMatrix();
            Gl.glTranslated(GLPaint.Width / 2 - 95, GLPaint.Height / 2 - 28, 0.0);
            Gl.glScaled(0.10, 0.10, 0.10);
            coord = "Y:"+ Math.Round((EarthSize - ((YAreaSize * Zoom - GLPaint.Height + Difference) / 2 + YOffset + MousePosition.Y) / Zoom), 2).ToString();
            for (int j = 0; j < coord.Length; j++)
                Glut.glutStrokeCharacter(Glut.GLUT_STROKE_ROMAN, coord[j]);
            Gl.glPopMatrix();
            Gl.glPopMatrix();
            #endregion
        }
        #region Потом

        public void add_layers(int XAreaSize, int layer_height, int NumberOfPoints)
        {
        }


        public void reset()
        {
        }

        public void change_pointY(Point mouse, int[] ij)
        {
        }   

        

        private Point GetPoint(int i, List<Point> point)
        {
            if (i < 0)
                return point[0];
            if (i < point.Count)
                return point[i];
            return point[point.Count - 1];
        }
        private void drawing_layers()
        {
            
        }
        #endregion
        #endregion
        /*Основной метод класса, вызывает отрисовку*/
        public void Draw()
        {
            Gl.glClearColor(1.0f, 1.0f, 1.0f, 0.0f);
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
            Gl.glPushMatrix();
            Gl.glTranslated(XOffset, YOffset, 0);
            Gl.glScaled(Zoom, Zoom, Zoom);
            DrawingGrid();

            Gl.glPopMatrix();
            Gl.glFinish();
            GLPaint.Invalidate();
        }
    }
}
