using System;
using System.Collections.Generic;
using System.Drawing;
namespace Second
{
    class Mineral
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
        /// Цвет минерала.
        /// </summary>
        private Color Color = new Color();
        /// <summary>
        /// Материал слоя.
        /// </summary>
        private Material Material;
        /// <summary>
        /// Количество точек разбиения BSpline.
        /// </summary>
        private int BSplineN = 10;
        #endregion

        #region Конструктор
        /// <summary>
        /// Констурктор.
        /// </summary>
        /// <param name="Color"> Цвет. </param>
        /// <param name="Material"> Материал. </param>
        /// <param name="Points"> Массив точек. </param>
        public Mineral(Color Color, Material Material, List<PointSpline> Points)
        {
            this.Color = Color;
            this.Material = Material;
            this.Points = Points;
            ReBuild();
        }
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="Material"> Матреиал. </param>
        /// <param name="Color"> Цвет. </param>
        public Mineral(Material Material, Color Color)
        {
            this.Material = Material;
            this.Color = Color;
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
        /// <returns> Выполнил или нет. </returns>
        public bool ReBuild()
        {
            try
            {
                BSpline();
                Points.Add(Points[0]);
                CSplinePoints = CubicSpline.FitParametric(Points);
                Points.RemoveAt(Points.Count - 1);
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
                Clon.Add(new PointMKE(new PointSpline(Partition[i].POINT.X, Partition[i].POINT.Y), Partition[i].MATERIAL, Partition[i].ITSLAYER));
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
            { return false; }
            return true;
        }

        /// <summary>
        /// Значение Y на отрезке AB.
        /// </summary>
        /// <param name="X"> Значение X. </param>
        /// <param name="A"> Точка начала отрезка. </param>
        /// <param name="B"> Точка конца отрезка. </param>
        /// <returns> Y(X). </returns>
        private double ReturnY(double X, PointSpline A, PointSpline B)
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
        /// Для отрисовки сплайна.
        /// </summary>
        /// <param name="i"> Номер точки, позицию которых надо вернуть. </param>
        /// <param name="Points"> Массив опорных точек сплайна. </param>
        /// <returns> Точка для отрисовки куска сплайна. </returns>
        private PointSpline GetPoint(int i, List<PointSpline> Points)
        {
            if (i < 0)
                return Points[Points.Count + i];
            if (i < Points.Count)
                return Points[i];
            return Points[i - Points.Count];
        }
        /// <summary>
        /// Построение BSpline.
        /// </summary>
        public bool BSpline()
        {
            try
            {
                /*Очищаем массив*/
                BSplinePoints.Clear();
                /*Считаем нужное количество точек*/
                for (int start_cv = -3, j = 0; j != Points.Count; ++j, ++start_cv)
                {
                    for (int k = 0; k != BSplineN + 1; ++k)
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
            catch { return false; }
            return true;
        }

        /// <summary>
        /// Смещение точек вправо.
        /// </summary>
        /// <param name="ChangeX"> На сколько смещать. </param>
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
                ReBuild();
            }
            catch { return false; }
            return true;
        }
        /// <summary>
        /// Добавление опорной точки.
        /// </summary>
        /// <param name="Add"> Добавляемая точка. </param>
        public bool AddPoint(PointSpline Add, out int Position)
        {
            Position = -1;
            try
            {
                if (Points.Count < 2)
                {
                    Position = Points.Count;
                    Points.Add(Add);
                }
                else
                {
                    int k = Points.Count;
                    int i;
                    double Min;
                    Min = PointsLength(Add, Points[0], Points[Points.Count - 1]);
                    for (i = 0; i < Points.Count - 1; i++)
                    {
                        if (Min > PointsLength(Add, Points[i], Points[i + 1]))
                        {
                            Min = PointsLength(Add, Points[i], Points[i + 1]);
                            k = i + 1;
                        }
                    }
                    Points.Insert(k, Add);
                    Position = k;
                    ReBuild();
                }
            }
            catch { return false; }
            return true;
        }
        /// <summary>
        /// Расстояние от точки до отрезка.
        /// </summary>
        /// <param name="A"> Точка. </param>
        /// <param name="B"> Первая точка отрезка. </param>
        /// <param name="C"> Вторая точка отрезка. </param>
        /// <returns></returns>
        private double PointsLength(PointSpline A, PointSpline B, PointSpline C)
        {
            PointSpline V = C - B;
            PointSpline W = A - B;

            double c1 = PointsLenght(W, V);
            if (c1 <= 0)
                return Math.Sqrt(PointsLenght(A - B, A - B));

            double c2 = PointsLenght(V, V);
            if (c2 <= c1)
                return Math.Sqrt(PointsLenght(A - C, A - C));

            double b = c1 / c2;
            PointSpline Pb = B + V * b;
            return Math.Sqrt(PointsLenght(A - Pb, A - Pb));
        }
        /// <summary>
        /// Скалярное произведение векторов.
        /// </summary>
        /// <param name="A"> Первая точка. </param>
        /// <param name="B"> Вторая точка. </param>
        /// <returns></returns>
        private double PointsLenght(PointSpline A, PointSpline B)
        {
            return A.X * B.X + A.Y * B.Y;
        }

        /// <summary>
        /// Удаление опорной точки.
        /// </summary>
        /// <param name="Delete"> Индекс удаляемой точки. </param>
        public bool DeletePoint(int Delete)
        {
            try
            {
                if (Points.Count > 3)
                {
                    Points.RemoveAt(Delete);
                    /*Перестраиваем массив*/
                    ReBuild();
                }
            }
            catch { return false; }
            return true;
        }
        /// <summary>
        /// Удаление опорных точек правее области.
        /// </summary>
        /// <param name="DeleteX"> На сколько уменьшилась область. </param>
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
        public bool MakePartition(double PartitionX, int Massive)
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
                    /*Идем по всем отрезкам BSpline*/
                    for (i = 0; i < Point.Count - 1; i++)
                    {
                        /*Если идем вперед*/
                        if (Point[i].X < Point[i + 1].X)
                        {
                            X = Math.Ceiling(Point[i].X / PartitionX) * PartitionX;
                            /*Пока точка лежит внутри*/
                            while (Point[i].X <= X && X <= Point[i + 1].X)
                            {
                                tmp = new PointMKE(new PointSpline(X, ReturnY(X, Point[i], Point[i + 1])), Material, true);
                                Partition.Add(tmp);
                                X += PartitionX;
                            }
                        }
                        else
                        /*Если идем вперед*/
                        if (Point[i].X > Point[i + 1].X)
                        {
                            X = Math.Floor(Point[i].X / PartitionX) * PartitionX;
                            /*Пока точка лежит внутри*/
                            while (Point[i].X >= X && X >= Point[i + 1].X)
                            {
                                tmp = new PointMKE(new PointSpline(X, ReturnY(X, Point[i], Point[i + 1])), Material, true);
                                Partition.Add(tmp);
                                X -= PartitionX;
                            }
                        }
                    }
                }
            }
            catch { return false; }
            return true;
        }
        #endregion
    }
}