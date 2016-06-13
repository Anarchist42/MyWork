using System;
using System.Collections.Generic;
using System.Drawing;
using Tao.OpenGl;
using Tao.FreeGlut;
using Tao.Platform.Windows;
using System.Linq;

namespace Second
{
    class Paint
    {
        #region Поля класса
        #region Нужны Get\Set
        /// <summary>
        /// Текущий зум.
        /// </summary>
        private double Zoom;
        /// <summary>
        /// Минимальный зум.
        /// </summary>
        private double MinZoom;
        /// <summary>
        /// Количество клеток на линии.
        /// </summary>
        private int CellsNumber;
        /// <summary>
        /// Смещение по оси Х.
        /// </summary>
        private double XOffset;
        /// <summary>
        /// Смещение по оси Y.
        /// </summary>
        private double YOffset;
        /// <summary>
        /// Текущая позиция мышки.
        /// </summary>
        private Point MousePosition;
        /// <summary>
        /// Размер исследуемой области по X.
        /// </summary>
        private double XAreaSize;
        /// <summary>
        /// Размер исследуемой области по Y.
        /// </summary>
        private double YAreaSize;
        /// <summary>
        /// Высота над уровнем земли.
        /// </summary>
        private double EarthSize;
        /// <summary>
        /// Минимальная высота исследуемой области при заданном X.
        /// </summary>
        private double DefYAreaSize;
        /// <summary>
        /// Нужна ли сетка.
        /// </summary>
        private bool Grid;
        /// <summary>
        /// Нужна ли разметка.
        /// </summary>
        private bool Marking;
        private bool SupportPoint;
        /// <summary>
        /// Нужна ли интерполяция линиями.
        /// </summary>
        /// 
        private bool Line;
        /// <summary>
        /// Нужна ли апроксимация сплайнами.
        /// </summary>
        private bool BSpline;
        /// <summary>
        /// Нужна ли интерполяция сплайнами.
        /// </summary>
        private bool CSpline;
        /// <summary>
        /// Опорные линии разбиения.
        /// </summary>
        private bool LinePartition;
        /// <summary>
        /// Точки разбиения.
        /// </summary>
        private bool PointPartition;
        /// <summary>
        /// Разбиение.
        /// </summary>
        private bool Partition;
        /// <summary>
        /// Цвет разбиения.
        /// </summary>
        private Color ColorPartition;        
        /// <summary>
        /// Шаг разбиения.
        /// </summary>
        private double PartitionX;
        #endregion

        #region Не нужны Get\Set
        /// <summary>
        /// Указатель на OpenGLControl где рисуем.
        /// </summary>
        private SimpleOpenGlControl GLPaint;
        /// <summary>
        /// Максимальный зум.
        /// </summary>
        private double MaxZoom;
        /// <summary>
        /// Шаг для прокрутки.
        /// </summary>
        private double ZoomWheel;
        /// <summary>
        /// Сплайны почвы.
        /// </summary>
        private List<Layer> Layers = new List<Layer>();
        /// <summary>
        /// Сплайны миниралов.
        /// </summary>
        private List<Mineral> Minerals = new List<Mineral>();   
        /// <summary>
        /// Рандом для цвета.
        /// </summary>
        private Random random = new Random();
        /// <summary>
        /// Используемый массив (0 - Points, 1 - BSpline, 2 - CSpline)
        /// </summary>
        private int MassivNumber;
        /// <summary>
        /// Конечные элементы.
        /// </summary>
        private List<FiniteElement> FiniteElements = new List<FiniteElement>();
        #endregion
        #endregion

        #region Конструктор
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="Paint"> Указатель на OpenGlControl. </param>
        public Paint(SimpleOpenGlControl Paint)
        {
            this.GLPaint = Paint;
            this.EarthSize = 0;
            this.CellsNumber = 10;
            this.XOffset = 0.0;
            this.YOffset = 0.0;
            this.XAreaSize = 0.0;
            this.YAreaSize = 0.0;
            this.DefYAreaSize = 0.0;
            this.Grid = false;
            this.Marking = false;
            this.SupportPoint = false;
            this.Line = true;
            this.BSpline = false;
            this.CSpline = false;
            this.LinePartition = false;
            this.PointPartition = false;
            this.MassivNumber = 0;
        }
        #endregion

        #region Сброс
        /// <summary>
        /// Сброс всех настроек.
        /// </summary>
        /// <returns></returns>
        public bool HardReset()
        {
            try
            {
                this.Zoom = 0.0;
                this.MinZoom = 0.0;
                this.CellsNumber = 10;
                this.XOffset = 0.0;
                this.YOffset = 0.0;
                this.MousePosition = new Point(0, 0);
                this.XAreaSize = 0.0;
                this.YAreaSize = 0.0;
                this.EarthSize = 0.0;
                this.DefYAreaSize = 0.0;
                this.Grid = false;
                this.Marking = false;
                this.SupportPoint = false;
                this.Line = true;
                this.BSpline = false;
                this.CSpline = false;
                this.MassivNumber = 0;
                this.LinePartition = false;
                this.PointPartition = false;
                this.ColorPartition = Color.Black;
                this.PartitionX = 0.0;
                this.MaxZoom = 0.0;
                this.Layers.RemoveRange(0, Layers.Count);
                this.Minerals.RemoveRange(0, Minerals.Count);
                this.FiniteElements.RemoveRange(0, FiniteElements.Count);
            }
            catch { return false; }
            return true;
        }
        #endregion

        #region SETs and GETs
        public double XAREASIZE
        {
            get { return this.XAreaSize; }
            set
            {
                /*Ширина области*/
                this.XAreaSize = (value > 582 / Math.Pow(10, GlobalConst.Accuracy)) ? Math.Round(value, GlobalConst.Accuracy) : 582 / Math.Pow(10, GlobalConst.Accuracy);
                /*Настриваем зумы*/
                GlobalConst.MinZoom = Convert.ToDouble(GLPaint.MinimumSize.Width - GlobalConst.Difference) / XAreaSize;
                this.MaxZoom = GlobalConst.MinZoom * XAreaSize * Math.Pow(10, GlobalConst.Accuracy) / Convert.ToDouble(GLPaint.MinimumSize.Width - GlobalConst.Difference);
                if (Convert.ToDouble(GLPaint.Width) / Convert.ToDouble(GLPaint.MinimumSize.Width) > Convert.ToDouble(GLPaint.Height) / Convert.ToDouble(GLPaint.MinimumSize.Height))
                    this.MINZOOM = GlobalConst.MinZoom * Convert.ToDouble(GLPaint.Width - GlobalConst.Difference) / Convert.ToDouble(GLPaint.MinimumSize.Width - GlobalConst.Difference);
                else
                    this.MINZOOM = GlobalConst.MinZoom * Convert.ToDouble(GLPaint.Height - GlobalConst.Difference)
                        / Convert.ToDouble(GLPaint.MinimumSize.Height - GlobalConst.Difference);
                this.MaxZoom = MinZoom * XAreaSize * Math.Pow(10, GlobalConst.Accuracy) / Convert.ToDouble(GLPaint.MinimumSize.Width - GlobalConst.Difference);
                ZoomWheel = 0.1;
                /*Высота области*/
                this.DefYAreaSize = Math.Round(Convert.ToDouble(GLPaint.MinimumSize.Height - GlobalConst.Difference) / GlobalConst.MinZoom, GlobalConst.Accuracy);
                if (this.YAreaSize < DefYAreaSize)
                    this.YAreaSize = DefYAreaSize;
            }
        }
        public double YAREASIZE
        {
            get { return this.YAreaSize; }
            set { this.YAreaSize = Math.Round(value, GlobalConst.Accuracy); }
        }
        public double EARTHSIZE
        {
            get { return this.EarthSize; }
            set { this.EarthSize = (Math.Round(value, GlobalConst.Accuracy) > 0) ? Math.Round(value, GlobalConst.Accuracy) : 0; }
        }
        public double DEFYAREASIZE
        {
            get { return DefYAreaSize; }
        }
        public double ZOOM
        {
            get { return this.Zoom; }
            set { this.Zoom = (value > this.MinZoom) ? (value < this.MaxZoom) ? value : this.MaxZoom : this.MinZoom; }
        }
        public double MINZOOM
        {
            get { return this.MinZoom; }
            set
            {
                this.MaxZoom = this.MaxZoom / MinZoom * value;
                this.MinZoom = value;
                this.Zoom = this.MinZoom;
            }
        }
        public double XOFFSET
        {
            get { return this.XOffset; }
            set
            {
                if (value == 0)
                    this.XOffset = value;
                else
                    this.XOffset = ((XAreaSize * Zoom - GLPaint.Width + GlobalConst.Difference) / 2 - Math.Abs(value) >= 0)
                    ? value
                    : (XAreaSize * Zoom - GLPaint.Width + GlobalConst.Difference) / 2 * value / Math.Abs(value);
            }
        }
        public double YOFFSET
        {
            get { return this.YOffset; }
            set
            {
                if (value == 0)
                    this.YOffset = value;
                else
                    this.YOffset = ((YAreaSize * Zoom - GLPaint.Height + GlobalConst.Difference) / 2 - Math.Abs(value) >= 0)
                    ? value
                    : (YAreaSize * Zoom - GLPaint.Height + GlobalConst.Difference) / 2 * value / Math.Abs(value);
            }
        }
        public int CELLSNUMBER
        {
            get { return this.CellsNumber; }
            set { this.CellsNumber = (value > 0 && value < 20) ? value : (value < 20) ? 1 : 20; }
        }
        public Point MOUSEPOSITION
        {
            get { return this.MousePosition; }
            set { this.MousePosition = value; }
        }
        public bool GRID
        {
            get { return this.Grid; }
            set { this.Grid = value; }
        }
        public bool MARKING
        {
            get { return this.Marking; }
            set { this.Marking = value; }
        }
        public bool SUPPORTPOINT
        {
            get { return this.SupportPoint; }
            set { this.SupportPoint = value; }
        }
        public bool LINE
        {
            get { return this.Line; }
            set { this.Line = value; if (value == true) MassivNumber = 0; }
        }
        public bool BSPLINE
        {
            get { return this.BSpline; }
            set { this.BSpline = value; if (value == true) MassivNumber = 1; }
        }
        public bool CSPLINE
        {
            get { return this.CSpline; }
            set { this.CSpline = value; if(value == true) MassivNumber = 2; }
        }
        public bool LINEPARTITION
        {
            get { return this.LinePartition; }
            set { this.LinePartition = value; }
        }
        public bool POINTPARTITION
        {
            get { return this.PointPartition; }
            set { this.PointPartition = value; }
        }
        public bool PARTITION
        {
            get { return this.Partition; }
            set { this.Partition = value; }
        }
        public Color COLORPARTITION
        {
            get { return this.ColorPartition; }
            set { this.ColorPartition = value; }
        }      
        public double PARTITIONX
        {
            get { return this.PartitionX; }
            set { this.PartitionX = value; }
        }
        #endregion

        #region Методы

        #region Сетка, Разметка
        /// <summary>
        /// Начальные значения для Сетки и Разметки.
        /// </summary>
        /// <param name="StartX"> Начало по Х. </param>
        /// <param name="StartY"> Начало по Y. </param>
        /// <param name="MaxMinDischarge"> Разрядность разницы Max-Min. </param>
        private void StartXY(out double StartX,out double StartY, out int MaxMinDischarge)
        {
            int i;
            /*Находим максимум и минимум*/
            double Max = Math.Round(((XAreaSize * Zoom - GLPaint.Width + GlobalConst.Difference) / 2 - XOffset + GLPaint.Width - GlobalConst.Difference) / Zoom, GlobalConst.Accuracy);
            double Min = Math.Round(((XAreaSize * Zoom - GLPaint.Width + GlobalConst.Difference) / 2 - XOffset) / Zoom, GlobalConst.Accuracy);
            Max = Max * Math.Pow(10, GlobalConst.Accuracy);
            Min = Min * Math.Pow(10, GlobalConst.Accuracy);
            /*Находим разность минимума и максимума*/
            double MaxMin = Max - Min;
            /*Находим разрядность*/
            MaxMinDischarge = (Math.Log10(MaxMin) > Convert.ToInt32(Math.Log10(MaxMin)))
                ? Convert.ToInt32(Math.Log10(MaxMin)) + 1
                : Convert.ToInt32(Math.Log10(MaxMin));
            /*СтартX*/
            string StrStart = "0";
            for (i = 0; i <= Min.ToString().Length - MaxMinDischarge; i++)
                if (Min.ToString()[i].ToString() != ",")
                    StrStart += Min.ToString()[i].ToString();
                else
                    StrStart += Min.ToString()[++i].ToString();
            StartX = Convert.ToDouble(StrStart);
            /*Находим минимум*/
            Min = Math.Round((EarthSize - ((YAreaSize * Zoom - GLPaint.Height + GlobalConst.Difference) / 2 + YOffset) / Zoom), GlobalConst.Accuracy);
            Min = Min * Math.Pow(10, GlobalConst.Accuracy);
            /*Учет знака*/
            if (Min > 0)
            {
                StrStart = "0";
                for (i = 0; i <= Min.ToString().Length - MaxMinDischarge; i++)
                    StrStart += Min.ToString()[i].ToString();
            }
            else
            {
                StrStart = "-";
                for (i = 0; i <= Min.ToString().Length - MaxMinDischarge - 1; i++)
                    StrStart += Min.ToString()[i + 1].ToString();
            }
            if (StrStart != "-")
                StartY = Convert.ToDouble(StrStart);
            else StartY = 0;
        }
        /// <summary>
        /// Построение сетки.
        /// </summary>
        private void DrawingGrid()
        {
            int i, MaxMinDischarge;
            double GridHalfDown = -GLPaint.Height / 2;
            double GridHalfUp = (GLPaint.Height - GlobalConst.Difference) / 2;
            double GridHalfLeft = -GLPaint.Width / 2;
            double GridHalfRight = (GLPaint.Width - GlobalConst.Difference) / 2;
            double StartX, StartY;
            StartXY(out StartX, out StartY, out MaxMinDischarge);
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glPushMatrix();
            Gl.glLineWidth(1);
            Gl.glColor3d(0.8, 0.8, 0.8);
            double a = 0;
            for (i = 0; i <= 10; i++)
            {
                Gl.glBegin(Gl.GL_LINES);
                a = (((StartY - i) * Math.Pow(10, MaxMinDischarge - GlobalConst.Accuracy - 1) - EarthSize) * Zoom) + YOffset + (YAreaSize * Zoom + GlobalConst.Difference) / 2;
                Gl.glVertex2d(GridHalfLeft, a - 4);
                Gl.glVertex2d(GridHalfRight, a - 4);
                a = ((i + StartX) * Math.Pow(10, MaxMinDischarge - GlobalConst.Accuracy - 1) * Zoom) + XOffset - (XAreaSize * Zoom + GlobalConst.Difference) / 2;
                Gl.glVertex2d(a + 1, GridHalfUp);
                Gl.glVertex2d(a + 1, GridHalfDown);
                Gl.glEnd();
            }
            Gl.glPopMatrix();
        }
        /// <summary>
        /// Построение разметки.
        /// </summary>
        private void DrawingMarking()
        {
            int i, MaxMinDischarge;
            double GridHalfUp = (GLPaint.Height - GlobalConst.Difference) / 2 + 1;
            double GridHalfLeft = -GLPaint.Width / 2 + 2;
            double StartX, StartY;
            StartXY(out StartX, out StartY, out MaxMinDischarge);
            Gl.glPushMatrix();
            Gl.glLineWidth(1);
            Gl.glColor3d(0.3, 0.3, 0.3);
            Gl.glPointSize(3);
            double a = 0;
            /*вертикальная*/
            for (i = 0; i < 10; i++)
            {
                a = (((StartY - i) * Math.Pow(10, MaxMinDischarge - GlobalConst.Accuracy - 1) - EarthSize) * Zoom) + YOffset + (YAreaSize * Zoom + GlobalConst.Difference) / 2;
                /*Основное тело цикла*/
                Gl.glPushMatrix();
                Gl.glBegin(Gl.GL_POINTS);
                Gl.glVertex2d(-GLPaint.Width / 2, a - 4);
                Gl.glEnd();
                Gl.glTranslated(-GLPaint.Width / 2, a - 2, 0.0);
                Gl.glScaled(0.08, 0.08, 0.08);
                string number = Math.Round(EarthSize - ((YAreaSize * Zoom + GlobalConst.Difference) / 2 + YOffset - a) / Zoom, GlobalConst.Accuracy).ToString();
                for (int j = 0; j < number.Length; j++)
                    Glut.glutStrokeCharacter(Glut.GLUT_STROKE_ROMAN, number[j]);
                Gl.glPopMatrix();
            }
            /*Получаем число*/
            double Acc = ((StartX) * Math.Pow(10, MaxMinDischarge - GlobalConst.Accuracy - 1) * Zoom);
            /*Узнаем сколько знаков можно записать*/
            int Accuarcy = ((Convert.ToInt32(Math.Pow(10, MaxMinDischarge - GlobalConst.Accuracy - 1) * Zoom - Acc.ToString().Length - 1) / 9) > (GlobalConst.Accuracy * 9)
                ? GlobalConst.Accuracy
                : Convert.ToInt32(Math.Pow(10, MaxMinDischarge - GlobalConst.Accuracy - 1) * Zoom - Acc.ToString().Length - 1) / 9);
            /*Если кол-во знаков переваливает за 15, то ставим 15*/
            if (Accuarcy > 15) Accuarcy = 15;
            /*горизонтальная*/
            for (i = 1; i <= 10; i++)
            {
                a = ((i + StartX) * Math.Pow(10, MaxMinDischarge - GlobalConst.Accuracy - 1) * Zoom) + XOffset - (XAreaSize * Zoom + GlobalConst.Difference) / 2;
                double b = i * Math.Pow(10, MaxMinDischarge - GlobalConst.Accuracy - 1) * Zoom;
                /*Основное тело цикла*/
                Gl.glPushMatrix();
                Gl.glBegin(Gl.GL_POINTS);
                Gl.glVertex2d(a, -GLPaint.Height / 2);
                Gl.glEnd();
                Gl.glTranslated(a + 4, -GLPaint.Height / 2, 0.0);
                Gl.glScaled(0.08, 0.08, 0.08);
                string number = Math.Round(((XAreaSize * Zoom + GlobalConst.Difference) / 2 - XOffset + a) / Zoom, Accuarcy).ToString();
                for (int j = 0; j < number.Length; j++)
                    Glut.glutStrokeCharacter(Glut.GLUT_STROKE_ROMAN, number[j]);
                Gl.glPopMatrix();
            }
            Gl.glPopMatrix();
        }
        #endregion

        #region Передача параметров
        /// <summary>
        /// Вычисление текущих координат.
        /// </summary>
        /// <param name="TypeCoordinate"> Тип координаты (0 - X, 1 - Y).</param>
        /// <returns> Значение искомой координаты. </returns>
        public double GetCoordinate(int TypeCoordinate)
        {
            if (TypeCoordinate == 0)
                return Math.Round(((XAreaSize * Zoom - GLPaint.Width + GlobalConst.Difference) / 2 - XOffset + MousePosition.X) / Zoom, GlobalConst.Accuracy);
            else
                return Math.Round(EarthSize - ((YAreaSize * Zoom - GLPaint.Height + GlobalConst.Difference) / 2 + YOffset + MousePosition.Y) / Zoom, GlobalConst.Accuracy);
        }
        /// <summary>
        /// Максимальное значение смещений для ползунка.
        /// </summary>
        /// <param name="TypeScroll"> Тип скролла (0 - вертикальный, 1 - горизонтальный). </param>
        /// <returns> Максимальное значение ползунка. </returns>
        public double ScrollMaximum(int TypeScroll)
        {
            if (TypeScroll == 0)
                return
                    Math.Round(
                    /*Сколько вверх*/
                    ((YAreaSize * Zoom - GLPaint.Height + GlobalConst.Difference) / 2 + YOffset) / Zoom +
                    /*Сколько вниз*/
                    YAreaSize - ((YAreaSize * Zoom - GLPaint.Height + GlobalConst.Difference) / 2 + YOffset + GLPaint.Height - GlobalConst.Difference) / Zoom,GlobalConst.Accuracy);
            else
                return
                    Math.Round(
                   /*Сколько влево*/
                   ((XAreaSize * Zoom - GLPaint.Width + GlobalConst.Difference) / 2 - XOffset) / Zoom +
                   /*Сколько вправо*/
                   XAreaSize - ((XAreaSize * Zoom - GLPaint.Width + GlobalConst.Difference) / 2 - XOffset + GLPaint.Width - GlobalConst.Difference) / Zoom, GlobalConst.Accuracy);
        }
        /// <summary>
        /// Вычисление значения ползунка.
        /// </summary>
        /// <param name="TypeScroll"> Тип скролла (0 - вертикальный, 1 - горизонтальный).</param>
        /// <returns> Значение ползунка. </returns>
        public double ScrollValue(int TypeScroll)
        {
            if (TypeScroll == 0)
                return Math.Round(Math.Abs(((YAreaSize * Zoom - GLPaint.Height + GlobalConst.Difference) / 2 + YOffset) / Zoom),GlobalConst.Accuracy);
            else
                return Math.Round(Math.Abs(((XAreaSize * Zoom - GLPaint.Width + GlobalConst.Difference) / 2 - XOffset) / Zoom),GlobalConst.Accuracy);
        }
        /// <summary>
        /// Изменение сдвига по осям (приближение).
        /// </summary>
        public void ChangeOffsetZoomIn()
        {
            double PrefZoom = Zoom;
            ZOOM += Zoom * ZoomWheel;
                this.YOFFSET =
                       (((YAreaSize * PrefZoom - GLPaint.Height + GlobalConst.Difference) / 2 + YOffset + MousePosition.Y) / PrefZoom) * Zoom
                       - (YAreaSize * Zoom - GLPaint.Height + GlobalConst.Difference) / 2 - MousePosition.Y;
                this.XOFFSET =
                    (XAreaSize * Zoom - GLPaint.Width + GlobalConst.Difference) / 2 - (((XAreaSize * PrefZoom - GLPaint.Width + GlobalConst.Difference) / 2
                    - XOffset + MousePosition.X) / PrefZoom) * Zoom + MousePosition.X;
        }
        /// <summary>
        /// Изменение сдвига по осям (отдаление).
        /// </summary>
        public void ChangeOffsetZoomOut()
        {
            double PrefZoom = Zoom;
            ZOOM -= Zoom * ZoomWheel;
            if (Zoom != MinZoom)
            {
                this.YOFFSET =
                       (((YAreaSize * PrefZoom - GLPaint.Height + GlobalConst.Difference) / 2 + YOffset + MousePosition.Y) / PrefZoom) * Zoom
                       - (YAreaSize * Zoom - GLPaint.Height + GlobalConst.Difference) / 2 - MousePosition.Y;
                this.XOFFSET =
                    (XAreaSize * Zoom - GLPaint.Width + GlobalConst.Difference) / 2 - (((XAreaSize * PrefZoom - GLPaint.Width + GlobalConst.Difference) / 2
                    - XOffset + MousePosition.X) / PrefZoom) * Zoom + MousePosition.X;
            }
            else
            {
                this.YOFFSET = 0.0;
                this.XOFFSET = 0.0;
            }
        }
        /// <summary>
        /// Изменение сдвига по осям (мышь). 
        /// </summary>
        /// <param name="MouseDownPosition"> Позиция нажатия мышкой. </param>
        public void ChangeOffsetZoomMouse(Point MouseDownPosition)
        {
                this.YOFFSET = (((YAreaSize * Zoom - GLPaint.Height + GlobalConst.Difference) / 2 + YOffset + MouseDownPosition.Y) / Zoom) * Zoom
                       - (YAreaSize * Zoom - GLPaint.Height + GlobalConst.Difference) / 2 - MousePosition.Y;
                this.XOFFSET = (XAreaSize * Zoom - GLPaint.Width + GlobalConst.Difference) / 2 - (((XAreaSize * Zoom - GLPaint.Width + GlobalConst.Difference) / 2
                        - XOffset + MouseDownPosition.X) / Zoom) * Zoom + MousePosition.X;
        }
        #endregion

        #region Слои и Минералы (общие функции)

        #region SET'S
        /// <summary>
        /// Задаем цвет сплайна.
        /// </summary>
        /// <param name="Color"> Задаваемый цвет.</param>
        /// <param name="FIJ"> Массив [0] - 0(нету),1(слой),2(минерал); 
        ///                           [1] - номер слоя\минерала;
        ///                           [2] - номер опорной точки.</param>
        /// <returns> Выполнил или нет. </returns>
        public bool SetSplineColor(Color Color, int[] FIJ)
        {
            try
            {
                //Если не используется, то меняем цвет.
                if (!CheckSplineColor(Color))
                    if (FIJ[0] == 1)
                        Layers[FIJ[1]].COLOR = Color;
                    else
                        Minerals[FIJ[1]].COLOR = Color;
                else
                    return false;
            }
            catch { return false; }
            return true;
        }
        /// <summary>
        /// Изменение точности сплайнов.
        /// </summary>
        /// <returns> Выполнил или нет. </returns>
        public bool SetSplineAccuracy()
        {
            try
            {
                int i;
                for (i = 0; i < Layers.Count; i++)
                    Layers[i].ChangeAccuracy();
                for (i = 0; i < Minerals.Count; i++)
                    Minerals[i].ChangeAccuracy();
            }
            catch { return false; }
            return true;
        }
        /// <summary>
        /// Изменение материала.
        /// </summary>
        /// <param name="FIJ"> Массив [0] - 0(нету),1(слой),2(минерал); 
        ///                           [1] - номер слоя\минерала;</param>
        /// <param name="Material"> Материал. </param>
        /// <returns></returns>
        public bool SetSplineMaterial(int[] FIJ, Material Material)
        {
            try
            {
                if (FIJ[0] == 1)
                    Layers[FIJ[1]].MATERIAL = Material;
                else
                    Minerals[FIJ[1]].MATERIAL = Material;
            }
            catch { return false; }
            return true;
        }
        #endregion

        #region GET'S
        /// <summary>
        /// Возвращает материал слоя или минерала.
        /// </summary>
        /// <param name="FIJ"> Массив [0] - 0(нету),1(слой),2(минерал); 
        ///                           [1] - номер слоя\минерала;</param>
        /// <param name="Material"> Материал. </param>
        /// <returns></returns>
        public bool GetSplineMaterial(int[] FIJ, out Material Material)
        {
            Material = new Material();
            try
            {
                if (FIJ[0] == 1)
                    Material = Layers[FIJ[1]].MATERIAL;
                else
                    Material = Minerals[FIJ[1]].MATERIAL;
            }
            catch { return false; }
            return true;
        }
        /// <summary>
        /// Возвращем значение в таблицу.
        /// </summary>
        /// <param name="NumberGrid"> Номер таблицы (0 - слои, 1 - минералы). </param>
        /// <param name="PositionNumber"> Позиция в таблице. </param>
        /// <param name="Color"> Цвет. </param>
        /// <param name="LayerPosition"> Максимальная и минимальная высота. </param>
        /// <param name="Material"> Материал. </param>
        /// <returns></returns>
        public bool GetSplineInformation(int NumberGrid, int PositionNumber, out Color Color, out string LayerPosition, out string Material)
        {
            Color = Color.White;
            LayerPosition = "";
            Material = "";
            try
            {
                //Если слой
                if (NumberGrid == 0)
                {
                    Color = Layers[PositionNumber].COLOR;
                    LayerPosition = Layers[PositionNumber].ReturnMaxY(MassivNumber).ToString() + "\n" + Layers[PositionNumber + 1].ReturnMinY(MassivNumber).ToString();
                    Material = Layers[PositionNumber].MATERIAL.NAME;
                }
                //Если минерал
                else
                {
                    Color = Minerals[PositionNumber].COLOR;
                    LayerPosition = Minerals[PositionNumber].ReturnMaxY(MassivNumber).ToString() + "\n" + Minerals[PositionNumber].ReturnMinY(MassivNumber).ToString();
                    Material = Minerals[PositionNumber].MATERIAL.NAME;
                }
            }
            catch { return false; }
            return true;
        }
        /// <summary>
        /// Получить рандомный цвет.
        /// </summary>
        /// <returns> Цвет. </returns>
        public Color GetRandomColor()
        {
            Color Color = new Color();
            Color = Color.FromArgb(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255));
            while (CheckSplineColor(Color))
                Color = Color.FromArgb(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255));
            return Color;
        }
        /// <summary>
        /// Возвращаем количество сплайнов слоя и минерала.
        /// </summary>
        /// <param name="LCount"> Количество сплайнов слоя. </param>
        /// <param name="MCount"> Количество сплайнов минерала. </param>
        /// <returns></returns>
        public bool GetCountSplines(out int LCount,out int MCount)
        {
            LCount = 0;
            MCount = 0;
            try
            {
                LCount = Layers.Count;
                MCount = Minerals.Count;
            }
            catch { return false; }
            return true;
        }
        #endregion

        #region Изменение количества точек
        /// <summary>
        /// Добавление опорной точки.
        /// </summary>
        /// <param name="FIJ"> Массив [0] - 0(нету),1(слой),2(минерал); 
        ///                           [1] - номер слоя\минерала;
        ///                           [2] - номер опорной точки.</param>
        /// <returns> Выполнил или нет. </returns>
        public bool AddPoint(int[] FIJ)
        {
            try
            {
                if (FIJ[0] == 1)
                {
                    Layers[FIJ[1]].AddPoint(new PointSpline(GetCoordinate(0), GetCoordinate(1)), out FIJ[2]);
                    /*Проверяем сплайны на пересечение*/
                    if (CheckLayersIntersectionRude(FIJ))
                    {
                        Layers[FIJ[1]].POINT.RemoveAt(FIJ[2]);
                        return false;
                    }
                    /*Перестраиваем массив*/
                    Layers[FIJ[1]].ReBuild();
                }
                else
                {
                    if (Layers.Count > 1 && GetCoordinate(1) < Layers[0].ReturnMaxYX(GetCoordinate(0), MassivNumber)
                   && GetCoordinate(1) > Layers[Layers.Count - 1].ReturnMinYX(GetCoordinate(0), MassivNumber))
                    {
                        Minerals[FIJ[1]].AddPoint(new PointSpline(GetCoordinate(0), GetCoordinate(1)), out FIJ[2]);
                    }
                    else return false;
                }
            }
            catch { return false; }
            return true;
        }
        /// <summary>
        /// Удаление опорной точки.
        /// </summary>
        /// <param name="FIJ"> Массив [0] - 0(нету),1(слой),2(минерал); 
        ///                           [1] - номер слоя\минерала;
        ///                           [2] - номер опорной точки.</param>
        /// <returns> Выполнил или нет. </returns>
        public bool DeletePoint(int[] FIJ)
        {
            try
            {
                if (FIJ[0] == 1)
                {
                    PointSpline PrefPoint = Layers[FIJ[1]].POINT[FIJ[2]];
                    Layers[FIJ[1]].DeletePoint(FIJ[2]);
                    /*Проверяем сплайны на пересечение*/
                    if (FIJ[2]> 0 && CheckLayersIntersectionRude(FIJ))
                    {
                        Layers[FIJ[1]].POINT.Insert(FIJ[2],PrefPoint);
                        return false;
                    }
                    /*Перестраиваем массив*/
                    Layers[FIJ[1]].ReBuild();
                }
                else
                    Minerals[FIJ[1]].DeletePoint(FIJ[2]);
            }
            catch { return false; }
            return true;
        }
        /// <summary>
        /// Удаляем все точки больше области.
        /// </summary>
        /// <returns> Выполнил или нет. </returns>
        public bool DeletePoints()
        {
            int i;
            try
            {
                for (i = 0; i < Layers.Count; i++)
                    Layers[i].DeletePoint(XAreaSize);
                for (i = 0; i < Minerals.Count; i++)
                    if (!Minerals[i].DeletePoint(XAreaSize))
                        Minerals.RemoveAt(i--);
            }
            catch { return false; }
            return true;
        }
        #endregion

        #region Удаление сплайнов
        /// <summary>
        /// Удаление сплайна с формы.
        /// </summary>
        /// <param name="FIJ"> Массив [0] - 0(нету),1(слой),2(минерал); 
        ///                           [1] - номер слоя\минерала;
        ///                           [2] - номер опорной точки.</param>
        /// <returns></returns>
        public bool DeleteSpline(int[] FIJ)
        {
            try
            {
                if (FIJ[0] == 1)
                {
                    if (Layers.Count > 1)

                        if (FIJ[1] == Layers.Count - 2)
                            Layers.RemoveAt(FIJ[1] + 1);
                        else Layers.RemoveAt(FIJ[1]);
                    else
                        return false;
                }
                else
                    Minerals.RemoveAt(FIJ[1]);
                YAreaSize = GetMaxPointLayers();
            }
            catch { return false; }
            return true;
        }
        #endregion

        #region Чеки сплайнов
        /// <summary>
        /// Проверка есть ли материал.
        /// </summary>
        /// <param name="NumberGrid"> Номер таблицы (0 - слои, 1 - минералы). </param>
        /// <param name="Material"> Материал. </param>
        /// <param name="Number"> Массив найденных совпадений. </param>
        /// <returns> Нашли материал или нет. </returns>
        public bool CheckMaterial(int NumberGrid, Material Material, out List<int> Number)
        {
            Number = new List<int>();
            int i;
            try
            {
                if (NumberGrid == 1)
                {
                    for (i = 0; i < Layers.Count - 1; i++)
                        if (Layers[i].MATERIAL == Material)
                            Number.Add(i + 1);
                }
                else
                {
                    for (i = 0; i < Minerals.Count; i++)
                        if (Minerals[i].MATERIAL == Material)
                            Number.Add(i + 1);
                }
            }
            catch { return false; }
            return Number.Count == 0;
        }
        /// <summary>
        /// Используется ли данный цвет где-то.
        /// </summary>
        /// <param name="Color"> Цвет. </param>
        /// <returns> Да\нет. </returns>
        private bool CheckSplineColor(Color Color)
        {
            try
            {
                int i;
                //Проверяем есть ли такой цвет в слоях.
                for (i = 0; i < Layers.Count; i++)
                    if (Layers[i].COLOR == Color)
                        return true;
                //Проверяем есть ли такой цвет в минералах.
                for (i = 0; i < Minerals.Count; i++)
                    if (Minerals[i].COLOR == Color)
                        return true;
            }
            catch { return true; }
            return false;
        }
        /// <summary>
        /// Площадь треугольника.
        /// </summary>
        /// <param name="A"> Первая точка треугольника. </param>
        /// <param name="B"> Вторая точка треугольника. </param>
        /// <param name="C"> Третья точка треугольника. </param>
        /// <returns></returns>
        private double AreaTriangle(PointSpline A, PointSpline B, PointSpline C)
        {
            return (B.X - A.X) * (C.Y - A.Y) - (B.Y - A.Y) * (C.X - A.X);
        }
        /// <summary>
        /// Пересечение двух отрезков.
        /// </summary>
        /// <param name="A"> Первая точка первого отрезка. </param>
        /// <param name="B"> Вторая точка первого отрезка. </param>
        /// <param name="C"> Первая точка второго отрезка. </param>
        /// <param name="D"> Вторая точка второго отрезка. </param>
        /// <returns></returns>
        private bool IntersectParts(PointSpline A, PointSpline B, PointSpline C, PointSpline D)
        {
            return AreaTriangle(A, B, C) * AreaTriangle(A, B, D) < 0
                && AreaTriangle(C, D, A) * AreaTriangle(C, D, B) < 0;
        }
        /// <summary>
        /// Пересекается ли сплайн сам с собой (грубый способ, по опорным линиям).
        /// </summary>
        /// <param name="NumberCheckPoint"> Номер движимой опорной точки. </param>
        /// <param name="Points"> Массив опорных точек. </param>
        /// <returns></returns>
        private bool CheckSplineSelfIntersectionRude(int NumberCheckPoint, List<PointSpline> Points)
        {
            try
            {
                int i;
                for (i = 0; i < Points.Count - 1; i++)
                {
                    if (IntersectParts(Points[i], Points[i + 1], Points[NumberCheckPoint - 1], Points[NumberCheckPoint])
                        || IntersectParts(Points[i], Points[i + 1], Points[NumberCheckPoint], Points[NumberCheckPoint + 1]))
                        return true;
                }               
            }
            catch { return true; }
            return false;
        }
        /// <summary>
        /// Пересекаются ли соседние слои.
        /// </summary>
        /// <param name="FIJ">Массив [0] - 0(нету),1(слой),2(минерал); 
        ///                           [1] - номер слоя\минерала;
        ///                           [2] - номер опорной точки.</param>
        /// <returns> Да\нет. </returns>
        private bool CheckLayersIntersectionRude(int[] FIJ)
        {
            try
            {
                if (Layers.Count > 1)
                {
                    if (FIJ[1] == 0)
                        return Check4LayersIntersectionRude(FIJ[2], Layers[0].ClonePoints(0), Layers[1].ClonePoints(0));
                    if (FIJ[1] == Layers.Count - 1)
                        return Check4LayersIntersectionRude(FIJ[2], Layers[Layers.Count - 1].ClonePoints(0), Layers[Layers.Count - 2].ClonePoints(0));
                    else
                        return Check4LayersIntersectionRude(FIJ[2], Layers[FIJ[1]].ClonePoints(0), Layers[FIJ[1] + 1].ClonePoints(0)) ||
                            Check4LayersIntersectionRude(FIJ[2], Layers[FIJ[1]].ClonePoints(0), Layers[FIJ[1] - 1].ClonePoints(0));
                }
            }
            catch { return true; }
            return false;
        }
        /// <summary>
        /// Пересекаются ли слои в заданной точки.
        /// </summary>
        /// <param name="NumberCheckPoint"> Точка которую анализируем. </param>
        /// <param name="First"> Первый слой. </param>
        /// <param name="Second"> Второй слой. </param>
        /// <returns> Да\нет. </returns>
        private bool Check4LayersIntersectionRude(int NumberCheckPoint, List<PointSpline> First, List<PointSpline> Second)
        {
            try
            {
                int i;
                for(i=0;i<Second.Count - 1;i++)
                {
                    if  (NumberCheckPoint > 1 && IntersectParts(Second[i], Second[i + 1], First[NumberCheckPoint - 1], First[NumberCheckPoint]) ||
                        (First.Count > NumberCheckPoint + 1 && IntersectParts(Second[i], Second[i + 1], First[NumberCheckPoint], First[NumberCheckPoint + 1])))
                        return true;
                }
            }
            catch { return true; }
            return false;
        }
        /// <summary>
        /// Проверка попали ли на опорную точку.
        /// </summary>
        /// <param name="MouseDownPosition"> Позиция нажатия мышки.</param>
        /// <param name="FIJ"> Массив [0] - 0(нету),1(слой),2(минерал); 
        ///                           [1] - номер слоя\минерала;
        ///                           [2] - номер опорной точки.</param>
        /// <returns> Выполнил или нет. </returns>
        public bool CheckPoint(Point MouseDownPosition, out int[] FIJ)
        {
            FIJ = new int[3];
            FIJ[0] = 0;
            FIJ[1] = 0;
            FIJ[2] = 0;
            try
            {
                if (SupportPoint)
                {
                    double[] X = new double[2];
                    double[] Y = new double[2];
                    int i, j;
                    X[0] = ((XAreaSize * Zoom - GLPaint.Width + GlobalConst.Difference) / 2 - XOffset + MouseDownPosition.X - 3) / Zoom;
                    X[1] = ((XAreaSize * Zoom - GLPaint.Width + GlobalConst.Difference) / 2 - XOffset + MouseDownPosition.X + 3) / Zoom;
                    Y[0] = EarthSize - ((YAreaSize * Zoom - GLPaint.Height + GlobalConst.Difference) / 2 + YOffset + MouseDownPosition.Y - 3) / Zoom;
                    Y[1] = EarthSize - ((YAreaSize * Zoom - GLPaint.Height + GlobalConst.Difference) / 2 + YOffset + MouseDownPosition.Y + 3) / Zoom;
                    List<PointSpline> Points;
                    for (i = 0; i < Layers.Count; i++)
                    {
                        Points = Layers[i].ClonePoints(0);
                        for (j = 0; j < Points.Count; j++)
                            if (Points[j].X >= X[0] && Points[j].X <= X[1] && Points[j].Y <= Y[0] && Points[j].Y >= Y[1])
                            {
                                FIJ[0] = 1;
                                FIJ[1] = i;
                                FIJ[2] = j;
                                return true;
                            }
                    }
                    for (i = 0; i < Minerals.Count; i++)
                    {
                        Points = Minerals[i].ClonePoints(0);
                        for (j = 0; j < Points.Count; j++)
                            if (Points[j].X >= X[0] && Points[j].X <= X[1] && Points[j].Y <= Y[0] && Points[j].Y >= Y[1])
                            {
                                FIJ[0] = 2;
                                FIJ[1] = i;
                                FIJ[2] = j;
                                return true;
                            }
                    }

                }
            }
            catch { return false; }
            return false;
        }
        #endregion

        #region Перемещение сплайнов
        /// <summary>
        /// Изменяем позицию опорной точки.
        /// </summary>
        /// <param name="FIJ"> Массив [0] - 0(нету),1(слой),2(минерал); 
        ///                           [1] - номер слоя\минерала;
        ///                           [2] - номер опорной точки.</param>
        /// <param name="Point"> Новая позиция точки. </param>
        /// <returns></returns>
        public bool ChangePoint(int[] FIJ, PointSpline Point)
        {
            try
            {
                List<PointSpline> Points;
                PointSpline PrefPoint;
                if (FIJ[0] == 1)
                {
                    Points = Layers[FIJ[1]].POINT;
                    PrefPoint = Points[FIJ[2]];
                    //Если не первая либо последняя, то двигать можно и по Х и по Y
                    if (FIJ[2] != 0 && FIJ[2] != Points.Count - 1)
                    {
                        Points[FIJ[2]] = new PointSpline(Point.X, Point.Y);
                        /*Проверка на самопересечение и пересечение соседних*/
                        if (CheckSplineSelfIntersectionRude(FIJ[2], Points) || CheckLayersIntersectionRude(FIJ))
                        {
                            /*Если пересек - сохраняем старое значение и выходим*/
                            Points[FIJ[2]] = PrefPoint;
                            return false;
                        }
                    }
                    //Если первая либо последняя, то двигать можно только по Y
                    else
                        if (CheckFirstAndLastLayerPoint(FIJ, Point.Y))
                        Points[FIJ[2]] = new PointSpline(Points[FIJ[2]].X, Point.Y);
                    /*Если сдвинули ниже максимума - добавить еще*/
                    if (Math.Abs(Points[FIJ[2]].Y) + XAreaSize / 100 + EarthSize > YAreaSize)
                    {
                        YAreaSize += XAreaSize / 100;
                        YOffset += XAreaSize / 100;
                    }
                    /*Перестраиваем сплайн*/
                    Layers[FIJ[1]].ReBuild();
                }
                else
                {
                    Points = Minerals[FIJ[1]].POINT;
                    /*Добавляем точку в начало и конец, что бы замкнуть контур с обеих сторон*/
                    Points.Insert(0, Points[Points.Count - 1]);
                    Points.Add(Points[Points.Count - 1]);
                    PrefPoint = Points[FIJ[2] + 1];
                    Points[FIJ[2] + 1] = new PointSpline(Point.X, Point.Y);
                    /*Проверка на самопересечение*/
                    if (Layers.Count > 1 && Point.Y < Layers[0].ReturnMaxYX(Point.X,0)
                    && Point.Y > Layers[Layers.Count - 1].ReturnMinYX(Point.X,0)
                            && CheckSplineSelfIntersectionRude(FIJ[2] + 1, Points))
                    {
                        /*Если пересек - сохраняем старое значение и выходим*/
                        Points[FIJ[2] + 1] = PrefPoint;
                        /*Убираем лишние значения*/
                        Points.RemoveAt(0);
                        Points.RemoveAt(Points.Count - 1);
                        return false;
                    }
                    /*Убираем лишние значения*/
                    Points.RemoveAt(0);
                    Points.RemoveAt(Points.Count - 1);
                    /*Перестраиваем сплайн*/
                    Minerals[FIJ[1]].ReBuild();
                }
            }
            catch { return false; }
            return true;
        }
        /// <summary>
        /// Перемещение сплайнов(вправо) на заданное расстояние.
        /// </summary>
        /// <param name="ChangeX"> Расстояние на которое перемещаем. </param>
        /// <returns> Выполнил или нет. </returns>
        public bool MoveSpline(double ChangeX)
        {
            int i;
            try
            {
                for (i = 0; i < Layers.Count; i++)
                    Layers[i].ChangeXPoints(ChangeX);
                for (i = 0; i < Minerals.Count; i++)
                    Layers[i].ChangeXPoints(ChangeX);
            }
            catch { return false; }
            return true;
        }
        #endregion

        #region Построение КЭ
        /// <summary>
        /// Разбиение.
        /// </summary>
        /// <returns> Выполнил или нет. </returns>
        public bool MakePartition()
        {
            try
            {
                int i;
                for (i = 0; i < Layers.Count; i++)
                    Layers[i].MakePartition(PARTITIONX, MassivNumber, i);
                for (i = 0; i < Minerals.Count; i++)
                    Minerals[i].MakePartition(PARTITIONX, MassivNumber, i);
                MakeFinit();
            }
            catch { return false; }
            return true;
        }
        /// <summary>
        /// Составление конечных элементов.
        /// </summary>
        /// <returns> Выполнил или нет. </returns>
        private bool MakeFinit()
        {
            try
            {
                int i, j, k, m;
                bool flagMineral;
                /*Создаем массив для группировки точек по Х*/
                int N = (XAreaSize % PartitionX != 0)
                    ? Convert.ToInt32(Math.Floor(XAreaSize / PartitionX)) + 2
                    : Convert.ToInt32(Math.Floor(XAreaSize / PartitionX)) + 1;
                List<PointMKE>[] FullMassive = new List<PointMKE>[N];
                for (i = 0; i < FullMassive.Length; i++)
                    FullMassive[i] = new List<PointMKE>();
                /*Записываем значения*/
                for (i = 0; i < Layers.Count; i++)
                {
                    List<PointMKE> a = Layers[i].ClonePartition();
                    for (j = 0; j < a.Count; j++)
                    {
                        int Number = (a[j].POINT.X % PartitionX != 0)
                            ? Convert.ToInt32(Math.Floor(a[j].POINT.X / PartitionX)) + 1
                            : Convert.ToInt32(a[j].POINT.X / PartitionX);
                        FullMassive[Number].Add(a[j]);
                    }
                }
                for (i = 0; i < Minerals.Count; i++)
                {
                    List<PointMKE> a = Minerals[i].ClonePartition();
                    for (j = 0; j < a.Count; j++)
                    {
                        int Number = (a[j].POINT.X % PartitionX != 0)
                            ? Convert.ToInt32(a[j].POINT.X / PartitionX) + 1
                            : Convert.ToInt32(a[j].POINT.X / PartitionX);
                        FullMassive[Number].Add(a[j]);
                    }
                }
                /*Сортируем по убыванию*/
                for (i = 0; i < FullMassive.Length; i++)
                    FullMassive[i].Sort(delegate (PointMKE x, PointMKE y)
                    {
                        return y.POINT.Y.CompareTo(x.POINT.Y);
                    });
                /*Очищаем масив конечных элементов*/
                FiniteElements.Clear();
                for (i = 0; i < FullMassive.Length - 1; i++)
                {
                    /*Копируем левую и правую границы что бы изменять их, если надо, а не основной массив данных*/
                    List<PointMKE> Left = new List<PointMKE>();
                    for (j = 0; j < FullMassive[i].Count; j++)
                        Left.Add(new PointMKE(new PointSpline(FullMassive[i][j].POINT.X, FullMassive[i][j].POINT.Y), FullMassive[i][j].NUMBERSPLINE, FullMassive[i][j].ITSLAYER));
                    List<PointMKE> Right = new List<PointMKE>();
                    for (j = 0; j < FullMassive[i + 1].Count; j++)
                        Right.Add(new PointMKE(new PointSpline(FullMassive[i + 1][j].POINT.X, FullMassive[i + 1][j].POINT.Y), FullMassive[i + 1][j].NUMBERSPLINE, FullMassive[i + 1][j].ITSLAYER));
                    flagMineral = false;
                    m = 0;
                    /*j - счетчик по левой границе, k - по правой границе*/
                    for (j = 0, k = 0; j < Left.Count - 1 && k < Right.Count - 1; j++, k++)
                    {
                        FiniteElement AddElement = new FiniteElement();
                        /*Верхние две точки*/
                        AddElement.POINTS[0] = Left[j].POINT;
                        AddElement.POINTS[1] = Right[k].POINT;
                        /*Нижние две точки*/
                        if (Left[j + 1].POINT.Y > Right[k + 1].POINT.Y)
                            Right.Insert(k + 1, new PointMKE(new PointSpline(Right[k + 1].POINT.X, Left[j + 1].POINT.Y), Left[j + 1].NUMBERSPLINE, Left[j + 1].ITSLAYER));                           
                        else
                            Left.Insert(j + 1, new PointMKE(new PointSpline(Left[j + 1].POINT.X, Right[k + 1].POINT.Y), Right[k + 1].NUMBERSPLINE, Right[k + 1].ITSLAYER));
                        AddElement.POINTS[2] = Left[j + 1].POINT;
                        AddElement.POINTS[3] = Right[k + 1].POINT;
                        /*Добавление материала*/
                        /*Если сейчас идет минерал*/
                        if(flagMineral == true)
                        {
                            AddElement.MATERIAL[0] = Layers[m].MATERIAL;
                            AddElement.MATERIAL[1] = Minerals[Left[j].NUMBERSPLINE].MATERIAL;
                        }
                        /*Если нет минерала*/
                        else
                            AddElement.MATERIAL[0] = Layers[Left[j].NUMBERSPLINE].MATERIAL;
                        ///*Если снизу начинается минерал, то сохраняем подкладку и включаем двойной материал*/
                        //if (Left[j + 1].ITSLAYER == false || Right[k + 1].ITSLAYER == false)
                        //{
                        //    flagMineral = true;
                        //    m = Left[j].NUMBERSPLINE;
                        //}
                        //else
                        //    if (flagMineral == true) flagMineral = false;
                        /*Добавляем конечный элемент в массив*/
                        FiniteElements.Add(AddElement);
                    }
                }
            }
            catch { return false; }
            return true;
        }

        #endregion

        #endregion

        #region Работа со слоями
        /// <summary>
        /// Добавление сплайна.
        /// </summary>
        /// <param name="LayerHeight"></param>
        /// <param name="NumberOfPoints"></param>
        /// <param name="Material"></param>
        public bool AddLayers(double LayerHeight, int NumberOfPoints, Material Material)
        {
            int i, NumbAdd;
            /*Если новый слой имеет глубину больше чем есть сейчас*/
            if (Math.Abs(LayerHeight) + EarthSize + XAreaSize / 10 > YAreaSize)
                this.YAREASIZE = Math.Abs(LayerHeight) + EarthSize + XAreaSize / 10;
            if (Layers.Count == 1)
                Layers[0].MATERIAL = Material;
            /*Добавляем новый слой*/
            Layer tmp = new Layer(XAreaSize, LayerHeight, NumberOfPoints, Material, GetRandomColor());
            Layers.Add(tmp);
            /*Сортируем по высоте*/
            Layers.Sort(delegate (Layer x, Layer y)
            {
                return y.POINT[0].Y.CompareTo(x.POINT[0].Y);
            });
            for (i = 0, NumbAdd = -1; i < Layers.Count && NumbAdd==-1; i++)
                if (Layers[i].POINT[0].Y == LayerHeight && Layers[i].ReturnMaxY(0) == Layers[i].ReturnMinY(0))
                    NumbAdd = i;
            int[] FIJ = { 0, NumbAdd, 1 };
            for (i = 1; i < Layers[NumbAdd].POINT.Count; i += 2)
            {
                FIJ[2] = i;
                if (CheckLayersIntersectionRude(FIJ))
                {
                    Layers.RemoveAt(NumbAdd);
                    return false;
                }
            }                        
            return true;
        }
        /// <summary>
        /// Минимальная высота области.
        /// </summary>
        /// <returns> Минимальная высота. </returns>
        public double GetMaxPointLayers()
        {
            if (Layers.Count == 0)
                return DefYAreaSize;
            else
            {
                double Height = Math.Abs(Layers[Layers.Count - 1].ReturnMinY(0)) + XAreaSize / 10 + EarthSize;
                return Height > DefYAreaSize ? Height : DefYAreaSize;
            }
        }
        /// <summary>
        /// Проверяем первую и последнюю точку сплайна.
        /// </summary>
        /// <param name="FIJ"> Массив [0] - 0(нету),1(слой),2(минерал); 
        ///                           [1] - номер слоя\минерала;
        ///                           [2] - номер опорной точки.</param>
        /// <param name="PointY"> Позиция Y новой точки. </param>
        /// <returns> Можно ли двигать. </returns>
        private bool CheckFirstAndLastLayerPoint(int[] FIJ, double PointY)
        {
            try
            {
                if (FIJ[2] == 0)
                {
                    if (Layers.Count > FIJ[1] + 1 && PointY < Layers[FIJ[1] + 1].POINT[0].Y)
                        return false;
                    else
                            if (FIJ[1] != 0 && PointY > Layers[FIJ[1] - 1].POINT[0].Y)
                        return false;
                }
                if (FIJ[2] == Layers[FIJ[1]].POINT.Count - 1)
                {
                    if (Layers.Count > FIJ[1] + 1 && PointY < Layers[FIJ[1] + 1].POINT[Layers[FIJ[1] + 1].POINT.Count - 1].Y)
                        return false;
                    else
                            if (FIJ[1] != 0 && PointY > Layers[FIJ[1] - 1].POINT[Layers[FIJ[1] - 1].POINT.Count - 1].Y)
                        return false;
                }
            }
            catch { return false; }
            return true;
        }
        /// <summary>
        /// Добавляем опорную точку в конец.
        /// </summary>
        /// <returns> Выполнил или нет. </returns>
        public bool AddLastPoint()
        {
            try
            {
                int i, Position;
                for (i = 0; i < Layers.Count; i++)
                    Layers[i].AddPoint(new PointSpline(XAreaSize, Layers[i].POINT[Layers[i].POINT.Count - 1].Y), out Position);
                Layers[i - 1].ReBuild();
            }
            catch { return false; }
            return true;
        }
        #endregion

        #region Работа с минералами
        /// <summary>
        /// Добавление нового минерала.
        /// </summary>
        /// <param name="Material"> Тип материала. </param>
        /// <returns> Выполнил или нет. </returns>
        public bool NewMinerals(Material Material)
        {
            try
            {
                Mineral tmp = new Mineral(Material, GetRandomColor());
                Minerals.Add(tmp);
            }
            catch { return false; }
            return true;
        }
        /// <summary>
        /// Проверка количества точек минерала.
        /// </summary>
        /// <returns> Больше 2 точек или нет. </returns>
        public bool CheckPointsMinerals()
        {
            if (Minerals[Minerals.Count - 1].POINT.Count > 2)
                return true;
            Minerals.RemoveAt(Minerals.Count - 1);
            return false;
        }
        /// <summary>
        /// Добавление точек минерала.
        /// </summary>
        /// <returns></returns>
        public bool AddPointMinerals()
        {
            try
            {
                if (Layers.Count > 1 && GetCoordinate(1) < Layers[0].ReturnMaxYX(GetCoordinate(0),0)
                    && GetCoordinate(1) > Layers[Layers.Count-1].ReturnMinYX(GetCoordinate(0),0))
                {
                    int Position;
                    Minerals[Minerals.Count - 1].AddPoint(new PointSpline(GetCoordinate(0), GetCoordinate(1)),out Position);
                    return true;
                }
            }
            catch { return false; }
            return false;
        }
        #endregion

        #region Отрисовка
        /// <summary>
        /// Рисуем.
        /// </summary>
        private void Drawing()
        {
            int i;
            Gl.glPushMatrix();
            /*Рисуем слои*/
            for (i = 0; i < Layers.Count; i++)
            {
                Color color = Layers[i].COLOR;
                DrawingSpline(Layers[i].ClonePoints(MassivNumber), color);
                if (SupportPoint)
                    DrawingSupportPoint(Layers[i].ClonePoints(0), color);
                else
                    if(i!=Layers.Count-1)
                    DrawingFilingLayers(Layers[i].ClonePoints(MassivNumber), Layers[i + 1].ClonePoints(MassivNumber), Layers[i].COLOR);
            }
            /*Рисуем минералы*/
            for (i = 0; i < Minerals.Count; i++)
            {
                Color color = Minerals[i].COLOR;
                DrawingSpline(Minerals[i].ClonePoints(MassivNumber), color);
                if (SupportPoint)
                        DrawingSupportPoint(Minerals[i].ClonePoints(0), color);
                else
                    DrawingFilingMinerals(Minerals[i].ClonePoints(MassivNumber), Minerals[i].COLOR);
            }
            Gl.glPopMatrix();
        }
        /// <summary>
        /// Вывод сплайна.
        /// </summary>
        /// <param name="Point"> Массив точек BSplin'а. </param>
        /// <param name="Color"> Цвет сплайна. </param>
        private void DrawingSpline(List<PointSpline> Point, Color Color)
        {
            int i;
            Gl.glColor3d(Color.R / 255.0, Color.G / 255.0, Color.B / 255.0);
            Gl.glPushMatrix();
            Gl.glLineWidth(2);
            Gl.glBegin(Gl.GL_LINE_STRIP);
            for (i = 0; i < Point.Count; i++)
                Gl.glVertex2d((Point[i].X - XAreaSize / 2) * Zoom + XOffset - 1, (YAreaSize / 2 - EarthSize + Point[i].Y) * Zoom + YOffset - 1);
            Gl.glEnd();
            Gl.glPopMatrix();
        }     
        /// <summary>
        /// Вывод опорных точек.
        /// </summary>
        /// <param name="Point"> Массив опорных точек. </param>
        /// <param name="Color"> Цвет сплайна. </param>
        private void DrawingSupportPoint(List<PointSpline> Point, Color Color)
        {
            int i;
            Gl.glColor4d(Color.R / 255.0, Color.G / 255.0, Color.B / 255.0, 0.7);
            /*Рисуем опорные точки*/
            Gl.glPushMatrix();
            Gl.glPointSize(7);
            Gl.glBegin(Gl.GL_POINTS);
            for (i = 0; i < Point.Count; i++)
                Gl.glVertex2d((Point[i].X - XAreaSize / 2) * Zoom + XOffset - 1, (YAreaSize / 2 - EarthSize + Point[i].Y) * Zoom + YOffset - 1);
            Gl.glEnd();
            Gl.glPopMatrix();
        }
        /// <summary>
        /// Заливка для слоев.
        /// </summary>
        /// <param name="FirstPoint"> Точки верхнего сплайна. </param>
        /// <param name="SecondPoint"> Точки нижнего сплайна. </param>
        /// <param name="Color"> Цвет слоя. </param>
        private void DrawingFilingLayers(List<PointSpline> FirstPoint, List<PointSpline> SecondPoint, Color Color)
        {
            int i;
            /*Вычисляем суммарое количество точек*/
            int N = FirstPoint.Count + SecondPoint.Count;
            /*Создаем массив для тесселяции*/
            double[][] TessPoints = new double[N][];
            /*Заполянем массив для тесселяции*/
            for (i = 0; i < FirstPoint.Count; i++)
            {
                TessPoints[i] = new double[3];
                TessPoints[i][0] = (FirstPoint[i].X - XAreaSize / 2) * Zoom + XOffset - 1;
                TessPoints[i][1] = (YAreaSize / 2 - EarthSize + FirstPoint[i].Y) * Zoom + YOffset - 1;
                TessPoints[i][2] = 0.0;
            }
            for (i = 0; i < SecondPoint.Count; i++)
            {
                TessPoints[i + FirstPoint.Count] = new double[3];
                TessPoints[i + FirstPoint.Count][0] = (SecondPoint[SecondPoint.Count - 1 - i].X - XAreaSize / 2) * Zoom + XOffset - 1;
                TessPoints[i + FirstPoint.Count][1] = (YAreaSize / 2 - EarthSize + SecondPoint[SecondPoint.Count - 1 - i].Y) * Zoom + YOffset - 1;
                TessPoints[i + FirstPoint.Count][2] = 0.0;
            }
            Tesselator(TessPoints, N, Color);
        }
        /// <summary>
        /// Заливка для минералов.
        /// </summary>
        /// <param name="Point"> Точки минерала. </param>
        /// <param name="Color"> Цвет минерала. </param>
        private void DrawingFilingMinerals(List<PointSpline> Point, Color Color)
        {
            int i;
            /*Создаем массив для тесселяции*/
            double[][] TessPoints = new double[Point.Count][];
            /*Заполянем массив для тесселяции*/
            for (i = 0; i < Point.Count; i++)
            {
                TessPoints[i] = new double[3];
                TessPoints[i][0] = (Point[i].X - XAreaSize / 2) * Zoom + XOffset - 1;
                TessPoints[i][1] = (YAreaSize / 2 - EarthSize + Point[i].Y) * Zoom + YOffset - 1;
                TessPoints[i][2] = 0.0;
            }
            Tesselator(TessPoints, Point.Count, Color);
        }
        /// <summary>
        /// Конвертируем double[3] в GLdouble.
        /// </summary>
        /// <param name="vertexData"> Массив с точками double[3]. </param>
        private void MyVertex3dv(System.IntPtr vertexData)
        {
            double[] v = new double[3];
            System.Runtime.InteropServices.Marshal.Copy(vertexData, v, 0, 3);
            Gl.glVertex3dv(v);
        }
        /// <summary>
        /// Тесселятор.
        /// </summary>
        /// <param name="TessPoints"> Массив точек тесселяции. </param>
        /// <param name="N"> Количество точек. </param>
        /// <param name="Color"> Цвет заливки. </param>
        private void Tesselator(double[][] TessPoints, int N, Color Color)
        {
            int i;
            /*Цвет*/
            Gl.glColor3d(Color.R / 255.0, Color.G / 255.0, Color.B / 255.0);
            /*Создаем объект тесселяции*/
            Glu.GLUtesselator pTess = Glu.gluNewTess();
            /*Заливка*/
            Glu.gluTessProperty(pTess, Glu.GLU_TESS_BOUNDARY_ONLY, Gl.GL_FALSE);
            /*Начало отрисовки*/
            Glu.gluTessCallback(pTess, Glu.GLU_TESS_BEGIN, new Glu.TessBeginCallback(Gl.glBegin));
            /*Конец отрисовки*/
            Glu.gluTessCallback(pTess, Glu.GLU_TESS_END, new Glu.TessEndCallback(Gl.glEnd));
            /*Передача координат*/
            Glu.gluTessCallback(pTess, Glu.GLU_TESS_VERTEX, new Glu.TessVertexCallback(MyVertex3dv));

            /*Начинаем тесселяцию*/
            Glu.gluTessBeginPolygon(pTess, IntPtr.Zero);
            /*Начинаем описание кривого полигона*/
            Glu.gluTessBeginContour(pTess);

            /*Заносим наши точки*/
            for (i = 0; i < N; i++)
                Glu.gluTessVertex(pTess, TessPoints[i], TessPoints[i]);

            /*Конец описания кривого полигона*/
            Glu.gluTessEndContour(pTess);
            /*Конец тесселяции*/
            Glu.gluTessEndPolygon(pTess);

            /*Удаляем тесселятор*/
            Glu.gluDeleteTess(pTess);
        }
        /// <summary>
        /// Вывод линий разбиения.
        /// </summary>
        private void DrawingLinePartition()
        {
            double i=0;
            double GridHalfDown = -GLPaint.Height / 2;
            double GridHalfUp = (GLPaint.Height - GlobalConst.Difference) / 2;
            Gl.glColor3d(ColorPartition.R / 255.0, ColorPartition.G / 255.0, ColorPartition.B / 255);
            Gl.glPushMatrix();
            Gl.glLineWidth(1);
            Gl.glBegin(Gl.GL_LINES);
            while(i<XAREASIZE)
            {
                Gl.glVertex2d((i - XAreaSize / 2) * Zoom + XOffset - 1, GridHalfDown);
                Gl.glVertex2d((i - XAreaSize / 2) * Zoom + XOffset - 1, GridHalfUp);
                i += PartitionX;
            }
            Gl.glEnd();
            Gl.glPopMatrix();
        }
        /// <summary>
        /// Вывод точек разбиения.
        /// </summary>
        private void DrawingPointPartition()
        {
            int i, j;
            Gl.glColor3d(ColorPartition.R / 255.0, ColorPartition.G / 255.0, ColorPartition.B / 255);
            Gl.glPushMatrix();
            Gl.glPointSize(4);
            Gl.glBegin(Gl.GL_POINTS);
            for (i = 0; i < Layers.Count; i++)
                for (j = 0; j < Layers[i].PARTITION.Count; j++)
                {
                    Gl.glVertex2d((Layers[i].PARTITION[j].POINT.X - XAreaSize / 2) * Zoom + XOffset - 1, (YAreaSize / 2 - EarthSize + Layers[i].PARTITION[j].POINT.Y) * Zoom + YOffset - 1);
                }
            Gl.glEnd();
            Gl.glPopMatrix();
            Gl.glPushMatrix();
            Gl.glPointSize(4);
            Gl.glBegin(Gl.GL_POINTS);
            for (i = 0; i < Minerals.Count; i++)
                for (j = 0; j < Minerals[i].PARTITION.Count; j++)
                {
                    Gl.glVertex2d((Minerals[i].PARTITION[j].POINT.X - XAreaSize / 2) * Zoom + XOffset - 1, (YAreaSize / 2 - EarthSize + Minerals[i].PARTITION[j].POINT.Y) * Zoom + YOffset - 1);
                }
            Gl.glEnd();
            Gl.glPopMatrix();
        }
        /// <summary>
        /// Вывод разбиения.
        /// </summary>
        private void DrawingPartition()
        {
            int i, j;
            Gl.glColor3d(ColorPartition.R / 255.0, ColorPartition.G / 255.0, ColorPartition.B / 255);           
            Gl.glLineWidth(2);            
            for (i = 0; i < FiniteElements.Count; i++)
            {
                Gl.glPushMatrix();
                Gl.glBegin(Gl.GL_LINES);
                FiniteElement KE = FiniteElements[i].Finit(1);
                Gl.glVertex2d((KE.POINTS[0].X - XAreaSize / 2) * Zoom + XOffset - 1, (YAreaSize / 2 - EarthSize + KE.POINTS[0].Y) * Zoom + YOffset - 1);
                Gl.glVertex2d((KE.POINTS[1].X - XAreaSize / 2) * Zoom + XOffset - 1, (YAreaSize / 2 - EarthSize + KE.POINTS[1].Y) * Zoom + YOffset - 1);
                Gl.glVertex2d((KE.POINTS[1].X - XAreaSize / 2) * Zoom + XOffset - 1, (YAreaSize / 2 - EarthSize + KE.POINTS[1].Y) * Zoom + YOffset - 1);
                Gl.glVertex2d((KE.POINTS[3].X - XAreaSize / 2) * Zoom + XOffset - 1, (YAreaSize / 2 - EarthSize + KE.POINTS[3].Y) * Zoom + YOffset - 1);
                Gl.glVertex2d((KE.POINTS[3].X - XAreaSize / 2) * Zoom + XOffset - 1, (YAreaSize / 2 - EarthSize + KE.POINTS[3].Y) * Zoom + YOffset - 1);
                Gl.glVertex2d((KE.POINTS[2].X - XAreaSize / 2) * Zoom + XOffset - 1, (YAreaSize / 2 - EarthSize + KE.POINTS[2].Y) * Zoom + YOffset - 1);
                Gl.glVertex2d((KE.POINTS[2].X - XAreaSize / 2) * Zoom + XOffset - 1, (YAreaSize / 2 - EarthSize + KE.POINTS[2].Y) * Zoom + YOffset - 1);
                Gl.glVertex2d((KE.POINTS[0].X - XAreaSize / 2) * Zoom + XOffset - 1, (YAreaSize / 2 - EarthSize + KE.POINTS[0].Y) * Zoom + YOffset - 1);
                Gl.glEnd();
                Gl.glPopMatrix();
            }                      
        }
        #endregion

        #region Работа с файлами
        /// <summary>
        /// Вывод сцены.
        /// </summary>
        /// <param name="Number"> Слой или минерал (1\2). </param>
        /// <param name="Color"> Цвет. </param>
        /// <param name="Material"> Материал. </param>
        /// <param name="Points"> Массив точек. </param>
        /// <returns> Выполнил или нет. </returns>
        public bool OutputScene(int Number,out List<string> Color,out List<string> Material, out List<string> Points)
        {
            Color = new List<string>();
            Material = new List<string>();
            Points = new List<string>();
            try
            {
                int i,j;
                string Buffer;
                if(Number == 1)
                    for(i=0;i<Layers.Count;i++)
                    {
                        Buffer = Layers[i].COLOR.R + " " + Layers[i].COLOR.G + " " + Layers[i].COLOR.B;
                        Color.Add(Buffer);
                        Buffer = Layers[i].MATERIAL.NAME + " " + Layers[i].MATERIAL.RESISTANCE;
                        Material.Add(Buffer);
                        Buffer = "";
                        for (j = 0; j < Layers[i].POINT.Count; j++)
                            Buffer += Layers[i].POINT[j].X + " " + Layers[i].POINT[j].Y + " ";
                        Points.Add(Buffer);
                    }
                if (Number == 2)
                    for (i = 0; i < Minerals.Count; i++)
                    {
                        Buffer = Minerals[i].COLOR.R + " " + Minerals[i].COLOR.G + " " + Minerals[i].COLOR.B;
                        Color.Add(Buffer);
                        Buffer = Minerals[i].MATERIAL.NAME + " " + Minerals[i].MATERIAL.RESISTANCE;
                        Material.Add(Buffer);
                        Buffer = "";
                        for (j = 0; j < Minerals[i].POINT.Count; j++)
                            Buffer += Minerals[i].POINT[j].X + " " + Minerals[i].POINT[j].Y + " ";
                        Points.Add(Buffer);
                    }
            }
            catch { return false; }
            return true;
        }
        /// <summary>
        /// Загрузка сцены.
        /// </summary>
        /// <param name="Number"> Слой или минерал (1\2). </param>
        /// <param name="ColorSpline"> Цвет. </param>
        /// <param name="MaterialSpline"> Материал. </param>
        /// <param name="PointsSpline"> Массив точек. </param>
        /// <returns></returns>
        public bool InputScene(int Number, string ColorSpline, string MaterialSpline, string PointsSpline)
        {
            try
            {
                int i;
                /*Разбиваем цвет на фрагменты*/
                string[] Str = ColorSpline.Split(' ');
                /*Сохраняем цвет*/
                Color _Color = Color.FromArgb(255, Convert.ToInt32(Str[0]), Convert.ToInt32(Str[1]), Convert.ToInt32(Str[2]));
                /*Разбиваем материал на фрагменты*/
                Str = MaterialSpline.Split(' ');
                /*Сохраняем материал*/
                string Name = "";
                for (i = 0; i < Str.Length - 2; i++)
                    Name += Str[i] + " ";
                Name = Name.TrimEnd(' ');
                Material _Material = new Material(Name, Str[i]);
                /*Разбиваем массив точек на фрагменты*/
                Str = PointsSpline.Split(' ');
                List<PointSpline> _Points = new List<PointSpline>();
                for (i = 0; i < Str.Length - 3; i += 2)
                    _Points.Add(new PointSpline(Str[i], Str[i + 1]));
                if (Number == 1)
                    Layers.Add(new Layer(_Color, _Material, _Points));
                if(Number == 2)
                    Minerals.Add(new Mineral(_Color, _Material, _Points));
            }
            catch { return false; }
            return true;
        }
        /// <summary>
        /// Вывод значений Y.
        /// </summary>
        /// <param name="PointY"> Массив значнеий Y. </param>
        /// <returns></returns>
        public bool OutputKEY(out List<double> PointY)
        {        
            try
            {
                int i;
                List<double> Point1 = new List<double>();
                for (i = 0; i < FiniteElements.Count; i++)
                {
                    Point1.Add(FiniteElements[i].Finit(1).POINTS[0].Y);
                    Point1.Add(FiniteElements[i].Finit(1).POINTS[2].Y);
                }
                Point1.Sort(delegate (double x, double y)
                {
                    return y.CompareTo(x);
                });
                PointY = new List<double>(Point1.Distinct());
            }
            catch { PointY = new List<double>(); return false; }
            return true;
        }
        /// <summary>
        /// Вывод значений X.
        /// </summary>
        /// <param name="PointX"> Массив значнеий X. </param>
        /// <returns></returns>
        public bool OutputKEX(out List<double> PointX)
        {
            try
            {
                int i;
                List<double> Point1 = new List<double>();
                for (i = 0; i < FiniteElements.Count; i++)
                {
                    Point1.Add(FiniteElements[i].Finit(1).POINTS[0].X);
                    Point1.Add(FiniteElements[i].Finit(1).POINTS[1].X);
                }
                Point1.Sort(delegate (double x, double y)
                {
                    return x.CompareTo(y);
                });
                PointX = new List<double>(Point1.Distinct());
            }
            catch { PointX = new List<double>(); return false; }
            return true;
        }

        public bool OutputKE(List<Material> ML, List<Material> MM, List<double> PointX, List<double> PointY, out List<string> KE)
        {
            KE = new List<string>();
            try
            {
                int i,j;
                for(i=0;i<FiniteElements.Count;i++)
                {
                    string Add="";
                    for(j=0;j<FiniteElements[i].POINTS.Length;j++)
                    {
                        FiniteElement FE = FiniteElements[i].Finit(1);
                        Add += " " + PointX.BinarySearch(FE.POINTS[j].X);
                        Add += " " + PointY.IndexOf(FE.POINTS[j].Y);
                    }
                    for(j=0;j<ML.Count;j++)
                        if(ML[j] == FiniteElements[i].MATERIAL[0])
                            Add += " " + j;
                    if (FiniteElements[i].MATERIAL[1] != null)
                        for (j = 0; j < ML.Count; j++)
                            if (MM[j] == FiniteElements[i].MATERIAL[0])
                                Add += " " + j + ML.Count;
                    KE.Add(Add);
                }
            }
            catch { return false; }
            return true;
        }
        #endregion

        #endregion

        /// <summary>
        /// Основной метод класса.
        /// </summary>
        public void Draw()
        {
            Gl.glClearColor(1.0f, 1.0f, 1.0f, 0.0f);
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
            Gl.glPushMatrix();
            /*Рисуем основное*/
            Drawing();
            /*Рисуем сетку*/
            if (Grid)
                DrawingGrid();
            /*Рисуем линии разбиения*/
            if (LinePartition)
                DrawingLinePartition();
            /*Рисуем точки разбиения*/
            if (PointPartition)
                DrawingPointPartition();
            /*Рисуем разбиение*/
            if (Partition)
                DrawingPartition();
            /*Рисуем разметку*/
            if (Marking)
                DrawingMarking();          
            Gl.glPopMatrix();
            Gl.glFinish();
            GLPaint.Invalidate();
        }
    }
}