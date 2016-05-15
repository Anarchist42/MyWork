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
        private double Zoom;
        /*Минимальный зум*/
        private double MinZoom;
        /*Количество клеток на линии*/
        private int CellsNumber;
        /*Смещение по оси X*/
        private double XOffset;
        /*Смещение по оси Y*/
        private double YOffset;
        /*Позиция мышки*/
        private Point MousePosition;
        /*Размер исследуемой области по оси X*/
        private double XAreaSize;
        /*Размер исследуемой области по оси Y*/
        private double YAreaSize;
        /*Высота уровня земли*/
        private double EarthSize;
        /*Начальная высота рабочей области*/
        private double DefYAreaSize;
        /**Начальная ширина рабочей области*/
        private double DefXAreaSize;
        /*Флаг нужна ли сетка*/
        private bool Grid;
        /*Флаг нужна ли разметка*/
        private bool Marking;
        /*Флаг нужна ли опорная линия*/
        private bool SupportLine;
        #endregion

        #region Не нужны Get\Set
        /*Указатель на окно где рисуем*/
        private SimpleOpenGlControl GLPaint;
        /*Максимальный зум*/
        private double MaxZoom;
        /*Cлои почвы*/
        private List<Layer> Layers = new List<Layer>();
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
            this.XAreaSize = 0.0;
            this.YAreaSize = 0.0;
            this.DefYAreaSize = 0.0;
            this.DefXAreaSize = 0.0;
            this.Grid = false;
            this.Marking = false;
            this.SupportLine = false;
        }
        #endregion

        #region SETs and GETs
        public double XAREASIZE
        {
            get
            {
                return this.XAreaSize;
            }
            set
            {
                this.XAreaSize = (value > 582 / Math.Pow(10, GlobalConst.Accuracy)) ? Math.Round(value, GlobalConst.Accuracy) : 582 / Math.Pow(10, GlobalConst.Accuracy);
                this.MinZoom = Convert.ToDouble(GLPaint.Width - GlobalConst.Difference) / XAreaSize;
                this.Zoom = this.MinZoom;
                this.MaxZoom = Math.Pow(10, GlobalConst.Accuracy);
                //GlobalConst.ZoomWheel = MaxZoom - 1;
                GlobalConst.MinZoom = this.MinZoom;
                this.XOffset = 0.0;
                this.YOffset = 0.0;
                this.DefXAreaSize = XAreaSize;
                if (this.YAreaSize < Math.Round(Convert.ToDouble(GLPaint.Height - GlobalConst.Difference) / Zoom, GlobalConst.Accuracy))
                {
                    this.YAreaSize = Math.Round(Convert.ToDouble(GLPaint.Height - GlobalConst.Difference) / Zoom, GlobalConst.Accuracy);                 
                    this.DefYAreaSize = YAreaSize;
                }
            }
        }
        public double YAREASIZE
        {
            get
            {
                return this.YAreaSize;
            }
            set
            {
                this.YAreaSize = Math.Round(value, GlobalConst.Accuracy);
            }
        }
        public double EARTHSIZE
        {
            get
            {
                return this.EarthSize;
            }
            set
            {
                this.EarthSize = (Math.Round(value, GlobalConst.Accuracy) > 0) ? Math.Round(value, GlobalConst.Accuracy) : 0;
            }
        }
        public double DEFXAREASIZE
        {
            get
            {
                return DefXAreaSize;
            }
        }
        public double DEFYAREASIZE
        {
            get
            {
                return DefYAreaSize;
            }
        }
        public double ZOOM
        {
            get
            {
                return this.Zoom;
            }
            set
            {
                this.Zoom = (value > MinZoom) 
                    ? (value<MaxZoom) 
                    ? value : MaxZoom: MinZoom;
            }
        }
        public double MINZOOM
        {
            get
            {
                return this.MinZoom;
            }
            set
            {
                this.MinZoom = value;
                this.Zoom = value;
            }
        }
        public int CELLSNUMBER
        {
            get
            {
                return this.CellsNumber;
            }
            set
            {
                this.CellsNumber = (value > 0 && value < 20)
                  ? value : (value < 20) ? 1 : 20;
            }
        }
        public double XOFFSET
        {
            get
            {
                return this.XOffset;
            }
            set
            {
                this.XOffset = (Math.Round((XAreaSize * Zoom - GLPaint.Width + GlobalConst.Difference) / 2 - Math.Abs(value), 15) >= 0)
                ? value
                : (XAreaSize * Zoom - GLPaint.Width + GlobalConst.Difference) / 2 * value / Math.Abs(value);
            }
        }
        public double YOFFSET
        {
            get
            {
                return this.YOffset;
            }
            set
            {
                this.YOffset = (Math.Round((YAreaSize * Zoom - GLPaint.Height + GlobalConst.Difference) / 2 - Math.Abs(value), 15) >= 0)
                ? value
                : (YAreaSize * Zoom - GLPaint.Height + GlobalConst.Difference) / 2 * value / Math.Abs(value);
            }
        }
        public Point MOUSEPOSITION
        {
            get
            {
                return this.MousePosition;
            }
            set
            {
                this.MousePosition = value;
            }
        }
        public bool GRID
        {
            get
            {
                return this.Grid;
            }
            set
            {
                this.Grid = value;
            }
        }
        public bool MARKING
        {
            get
            {
                return this.Marking;
            }
            set
            {
                this.Marking = value;
            }
        }
        public bool SUPPORTLINE
        {
            get
            {
                return this.SupportLine;
            }
            set
            {
                this.SupportLine = value;
            }
        }
        #endregion

        #region Методы

        #region Сетка, Разметка
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

        //private void DrawingCoords()
        //{
        //    /*Пробел, X\Y, :, -, Максимальная длина числа, пять знаков после запятой*/
        //    int LengthCoords = (4 + XAreaSize.ToString().Length + 6) * 8;
        //    /*Фон для координат*/
        //    Gl.glPushMatrix();
        //    Gl.glColor3d(1.0, 1.0, 1.0);
        //    Gl.glLineWidth(1);
        //    Gl.glPushMatrix();
        //    Gl.glBegin(Gl.GL_QUADS);
        //    Gl.glVertex2d(GLPaint.Width / 2 - LengthCoords, GLPaint.Height / 2);
        //    Gl.glVertex2d(GLPaint.Width / 2 - 4, GLPaint.Height / 2);
        //    Gl.glVertex2d(GLPaint.Width / 2 - 4, GLPaint.Height / 2 - 30);
        //    Gl.glVertex2d(GLPaint.Width / 2 - LengthCoords, GLPaint.Height / 2 - 30);
        //    Gl.glEnd();
        //    Gl.glPopMatrix();
        //    /*Границы*/
        //    Gl.glPushMatrix();
        //    Gl.glColor3d(0.0, 0.0, 0.0);
        //    Gl.glBegin(Gl.GL_LINES);
        //    Gl.glVertex2d(GLPaint.Width / 2 - LengthCoords, GLPaint.Height / 2);
        //    Gl.glVertex2d(GLPaint.Width / 2 - 4, GLPaint.Height / 2);
        //    Gl.glVertex2d(GLPaint.Width / 2 - 4, GLPaint.Height / 2);
        //    Gl.glVertex2d(GLPaint.Width / 2 - 4, GLPaint.Height / 2 - 30);
        //    Gl.glVertex2d(GLPaint.Width / 2 - 4, GLPaint.Height / 2 - 30);
        //    Gl.glVertex2d(GLPaint.Width / 2 - LengthCoords, GLPaint.Height / 2 - 30);
        //    Gl.glVertex2d(GLPaint.Width / 2 - LengthCoords, GLPaint.Height / 2 - 30);
        //    Gl.glVertex2d(GLPaint.Width / 2 - LengthCoords, GLPaint.Height / 2);
        //    Gl.glEnd();
        //    Gl.glPopMatrix();
        //    /*Координаты*/
        //    Gl.glColor3d(0.0, 0.0, 0.0);
        //    Gl.glPushMatrix();
        //    Gl.glTranslated(GLPaint.Width / 2 - LengthCoords + 5, GLPaint.Height / 2 - 16, 0.0);
        //    Gl.glScaled(0.10, 0.10, 0.10);
        //    string Coordinate = "X: " + Math.Round(((XAreaSize * Zoom - GLPaint.Width + GlobalConst.Difference) / 2 - XOffset + MousePosition.X) / Zoom, GlobalConst.Accuracy).ToString();
        //    for (int j = 0; j < Coordinate.Length; j++)
        //        Glut.glutStrokeCharacter(Glut.GLUT_STROKE_ROMAN, Coordinate[j]);
        //    Gl.glPopMatrix();
        //    Gl.glPushMatrix();
        //    Gl.glTranslated(GLPaint.Width / 2 - LengthCoords + 5, GLPaint.Height / 2 - 28, 0.0);
        //    Gl.glScaled(0.10, 0.10, 0.10);
        //    Coordinate = "Y: " + Math.Round((EarthSize - ((YAreaSize * Zoom - GLPaint.Height + GlobalConst.Difference) / 2 + YOffset + MousePosition.Y) / Zoom), GlobalConst.Accuracy).ToString();
        //    for (int j = 0; j < Coordinate.Length; j++)
        //        Glut.glutStrokeCharacter(Glut.GLUT_STROKE_ROMAN, Coordinate[j]);
        //    Gl.glPopMatrix();
        //    Gl.glPopMatrix();
        //}
        #endregion

        #region Передача параметров
        /*Вычисление текущих координат*/
        /*Параметры: TypeCoordinate - тип координаты (0 - Х, 1 - Y)*/
        /*Возвращает: Значение искомого типа координаты*/
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
        public int ScrollMaximum(int TypeScroll)
        {
            if (TypeScroll == 0)
                return
                    /*Исходный размер*/
                    Convert.ToInt32(YAreaSize) +
                    /*Сколько вверх*/
                    Convert.ToInt32(((YAreaSize * Zoom - GLPaint.Height + GlobalConst.Difference) / 2 + YOffset) / Zoom) +
                    /*Сколько вниз*/
                    Convert.ToInt32(YAreaSize) - Convert.ToInt32(((YAreaSize * Zoom - GLPaint.Height + GlobalConst.Difference) / 2 + YOffset + GLPaint.Height - GlobalConst.Difference) / Zoom);
            else
                return
                   /*Исодный размер*/
                   Convert.ToInt32(XAreaSize) +
                   /*Сколько влево*/
                   Convert.ToInt32(((XAreaSize * Zoom - GLPaint.Width + GlobalConst.Difference) / 2 - XOffset) / Zoom) +
                   /*Сколько вправо*/
                   Convert.ToInt32(XAreaSize) - Convert.ToInt32(((XAreaSize * Zoom - GLPaint.Width + GlobalConst.Difference) / 2 - XOffset + GLPaint.Width - GlobalConst.Difference) / Zoom);
        }

        /*Вычисление значения ползунка*/
        /*Параметры: TypeScroll ( 0 - для вертикального, 1 - для горизонтального)*/
        /*Возвращает: Значение искомого типа ползунка*/
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

        /*Вычисление шага ползунка*/
        /*Параметры: TypeScroll ( 0 - для вертикального, 1 - для горизонтального)*/
        /*           MaximumScroll - сколько доступно значений для прокрутки*/
        /*Возвращает: Значение шага искомого типа ползунка*/
        public double ScrollStep(int TypeScroll, int MaximumScroll)
        {
            if (TypeScroll == 0)
                return YAreaSize * Zoom / MaximumScroll;
            else
                return XAreaSize * Zoom / MaximumScroll;
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

        #region Работа со слоями
        /*Изначальная позиция передвигаемой опорной точки*/
        private PointSpline StartMoveSpline;

        /*Добавляем слой почвы*/
        /*Параметры: LayerHeight - высота на которой заканчивается слой*/
        /*           NumberOfPoints - количество опорных точек сплайна*/
        public void AddLayers(double LayerHeight, int NumberOfPoints)
        {
            /*Если новый слой имеет глубину больше чем есть сейчас*/
            if (LayerHeight + EarthSize > YAreaSize)
            {
                int i;
                double NewYAreaSize = LayerHeight + EarthSize + XAreaSize / 10;
                /*Меняем значения Y предыдущих слоев*/
                for (i = 0; i < Layers.Count; i++)
                    Layers[i].ChangeY((NewYAreaSize - YAreaSize) / 2);
                /*Меняем значение высоты*/
                this.YAREASIZE = NewYAreaSize;
            }
            /*Добавляем новый слой*/
            Layer tmp = new Layer(XAreaSize, LayerHeight, YAreaSize, EarthSize, NumberOfPoints, Layers.Count + 1);
            Layers.Add(tmp);
            /*Сортируем по высоте*/
            Layers.Sort(delegate (Layer x, Layer y)
            {
                return y.ReturnY(0).CompareTo(x.ReturnY(0));
            });
        }

        /*Вернуть глубину слоя*/
        /*Возвращает: Значение глубины в месте нажатия мыши*/
        public int GetLayerHeight()
        {
            return Convert.ToInt32((EarthSize - ((YAreaSize * Zoom - GLPaint.Height + GlobalConst.Difference) / 2 + YOffset + MousePosition.Y) / Zoom));
        }

        /*Вернуть цвет слоя*/
        /*Параметры: IndexNumber - порядковый номер слоя*/
        /*Возвращает: Color - цвет слоя с заданным порядковым номером*/
        public Color GetLayerColor(int IndexNumber)
        {
            int i;
            for (i = 0; i < Layers.Count; i++)
                if (Layers[i].INDEXNMUMBER == IndexNumber)
                    return Layers[i].COLOR;
            return Color.White;
        }

        /*Удаляем слой почвы (на сетке)*/
        /*Параметры: LayerNumber - номер удаляемого слоя*/
        /*Возвращает: IndexNumber - порядковый номер удаляемого слоя*/
        public int DeleteLayersNumber(int LayerNumber)
        {
            int i, IndexNumber;
            /*Получаем порядковый номер слоя*/
            IndexNumber = Layers[LayerNumber].INDEXNMUMBER;
            /*Меняем Y у слоев*/
            ForDeleteLayers(LayerNumber);
            /*Удаляем слой*/
            Layers.RemoveAt(LayerNumber);
            /*Уменьшаем порядковые номера всех последующих слоев*/
            for (i = 0; i < Layers.Count; i++)
                if (Layers[i].INDEXNMUMBER > IndexNumber) Layers[i].INDEXNMUMBER--;
            /*-1, так как номера слоя начинаются с 1*/
            return IndexNumber - 1;
        }

        /*Удаляем слой почвы (на таблице)*/
        /*Параметры: IndexNumber - порядковый номер удаляемого слоя*/
        public void DeleteLayersIndex(int IndexNumber)
        {
            int i, k;
            for (i = 0, k = -1; i < Layers.Count; i++)
            {
                /*Находим какой слой нужно удалить*/
                if (Layers[i].INDEXNMUMBER == IndexNumber + 1 && k == -1)
                    k = i;
                /*Уменьшаем порядковые номера всех последующих слоев*/
                if (Layers[i].INDEXNMUMBER > IndexNumber + 1)
                    Layers[i].INDEXNMUMBER--;
            }
            /*Меняем Y у слоев*/
            ForDeleteLayers(k);
            /*Удаляем слой*/
            Layers.RemoveAt(k);
        }

        /*Изменяем значения Y у Layers.Point'ов*/
        /*Параметры: LayerNumber - номер удаляемого слоя*/
        private void ForDeleteLayers(int LayerNumber)
        {
            //int i;
            ///*Если слой был один*/
            //if (Layers.Count == 1)
            //    YAREASIZE = DefYAreaSize;
            ///*Если удаляемый слой последний, то надо убрать лишние по Y*/
            //if (LayerNumber == Layers.Count - 1 && Layers.Count > 1)
            //{
            //    /*Поиск максимальной глубины предпоследнего слоя*/
            //    List<Point> Points = Layers[LayerNumber - 1].POINT;
            //    int Max = Points[0].Y;
            //    for (i = 1; i < Points.Count; i++)
            //        if (Max < Points[i].Y) Max = Points[i].Y;
            //    Max = YAreaSize / 2 - EarthSize - Max;
            //    /*Если значение меньше изначального*/
            //    if (Max < DefYAreaSize)
            //    {
            //        /*Меняем значения Y предыдущих слоев*/
            //        for (i = 0; i < Layers.Count - 1; i++)
            //            Layers[i].ChangeY((DefYAreaSize - YAreaSize) / 2);
            //        /*Ставим дефолтное значение*/
            //        YAREASIZE = DefYAreaSize;
            //    }
            //    else
            //    {
            //        /*Меняем значения Y предыдущих слоев*/
            //        for (i = 0; i < Layers.Count - 1; i++)
            //            Layers[i].ChangeY((Max + EarthSize + XAreaSize / 10 - YAreaSize) / 2);
            //        YAREASIZE = Max + XAreaSize / 10;
            //    }
            //}
        }

        /*Проверка попала ли мышь на опорную точку сплайна*/
        /*Параметры: MouseDownPosition - позиция точки нажатия*/
        /*Возвращает: FIJ[3] - массив, где [1] - нашел точку (0/1),*/
        /*            [2] - номер слоя, [3] - номер точки*/
        public int[] CheckPoint(Point MouseDownPosition)
        {
            int[] FIJ = { 0, 0, 0 };
            if (SupportLine)
            {
                int[] X = new int[2];
                int[] Y = new int[2];
                int i, j;
                X[0] = Convert.ToInt32(((XAreaSize * Zoom - GLPaint.Width + GlobalConst.Difference) / 2 - XOffset + MouseDownPosition.X - 4) / Zoom - XAreaSize / 2);
                X[1] = Convert.ToInt32(((XAreaSize * Zoom - GLPaint.Width + GlobalConst.Difference) / 2 - XOffset + MouseDownPosition.X + 4) / Zoom - XAreaSize / 2);
                Y[0] = Convert.ToInt32(YAreaSize / 2 - ((YAreaSize * Zoom - GLPaint.Height + GlobalConst.Difference) / 2 + YOffset + MouseDownPosition.Y + 4) / Zoom);
                Y[1] = Convert.ToInt32(YAreaSize / 2 - ((YAreaSize * Zoom - GLPaint.Height + GlobalConst.Difference) / 2 + YOffset + MouseDownPosition.Y - 4) / Zoom);
                for (i = 0; i < Layers.Count; i++)
                {
                    List<PointSpline> Points = Layers[i].POINT;
                    for (j = 0; j < Points.Count; j++)
                        if (Points[j].X >= X[0] && Points[j].X <= X[1] && Points[j].Y >= Y[0] && Points[j].Y <= Y[1])
                        {
                            FIJ[0] = 1;
                            FIJ[1] = i;
                            FIJ[2] = j;
                            StartMoveSpline = new PointSpline(Points[j].X, Points[j].Y);
                            return FIJ;
                        }
                }
            }
            return FIJ;
        }

        /*Изменяем позицию опорной точки*/
        /*Параметры: MouseDownPosition - позиция точки нажатия*/
        /*           FIJ[3] - массив, где[1] - нашел точку(0/1),*/
        /*           [2] - номер слоя, [3] - номер точки*/
        public void ChangePoint(Point MouseDownPosition, int[] FIJ)
        {
            List<PointSpline> Points = Layers[FIJ[1]].POINT;
            PointSpline Tmp = new PointSpline();
            if (FIJ[2] != 0 && FIJ[2] != Points.Count - 1)
                Tmp.X = Convert.ToInt32(StartMoveSpline.X + (MousePosition.X - MouseDownPosition.X) / Zoom);
            else
                Tmp.X = Points[FIJ[2]].X;
            Tmp.Y = Convert.ToInt32(StartMoveSpline.Y + (MouseDownPosition.Y - MousePosition.Y) / Zoom);
            Points[FIJ[2]] = Tmp;
        }

        /*Добавляем точку*/
        /*Параметры: FIJ[3] - массив, где[1] - нашел точку(0/1),*/
        /*           [2] - номер слоя, [3] - номер точки*/
        public int[] AddPointLayers(int NumberLayer)
        {
            //int X = Convert.ToInt32(((XAreaSize * Zoom - GLPaint.Width + GlobalConst.Difference) / 2 - XOffset + MousePosition.X) / Zoom) - XAreaSize / 2,
            //    Y = YAreaSize / 2 - EarthSize + Convert.ToInt32((EarthSize - ((YAreaSize * Zoom - GLPaint.Height + GlobalConst.Difference) / 2 + YOffset + MousePosition.Y) / Zoom));
            //Layers[NumberLayer].AddPoint(new Point(X, Y));
            return new int[] { Layers[NumberLayer].POINT.Count, Layers[NumberLayer].INDEXNMUMBER };
        }

        /*Удаляем точку*/
        /*Параметры: FIJ[3] - массив, где[1] - нашел точку(0/1),*/
        /*           [2] - номер слоя, [3] - номер точки*/
        /*Возвращает: Count - количество точек слоя*/
        /*            IndexNumber - порядковый номер*/
        public int[] DeletePointLayers(int[] FIJ)
        {
            Layers[FIJ[1]].DeletePoint(FIJ[2]);
            return new int[] { Layers[FIJ[1]].POINT.Count, Layers[FIJ[1]].INDEXNMUMBER };
        }

        /*Для отрисовки сплайна*/
        /*Параметры: i - номер точки позицию которой надо вернуть*/
        /*           Points - массив опорных точек сплайна*/
        /*Возвращает: Points[1] - точка для отрисовки куска сплайна*/
        private PointSpline GetPoint(int i, List<PointSpline> Points)
        {
            if (i < 0)
                return Points[0];
            if (i < Points.Count)
                return Points[i];
            return Points[Points.Count - 1];
        }

        /*Рисуем слои почвы*/
        private void DrawingLayers()
        {
            int N = 10;
            Gl.glPushMatrix();
            List<PointSpline> point = new List<PointSpline>();
            for (int i = 0; i < Layers.Count; i++)
            {
                point = Layers[i].POINT;
                point.Add(point[point.Count - 1]);
                point.Add(point[point.Count - 1]);
                Color color = Layers[i].COLOR;
                Gl.glColor3d(color.R / 255.0, color.G / 255.0, color.B / 255.0);
                Gl.glPushMatrix();
                Gl.glLineWidth(2);
                Gl.glBegin(Gl.GL_LINE_STRIP);
                for (int start_cv = -3, j = 0; j != point.Count; ++j, ++start_cv)
                {
                    for (int k = 0; k != N; ++k)
                    {
                        double t = (double)k / N;
                        double it = 1.0f - t;
                        double b0 = it * it * it / 6.0f;
                        double b1 = (3 * t * t * t - 6 * t * t + 4) / 6.0f;
                        double b2 = (-3 * t * t * t + 3 * t * t + 3 * t + 1) / 6.0f;
                        double b3 = t * t * t / 6.0f;
                        double x = b0 * GetPoint(start_cv + 0, point).X +
                                  b1 * GetPoint(start_cv + 1, point).X +
                                  b2 * GetPoint(start_cv + 2, point).X +
                                  b3 * GetPoint(start_cv + 3, point).X;
                        double y = b0 * GetPoint(start_cv + 0, point).Y +
                                  b1 * GetPoint(start_cv + 1, point).Y +
                                  b2 * GetPoint(start_cv + 2, point).Y +
                                  b3 * GetPoint(start_cv + 3, point).Y;
                        Gl.glVertex2d((x - XAreaSize / 2) * Zoom + XOffset, (YAreaSize / 2 - EarthSize + y) * Zoom + YOffset);
                    }
                }
                Gl.glEnd();
                Gl.glPopMatrix();
                if (SupportLine)
                {
                    Gl.glColor4d(color.R / 255.0, color.G / 255.0, color.B / 255.0, 0.7);
                    /*Рисуем опорные линии*/
                    Gl.glPushMatrix();
                    Gl.glLineWidth(2);
                    Gl.glBegin(Gl.GL_LINE_STRIP);
                    for (int j = 0; j < point.Count; j++)
                        Gl.glVertex2d((point[j].X - XAreaSize / 2) * Zoom + XOffset, (YAreaSize / 2 - EarthSize + point[j].Y) * Zoom + YOffset);
                    Gl.glEnd();
                    Gl.glPopMatrix();
                    /*Рисуем опорные точки*/
                    Gl.glPushMatrix();
                    Gl.glPointSize(7);
                    Gl.glBegin(Gl.GL_POINTS);
                    for (int j = 0; j < point.Count; j++)
                        Gl.glVertex2d((point[j].X - XAreaSize / 2) * Zoom + XOffset, (YAreaSize / 2 - EarthSize + point[j].Y) * Zoom + YOffset);
                    Gl.glEnd();
                    Gl.glPopMatrix();
                }
                point.RemoveRange(point.Count - 2, 2);
            }
            Gl.glPopMatrix();
        }
        #endregion

        #region Работа с минералами

        #endregion

        #endregion

        /*Основной метод класса, вызывает отрисовку*/
        public void Draw()
        {
            Gl.glClearColor(1.0f, 1.0f, 1.0f, 0.0f);
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
            Gl.glPushMatrix();
            /*Рисуем слои*/
            DrawingLayers();
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
