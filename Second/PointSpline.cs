using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Second
{
    class PointSpline
    {
        #region Поля класса
        double x;
        double y;
        #endregion

        #region Конструктор
        public PointSpline()
        {
        }
        public PointSpline(double X, double Y)
        {
            this.x = X;
            this.y = Y;
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
    }
}
