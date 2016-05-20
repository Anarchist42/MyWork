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
        private Color Colors = new Color();
        /// <summary>
        /// Количество точек разбиения BSpline.
        /// </summary>
        private int BSplineN = 15;
        /// <summary>
        /// Материал слоя.
        /// </summary>
        private Material Material;
        #endregion

        #region Конструктор
        public Mineral(Material Material)
        {
            this.Material = Material;
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
            set { this.Colors = value; }
            get { return Colors; }
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
            double Min = Points[0].Y;
            for (i = 1; i < Points.Count; i++)
                if (Min > Points[i].Y) Min = Points[i].Y;
            return Min;
        }
        /// <summary>
        /// Минимальное значение глубины.
        /// </summary>
        /// <returns> Максимальное значение Y. </returns>
        public double ReturnMaxY()
        {
            int i;
            double Max = Points[0].Y;
            for (i = 1; i < Points.Count; i++)
                if (Max < Points[i].Y) Max = Points[i].Y;
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
        /*Заполняем массив BSpline*/
        public void BSpline()
        {
            /*Очищаем массив*/
            BSplinePoints.Clear();
            /*Добавляем самую первую точку*/
            //Points.Add(Points[0]);
            /*Необходимо добавить несколько точек, что бы рисовал до самого конца*/
            Points.Add(Points[Points.Count - 1]);
            Points.Add(Points[Points.Count - 1]);
            Points.Add(Points[Points.Count - 1]);
            /*Считаем нужное количество точек*/
            for (int start_cv = -3, j = 0; j != Points.Count; ++j, ++start_cv)
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
            Points.RemoveRange(Points.Count - 3, 3);
        }





        /*Добавление опорной точки*/
        /*Параметры: Add - координаты точки которую хотим добавить*/
        public void AddPoint(PointSpline Add)
        {
            Points.Add(Add);
            /*Перестраиваем массив*/
            BSpline();
        }

        /*Удаление опорной точки*/
        /*Параметры: Delete - индекс удаляемой точки в массиве*/
        public void DeletePoint(int Delete)
        {
            if (Points.Count > 3)
            {
                Points.RemoveAt(Delete);
                /*Перестраиваем массив*/
                BSpline();
            }
        }

        /*Удаление опорных точек меньше рабочей зоны*/
        /*Параметры: DeleteX*/
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
