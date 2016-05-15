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
        private List<PointSpline> Points = new List<PointSpline>();
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
        public Layer(double XAreaSize, double LayerHeight, double YAreaSize, double EarthSize, int NumberOfPoints, int IndexNumber)
        {
            this.IndexNumber = IndexNumber;
            double Step = (XAreaSize) / (NumberOfPoints - 1) > 0 ? (XAreaSize) / (NumberOfPoints - 1) : 1;
            for (int i = 0; i < NumberOfPoints; i++)
                this.Points.Add(new PointSpline(i * Step, LayerHeight));
            Random rand = new Random(Convert.ToInt32(LayerHeight));
            this.Colors = Color.FromArgb(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255));

            //this.Points.Add(new PointSpline(-XAreaSize / 2, YAreaSize / 2 - EarthSize - LayerHeight));
            //for (int i = -(NumberOfPoints - 2) / 2; i <= (NumberOfPoints - 2) / 2; i++)
            //    this.Points.Add(new PointSpline(-XAreaSize / 2 + i * Step, YAreaSize / 2 - EarthSize - LayerHeight));
            //this.Points.Add(new PointSpline(XAreaSize / 2, YAreaSize / 2 - EarthSize - LayerHeight));


        }
        #endregion

        #region SETs and GETs
        public List<PointSpline> POINT
        {
            get
            {
                return Points;
            }
        }
        public Color COLOR
        {
            set
            {
                this.Colors = value;
            }
            get
            {
                return Colors;
            }
        }
        public int INDEXNMUMBER
        {
            get
            {
                return IndexNumber;
            }
            set
            {
                this.IndexNumber = value;
            }
        }
        #endregion

        #region Методы        
        /*Значение Y заданной точки (надо для сортировки слоев)*/
        /*Параметры: Number - номер точки*/
        /*Возвращает: Y - значение Y заданной точки*/
        public double ReturnY(int Number)
        {
            return Points[Number].Y;
        }

        /*Изменение значений Y при изменение YAreaSize*/
        /*Параметры: Increase - величина на которую надо изменить Y*/
        public void ChangeY(double Increase)
        {
            int i;
            for (i = 0; i < Points.Count; i++)
                Points[i] = new PointSpline(Points[i].X,Points[i].Y+Increase);
        }

        /*Добавление опорной точки*/
        /*Параметры: Add - координаты точки которую хотим добавить*/
        public void AddPoint(PointSpline Add)
        {
            int i;
            /*Ищем куда вставить*/
            for(i=1;i<Points.Count;i++)
                if(Points[i].X > Add.X)
                {
                    /*Вставляем элементы*/
                    Points.Insert(i, Add);
                    return;
                }
        }

        /*Удаление опорной точки*/
        /*Параметры: Delete - индекс удаляемой точки в массиве*/
        public void DeletePoint(int Delete)
        {
            if (Delete > 0 && Delete < Points.Count - 1)
                Points.RemoveAt(Delete);
        }
        #endregion
    }
}
