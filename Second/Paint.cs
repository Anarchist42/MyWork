using System;
using System.Collections.Generic;
using System.Drawing;

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
        /// <summary>
        /// Рандом для цвета.
        /// </summary>
        Random random = new Random();    
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
                ZoomWheel = 0.01;
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
            ZOOM += ZoomWheel;
                this.YOFFSET =
                       (((YAreaSize * (Zoom - ZoomWheel) - GLPaint.Height + GlobalConst.Difference) / 2 + YOffset + MousePosition.Y) / (Zoom - ZoomWheel)) * Zoom
                       - (YAreaSize * Zoom - GLPaint.Height + GlobalConst.Difference) / 2 - MousePosition.Y;
                this.XOFFSET =
                    (XAreaSize * Zoom - GLPaint.Width + GlobalConst.Difference) / 2 - (((XAreaSize * (Zoom - ZoomWheel) - GLPaint.Width + GlobalConst.Difference) / 2
                    - XOffset + MousePosition.X) / (Zoom - ZoomWheel)) * Zoom + MousePosition.X;
        }
        /// <summary>
        /// Изменение сдвига по осям (отдаление).
        /// </summary>
        public void ChangeOffsetZoomOut()
        {
            ZOOM -= ZoomWheel;
            if (Zoom != MinZoom)
            {
                this.YOFFSET =
                       (((YAreaSize * (Zoom - ZoomWheel) - GLPaint.Height + GlobalConst.Difference) / 2 + YOffset + MousePosition.Y) / (Zoom - ZoomWheel)) * Zoom
                       - (YAreaSize * Zoom - GLPaint.Height + GlobalConst.Difference) / 2 - MousePosition.Y;
                this.XOFFSET =
                    (XAreaSize * Zoom - GLPaint.Width + GlobalConst.Difference) / 2 - (((XAreaSize * (Zoom - ZoomWheel) - GLPaint.Width + GlobalConst.Difference) / 2
                    - XOffset + MousePosition.X) / (Zoom - ZoomWheel)) * Zoom + MousePosition.X;
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
                    Layers[FIJ[1]].BSpline();
                }
                else
                    Minerals[FIJ[1]].AddPoint(new PointSpline(GetCoordinate(0), GetCoordinate(1)));
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
                    if (CheckLayersIntersectionRude(FIJ))
                    {
                        Layers[FIJ[1]].POINT.Insert(FIJ[2],PrefPoint);
                        return false;
                    }
                    /*Перестраиваем массив*/
                    Layers[FIJ[1]].BSpline();
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
                    Minerals[i].DeletePoint(XAreaSize);
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



        private bool CheckLayersIntersectionRude(int[] FIJ)
        {
            try
            {
                if (Layers.Count > 1)
                {
                    if (FIJ[1] == 0)
                        return Check4LayersIntersectionRude(FIJ[2], Layers[0].POINT, Layers[1].POINT);
                    if (FIJ[1] == Layers.Count - 1)
                        return Check4LayersIntersectionRude(FIJ[2], Layers[Layers.Count - 1].POINT, Layers[Layers.Count - 2].POINT);
                    else
                        return Check4LayersIntersectionRude(FIJ[2], Layers[FIJ[1]].POINT, Layers[FIJ[1] + 1].POINT) ||
                            Check4LayersIntersectionRude(FIJ[2], Layers[FIJ[1]].POINT, Layers[FIJ[1] - 1].POINT);
                }
            }
            catch { return true; }
            return false;
        }

        private bool Check4LayersIntersectionRude(int NumberCheckPoint, List<PointSpline> First, List<PointSpline> Second)
        {
            try
            {
                int i;
                for(i=0;i<Second.Count - 1;i++)
                {
                    if ((IntersectParts(Second[i], Second[i + 1], First[NumberCheckPoint - 1], First[NumberCheckPoint]) ||
                        IntersectParts(Second[i], Second[i + 1], First[NumberCheckPoint], First[NumberCheckPoint + 1])))
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
                if (SupportLine)
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
                        /*Проверка на самопересечение*/
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
                    Layers[FIJ[1]].BSpline();
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
                    if (CheckSplineSelfIntersectionRude(FIJ[2] + 1, Points))
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
                    Minerals[FIJ[1]].BSpline();
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
                double Height = Math.Abs(Layers[Layers.Count - 1].ReturnMinY()) + XAreaSize / 10 + EarthSize;
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
                int i,Position;
                for (i = 0; i < Layers.Count; i++)
                    Layers[i].AddPoint(new PointSpline(XAreaSize, Layers[i].POINT[Layers[i].POINT.Count - 1].Y), out Position);
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
                Minerals[Minerals.Count - 1].AddPoint(new PointSpline(GetCoordinate(0), GetCoordinate(1)));
            }
            catch { return false; }
            return true;
        }
        #endregion

        #region Отрисовка
        /// <summary>
        /// Рисуем.
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
                else
                    if(i!=Layers.Count-1)
                    DrawingFilingLayers(Layers[i].BSPLINEPOINT, Layers[i + 1].BSPLINEPOINT, Layers[i].COLOR);
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
                else
                    DrawingFilingMinerals(Minerals[i].BSPLINEPOINT, Minerals[i].COLOR);
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
            /*Рисуем разметку*/
            if (Marking)
                DrawingMarking();        
            Gl.glPopMatrix();
            Gl.glFinish();
            GLPaint.Invalidate();
        }
    }
}