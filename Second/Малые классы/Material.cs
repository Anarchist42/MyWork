namespace Second
{
    class Material
    {
        #region Поля класса
        /// <summary>
        /// Название материала.
        /// </summary>
        string Name;
        /// <summary>
        /// Сопротивление.
        /// </summary>
        double Resistance;
        #endregion

        #region Конструктор
        /// <summary>
        /// Конструктор.
        /// </summary>
        public Material()
        {
        }
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="Name"> Название материала. </param>
        /// <param name="Resistance"> Сопротивление. </param>
        public Material(string Name, double Resistance)
        {
            this.Name = Name;
            this.Resistance = Resistance;
        }
        #endregion

        #region SETs and GETs
        public string NAME
        {
            get { return this.Name; }
            set { this.Name = value; }
        }
        public double RESISTANCE
        {
            get { return this.Resistance; }
            set { this.Resistance = value; }
        }
        #endregion

    }
}
