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
        /// Цвет слоя.
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
        /// Изменение точности.
        /// </summary>
        /// <returns></returns>
        public bool ChangeAccuracy()
        {
            int i;
            try
            {
                for (i = 0; i < Points.Count; i++)
                    Points[i].Round();
                for (i = 0; i < BSplinePoints.Count; i++)
                    BSplinePoints[i].Round();
            }
            catch
            {
                return false;
            }
            return true;
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
        /// Смещение точек вправо.
        /// </summary>
        /// <param name="ChangeX"> На сколько смещать. </param>
        public void ChangeXPoints(double ChangeX)
        {
            int i;
            for (i = 0; i < Points.Count; i++)
                Points[i].X += ChangeX;
            /*Добавляем точку в начало*/
            Points.Insert(0, new PointSpline(0, Points[0].Y));
            /*Перестраиваем массив*/
            BSpline();
        }
        /// <summary>
        /// Добавление опорной точки.
        /// </summary>
        /// <param name="Add"> Добавляемая точка. </param>
        public bool AddPoint(PointSpline Add,out int Position)
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
                    BSpline();
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
            if(c2<=c1)
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
        public void BSpline()
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
        /// <summary>
        /// Удаление опорной точки.
        /// </summary>
        /// <param name="Delete"> Индекс удаляемой точки. </param>
        public void DeletePoint(int Delete)
        {
            if (Points.Count > 3)
            {
                Points.RemoveAt(Delete);
                /*Перестраиваем массив*/
                BSpline();
            }
        }
        /// <summary>
        /// Удаление опорных точек правее области.
        /// </summary>
        /// <param name="DeleteX"> На сколько уменьшилась область. </param>
        public void DeletePoint(double DeleteX)
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
        #endregion
    }
}