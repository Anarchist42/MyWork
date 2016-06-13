namespace Second
{
    class PointMKE
    {
        #region Поля класса
        /// <summary>
        /// Точка.
        /// </summary>
        PointSpline Point = new PointSpline();
        /// <summary>
        /// Номер слоя или минерала.
        /// </summary>
        int NumberSpline;
        /// <summary>
        /// Слой или минерал.
        /// </summary>
        bool ItsLayer;
        #endregion

        #region Конструктор
        /// <summary>
        /// Конструктор.
        /// </summary>
        public PointMKE() { }
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="Point"> Координаты. </param>
        /// <param name="ItsLayer"> Слой или минерал. </param>
        public PointMKE(PointSpline Point, bool ItsLayer)
        {
            this.Point = Point;
            this.NumberSpline = -1;
            this.ItsLayer = ItsLayer;
        }
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="Point"> Координаты. </param>
        /// <param name="NumberSpline"> Номер слоя или минерала. </param>
        /// <param name="ItsLayer"> Слой или минерал. </param>
        public PointMKE(PointSpline Point, int NumberSpline, bool ItsLayer)
        {
            this.Point = Point;
            this.NumberSpline = NumberSpline;
            this.ItsLayer = ItsLayer;
        }
        #endregion

        #region GET'S and SET's
        public PointSpline POINT
        {
            get { return this.Point; }
            set { this.Point = value; }
        }
        public int NUMBERSPLINE
        {
            get { return this.NumberSpline; }
            set { this.NumberSpline = value; }
        }
        public bool ITSLAYER
        {
            get { return this.ItsLayer; }
            set { this.ItsLayer = value; }
        }
        #endregion

        #region Методы
        #endregion
    }
}