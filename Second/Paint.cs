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
        /*Указатель на окно где рисуем*/
        SimpleOpenGlControl GLPaint;
        /*Размер бардюра у GlControl*/
        int Difference;
        /**/
        #endregion
        #region Конструкторы
        public Paint()
        {

        }
        public Paint(SimpleOpenGlControl Paint)
        {

        }
        #endregion
        #region SETs and GETs
        public SimpleOpenGlControl GLPAINT
        {
            get { return GLPaint; }
            set
            {
                /*Меняем размер бардюра*/
                this.Difference = (value.BorderStyle.ToString() == "None") 
                    ? 1 
                    : (value.BorderStyle.ToString() == "FixedSingle") 
                    ? 3 : 5;
                this.GLPaint = value;
            }
        }
        #endregion

        public void add_layers(int size_x, int layer_height, int NumberOfPoints)
        {
        }


        /***************************Сброс настроек****************************/
        public void reset()
        {
        }

        //public int[] check_point(Point mouse)
        //{
        //}
        public void change_pointY(Point mouse, int[] ij)
        {
        }
        //******************************Cетка********************************//      
        private void drawing_grid()
        { 
        }                                                                              
        private void drawing_coordinates()
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


        public void Draw()
        {
            
        }
    }
}
