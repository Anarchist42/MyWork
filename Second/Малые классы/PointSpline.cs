using System;
namespace Second
{
    class PointSpline
    {
        #region Поля класса
        /// <summary>
        /// Координата Х.
        /// </summary>
        double x;
        /// <summary>
        /// Координата Y.
        /// </summary>
        double y;
        #endregion
        #region Конструктор
        /// <summary>
        /// Конструктор.
        /// </summary>
        public PointSpline()
        {
        }
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="X"> Координата X. </param>
        /// <param name="Y"> Координата Y. </param>
        public PointSpline(double X, double Y)
        {
            this.x = Math.Round(X, GlobalConst.Accuracy);
            this.y = Math.Round(Y, GlobalConst.Accuracy);
        }
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="X"> Координата X. </param>
        /// <param name="Y"> Координата Y. </param>
        public PointSpline(string X, string Y)
        {
            this.x = Math.Round(Convert.ToDouble(X), GlobalConst.Accuracy);
            this.y = Math.Round(Convert.ToDouble(Y), GlobalConst.Accuracy);
        }
        #endregion
        #region SETs and GETs
        public double X
        {
            get { return x; }
            set { this.x = value; }
        }
        public double Y
        {
            get { return y; }
            set { this.x = value; }
        }
        #endregion
        #region Перегрузка операторов
        public static PointSpline operator +(PointSpline A, PointSpline B)
        {
            return new PointSpline(A.x + B.x, A.y + B.y);
        }
        public static PointSpline operator +(PointSpline A, double B)
        {
            return new PointSpline(A.x + B, A.y + B);
        }
        public static PointSpline operator -(PointSpline A, PointSpline B)
        {
            return new PointSpline(A.x - B.x, A.y - B.y);
        }
        public static PointSpline operator -(PointSpline A, double B)
        {
            return new PointSpline(A.x - B, A.y - B);
        }
        public static PointSpline operator *(PointSpline A, double B)
        {
            return new PointSpline(A.x*B, A.y*B);
        }
        public static bool operator ==(PointSpline A, PointSpline B)
        {
            if (A.x == B.x && A.y == B.y) return true;
            return false;
        }
        public static bool operator !=(PointSpline A, PointSpline B)
        {
            if (A.x != B.x || A.y != B.y) return true;
            return false;
        }
        #endregion
        #region Методы
        public bool Round()
        {
            try
            {
                this.x = Math.Round(this.x, GlobalConst.Accuracy);
                this.y = Math.Round(this.y, GlobalConst.Accuracy);
            }
            catch { return false; }
            return true;
        }
        #endregion
    }
}