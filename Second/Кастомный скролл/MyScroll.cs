using System;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace Second
{
    public class MyScroll : Control
    {
        #region Поля класса
        /// <summary>
        /// Текущение значение скролла.
        /// </summary>
        private double @value;
        /// <summary>
        /// Максимальное значение скролла.
        /// </summary>
        private double maximum = 100;
        /// <summary>
        /// Дефолтное значение скролла.
        /// </summary>
        private double largechange = 100;
        /// <summary>
        /// Размер ползунка.
        /// </summary>
        private int thumbSize = 10;
        /// <summary>
        /// Цвет пользунка.
        /// </summary>
        private Color thumbColor = Color.Gray;
        /// <summary>
        /// Цвет подложки.
        /// </summary>
        private Color borderColor = Color.Silver;
        /// <summary>
        /// Ориентация ползунка.
        /// </summary>
        private ScrollOrientation orientation;       
        #endregion

        #region Конструктор
        public MyScroll()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint, true);
            SmallStep = 1;
            ShowButtons = true;
        }
        #endregion

        #region SETs and GETs
        public double Value
        {
            get { return value; }
            set
            {
                if (this.value == value)
                    return;
                this.value = value;
                Refresh();
                OnScroll();
            }
        }
        public double Maximum
        {
            get { return maximum; }
            set { maximum = value; Invalidate(); }
        }
        public double LargeChange
        {
            get { return largechange; }
            set { largechange = value; Invalidate(); }
        }
        public int ThumbSize
        {
            get { return thumbSize; }
            set { thumbSize = value; Invalidate(); }
        }
        public Color ThumbColor
        {
            get { return thumbColor; }
            set { thumbColor = value; Invalidate(); }
        }
        public Color BorderColor
        {
            get { return borderColor; }
            set { borderColor = value; Invalidate(); }
        }
        public ScrollOrientation Orientation
        {
            get { return orientation; }
            set { orientation = value; Invalidate(); }
        }
        [DefaultValue(1)]
        public double SmallStep { get; set; }
        [DefaultValue(true)]
        public bool ShowButtons { get; set; }       
        #endregion

        #region Методы
        /// <summary>
        /// Эвент изменения значения ползунка.
        /// </summary>
        public event EventHandler ValueChanged;
        /// <summary>
        /// Переопределение функции нажатия мыши.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                MouseScroll(e);
            base.OnMouseDown(e);
        }
        /// <summary>
        /// Переопределение функции передвижения мыши.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                MouseScroll(e);
            base.OnMouseMove(e);
        }
        /// <summary>
        /// Изменение ползунка от скролла.
        /// </summary>
        /// <param name="e"></param>
        private void MouseScroll(MouseEventArgs e)
        {
            var v = Value;
            var pad = ButtonPadding;
            switch (Orientation)
            {
                case ScrollOrientation.VerticalScroll:
                    if (e.Y < pad) v -= SmallStep;
                    else
                    if (e.Y > Height - pad) v += SmallStep;
                    else v = Maximum * (e.Y - thumbSize / 2 - pad) / (Height - thumbSize - pad * 2);
                    break;
                case ScrollOrientation.HorizontalScroll:
                    if (e.X < pad) v -= SmallStep;
                    else
                    if (e.X > Width - pad) v += SmallStep;
                    else v = Maximum * (e.X - thumbSize / 2 - pad) / (Width - thumbSize - pad * 2);
                    break;
            }
            Value = Math.Max(0, Math.Min(Maximum, v));
        }
        /// <summary>
        /// Виртуальная функция вызывающая эвент изменения значения.
        /// </summary>
        /// <param name="type"></param>
        public virtual void OnScroll(ScrollEventType type = ScrollEventType.ThumbPosition)
        {
            if (ValueChanged != null)
                ValueChanged(this, EventArgs.Empty);
        }
        /// <summary>
        /// Переопределение функции отрисовки.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            if (Maximum <= 0)
                return;
            var w = Width;
            var h = Height;
            Rectangle thumbRect = Rectangle.Empty;
            switch (Orientation)
            {
                case ScrollOrientation.HorizontalScroll:
                    w -= ButtonPadding * 2;
                    thumbRect = new Rectangle((int)(value * (w - thumbSize) / Maximum) + ButtonPadding, 2, thumbSize, h - 4);
                    if (ShowButtons)
                        using (var pen = new Pen(BorderColor, 4) { StartCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor })
                        {
                            e.Graphics.DrawLine(pen, 0, Height / 2, ButtonPadding - 3, Height / 2);
                            e.Graphics.DrawLine(pen, Width - 1, Height / 2, Width - ButtonPadding + 2, Height / 2);
                        }
                    break;
                case ScrollOrientation.VerticalScroll:
                    h -= ButtonPadding * 2;
                    thumbRect = new Rectangle(2, (int)(value * (h - thumbSize) / Maximum) + ButtonPadding, w - 4, thumbSize);

                    if (ShowButtons)
                        using (var pen = new Pen(BorderColor, 4) { StartCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor })
                        {
                            e.Graphics.DrawLine(pen, Width / 2, 0, Width / 2, ButtonPadding - 3);
                            e.Graphics.DrawLine(pen, Width / 2, Height - 1, Width / 2, Height - ButtonPadding + 2);
                        }
                    break;
            }
            using (var brush = new SolidBrush(thumbColor))
                e.Graphics.FillRectangle(brush, thumbRect);
            using (var pen = new Pen(borderColor))
                switch (Orientation)
                {
                    case ScrollOrientation.HorizontalScroll:
                        e.Graphics.DrawRectangle(pen, new Rectangle(ButtonPadding, 0, w - 1, h - 1));
                        break;
                    case ScrollOrientation.VerticalScroll:
                        e.Graphics.DrawRectangle(pen, new Rectangle(0, ButtonPadding, w - 1, h - 1));
                        break;
                }
        }
        /// <summary>
        /// Попали ли на кнопку.
        /// </summary>
        private int ButtonPadding { get { return ShowButtons ? 9 : 0; } }
        #endregion
    }
}