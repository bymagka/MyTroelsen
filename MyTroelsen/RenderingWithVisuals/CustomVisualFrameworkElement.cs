using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace RenderingWithVisuals
{
    public class CustomVisualFrameworkElement : FrameworkElement
    {
        VisualCollection theVisuals;

        public CustomVisualFrameworkElement()
        {
            theVisuals = new VisualCollection(this) { AddRect(), AddCircle() };
            this.MouseDown += CustomVisualFrameworkElement_MouseDown;
        }

        private void CustomVisualFrameworkElement_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Point t = e.GetPosition((UIElement)sender);

            VisualTreeHelper.HitTest(this, null, new HitTestResultCallback(myCallback), new PointHitTestParameters(t));
        }

        private HitTestResultBehavior myCallback(HitTestResult result)
        {
            if(result.VisualHit.GetType() == typeof(DrawingVisual))
            {
                if(((DrawingVisual)result.VisualHit).Transform == null)
                {
                    ((DrawingVisual)result.VisualHit).Transform = new SkewTransform(20, 20);
                }
            }
            else ((DrawingVisual)result.VisualHit).Transform = null;

            return HitTestResultBehavior.Stop;
        }

        private Visual AddCircle()
        {
            DrawingVisual drawingVisual = new DrawingVisual();

            using (DrawingContext dc = drawingVisual.RenderOpen())
            {
                dc.DrawEllipse(Brushes.Green, null, new Point(70, 90), 40, 50);
            }

            return drawingVisual;
        }

        private Visual AddRect()
        {
            DrawingVisual drawingVisual = new DrawingVisual();

            using (DrawingContext dc = drawingVisual.RenderOpen())
            {
                dc.DrawRectangle(Brushes.Black, new Pen(Brushes.Yellow, 2), new Rect(new Point(100, 100), new Size(320, 80)));
            }

            return drawingVisual;
        }

        protected override int VisualChildrenCount => theVisuals.Count;

        protected override Visual GetVisualChild(int index)
        {
            if (index < 0 || index >= theVisuals.Count)
            {
                throw new ArgumentOutOfRangeException();
            }

            return theVisuals[index];
        }
    }
}
