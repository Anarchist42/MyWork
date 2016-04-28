using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

using Tao.DevIl;
using Tao.OpenGl;
using Tao.FreeGlut;
using Tao.Platform.Windows;

namespace Second
{
    class Paint
    {
        #region Поля класса
        #region Нужны Get\Set
        /*Зум*/
        double Zoom;
        /*Количество клеток на линии*/
        int CellsNumber;
        /*Смещение по оси X*/
        double XOffset;
        /*Смещение по оси Y*/
        double YOffset;
        /*Позиция мышки*/
        Point MousePosition;
        /*Размер исследуемой области по оси X*/
        int XAreaSize;
        /*Размер исследуемой области по оси Y*/
        int YAreaSize;
        /*Высота уровня земли*/
        int EarthSize;
        #endregion
        #region Не нужны Get\Set
        /*Указатель на окно где рисуем*/
        SimpleOpenGlControl GLPaint;
        /*Минимальный зум*/
        double MinZoom;
        /*Cлои почвы*/
        List<Layer> Layers = new List<Layer>();
        #endregion
        #endregion

        #region Константы класса
        /*Размер бардюра у GlControl*/
        const int Difference = 5;
        #endregion

        #region Конструктор
        public Paint(SimpleOpenGlControl Paint)
        {
            this.GLPaint = Paint;
            this.MousePosition = new Point(0, 0);
            this.EarthSize = 100;
            this.CellsNumber = 10;
            this.XOffset = 0.0;
            this.YOffset = 0.0;
            this.XAreaSize = 0;
            this.YAreaSize = 0;
        }
        #endregion

        #region SETs and GETs
        public double ZOOM
        {
            get { return this.Zoom; }
            set { this.Zoom = value; }
        }
        public int CELLSNUMBER
        {
            get { return this.CellsNumber; }
            set
            {
                this.CellsNumber = (value > 0 && value < 20)
                  ? value : (value < 20) ? 0 : 20;
            }
        }
        public double XOFFSET
        {
            get { return this.XOffset; }
            set { this.XOffset = value; }
        }
        public double YOFFSET
        {
            get { return this.YOffset; }
            set { this.YOffset = value; }
        }
        public Point MOUSEPOSITION
        {
            get { return this.MousePosition; }
            set { this.MousePosition = value; }
        }
        public int XAREASIZE
        {
            get { return this.XAreaSize; }
            set 
            {
                this.XAreaSize = value;
                this.MinZoom = (GLPaint.Width - Difference) / XAreaSize;
                this.Zoom = this.MinZoom;
            }
        }
        public int YAREASIZE
        {
            get { return this.YAreaSize; }
            set
            {
                this.YAreaSize = value;
            }
        }
        public int EARTHSIZE
        {
            get { return this.EarthSize; }
            set { this.EarthSize = (value > 0) ? value : 0; }
        }
        #endregion

        #region Методы
        /*Метод отрсиовки сетки*/
        private void DrawingGrid()
        {

        }
        #region Потом

        public void add_layers(int size_x, int layer_height, int NumberOfPoints)
        {
        }


        public void reset()
        {
        }

        public void change_pointY(Point mouse, int[] ij)
        {
        }   

        

        private Point GetPoint(int i, List<Point> point)
        {
            if (i < 0)
                return point[0];
            if (i < point.Count)
                return point[i];
            return point[point.Count - 1];
        }
        private void drawing_layers()
        {
            
        }
        #endregion
        #endregion
        /*Основной метод класса, вызывает отрисовку*/
        public void Draw()
        {
            Gl.glClearColor(1.0f, 1.0f, 1.0f, 0.0f);
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
            Gl.glPushMatrix();
            Gl.glTranslated(XOffset, YOffset, 0);
            Gl.glScaled(Zoom, Zoom, Zoom);
            DrawingGrid();
        }
    }
}
