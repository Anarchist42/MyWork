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
    public class GlobalConst
    {
        #region Константы
        /*Размер бардюра у GlControl*/
        public static int Difference = 5;
        /*Шаг для прокрутки*/
        public static double ZoomWheel = 0.01;
        /*Минимальный зум для окна*/
        public static double MinZoom;
        #endregion
    }

    class Paint
    {
        #region Поля класса

        #region Нужны Get\Set
        /*Зум*/
        double Zoom;
        /*Минимальный зум*/
        double MinZoom;
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
        /*Cлои почвы*/
        List<Layer> Layers = new List<Layer>();
        #endregion

        #endregion

        #region Конструктор
        public Paint(SimpleOpenGlControl Paint)
        {
            this.GLPaint = Paint;
            this.MousePosition = new Point(0, 0);
            this.EarthSize = 0;
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
        public double MINZOOM
        {
            get { return this.MinZoom; }
            set { this.MinZoom = value; this.Zoom = value; }
        }
        public int CELLSNUMBER
        {
            get { return this.CellsNumber; }
            set
            {
                this.CellsNumber = (value > 0 && value < 20)
                  ? value : (value < 20) ? 1 : 20;
            }
        }
        public double XOFFSET
        {
            get { return this.XOffset; }
            set
            {
                this.XOffset = (Math.Round((XAreaSize * Zoom - GLPaint.Width + GlobalConst.Difference) / 2 - Math.Abs(value), 5) >= 0)
                ? value
                : (XAreaSize * Zoom - GLPaint.Width + GlobalConst.Difference) / 2 * value / Math.Abs(value);
            }
        }
        public double YOFFSET
        {
            get { return this.YOffset; }
            set
            {
                this.YOffset = (Math.Round((YAreaSize * Zoom - GLPaint.Height + GlobalConst.Difference) / 2 - Math.Abs(value), 5) >= 0)
                ? value
                : (YAreaSize * Zoom - GLPaint.Height + GlobalConst.Difference) / 2 * value / Math.Abs(value);
            }
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
                this.MinZoom = Convert.ToDouble((GLPaint.Width - GlobalConst.Difference)) / Convert.ToDouble(XAreaSize);
                this.Zoom = this.MinZoom;
                GlobalConst.MinZoom = this.MinZoom;
                this.YAreaSize = Convert.ToInt32((GLPaint.Height - GlobalConst.Difference) / Zoom);
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
            int i, k;
            #region Сетка
            Gl.glPushMatrix();
            /*так как максимум на линии - cells_number клеток, 
            то надо выбрать по Х или Y будем выбирать шаг*/
            int GridStep = (Convert.ToInt32(GLPaint.Width / CellsNumber)
                > Convert.ToInt32(GLPaint.Height / CellsNumber))
                ? Convert.ToInt32(GLPaint.Width / CellsNumber)
                : Convert.ToInt32(GLPaint.Height / CellsNumber);
            if (GridStep == 0) GridStep = 1;
            Gl.glLineWidth(1);
            Gl.glColor3d(0.8, 0.8, 0.8);
            double GridHalfDown = (-GLPaint.Height / 2 - YOffset);
            double GridHalfUp = (GLPaint.Height / 2 - YOffset);
            double GridHalfLeft = (-GLPaint.Width / 2 - XOffset);
            double GridHalfRight = (GLPaint.Width / 2 - XOffset);
            /*Скалируем до 1*/
            Gl.glScaled(1 / Zoom, 1 / Zoom, 1 / Zoom);
            for (i = 0; i > GridHalfDown; i -= GridStep)
            {
                Gl.glBegin(Gl.GL_LINES);
                Gl.glVertex2d(GridHalfLeft, i - 2);
                Gl.glVertex2d(GridHalfRight, i - 2);
                Gl.glEnd();
            }
            for (i = 0; i <= GridHalfUp; i += GridStep)
            {
                Gl.glBegin(Gl.GL_LINES);
                Gl.glVertex2d(GridHalfLeft, i - 2);
                Gl.glVertex2d(GridHalfRight, i - 2);
                Gl.glEnd();
            }
            for (i = 0; i > GridHalfLeft; i -= GridStep)
            {
                Gl.glBegin(Gl.GL_LINES);
                Gl.glVertex2d(i - 2, GridHalfDown);
                Gl.glVertex2d(i - 2, GridHalfUp);
                Gl.glEnd();
            }
            for (i = 0; i < GridHalfRight; i += GridStep)
            {
                Gl.glBegin(Gl.GL_LINES);
                Gl.glVertex2d(i - 2, GridHalfDown);
                Gl.glVertex2d(i - 2, GridHalfUp);
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
                Gl.glVertex2d(-2.0, i - 2);
                Gl.glEnd();
                Gl.glTranslated(5.0, i + 5.0, 0.0);
                Gl.glScaled(0.08, 0.08, 0.08);
                string number = Convert.ToInt32((i / Zoom + y)).ToString();
                for (int j = 0; j < number.Length; j++)
                    Glut.glutStrokeCharacter(Glut.GLUT_STROKE_ROMAN, number[j]);
                Gl.glPopMatrix();
            }
            for (i = -LabelStart, k = -1; i > GridHalfLeft; i -= GridStep, k--)
            {
                Gl.glPushMatrix();
                Gl.glScaled(1 / Zoom, 1 / Zoom, 1 / Zoom);
                Gl.glBegin(Gl.GL_POINTS);
                Gl.glVertex2d(i - 2, -2.0);
                Gl.glEnd();
                if (i % (GridStep * 2) == 0)
                    Gl.glTranslated(i + 5.0, 5.0, 0.0);
                else
                    Gl.glTranslated(i + 5.0, -15.0, 0.0);
                Gl.glScaled(0.08, 0.08, 0.08);
                string number = Convert.ToInt32((i / Zoom + x)).ToString();
                for (int j = 0; j < number.Length; j++)
                    Glut.glutStrokeCharacter(Glut.GLUT_STROKE_ROMAN, number[j]);
                Gl.glPopMatrix();
            }
            for (i = LabelStart, k = 1; i <= GridHalfUp; i += GridStep, k++)
            {
                Gl.glPushMatrix();
                Gl.glScaled(1 / Zoom, 1 / Zoom, 1 / Zoom);
                Gl.glBegin(Gl.GL_POINTS);
                Gl.glVertex2d(-2.0, i - 2);
                Gl.glEnd();
                Gl.glTranslated(5.0, i + 5.0, 0.0);
                Gl.glScaled(0.08, 0.08, 0.08);
                string number = Convert.ToInt32((i / Zoom + y)).ToString();
                for (int j = 0; j < number.Length; j++)
                    Glut.glutStrokeCharacter(Glut.GLUT_STROKE_ROMAN, number[j]);
                Gl.glPopMatrix();
            }
            for (i = 0, k = 0; i <= GridHalfRight; i += GridStep, k++)
            {
                Gl.glPushMatrix();
                Gl.glScaled(1 / Zoom, 1 / Zoom, 1 / Zoom);
                Gl.glBegin(Gl.GL_POINTS);
                Gl.glVertex2d(i - 2, -2.0);
                Gl.glEnd();
                if (i % (GridStep * 2) == 0)
                    Gl.glTranslated(i + 5.0, 5.0, 0.0);
                else
                    Gl.glTranslated(i + 5.0, -15.0, 0.0);
                Gl.glScaled(0.08, 0.08, 0.08);
                string number = Convert.ToInt32((i / Zoom + x)).ToString();
                for (int j = 0; j < number.Length; j++)
                    Glut.glutStrokeCharacter(Glut.GLUT_STROKE_ROMAN, number[j]);
                Gl.glPopMatrix();
            }
            Gl.glPopMatrix();
            #endregion

            #region Координаты

            /*Скалируем до 1*/
            Gl.glScaled(1 / Zoom, 1 / Zoom, 1 / Zoom);
            /*Переносим центр оси*/
            Gl.glTranslated(-XOffset, -YOffset, 0);
            /*Пробел, X\Y, :, -, Максимальная длина числа, два знака после запятой*/
            int LengthCoords = (4 + XAreaSize.ToString().Length + 3) * 8;
            /*Фон для координат*/
            Gl.glPushMatrix();
            Gl.glColor3d(1.0, 1.0, 1.0);
            Gl.glPushMatrix();
            Gl.glBegin(Gl.GL_QUADS);
            Gl.glVertex2d(GLPaint.Width / 2 - LengthCoords, GLPaint.Height / 2);
            Gl.glVertex2d(GLPaint.Width / 2 - 4, GLPaint.Height / 2);
            Gl.glVertex2d(GLPaint.Width / 2 - 4, GLPaint.Height / 2 - 30);
            Gl.glVertex2d(GLPaint.Width / 2 - LengthCoords, GLPaint.Height / 2 - 30);
            Gl.glEnd();
            Gl.glPopMatrix();
            /*Границы*/
            Gl.glPushMatrix();
            Gl.glColor3d(0.0, 0.0, 0.0);
            Gl.glBegin(Gl.GL_LINES);
            Gl.glVertex2d(GLPaint.Width / 2 - LengthCoords, GLPaint.Height / 2);
            Gl.glVertex2d(GLPaint.Width / 2 - 4, GLPaint.Height / 2);
            Gl.glVertex2d(GLPaint.Width / 2 - 4, GLPaint.Height / 2);
            Gl.glVertex2d(GLPaint.Width / 2 - 4, GLPaint.Height / 2 - 30);
            Gl.glVertex2d(GLPaint.Width / 2 - 4, GLPaint.Height / 2 - 30);
            Gl.glVertex2d(GLPaint.Width / 2 - LengthCoords, GLPaint.Height / 2 - 30);
            Gl.glVertex2d(GLPaint.Width / 2 - LengthCoords, GLPaint.Height / 2 - 30);
            Gl.glVertex2d(GLPaint.Width / 2 - LengthCoords, GLPaint.Height / 2);
            Gl.glEnd();
            Gl.glPopMatrix();
            /*Координаты*/
            Gl.glColor3d(0.0, 0.0, 0.0);
            Gl.glPushMatrix();
            Gl.glTranslated(GLPaint.Width / 2 - LengthCoords + 5, GLPaint.Height / 2 - 16, 0.0);
            Gl.glScaled(0.10, 0.10, 0.10);
            string Coordinate = "X: " + Math.Round(((XAreaSize * Zoom - GLPaint.Width + GlobalConst.Difference) / 2 - XOffset + MousePosition.X) / Zoom, 2).ToString();
            for (int j = 0; j < Coordinate.Length; j++)
                Glut.glutStrokeCharacter(Glut.GLUT_STROKE_ROMAN, Coordinate[j]);
            Gl.glPopMatrix();
            Gl.glPushMatrix();
            Gl.glTranslated(GLPaint.Width / 2 - LengthCoords + 5, GLPaint.Height / 2 - 28, 0.0);
            Gl.glScaled(0.10, 0.10, 0.10);
            int CoordinateY = Convert.ToInt32((EarthSize - ((YAreaSize * Zoom - GLPaint.Height + GlobalConst.Difference) / 2 + YOffset + MousePosition.Y) / Zoom));
            Coordinate = (CoordinateY >= 0) ? "Y: " + CoordinateY.ToString() : "Y:" + CoordinateY.ToString();
            for (int j = 0; j < Coordinate.Length; j++)
                Glut.glutStrokeCharacter(Glut.GLUT_STROKE_ROMAN, Coordinate[j]);
            Gl.glPopMatrix();
            Gl.glPopMatrix();
            #endregion
        }

        #region Передача параметров
        /*Вычисление максимального значения ползунка*/
        /*Параметры: TypeScroll ( 0 - для вертикального, 1 - для горизонтального)*/
        public int ScrollMaximum(int TypeScroll)
        {
            if (TypeScroll == 0)
                return
                    /*Исходный размер*/
                    YAreaSize +
                    /*Сколько вверх*/
                    Convert.ToInt32(((YAreaSize * Zoom - GLPaint.Height + GlobalConst.Difference) / 2 + YOffset) / Zoom) +
                    /*Сколько вниз*/
                    YAreaSize - Convert.ToInt32(((YAreaSize * Zoom - GLPaint.Height + GlobalConst.Difference) / 2 + YOffset + GLPaint.Height - GlobalConst.Difference) / Zoom);
            else
                return
                   /*Исодный размер*/
                   XAreaSize +
                   /*Сколько влево*/
                   Convert.ToInt32(((XAreaSize * Zoom - GLPaint.Width + GlobalConst.Difference) / 2 - XOffset) / Zoom) +
                   /*Сколько вправо*/
                   XAreaSize - Convert.ToInt32(((XAreaSize * Zoom - GLPaint.Width + GlobalConst.Difference) / 2 - XOffset + GLPaint.Width - GlobalConst.Difference) / Zoom);
        }

        /*Вычисление значения ползунка*/
        /*Параметры: TypeScroll ( 0 - для вертикального, 1 - для горизонтального)*/
        public int ScrollValue(int TypeScroll)
        {
            int Result;
            if (TypeScroll == 0)
            {
                Result = Convert.ToInt32(((YAreaSize * Zoom - GLPaint.Height + GlobalConst.Difference) / 2 + YOffset) / Zoom);
                return
                    Result > 0 ? Result : 0;
            }
            else
            {
                Result = Convert.ToInt32(((XAreaSize * Zoom - GLPaint.Width + GlobalConst.Difference) / 2 - XOffset) / Zoom);
                return
                    Result > 0 ? Result : 0;
            }
        }

        /*Изменение сдвига по осям (приближение)*/
        public void ChangeOffsetZoomIn()
        {
            this.YOFFSET =
                   (((YAreaSize * (Zoom - GlobalConst.ZoomWheel) - GLPaint.Height + GlobalConst.Difference) / 2 + YOffset + MousePosition.Y) / (Zoom - GlobalConst.ZoomWheel)) * Zoom
                   - (YAreaSize * Zoom - GLPaint.Height + GlobalConst.Difference) / 2 - MousePosition.Y;
            this.XOFFSET =
                (XAreaSize * Zoom - GLPaint.Width + GlobalConst.Difference) / 2 - (((XAreaSize * (Zoom - GlobalConst.ZoomWheel) - GLPaint.Width + GlobalConst.Difference) / 2
                - XOffset + MousePosition.X) / (Zoom - GlobalConst.ZoomWheel)) * Zoom + MousePosition.X;
        }

        /*Изменение сдвига по осям (отдаление)*/
        public void ChangeOffsetZoomOut()
        {
            if (Zoom != MinZoom)
            {
                this.YOFFSET -= (YOffset / ((Zoom - MinZoom) / GlobalConst.ZoomWheel));
                this.XOFFSET -= (XOffset / ((Zoom - MinZoom) / GlobalConst.ZoomWheel));
            }
            else
            {
                this.YOFFSET = 0.0;
                this.XOFFSET = 0.0;
            }
        }

        /*Изменение сдвига по осям (мышка)*/
        public void ChangeOffsetZoomMouse(Point MouseDownPosition)
        {
            this.YOFFSET = (((YAreaSize * Zoom - GLPaint.Height + GlobalConst.Difference) / 2 + YOffset + MouseDownPosition.Y) / Zoom) * Zoom
                       - (YAreaSize * Zoom - GLPaint.Height + GlobalConst.Difference) / 2 - MousePosition.Y;
            this.XOFFSET = (XAreaSize * Zoom - GLPaint.Width + GlobalConst.Difference) / 2 - (((XAreaSize * Zoom - GLPaint.Width + GlobalConst.Difference) / 2
                    - XOffset + MouseDownPosition.X) / Zoom) * Zoom + MousePosition.X;
        }
        #endregion







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
