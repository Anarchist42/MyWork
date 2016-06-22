namespace Second
{
    public class TriDiagonalMatrixF
    {
        #region Поля класса
        /// <summary>
        /// Нижняя диагональ. A[0] никогда не используется.
        /// </summary>
        public double[] A;
        /// <summary>
        /// Главная диагональ.
        /// </summary>
        public double[] B;
        /// <summary>
        /// Верхняя дигональ.. C[C.Length-1] никогда не используется.
        /// </summary>
        public double[] C;
        /// <summary>
        /// Размерность матрицы.
        /// </summary>
        public int N
        {
            get { return (A != null ? A.Length : 0); }
        }
        #endregion

        #region Конструктор
        /// <summary>
        /// Конструктор.
        /// </summary>
        public TriDiagonalMatrixF(int n)
        {
            this.A = new double[n];
            this.B = new double[n];
            this.C = new double[n];
        }
        #endregion

        #region Методы
        /// <summary>
        /// Решение трех-диаганальный матрицы.
        /// </summary>
        /// <param name="d"> Правая часть. </param>
        public double[] Solve(double[] d)
        {
            /*Прямой ход*/
            double[] cPrime = new double[N];
            cPrime[0] = C[0] / B[0];
            for (int i = 1; i < N; i++)
                cPrime[i] = C[i] / (B[i] - cPrime[i - 1] * A[i]);
            /*Прямой ход*/
            double[] dPrime = new double[N];
            dPrime[0] = d[0] / B[0];
            for (int i = 1; i < N; i++)
                dPrime[i] = (d[i] - dPrime[i - 1] * A[i]) / (B[i] - cPrime[i - 1] * A[i]);
            /*Обратный ход*/
            double[] x = new double[N];
            x[N - 1] = dPrime[N - 1];
            for (int i = N - 2; i >= 0; i--)
                x[i] = dPrime[i] - cPrime[i] * x[i + 1];
            return x;
        }
        #endregion
    }
}