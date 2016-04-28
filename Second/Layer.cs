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
        private List<Point> point = new List<Point>();
        private Color color = new Color();
        private int NumberOfPoints;
        //****************************Конструктор****************************//
        public Layer(int size_x,int layer_heigh, int size_y, int size_y_earth,int NumberOfPoints)
        {
            Point tmp = new Point();
            this.NumberOfPoints = NumberOfPoints;
            int step = (size_x+14) / (NumberOfPoints-1);
            this.point.Add(new Point(-size_x / 2, size_y / 2 - size_y_earth + layer_heigh));
            for (int i = -size_x / 2 + step; i < size_x / 2; i+= step)
            {
                tmp.X = i;
                tmp.Y = size_y / 2 - size_y_earth + layer_heigh;
                this.point.Add(tmp);
            }
            this.point.Add(new Point(size_x / 2 - 4, size_y / 2 - size_y_earth + layer_heigh));
            Random rand = new Random(layer_heigh);
            this.color = Color.FromArgb(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255));
        }
        //****************************SET'ы GET'ы****************************//
        public List<Point> POINT
        {
            get { return point; }
        }
        public Color COLOR
        {
            get { return color; }
        }
        //*******************************Методы*****************************//
        public List<Point> Clone()
        {
            List<Point> a = new List<Point>();
            for (int i = 0; i < point.Count; i++)
                a.Add(point[i]);
            return a;
        }
        public int ReturnY(int number)
        {
            return point[number].Y;
        }
    }
}
