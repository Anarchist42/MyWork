using System;
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
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="Name"> Название материала. </param>
        /// <param name="Resistance"> Сопротивление. </param>
        public Material(string Name, string Resistance)
        {
            this.Name = Name;
            this.Resistance = Convert.ToDouble(Resistance);
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

        #region Перегрузка операторов
        public static bool operator ==(Material A, Material B)
        {
            return A.Name == B.Name && A.Resistance == B.Resistance;
        }
        public static bool operator !=(Material A, Material B)
        {
            if ((object)A == null)
                return false;
            if ((object)B == null)
                return true;
            return A.Name != B.Name || A.Resistance != B.Resistance;
        }
        #endregion
    }
}