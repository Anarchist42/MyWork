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


        /*Флаг нужна ли сетка*/
        bool Grid;
        /*Флаг нужна ли разметка*/
        bool Marking;
        /*Флаг нужна ли опорная линия*/
        bool SupportLine;
        #endregion

        #region Не нужны Get\Set
        /*Указатель на окно где рисуем*/
        SimpleOpenGlControl GLPaint;
        /*Cлои почвы*/
        List<Layer> Layers = new List<Layer>();
        /*Начальная высота рабочей области*/
        int DefYAreaSize;
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
            this.DefYAreaSize = 0;
            this.Grid = false;
            this.Marking = false;
            this.SupportLine = false;
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
                this.MinZoom = Convert.ToDouble(GLPaint.Width - GlobalConst.Difference) / Convert.ToDouble(XAreaSize);
                this.Zoom = this.MinZoom;
                GlobalConst.MinZoom = this.MinZoom;
                this.YAreaSize = Convert.ToInt32((GLPaint.Height - GlobalConst.Difference) / Zoom);
                this.DefYAreaSize = YAreaSize;
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
        #endregion

        #region Методы

        #region Сетка, Разметка, Координаты
        private void DrawingGrid()
        {
            int i;
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
        }

        private void DrawingMarking()
        {
            int i, k;
            Gl.glPushMatrix();
            Gl.glColor3d(0.3, 0.3, 0.3);
            /*так как максимум на линии - cells_number клеток, 
           то надо выбрать по Х или Y будем выбирать шаг*/
            int GridStep = (Convert.ToInt32(GLPaint.Width / CellsNumber)
                > Convert.ToInt32(GLPaint.Height / CellsNumber))
                ? Convert.ToInt32(GLPaint.Width / CellsNumber)
                : Convert.ToInt32(GLPaint.Height / CellsNumber);
            if (GridStep == 0) GridStep = 1;
            Gl.glLineWidth(1);
            double GridHalfDown = (-GLPaint.Height / 2 - YOffset);
            double GridHalfUp = (GLPaint.Height / 2 - YOffset);
            double GridHalfLeft = (-GLPaint.Width / 2 - XOffset);
            double GridHalfRight = (GLPaint.Width / 2 - XOffset);
            int LabelStart;
            int x = XAreaSize / 2;
            int y = -YAreaSize / 2 + EarthSize;
            LabelStart = GridStep;
            Gl.glPointSize(3);
            for (i = -LabelStart, k = -1; i > GridHalfDown; i -= GridStep, k--)
            {
                Gl.glPushMatrix();
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
        }

        private void DrawingCoords()
        {
            /*Переносим центр оси*/
            Gl.glTranslated(-XOffset, -YOffset, 0);
            /*Пробел, X\Y, :, -, Максимальная длина числа, два знака после запятой*/
            int LengthCoords = (4 + XAreaSize.ToString().Length + 3) * 8;
            /*Фон для координат*/
            Gl.glPushMatrix();
            Gl.glColor3d(1.0, 1.0, 1.0);
            Gl.glLineWidth(1);
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
        }
        #endregion

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
        private Point StartMoveSpline;

        /*Вернуть глубину слоя*/
        /*Возвращает: Значение глубины в месте нажатия мыши*/
        public int GetLayerHeight()
        {
            return Math.Abs(Convert.ToInt32((EarthSize - ((YAreaSize * Zoom - GLPaint.Height + GlobalConst.Difference) / 2 + YOffset + MousePosition.Y) / Zoom)));
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

        /*Добавляем слой почвы*/
        /*Параметры: LayerHeight - высота на которой заканчивается слой*/
        /*           NumberOfPoints - количество опорных точек сплайна*/
        public void AddLayers(int LayerHeight, int NumberOfPoints)
        {
            /*Если новый слой имеет глубину больше чем есть сейчас*/
            if (LayerHeight + EarthSize > YAreaSize)
            {
                int i, NewYAreaSize;
                NewYAreaSize = LayerHeight + EarthSize + XAreaSize / 10;
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

        /*Удаляем слой почвы (на сетке)*/
        /*Параметры: LayerNumber - номер удаляемого слоя*/
        /*Возвращает: IndexNumber - порядковый номер удаляемого слоя*/
        public int DeleteLayersNumber(int LayerNumber)
        {
            int i,IndexNumber;
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
            int i;
            /*Если слой был один*/
            if (Layers.Count == 1)
                YAREASIZE = DefYAreaSize;
            /*Если удаляемый слой последний, то надо убрать лишние по Y*/
            if (LayerNumber == Layers.Count - 1 && Layers.Count > 1)
            {
                /*Поиск максимальной глубины предпоследнего слоя*/
                List<Point> Points = Layers[LayerNumber - 1].POINT;
                int Max = Points[0].Y;
                for (i = 1; i < Points.Count; i++)
                    if (Max < Points[i].Y) Max = Points[i].Y;
                Max = YAreaSize / 2 - EarthSize - Max;
                /*Если значение меньше изначального*/
                if (Max < DefYAreaSize)
                {
                    /*Меняем значения Y предыдущих слоев*/
                    for (i = 0; i < Layers.Count - 1; i++)
                        Layers[i].ChangeY((DefYAreaSize - YAreaSize) / 2);
                    /*Ставим дефолтное значение*/
                    YAREASIZE = DefYAreaSize;
                }
                else
                {
                    /*Меняем значения Y предыдущих слоев*/
                    for (i = 0; i < Layers.Count - 1; i++)
                        Layers[i].ChangeY((Max + EarthSize + XAreaSize / 10 - YAreaSize) / 2);
                    YAREASIZE = Max + XAreaSize / 10;
                }
            }
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
                    List<Point> Points = Layers[i].POINT;
                    for (j = 0; j < Points.Count; j++)
                        if (Points[j].X >= X[0] && Points[j].X <= X[1] && Points[j].Y >= Y[0] && Points[j].Y <= Y[1])
                        {
                            FIJ[0] = 1;
                            FIJ[1] = i;
                            FIJ[2] = j;
                            StartMoveSpline = new Point(Points[j].X, Points[j].Y);
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
            List<Point> Points = Layers[FIJ[1]].POINT;
            Point Tmp = new Point();
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
            int X = Convert.ToInt32(((XAreaSize * Zoom - GLPaint.Width + GlobalConst.Difference) / 2 - XOffset + MousePosition.X) / Zoom) - XAreaSize / 2,
                Y = YAreaSize / 2 - EarthSize + Convert.ToInt32((EarthSize - ((YAreaSize * Zoom - GLPaint.Height + GlobalConst.Difference) / 2 + YOffset + MousePosition.Y) / Zoom));
            Layers[NumberLayer].AddPoint(new Point(X, Y));
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
        private Point GetPoint(int i, List<Point> Points)
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
            int N = 100;
            Gl.glPushMatrix();
            List<Point> point = new List<Point>();
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
                        Gl.glVertex2d(x * Zoom + XOffset, y * Zoom + YOffset);
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
                        Gl.glVertex2d(point[j].X * Zoom + XOffset, point[j].Y * Zoom + YOffset);
                    Gl.glEnd();
                    Gl.glPopMatrix();
                    /*Рисуем опорные точки*/
                    Gl.glPushMatrix();
                    Gl.glPointSize(7);
                    Gl.glBegin(Gl.GL_POINTS);
                    for (int j = 0; j < point.Count; j++)
                        Gl.glVertex2d(point[j].X * Zoom + XOffset, point[j].Y * Zoom + YOffset);
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
            /*Переносим центр оси*/
            Gl.glTranslated(XOffset, YOffset, 0);
            /*Рисуем сетку*/
            if (Grid)
                DrawingGrid();          
            /*Рисуем разметку*/
            if (Marking)
                DrawingMarking();          
            /*Рисуем координаты*/
            DrawingCoords();           

            Gl.glPopMatrix();
            Gl.glFinish();
            GLPaint.Invalidate();
        }
    }
}
