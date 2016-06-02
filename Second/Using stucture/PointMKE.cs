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
        /// Материал.
        /// </summary>
        Material Material = new Material();
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
        /// <param name="Material"> Материал. </param>
        /// <param name="ItsLayer"> Слой или минерал. </param>
        public PointMKE(PointSpline Point, Material Material, bool ItsLayer)
        {
            this.Point = Point;
            this.Material = Material;
            this.ItsLayer = ItsLayer;
        }
        #endregion

        #region GET'S and SET's
        public PointSpline POINT
        {
            get { return this.Point; }
            set { this.Point = value; }
        }
        public Material MATERIAL
        {
            get { return this.Material; }
            set { this.Material = value; }
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