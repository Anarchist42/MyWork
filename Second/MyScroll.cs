using System;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace Second
{
    public class MyScroll : Control
    {
        private long @value;

        public long Value
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

        private long maximum = 100;
        public long Maximum
        {
            get { return maximum; }
            set { maximum = value; Invalidate(); }
        }

        private long largechange = 100;
        public long LargeChange
        {
            get { return largechange; }
            set { largechange = value; Invalidate(); }
        }

        private int thumbSize = 10;
        public int ThumbSize
        {
            get { return thumbSize; }
            set { thumbSize = value; Invalidate(); }
        }

        private Color thumbColor = Color.Gray;
        public Color ThumbColor
        {
            get { return thumbColor; }
            set { thumbColor = value; Invalidate(); }
        }

        private Color borderColor = Color.Silver;
        public Color BorderColor
        {
            get { return borderColor; }
            set { borderColor = value; Invalidate(); }
        }

        private ScrollOrientation orientation;
        public ScrollOrientation Orientation
        {
            get { return orientation; }
            set { orientation = value; Invalidate(); }
        }

        [DefaultValue(1)]
        public long SmallStep { get; set; }

        [DefaultValue(true)]
        public bool ShowButtons { get; set; }

        private int ButtonPadding { get { return ShowButtons ? 9 : 0; } }

        public event EventHandler ValueChanged;

        public MyScroll()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint, true);

            SmallStep = 1;
            ShowButtons = true;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                MouseScroll(e);
            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                MouseScroll(e);
            base.OnMouseMove(e);
        }

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

        public virtual void OnScroll(ScrollEventType type = ScrollEventType.ThumbPosition)
        {
            if (ValueChanged != null)
                ValueChanged(this, EventArgs.Empty);
        }

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
    }
}
