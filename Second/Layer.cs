using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Second
{
    class Layer
    {
        #region Поля класса
        /*Массив опорных точек*/
        private List<Point> Points = new List<Point>();
        /*Цвет слоя*/
        private Color Colors = new Color();
        /*Порядковый номер слоя*/
        private int IndexNumber;
        #endregion

        #region Конструктор
        /*Конструктор слоя*/
        /*Параметры: XAreaSize - ширина рабочей области*/
        /*           LayerHeight - на какой глубине находится слой*/
        /*           YAreaSize - высота рабочей области*/
        /*           EarthSize - высота над уровнем земли*/
        /*           NumberOfPoints - количество опорных точек*/
        /*           IndexNumber - порядковый номер слоя*/
        public Layer(int XAreaSize,int LayerHeight, int YAreaSize, int EarthSize,int NumberOfPoints, int IndexNumber)
        {
            Point tmp = new Point();
            this.IndexNumber = IndexNumber;
            int step = (XAreaSize) / (NumberOfPoints-1) > 0 ? (XAreaSize) / (NumberOfPoints - 1) : 1 ;
            this.Points.Add(new Point(-XAreaSize / 2, YAreaSize / 2 - EarthSize - LayerHeight));
            for (int i = -XAreaSize / 2 + step; i < XAreaSize / 2; i+= step)
            {
                tmp.X = i;
                tmp.Y = YAreaSize / 2 - EarthSize - LayerHeight;
                this.Points.Add(tmp);
            }
            this.Points.Add(new Point(XAreaSize / 2, YAreaSize / 2 - EarthSize - LayerHeight));
            Random rand = new Random(LayerHeight);
            this.Colors = Color.FromArgb(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255));
        }
        #endregion

        #region SETs and GETs
        public List<Point> POINT
        {
            get { return Points; }
        }
        public Color COLOR
        {
            set { this.Colors = value; }
            get { return Colors; }
        }
        public int INDEXNMUMBER
        {
            get { return IndexNumber; }
            set { this.IndexNumber = value; }
        }
        #endregion

        #region Методы        
        /*Значение Y заданной точки (надо для сортировки слоев)*/
        /*Параметры: Number - номер точки*/
        /*Возвращает: Y - значение Y заданной точки*/
        public int ReturnY(int Number)
        {
            return Points[Number].Y;
        }

        /*Изменение значений Y при изменение YAreaSize*/
        /*Параметры: Increase - величина на которую надо изменить Y*/
        public void ChangeY(int Increase)
        {
            int i;
            for (i = 0; i < Points.Count; i++)
                Points[i] = new Point(Points[i].X,Points[i].Y+Increase);
        }
        #endregion
    }
}
