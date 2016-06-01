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
        /// Массив точек CSpline.
        /// </summary>
        private List<PointSpline> CSplinePoints = new List<PointSpline>();
        /// <summary>
        /// Массив значений при разбиение.
        /// </summary>
        private List<PointMKE> Partition = new List<PointMKE>();
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
        /// Констурктор.
        /// </summary>
        /// <param name="Color"> Цвет. </param>
        /// <param name="Material"> Материал. </param>
        /// <param name="Points"> Массив точек. </param>
        public Layer(Color Color, Material Material, List<PointSpline> Points)
        {
            this.Color = Color;
            this.Material = Material;
            this.Points = Points;
            ReBuild();
        }
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="XAreaSize"> Ширина рабочей области. </param>
        /// <param name="LayerHeight"> Глубина слоя. </param>
        /// <param name="NumberOfPoints"> Количество опорных точек. </param>
        /// <param name="Material"> Материал. </param>
        /// <param name="Color"> Цвет. </param>
        public Layer(double XAreaSize, double LayerHeight, int NumberOfPoints, Material Material, Color Color)
        {
            double Step = (XAreaSize) / (NumberOfPoints - 1) > 0 ? (XAreaSize) / (NumberOfPoints - 1) : 1;
            for (int i = 0; i < NumberOfPoints; i++)
                this.Points.Add(new PointSpline(i * Step, LayerHeight));
            this.Material = Material;
            this.Color = Color;
            ReBuild();
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
        public List<PointSpline> CSPLINEPOINT
        {
            get { return CSplinePoints; }
        }
        public List<PointMKE> PARTITION
        {
            get { return this.Partition; }
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
        /// Перестройка BSpline,CSpline.
        /// </summary>
        /// <returns></returns>
        public bool ReBuild()
        {
            try
            {
                BSpline();
            }
            catch { return false; }
            return true;
        }

        /// <summary>
        /// Клонирование массива.
        /// </summary>
        /// <param name="Massive"> Какого массива (0 - опорные линии, 1 - BSpline, 2 - CSpline). </param>
        /// <returns> Массив. </returns>
        public List<PointSpline> ClonePoint(int Massive)
        {
            int i;
            List<PointSpline> Clon = new List<PointSpline>();
            List<PointSpline> Point;
            switch (Massive)
            {
                case 0: Point = Points; break;
                case 1: Point = BSplinePoints; break;
                case 2: Point = CSplinePoints; break;
                default: Point = Points; break;
            }
            for (i = 0; i < Points.Count; i++)
                Clon.Add(new PointSpline(Points[i].X, Points[i].Y));
            return Clon;
        }
        /// <summary>
        /// Клонирование точек разбиения.
        /// </summary>
        /// <returns> Массив. </returns>
        public List<PointMKE> ClonePartition()
        {
            int i;
            List<PointMKE> Clon = new List<PointMKE>();
            for (i = 0; i < Points.Count; i++)
                Clon.Add(new PointMKE(new PointSpline(Partition[i].POINT.X, Partition[i].POINT.Y),Partition[i].MATERIAL,Partition[i].ITSLAYER));
            return Clon;
        }

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
            catch { return false; }
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
                ReBuild();
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
        /// <returns> Y(X). </returns>
        private double ReturnY(double X,PointSpline A, PointSpline B)
        {
            return (X - A.X) * (B.Y - A.Y) / (B.X - A.X) + A.Y;
        }
        /// <summary>
        /// Максимальное значение Y.
        /// </summary>
        /// <param name="Massive"> В каком массиве искать (0 - опорные линии, 1 - BSpline, 2 - CSpline). </param>
        /// <returns> Максимальное значение Y. </returns>
        public double ReturnMaxY(int Massive)
        {
            int i;
            List<PointSpline> Point;
            switch (Massive)
            {
                case 0: Point = Points; break;
                case 1: Point = BSplinePoints; break;
                case 2: Point = CSplinePoints; break;
                default: Point = Points; break;
            }
            double Max = Point[0].Y;
            for (i = 1; i < Point.Count; i++)
                if (Max < Point[i].Y) Max = Point[i].Y;
            return Max;
        }
        /// <summary>
        /// Минимальное значение Y.
        /// </summary>
        /// <param name="Massive"> В каком массиве искать (0 - опорные линии, 1 - BSpline, 2 - CSpline). </param>
        /// <returns> Минимальное значение Y. </returns>
        public double ReturnMinY(int Massive)
        {
            int i;
            List<PointSpline> Point;
            switch (Massive)
            {
                case 0: Point = Points; break;
                case 1: Point = BSplinePoints; break;
                case 2: Point = CSplinePoints; break;
                default: Point = Points; break;
            }
            double Min = Point[0].Y;
            for (i = 1; i < Point.Count; i++)
                if (Min > Point[i].Y) Min = Point[i].Y;
            return Min;
        }
        /// <summary>
        /// Возвращение максимального значения Y(X).
        /// </summary>
        /// <param name="X"> Значение Х. </param>
        /// <param name="Massive"> В каком массиве искать (0 - опорные линии, 1 - BSpline, 2 - CSpline). </param>
        /// <returns></returns>
        public double ReturnMaxYX(double X,int Massive)
        {
            List<PointSpline> Point;
            switch (Massive)
            {
                case 0: Point = Points; break;
                case 1: Point = BSplinePoints; break;
                case 2: Point = CSplinePoints; break;
                default: Point = Points; break;
            }
            int i;
            double Max = -double.MaxValue;
            for (i = 0; i < Point.Count - 1; i++)
                if ((X > Point[i].X && X < Point[i + 1].X) || (X < Point[i].X && X > Point[i + 1].X))
                    if (Max < ReturnY(X, Point[i], Point[i + 1]))
                        Max = ReturnY(X, Point[i], Point[i + 1]);
            return Max;
        }
        /// <summary>
        /// Возвращение минимального значения Y(X).
        /// </summary>
        /// <param name="X"> Значение Х. </param>
        /// <param name="Massive"> В каком массиве искать (0 - опорные линии, 1 - BSpline, 2 - CSpline). </param>
        /// <returns></returns>
        public double ReturnMinYX(double X, int Massive)
        {
            List<PointSpline> Point;
            switch (Massive)
            {
                case 0: Point = Points; break;
                case 1: Point = BSplinePoints; break;
                case 2: Point = CSplinePoints; break;
                default: Point = Points; break;
            }
            int i;
            double Min = double.MaxValue;
            for (i = 0; i < Point.Count - 1; i++)
                if ((X > Point[i].X && X < Point[i + 1].X) || (X < Point[i].X && X > Point[i + 1].X))
                    if (Min > ReturnY(X, Point[i], Point[i + 1]))
                        Min = ReturnY(X, Point[i], Point[i + 1]);
            return Min;
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
        /// <returns> Выполнил или нет. </returns>
        public bool BSpline()
        {
            try
            {
                /*Очищаем массив*/
                BSplinePoints.Clear();
                /*Считаем нужное количество точек*/
                for (int start_cv = -2, j = 0; j <= Points.Count; ++j, ++start_cv)
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
                ReBuild();
            }
            catch { return false; }
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
            catch { return false; }
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
                ReBuild();
            }
            catch { return false; }
            return true;
        }

        /// <summary>
        /// Находим точки разбиения.
        /// </summary>
        /// <param name="PartitionX"> Шаг разбиения. </param>
        /// <param name="Massive"> В каком массиве искать (0 - опорные линии, 1 - BSpline, 2 - CSpline). </param>
        /// <returns> Выполнил или нет. </returns>
        public bool MakePartition(double PartitionX,int Massive)
        {
            List<PointSpline> Point;
            switch (Massive)
            {
                case 0: Point = Points; break;
                case 1: Point = BSplinePoints; break;
                case 2: Point = CSplinePoints; break;
                default: Point = Points; break;
            }
            try
            {
                if (PartitionX != 0)
                {
                    int i;
                    double X = 0;
                    PointMKE tmp;
                    Partition.Clear();
                    /*Идем по всем отрезкам*/
                    for (i = 0; i < Point.Count - 1; i++)
                    {
                        /*Если идем вперед*/
                        if (Point[i].X < Point[i + 1].X)
                        {
                            /*Пока точка лежит внутри*/
                            while (Point[i].X <= X && X <= Point[i + 1].X)
                            {
                                tmp = new PointMKE(new PointSpline(X, ReturnY(X, Point[i], Point[i + 1])), Material, true);
                                Partition.Add(tmp);
                                X += PartitionX;
                            }
                        }
                        /*Если идем назад*/
                        if (Point[i].X > Point[i + 1].X)
                        {                        
                            X -= PartitionX;
                            /*Пока точка лежит внутри*/
                            while (Point[i].X >= X && X >= Point[i + 1].X)
                            {
                                tmp = new PointMKE(new PointSpline(X, ReturnY(X, Point[i], Point[i + 1])), Material, true);
                                Partition.Add(tmp);
                                X -= PartitionX;
                            }
                            X += PartitionX;
                        }
                    }
                    /*Если в конце есть еще кусок - добавляем*/
                    if(X< Point[Point.Count-1].X)
                    {
                        tmp = new PointMKE(new PointSpline(X, Point[Point.Count - 1].Y), Material, true);
                        Partition.Add(tmp);
                    }
                }
            }
            catch { return false; }
            return true;
        }
        #endregion
    }
}