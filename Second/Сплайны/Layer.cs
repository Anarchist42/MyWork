using System.Collections.Generic;
using System.Drawing;

namespace Second
{
    class Layer
    {
        #region Поля класса
        /// <summary>
        /// Массив опорных точек.
        /// </summary>
        private List<PointSpline> Points = new List<PointSpline>();
        /// <summary>
        /// Массив точек BSpline.
        /// </summary>
        private List<PointSpline> BSplinePoints = new List<PointSpline>();
        /// <summary>
        /// Массив значений Y при разбиение.
        /// </summary>
        private List<double> Partition = new List<double>();
        /// <summary>
        /// Цвет слоя.
        /// </summary>
        private Color Color = new Color();
        /// <summary>
        /// Количество точек разбиения BSpline.
        /// </summary>
        private int BSplineN = 10;
        /// <summary>
        /// Материал слоя.
        /// </summary>
        private Material Material;
        #endregion

        #region Конструктор
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="XAreaSize"> Ширина рабочей области. </param>
        /// <param name="LayerHeight"> Глубина слоя. </param>
        /// <param name="NumberOfPoints"> Количество опорных точек. </param>
        /// <param name="Material"> Материал. </param>
        public Layer(double XAreaSize, double LayerHeight, int NumberOfPoints, Material Material, Color Color)
        {
            double Step = (XAreaSize) / (NumberOfPoints - 1) > 0 ? (XAreaSize) / (NumberOfPoints - 1) : 1;
            for (int i = 0; i < NumberOfPoints; i++)
                this.Points.Add(new PointSpline(i * Step, LayerHeight));
            this.Material = Material;
            this.Color = Color;
            BSpline();
        }
        #endregion

        #region SETs and GETs
        public List<PointSpline> POINT
        {
            get { return Points; }
        }
        public List<PointSpline> BSPLINEPOINT
        {
            get { return BSplinePoints; }
        }
        public Color COLOR
        {
            set { this.Color = value; }
            get { return Color; }
        }
        public Material MATERIAL
        {
            get { return this.Material; }
            set { this.Material = value; }
        }
        #endregion

        #region Методы      
        /// <summary>
        /// Удаление дублирующих точек.
        /// </summary>
        /// <returns> Выполнил или нет. </returns>
        private bool RemoveDuplicate()
        {
            try
            {
                int i;
                for (i = 0; i < Points.Count - 1; i++)
                {
                    if (Points[i] == Points[i + 1])
                    {
                        Points.RemoveAt(i);
                        i--;
                    }
                }
            }
            catch
            {return false;}
            return true;
        }
        /// <summary>
        /// Изменение точности.
        /// </summary>
        /// <returns> Выполнил или нет. </returns>
        public bool ChangeAccuracy()
        {
            int i;
            try
            {
                for (i = 0; i < Points.Count; i++)
                    Points[i].Round();
                RemoveDuplicate();
                BSpline();
            }
            catch
            {return false;}
            return true;
        }
        /// <summary>
        /// Значение Y на отрезке AB.
        /// </summary>
        /// <param name="X"> Значение X. </param>
        /// <param name="A"> Точка начала отрезка. </param>
        /// <param name="B"> Точка конца отрезка. </param>
        /// <returns></returns>
        private double ReturnY(double X,PointSpline A, PointSpline B)
        {
            return (X - A.X) * (B.Y - A.Y) / (B.X - A.X) + A.Y;
        }
        /// <summary>
        /// Возвращение максимального грубого(по опорной линии) значения Y(Х).
        /// </summary>
        /// <param name="X"> Значение Х. </param>
        /// <returns> Значение Y(X). </returns>
        public double ReturnMaxYRude(double X)
        {
            int i;
            double Max = -double.MaxValue;
            for (i = 0; i < Points.Count - 1; i++)
                if ((X > Points[i].X && X < Points[i + 1].X) || (X < Points[i].X && X > Points[i+1].X))
                    if (Max < ReturnY(X, Points[i], Points[i + 1]))
                        Max = ReturnY(X, Points[i], Points[i + 1]);
            return Max;
        }
        /// <summary>
        /// Возвращение минимального грубого(по опорной линии) значения Y(Х).
        /// </summary>
        /// <param name="X"> Значение Х. </param>
        /// <returns> Значение Y(X). </returns>
        public double ReturnMinYRude(double X)
        {
            int i;
            double Min = double.MaxValue;
            for (i = 0; i < Points.Count - 1; i++)
                if ((X > Points[i].X && X < Points[i + 1].X) || (X < Points[i].X && X > Points[i + 1].X))
                    if (Min > ReturnY(X, Points[i], Points[i + 1]))
                        Min = ReturnY(X, Points[i], Points[i + 1]);
            return Min;
        }
        /// <summary>
        /// Максимальное значение глубины.
        /// </summary>
        /// <returns> Минимальное значение Y. </returns>
        public double ReturnMinY()
        {
            int i;
            double Min = BSplinePoints[0].Y;
            for (i = 1; i < BSplinePoints.Count; i++)
                if (Min > BSplinePoints[i].Y) Min = BSplinePoints[i].Y;
            return Min;
        }
        /// <summary>
        /// Минимальное значение глубины.
        /// </summary>
        /// <returns> Максимальное значение Y. </returns>
        public double ReturnMaxY()
        {
            int i;
            double Max = BSplinePoints[0].Y;
            for (i = 1; i < BSplinePoints.Count; i++)
                if (Max < BSplinePoints[i].Y) Max = BSplinePoints[i].Y;
            return Max;
        }
        /// <summary>
        /// Для отрисовки сплайна.
        /// </summary>
        /// <param name="i"> Номер точки, позицию которых надо вернуть. </param>
        /// <param name="Points"> Массив опорных точек сплайна. </param>
        /// <returns> Точка для отрисовки куска сплайна. </returns>
        private PointSpline GetPoint(int i, List<PointSpline> Points)
        {
            if (i < 0)
                return Points[0];
            if (i < Points.Count)
                return Points[i];
            return Points[Points.Count - 1];
        }
        /// <summary>
        /// Построение сплайна.
        /// </summary>
        /// <param name="Start"> Точка начала построения. </param>
        /// <param name="End"> Точка конца построения. </param>
        /// /// <returns> Выполнил или нет. </returns>
        public bool BSpline()
        {
            try
            {
                /*Очищаем массив*/
                BSplinePoints.Clear();
                /*Необходимо добавить несколько точек, что бы рисовал до самого конца*/
                Points.Add(Points[Points.Count - 1]);
                Points.Add(Points[Points.Count - 1]);
                /*Считаем нужное количество точек*/
                for (int start_cv = -2, j = 1; j != Points.Count; ++j, ++start_cv)
                {
                    for (int k = 0; k != BSplineN; ++k)
                    {
                        double t = (double)k / BSplineN;
                        double it = 1.0f - t;
                        double b0 = it * it * it / 6.0f;
                        double b1 = (3 * t * t * t - 6 * t * t + 4) / 6.0f;
                        double b2 = (-3 * t * t * t + 3 * t * t + 3 * t + 1) / 6.0f;
                        double b3 = t * t * t / 6.0f;
                        double x = b0 * GetPoint(start_cv + 0, Points).X +
                                  b1 * GetPoint(start_cv + 1, Points).X +
                                  b2 * GetPoint(start_cv + 2, Points).X +
                                  b3 * GetPoint(start_cv + 3, Points).X;
                        double y = b0 * GetPoint(start_cv + 0, Points).Y +
                                  b1 * GetPoint(start_cv + 1, Points).Y +
                                  b2 * GetPoint(start_cv + 2, Points).Y +
                                  b3 * GetPoint(start_cv + 3, Points).Y;
                        BSplinePoints.Add(new PointSpline(x, y));
                    }
                }
                /*Удаляем добавленные точки*/
                Points.RemoveRange(Points.Count - 2, 2);
            }
            catch
            { return false; }
            return true;
        }
        /// <summary>
        /// Смещение точек вправо.
        /// </summary>
        /// <param name="ChangeX"> Величина смещения. </param>
        /// <returns> Выполнил или нет. </returns>
        public bool ChangeXPoints(double ChangeX)
        {
            try
            {
                int i;
                for (i = 0; i < Points.Count; i++)
                    Points[i].X += ChangeX;
                /*Добавляем точку в начало*/
                Points.Insert(0, new PointSpline(0, Points[0].Y));
                /*Перестраиваем массив*/
                BSpline();
            }
            catch
            { return false; }
            return true;
        }
        /// <summary>
        /// Добавление опорных точек.
        /// </summary>
        /// <param name="Add"> Добовляемая точка. </param>
        /// <returns> Выполнил или нет. </returns>
        public bool AddPoint(PointSpline Add, out int Position)
        {
            Position = -1;
            try
            {
                int i;
                /*Ищем куда вставить*/
                for (i = 1; i < Points.Count; i++)
                    if (Points[i].X > Add.X && Points[i-1].X!=Add.X)
                    {
                        /*Вставляем элементы*/
                        Points.Insert(i, Add);
                        Position = i;
                        return true;
                    }
                /*если не нашли и он больше последнего*/
                if (Add.X > Points[Points.Count - 1].X)
                {
                    Points.Add(Add);
                    Position = Points.Count - 1;
                    return true;
                }
            }
            catch { return false; }
            return true;
        }
        /// <summary>
        /// Удаление опорной точки.
        /// </summary>
        /// <param name="Delete"> Индекс удаляемой точки. </param>
        /// <returns> Выполнил или нет. </returns>
        public bool DeletePoint(int Delete)
        {
            try
            {
                if (Delete > 0 && Delete < Points.Count - 1)
                    Points.RemoveAt(Delete);
            }
            catch
            { return false; }
            return true;
        }
        /// <summary>
        /// Удаление опорных точек правее области.
        /// </summary>
        /// <param name="DeleteX"> На сколько уменьшилась область. </param>
        /// <returns> Выполнил или нет. </returns>
        public bool DeletePoint(double DeleteX)
        {
            try
            {
                int i;
                double Y = 0;
                for (i = Points.Count - 1; i >= 0 && Points[i].X > DeleteX; i--)
                {
                    /*Запоминаем Y удаляемого элемента*/
                    Y = Points[i].Y;
                    Points.RemoveAt(i);
                }
                /*Добавление последней точки*/
                Points.Add(new PointSpline(DeleteX, Y));
                /*Перестраиваем массив*/
                BSpline();
            }
            catch { return false; }
            return true;
        }

        public bool MakePartition(double PartitionX)
        {
            try
            {
                int i;
                for (i = 0; i < BSplinePoints.Count; i++)
                    i++;
            }
            catch { return false; }
            return true;
        }

        #endregion
    }
}