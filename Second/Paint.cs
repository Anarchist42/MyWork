using System;
using System.Collections.Generic;
using System.Drawing;


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
        /// <summary>
        /// Нужна ли опорная линия.
        /// </summary>
        private bool SupportLine;
        /// <summary>
        /// Нужна ли апроксимация сплайнами.
        /// </summary>
        private bool BSpline;
        /// <summary>
        /// Нужна ли интерполяция сплайнами.
        /// </summary>
        private bool CSpline;
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
        
        #endregion

        bool flag = false;
        #endregion

        #region Конструктор
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="Paint"> Указатель на OpenGlControl. </param>
        public Paint(SimpleOpenGlControl Paint)
        {
            this.GLPaint = Paint;
            this.MousePosition = new Point(0, 0);
            this.ZoomWheel = 0.01;
            this.EarthSize = 0;
            this.CellsNumber = 10;
            this.XOffset = 0.0;
            this.YOffset = 0.0;
            this.XAreaSize = 0.0;
            this.YAreaSize = 0.0;
            this.DefYAreaSize = 0.0;
            this.Grid = false;
            this.Marking = false;
            this.SupportLine = false;
            this.BSpline = false;
            this.CSpline = false;
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
                if (Convert.ToDouble(GLPaint.Width) / Convert.ToDouble(GLPaint.MinimumSize.Width) > Convert.ToDouble(GLPaint.Height) / Convert.ToDouble(GLPaint.MinimumSize.Height))
                    this.MINZOOM = GlobalConst.MinZoom * Convert.ToDouble(GLPaint.Width - GlobalConst.Difference) / Convert.ToDouble(GLPaint.MinimumSize.Width - GlobalConst.Difference);
                else
                    this.MINZOOM = GlobalConst.MinZoom * Convert.ToDouble(GLPaint.Height - GlobalConst.Difference)
                        / Convert.ToDouble(GLPaint.MinimumSize.Height - GlobalConst.Difference);
                this.MaxZoom = MinZoom * XAreaSize * Math.Pow(10, GlobalConst.Accuracy) / Convert.ToDouble(GLPaint.MinimumSize.Width - GlobalConst.Difference);
                ZoomWheel = (MaxZoom - MinZoom) / 10;
                /*Высота области*/
                this.DefYAreaSize = Math.Round(Convert.ToDouble(GLPaint.MinimumSize.Height - GlobalConst.Difference) / GlobalConst.MinZoom, GlobalConst.Accuracy);
                if (this.YAreaSize < DefYAreaSize)
                    this.YAreaSize = DefYAreaSize;
                /*Сбрасываем смещение*/
                this.XOffset = 0.0;
                this.YOffset = 0.0;
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
            set { this.Zoom = (value > MinZoom) ? (value < MaxZoom - ZoomWheel) ? value : MaxZoom - ZoomWheel : MinZoom; }
        }
        public double MINZOOM
        {
            get { return this.MinZoom; }
            set { this.MinZoom = value; this.Zoom = value; }
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
        public bool SUPPORTLINE
        {
            get { return this.SupportLine; }
            set { this.SupportLine = value; }
        }
        public bool BSPLINE
        {
            get { return this.BSpline; }
            set { this.BSpline = value; }
        }
        public bool CSPLINE
        {
            get { return this.CSpline; }
            set { this.CSpline = value; }
        }
        #endregion

        #region Методы

        #region Сетка, Разметка
        /// <summary>
        /// Построение сетки.
        /// </summary>
        private void DrawingGrid()
        {
            int i;
            double GridHalfDown = -GLPaint.Height / 2;
            double GridHalfUp = (GLPaint.Height - GlobalConst.Difference) / 2;
            double GridHalfLeft = -GLPaint.Width / 2;
            double GridHalfRight = (GLPaint.Width - GlobalConst.Difference) / 2;
            /*так как максимум на линии - cells_number клеток, 
            то надо выбрать по Х или Y будем выбирать шаг*/
            int GridStep = (Convert.ToInt32((GLPaint.Width - GlobalConst.Difference) / CellsNumber)
                > Convert.ToInt32((GLPaint.Height - GlobalConst.Difference) / CellsNumber))
                ? Convert.ToInt32((GLPaint.Width - GlobalConst.Difference) / CellsNumber)
                : Convert.ToInt32((GLPaint.Height - GlobalConst.Difference) / CellsNumber);
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glPushMatrix();
            Gl.glLineWidth(1);
            Gl.glColor3d(0.8, 0.8, 0.8);
            for (i = -CellsNumber / 2 - 5; i < CellsNumber / 2 + 5; i++)
            {
                Gl.glBegin(Gl.GL_LINES);
                Gl.glVertex2d(GridHalfLeft, i * GridStep - 2 + YOffset % GridStep);
                Gl.glVertex2d(GridHalfRight, i * GridStep - 2 + YOffset % GridStep);

                Gl.glVertex2d(i * GridStep - 2 + XOffset % GridStep, GridHalfDown);
                Gl.glVertex2d(i * GridStep - 2 + XOffset % GridStep, GridHalfUp);
                Gl.glEnd();
            }
            Gl.glPopMatrix();
        }
        /// <summary>
        /// Построение разметки.
        /// </summary>
        private void DrawingMarking()
        {
            int i;
            double iX, iY;
            double GridHalfUp = (GLPaint.Height - GlobalConst.Difference) / 2 + 1;
            double GridHalfLeft = -GLPaint.Width / 2 + 2;
            double iYCoord = (YAreaSize * Zoom - GLPaint.Height + GlobalConst.Difference) / 2 + YOffset;
            double iXCoord = (XAreaSize * Zoom - GLPaint.Width + GlobalConst.Difference) / 2 - XOffset;
            /*так как максимум на линии - cells_number клеток, 
             то надо выбрать по Х или Y будем выбирать шаг*/
            int GridStep = (Convert.ToInt32((GLPaint.Width - GlobalConst.Difference) / CellsNumber)
                > Convert.ToInt32((GLPaint.Height - GlobalConst.Difference) / CellsNumber))
                ? Convert.ToInt32((GLPaint.Width - GlobalConst.Difference) / CellsNumber)
                : Convert.ToInt32((GLPaint.Height - GlobalConst.Difference) / CellsNumber);
            Gl.glPushMatrix();
            Gl.glLineWidth(1);
            Gl.glColor3d(0.3, 0.3, 0.3);
            Gl.glPointSize(3);
            /*вертикальная*/
            for (i = -CellsNumber / 2 - 5; i < CellsNumber / 2 + 5; i++)
            {
                /*Переменные*/
                iX = XOffset % GridStep;
                iY = YOffset % GridStep;
                /*Основное тело цикла*/
                Gl.glPushMatrix();
                Gl.glBegin(Gl.GL_POINTS);
                Gl.glVertex2d(-2.0 + iX, i * GridStep - 2 + iY);
                Gl.glEnd();
                Gl.glTranslated(5.0 + iX, i * GridStep + 5.0 + iY, 0.0);
                Gl.glScaled(0.08, 0.08, 0.08);
                string number = Math.Round(EarthSize - (iYCoord - Convert.ToInt32(i * GridStep + iY - GridHalfUp)) / Zoom, 1).ToString();
                for (int j = 0; j < number.Length; j++)
                    Glut.glutStrokeCharacter(Glut.GLUT_STROKE_ROMAN, number[j]);
                Gl.glPopMatrix();
            }
            /*горизонтальная*/
            for (i = -CellsNumber / 2 - 5; i < CellsNumber / 2 + 5; i++)
            {
                if (i != 0)
                {
                    /*Переменные*/
                    iX = XOffset % GridStep;
                    iY = YOffset % GridStep;
                    /*Основное тело цикла*/
                    Gl.glPushMatrix();
                    Gl.glBegin(Gl.GL_POINTS);
                    Gl.glVertex2d(i * GridStep - 2 + iX, -2.0 + iY);
                    Gl.glEnd();
                    Gl.glTranslated(i * GridStep + 5.0 + iX, -15.0 + iY, 0.0);
                    Gl.glScaled(0.08, 0.08, 0.08);
                    string number = Math.Round((iXCoord + Convert.ToInt32(i * GridStep + iX - GridHalfLeft)) / Zoom, 1).ToString();
                    for (int j = 0; j < number.Length; j++)
                        Glut.glutStrokeCharacter(Glut.GLUT_STROKE_ROMAN, number[j]);
                    Gl.glPopMatrix();
                }
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
                return Math.Round((EarthSize - ((YAreaSize * Zoom - GLPaint.Height + GlobalConst.Difference) / 2 + YOffset + MousePosition.Y) / Zoom), GlobalConst.Accuracy);
        }
        /*Вычисление максимального значения ползунка*/
        /*Параметры: TypeScroll - тип скролла ( 0 - для вертикального, 1 - для горизонтального)*/
        /*Возвращает: Максимальное значение искомого типа ползунка*/
        public double ScrollMaximum(int TypeScroll)
        {
            if (TypeScroll == 0)
                return
                    Math.Round(
                    /*Исходный размер*/
                    
                    /*Сколько вверх*/
                    ((YAreaSize * Zoom - GLPaint.Height + GlobalConst.Difference) / 2 + YOffset) / Zoom +
                    /*Сколько вниз*/
                    YAreaSize - ((YAreaSize * Zoom - GLPaint.Height + GlobalConst.Difference) / 2 + YOffset + GLPaint.Height - GlobalConst.Difference) / Zoom,GlobalConst.Accuracy);
            else
                return
                    Math.Round(
                   /*Исодный размер*/
                   
                   /*Сколько влево*/
                   ((XAreaSize * Zoom - GLPaint.Width + GlobalConst.Difference) / 2 - XOffset) / Zoom +
                   /*Сколько вправо*/
                   XAreaSize - ((XAreaSize * Zoom - GLPaint.Width + GlobalConst.Difference) / 2 - XOffset + GLPaint.Width - GlobalConst.Difference) / Zoom, GlobalConst.Accuracy);
        }
        /*Вычисление значения ползунка*/
        /*Параметры: TypeScroll ( 0 - для вертикального, 1 - для горизонтального)*/
        /*Возвращает: Значение искомого типа ползунка*/
        public double ScrollValue(int TypeScroll)
        {
            if (TypeScroll == 0)
                return Math.Round(Math.Abs(((YAreaSize * Zoom - GLPaint.Height + GlobalConst.Difference) / 2 + YOffset) / Zoom),GlobalConst.Accuracy);
            else
                return Math.Round(Math.Abs(((XAreaSize * Zoom - GLPaint.Width + GlobalConst.Difference) / 2 - XOffset) / Zoom),GlobalConst.Accuracy);
        }
        /*Вычисление шага ползунка*/
        /*Параметры: TypeScroll ( 0 - для вертикального, 1 - для горизонтального)*/
        /*           MaximumScroll - сколько доступно значений для прокрутки*/
        /*Возвращает: Значение шага искомого типа ползунка*/
        public double ScrollStep(int TypeScroll, int MaximumScroll)
        {
            if (TypeScroll == 0)
                return
                    /*Сколько вверх*/
                    (((YAreaSize * Zoom - GLPaint.Height + GlobalConst.Difference) / 2 + YOffset) / Zoom +
                    /*Сколько вниз*/
                    YAreaSize - ((YAreaSize * Zoom - GLPaint.Height + GlobalConst.Difference) / 2 + YOffset + GLPaint.Height - GlobalConst.Difference) / Zoom) / MaximumScroll * Zoom;
            else
                return
                   /*Сколько влево*/
                   (((XAreaSize * Zoom - GLPaint.Width + GlobalConst.Difference) / 2 - XOffset) / Zoom +
                   /*Сколько вправо*/
                   XAreaSize - ((XAreaSize * Zoom - GLPaint.Width + GlobalConst.Difference) / 2 - XOffset + GLPaint.Width - GlobalConst.Difference) / Zoom) / MaximumScroll * Zoom;
        }
        /*Изменение сдвига по осям (приближение)*/
        public void ChangeOffsetZoomIn()
        {
            ZOOM += ZoomWheel;
            if (flag==false)
            {
                this.YOFFSET =
                       (((YAreaSize * (Zoom - ZoomWheel) - GLPaint.Height + GlobalConst.Difference) / 2 + YOffset + MousePosition.Y) / (Zoom - ZoomWheel)) * Zoom
                       - (YAreaSize * Zoom - GLPaint.Height + GlobalConst.Difference) / 2 - MousePosition.Y;
                this.XOFFSET =
                    (XAreaSize * Zoom - GLPaint.Width + GlobalConst.Difference) / 2 - (((XAreaSize * (Zoom - ZoomWheel) - GLPaint.Width + GlobalConst.Difference) / 2
                    - XOffset + MousePosition.X) / (Zoom - ZoomWheel)) * Zoom + MousePosition.X;
            }
            if (Zoom == MaxZoom - ZoomWheel)
                flag = true;
        }
        /*Изменение сдвига по осям (отдаление)*/
        public void ChangeOffsetZoomOut()
        {
            ZOOM -= ZoomWheel;
            if (Zoom != MinZoom)
            {
                this.YOFFSET -= (YOffset / ((Zoom - MinZoom) / ZoomWheel));
                this.XOFFSET -= (XOffset / ((Zoom - MinZoom) / ZoomWheel));
            }
            else
            {
                this.YOFFSET = 0.0;
                this.XOFFSET = 0.0;
            }
            flag = false;
        }
        /*Изменение сдвига по осям (мышка)*/
        /*Параметры: MouseDownPosition - позиция нажатия левой кнопки мыши*/
        public void ChangeOffsetZoomMouse(Point MouseDownPosition)
        {
            //if (Zoom != MinZoom || YAreaSize * Zoom== GLPaint.Height + GlobalConst.Difference)
            {
                this.YOFFSET = (((YAreaSize * Zoom - GLPaint.Height + GlobalConst.Difference) / 2 + YOffset + MouseDownPosition.Y) / Zoom) * Zoom
                       - (YAreaSize * Zoom - GLPaint.Height + GlobalConst.Difference) / 2 - MousePosition.Y;
                this.XOFFSET = (XAreaSize * Zoom - GLPaint.Width + GlobalConst.Difference) / 2 - (((XAreaSize * Zoom - GLPaint.Width + GlobalConst.Difference) / 2
                        - XOffset + MouseDownPosition.X) / Zoom) * Zoom + MousePosition.X;
            }
        }
        #endregion


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



        #region Слои и Минералы (общие функции)
        /// <summary>
        /// Задаем цвет сплайна.
        /// </summary>
        /// <param name="Color"> Задаваемый цвет.</param>
        /// <param name="FIJ"> Массив [0] - 0(нету),1(слой),2(минерал); 
        ///                           [1] - номер слоя\минерала;
        ///                           [2] - номер опорной точки.</param>
        /// <returns></returns>
        public bool SetLayerColor(Color Color, int[] FIJ)
        {
            try
            {
                int i;
                //Проверяем есть ли такой цвет в слоях.
                for (i = 0; i < Layers.Count; i++)
                    if (Layers[i].COLOR == Color)
                        return false;
                //Проверяем есть ли такой цвет в минералах.
                for (i = 0; i < Minerals.Count; i++)
                    if (Minerals[i].COLOR == Color)
                        return false;
                //Если нету, то меняем цвем.
                if (FIJ[0] == 1)
                    Layers[FIJ[1]].COLOR = Color;
                else
                    Minerals[FIJ[1]].COLOR = Color;
            }
            catch
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// Изменение точности сплайнов.
        /// </summary>
        /// <returns></returns>
        public bool ChangeAccuracy()
        {          
            try
            {
                int i;
                for (i = 0; i < Layers.Count; i++)
                    Layers[i].ChangeAccuracy();
                for (i = 0; i < Minerals.Count; i++)
                    Minerals[i].ChangeAccuracy();
            }
            catch
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// Добавление опорной точки.
        /// </summary>
        /// <param name="FIJ"> Массив [0] - 0(нету),1(слой),2(минерал); 
        ///                           [1] - номер слоя\минерала;
        ///                           [2] - номер опорной точки.</param>
        /// <returns></returns>
        public bool AddPoint(int[] FIJ)
        {
            try
            {
                if(FIJ[0]==1)
                    Layers[FIJ[1]].AddPoint(new PointSpline(GetCoordinate(0), GetCoordinate(1)));
                else
                    Minerals[FIJ[1]].AddPoint(new PointSpline(GetCoordinate(0), GetCoordinate(1)));
            }
            catch
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// Удаление опорной точки.
        /// </summary>
        /// <param name="FIJ"> Массив [0] - 0(нету),1(слой),2(минерал); 
        ///                           [1] - номер слоя\минерала;
        ///                           [2] - номер опорной точки.</param>
        /// <returns></returns>
        public bool DeletePoint(int[] FIJ)
        {
            if (FIJ[0] == 1)
                Layers[FIJ[1]].DeletePoint(FIJ[2]);
            else
                Minerals[FIJ[1]].DeletePoint(FIJ[2]);
            return true;
        }
        /// <summary>
        /// Перемещение сплайнов(вправо) на заданное расстояние.
        /// </summary>
        /// <param name="ChangeX"> Расстояние на которое перемещаем. </param>
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
            catch
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// Проверка попали ли на опорную точку.
        /// </summary>
        /// <param name="MouseDownPosition"> Позиция нажатия мышки.</param>
        /// <param name="FIJ"> Массив [0] - 0(нету),1(слой),2(минерал); 
        ///                           [1] - номер слоя\минерала;
        ///                           [2] - номер опорной точки.</param>
        /// <returns></returns>
        public bool CheckPoint(Point MouseDownPosition, out int[] FIJ)
        {
            FIJ = new int[3];
            FIJ[0] = 0;
            FIJ[1] = 0;
            FIJ[2] = 0;
            try
            {
                if (SupportLine)
                {
                    double[] X = new double[2];
                    double[] Y = new double[2];
                    int i, j;
                    X[0] = ((XAreaSize * Zoom - GLPaint.Width + GlobalConst.Difference) / 2 - XOffset + MousePosition.X - 3) / Zoom;
                    X[1] = ((XAreaSize * Zoom - GLPaint.Width + GlobalConst.Difference) / 2 - XOffset + MousePosition.X + 3) / Zoom;
                    Y[0] = EarthSize - ((YAreaSize * Zoom - GLPaint.Height + GlobalConst.Difference) / 2 + YOffset + MousePosition.Y - 3) / Zoom;
                    Y[1] = EarthSize - ((YAreaSize * Zoom - GLPaint.Height + GlobalConst.Difference) / 2 + YOffset + MousePosition.Y + 3) / Zoom;
                    List<PointSpline> Points;
                    for (i = 0; i < Layers.Count; i++)
                    {
                        Points = Layers[i].POINT;
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
                        Points = Minerals[i].POINT;
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
            catch
            {
                return false;
            }
            return true;
        }

        private bool CheckSpline(PointSpline Point)
        {

            return true;
        }
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
                    if (FIJ[2] != 0 && FIJ[2] != Points.Count - 1)
                        Points[FIJ[2]] = new PointSpline(Point.X, Point.Y);
                    else
                        Points[FIJ[2]] = new PointSpline(Points[FIJ[2]].X, Point.Y);
                    /*Проверка на самопересечение*/
                    for (int i = 0; i < Points.Count - 1; i++)
                        for (int j = i + 2; j < Points.Count - 1; j++)
                            if (IntersectParts(Points[i], Points[i + 1], Points[j], Points[j + 1]))
                            {
                                /*Если пересек - сохраняем старое значение и выходим*/
                                Points[FIJ[2]] = PrefPoint;
                                return false;
                            }
                    if (Math.Abs(Points[FIJ[2]].Y) + XAreaSize / 100 + EarthSize > YAreaSize)
                    {
                        YAreaSize += XAreaSize / 100;
                        YOffset += XAreaSize / 100;
                    }
                    Layers[FIJ[1]].BSpline();
                }
                else
                {
                    Points = Minerals[FIJ[1]].POINT;
                    Points.Add(Points[Points.Count - 1]);
                    PrefPoint = Points[FIJ[2]];
                    Points[FIJ[2]] = new PointSpline(Point.X, Point.Y);
                    /*Проверка на самопересечение*/
                    for (int i = 0; i < Points.Count - 1; i++)
                        for (int j = i + 1; j < Points.Count - 1; j++)
                            if (IntersectParts(Points[i], Points[i + 1], Points[j], Points[j + 1]))
                            {
                                /*Если пересек - сохраняем старое значение и выходим*/
                                Points[FIJ[2]] = PrefPoint;
                                return false;
                            }
                    Points.RemoveAt(Points.Count - 1);
                    Minerals[FIJ[1]].BSpline();
                }
            }
            catch
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// Удаление сплайна с формы.
        /// </summary>
        /// <param name="FIJ"> Массив [0] - 0(нету),1(слой),2(минерал); 
        ///                           [1] - номер слоя\минерала;
        ///                           [2] - номер опорной точки.</param>
        /// <returns></returns>
        public bool DeleteSplineForm(int[] FIJ)
        {
            try
            {
                if (FIJ[0] == 1)
                {
                    if(Layers.Count > 1)
                        Layers.RemoveAt(FIJ[1]);
                    else
                        return false;
                }
                else
                    Minerals.RemoveAt(FIJ[1]);
                YAreaSize = GetMaxPointLayers();
            }
            catch
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// Удаление сплайна с таблицы.
        /// </summary>
        /// <param name="NumberGrid"> Номер таблицы - 1\2 (слои\минералы). </param>
        /// <param name="IndexNumber"></param>
        /// <returns></returns>
        public bool DeleteSplineGrid(int NumberGrid, int IndexNumber)
        {
            int i, k;
            try
            {
                if (NumberGrid == 1)
                {
                    Layers.RemoveAt(IndexNumber+1);
                    YAreaSize = GetMaxPointLayers();
                }
                else
                    Minerals.RemoveAt(IndexNumber);
            }
            catch
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// Удаляем все точки больше области.
        /// </summary>
        public bool DeletePoints()
        {
            int i;
            try
            {
                for (i = 0; i < Layers.Count; i++)
                    Layers[i].DeletePoint(XAreaSize);
                for (i = 0; i < Minerals.Count; i++)
                    Minerals[i].DeletePoint(XAreaSize);
            }
            catch
            {
                return false;
            }
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
        public bool ReturnInformation(int NumberGrid,int PositionNumber, out Color Color, out string LayerPosition, out string Material)
        {
            Color = Color.White;
            LayerPosition = "";
            Material = "";
            try
            {
                //Если слой
                if(NumberGrid==0)
                {
                    Color = Layers[PositionNumber].COLOR;
                    LayerPosition = Layers[PositionNumber].ReturnMaxY().ToString() + "\n" + Layers[PositionNumber + 1].ReturnMinY().ToString();
                    Material = Layers[PositionNumber].MATERIAL.NAME;
                }
                //Если минерал
                else
                {
                    Color = Minerals[PositionNumber].COLOR;
                    LayerPosition = Minerals[PositionNumber].ReturnMaxY().ToString() + "\n" + Minerals[PositionNumber].ReturnMinY().ToString();
                    Material = Minerals[PositionNumber].MATERIAL.NAME;
                }
            }
            catch
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// Изменение материала.
        /// </summary>
        /// <param name="FIJ"> Массив [0] - 0(нету),1(слой),2(минерал); 
        ///                           [1] - номер слоя\минерала;</param>
        /// <param name="Material"> Материал. </param>
        /// <returns></returns>
        public bool ChangeMaterial(int[] FIJ,Material Material)
        {
            try
            {
                if (FIJ[0] == 1)
                    Layers[FIJ[1]].MATERIAL = Material;
                else
                    Minerals[FIJ[1]].MATERIAL = Material;
            }
            catch
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// Возвращает материал слоя или минерала.
        /// </summary>
        /// <param name="FIJ"> Массив [0] - 0(нету),1(слой),2(минерал); 
        ///                           [1] - номер слоя\минерала;</param>
        /// <param name="Material"> Материал. </param>
        /// <returns></returns>
        public bool ReturnMaterial(int[] FIJ,out Material Material)
        {
            Material = new Material();
            try
            {
                if (FIJ[0] == 1)
                    Material = Layers[FIJ[1]].MATERIAL;
                else
                    Material = Minerals[FIJ[1]].MATERIAL;
            }
            catch
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// Проверка есть ли материал.
        /// </summary>
        /// <param name="NumberGrid"> Номер таблицы (0 - слои, 1 - минералы). </param>
        /// <param name="Material"> Материал. </param>
        /// <param name="Number"> Массив найденных совпадений. </param>
        /// <returns></returns>
        public bool CheckMaterial(int NumberGrid,Material Material, out List<int> Number)
        {
            Number = new List<int>();
            int i;
            try
            {
                if(NumberGrid==0)
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
            catch
            {
                return false;
            }
            return Number.Count==0;
        }
        
        #endregion


        #region Работа со слоями
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
                double Height = Math.Abs(Layers[Layers.Count - 1].ReturnMinY()) + XAreaSize / 10 + EarthSize;
                return Height > DefYAreaSize ? Height : DefYAreaSize;
            }
        }
        /// <summary>
        /// Добавление сплайна.
        /// </summary>
        /// <param name="LayerHeight"></param>
        /// <param name="NumberOfPoints"></param>
        /// <param name="Material"></param>
        public bool AddLayers(double LayerHeight, int NumberOfPoints, Material Material)
        {
            /*Если новый слой имеет глубину больше чем есть сейчас*/
            if (Math.Abs(LayerHeight) + EarthSize + XAreaSize / 10 > YAreaSize)
                this.YAREASIZE = Math.Abs(LayerHeight) + EarthSize + XAreaSize / 10;
            if (Layers.Count == 1)
                Layers[0].MATERIAL = Material;
            /*Добавляем новый слой*/
            Layer tmp = new Layer(XAreaSize, LayerHeight, NumberOfPoints, Material);
            Layers.Add(tmp);
            /*Сортируем по высоте*/
            Layers.Sort(delegate (Layer x, Layer y)
            {
                return y.POINT[0].Y.CompareTo(x.POINT[0].Y);
            });
            return true;
        }
        /// <summary>
        /// Добавляем опорную точку в конец.
        /// </summary>
        public void AddLastPoint()
        {
            int i = 0;
            for (i = 0; i < Layers.Count; i++)
                Layers[i].AddPoint(new PointSpline(XAreaSize, Layers[i].POINT[Layers[i].POINT.Count - 1].Y));
        }
        #endregion




        #region Работа с минералами
        /// <summary>
        /// Добавление нового минерала.
        /// </summary>
        /// <param name="Material"> Тип материала. </param>
        /// <returns></returns>
        public bool NewMinerals(Material Material)
        {
            try
            {
                Mineral tmp = new Mineral(Material);
                Minerals.Add(tmp);
            }
            catch
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// Проверка количества точек минерала.
        /// </summary>
        /// <returns></returns>
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
                Minerals[Minerals.Count - 1].AddPoint(new PointSpline(GetCoordinate(0), GetCoordinate(1)));
            }
            catch
            {
                return false;
            }
            return true;
        }
        #endregion



        /// <summary>
        /// Рисуем сплайны.
        /// </summary>
        private void Drawing()
        {
            int i;
            List<PointSpline> point = new List<PointSpline>();
            Gl.glPushMatrix();
            /*Рисуем слои*/
            for (i = 0; i < Layers.Count; i++)
            {
                point = Layers[i].BSPLINEPOINT;
                Color color = Layers[i].COLOR;
                DrawingBSpline(point, color);
                if (SupportLine)
                {
                    point = Layers[i].POINT;
                    DrawingSupportLine(point, color);
                }
            }
            /*Рисуем минералы*/
            for (i = 0; i < Minerals.Count; i++)
            {
                point = Minerals[i].BSPLINEPOINT;
                Color color = Minerals[i].COLOR;
                DrawingBSpline(point, color);
                if (SupportLine)
                {
                    point = Minerals[i].POINT;
                    if (point.Count != 0)
                    {
                        point.Add(point[0]);
                        DrawingSupportLine(point, color);
                        point.RemoveAt(point.Count - 1);
                    }
                }
            }
            Gl.glPopMatrix();
        }
        /// <summary>
        /// Вывод сплайна.
        /// </summary>
        /// <param name="Point"> Массив точек BSplin'а. </param>
        /// <param name="Color"> Цвет сплайна. </param>
        private void DrawingBSpline(List<PointSpline> Point, Color Color)
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
        /// Вывод опорных линий.
        /// </summary>
        /// <param name="Point"> Массив опорных точек. </param>
        /// <param name="Color"> Цвет сплайна. </param>
        private void DrawingSupportLine(List<PointSpline> Point, Color Color)
        {
            int i;
            Gl.glColor4d(Color.R / 255.0, Color.G / 255.0, Color.B / 255.0, 0.7);
            /*Рисуем опорные линии*/
            Gl.glPushMatrix();
            Gl.glLineWidth(2);
            Gl.glBegin(Gl.GL_LINE_STRIP);
            for (i = 0; i < Point.Count; i++)
                Gl.glVertex2d((Point[i].X - XAreaSize / 2) * Zoom + XOffset - 1, (YAreaSize / 2 - EarthSize + Point[i].Y) * Zoom + YOffset - 1);
            Gl.glEnd();
            Gl.glPopMatrix();
            /*Рисуем опорные точки*/
            Gl.glPushMatrix();
            Gl.glPointSize(7);
            Gl.glBegin(Gl.GL_POINTS);
            for (i = 0; i < Point.Count; i++)
                Gl.glVertex2d((Point[i].X - XAreaSize / 2) * Zoom + XOffset - 1, (YAreaSize / 2 - EarthSize + Point[i].Y) * Zoom + YOffset - 1);
            Gl.glEnd();
            Gl.glPopMatrix();
        }
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
            /*Рисуем разметку*/
            if (Marking)
                DrawingMarking();        
            Gl.glPopMatrix();
            Gl.glFinish();
            GLPaint.Invalidate();
        }
    }
}
