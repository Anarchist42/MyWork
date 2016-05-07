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
        private List<Point> Points = new List<Point>();
        private Color Colors = new Color();
        private int NumberOfPoints;
        //****************************Конструктор****************************//
        public Layer(int XAreaSize,int LayerHeight, int YAreaSize, int EarthSize,int NumberOfPoints, double Zoom)
        {
            Point tmp = new Point();
            this.NumberOfPoints = NumberOfPoints;
            int step = (XAreaSize + 14) / (NumberOfPoints-1);
            this.Points.Add(new Point(-XAreaSize / 2, YAreaSize / 2 - EarthSize + LayerHeight));
            for (int i = -XAreaSize / 2 + step; i < XAreaSize / 2; i+= step)
            {
                tmp.X = i;
                tmp.Y = YAreaSize / 2 - EarthSize + LayerHeight;
                this.Points.Add(tmp);
            }
            this.Points.Add(new Point(XAreaSize / 2 - Convert.ToInt32(5.0/Zoom), YAreaSize / 2 - EarthSize + LayerHeight));
            Random rand = new Random(LayerHeight);
            this.Colors = Color.FromArgb(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255));
        }
        //****************************SET'ы GET'ы****************************//
        public List<Point> POINT
        {
            get { return Points; }
        }
        public Color COLOR
        {
            get { return Colors; }
        }
        //*******************************Методы*****************************//
        public List<Point> Clone()
        {
            List<Point> a = new List<Point>();
            for (int i = 0; i < Points.Count; i++)
                a.Add(Points[i]);
            return a;
        }

        public int ReturnY(int number)
        {
            return Points[number].Y;
        }
    }
}
