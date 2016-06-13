namespace Second
{
    class FiniteElement
    {
        #region Поля класса
        /// <summary>
        /// Точки конечного элемента.
        /// Слева на право, сверху вниз.
        /// </summary>
        private PointSpline[] Points = new PointSpline[4];
        /// <summary>
        /// Материал конечного элемента.
        /// </summary>
        private Material[] Materials = new Material[2];
        #endregion

        #region Конструктор
        /// <summary>
        /// Конструктор.
        /// </summary>
        public FiniteElement()
        { }
        #endregion

        #region GETs and SETs
        public PointSpline[] POINTS
        {
            get { return this.Points; }
            set { this.Points = value; }
        }
        public Material[] MATERIAL
        {
            get { return this.Materials; }
            set { this.Materials = value; }
        }
        #endregion

        #region Методы
        /// <summary>
        /// Получение конечного элемента с заданным типом.
        /// </summary>
        /// <param name="Type"> Тип конечного элемента (0 - трапеции, 1 - прямоугольники). </param>
        /// <returns> Конечный элемент с заданным типом. </returns>
        public FiniteElement Finit(int Type)
        {
            FiniteElement Clone = new FiniteElement();
            switch (Type)
            {
                case 0:
                    {
                        Clone.Points[0] = new PointSpline(this.Points[0].X, this.Points[0].Y);
                        Clone.Points[1] = new PointSpline(this.Points[1].X, this.Points[1].Y);
                        Clone.Points[2] = new PointSpline(this.Points[2].X, this.Points[2].Y);
                        Clone.Points[3] = new PointSpline(this.Points[3].X, this.Points[3].Y);
                        Clone.MATERIAL[0] = this.Materials[0];
                        Clone.MATERIAL[1] = this.Materials[1];
                    }; break;
                case 1:
                    {
                        Clone.Points[0] = new PointSpline(this.Points[0].X, (this.Points[0].Y + this.Points[1].Y) / 2);
                        Clone.Points[1] = new PointSpline(this.Points[1].X, (this.Points[0].Y + this.Points[1].Y) / 2);
                        Clone.Points[2] = new PointSpline(this.Points[2].X, (this.Points[2].Y + this.Points[3].Y) / 2);
                        Clone.Points[3] = new PointSpline(this.Points[3].X, (this.Points[2].Y + this.Points[3].Y) / 2);
                        Clone.MATERIAL[0] = this.Materials[0];
                        Clone.MATERIAL[1] = this.Materials[1];
                    }; break;
            }
            return Clone;
        }
        #endregion
    }
}
