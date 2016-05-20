using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Second
{
    public class GlobalConst
    {
        #region Константы
        /// <summary>
        /// Размер бордюра OpenGlControl.
        /// </summary>
        public static int Difference;
        /// <summary>
        /// Минимальный зум для MainPaint.
        /// </summary>
        public static double MinZoom;
        /// <summary>
        /// Точность(Знаков после запятой).
        /// </summary>
        public static int Accuracy;
        /// <summary>
        /// Буфер.
        /// </summary>
        public static string[] Buffer = new string[2];
        #endregion
    }
}
