using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RenderingWithShapes
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        enum SelectedShape
        {
            Circle,Rectangle,Line,Path
        }

        SelectedShape currentShape;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void canvasDrawingArea_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Shape shapeToRender = null;

            switch (currentShape)
            {
                case SelectedShape.Circle:
                    shapeToRender = new Ellipse {  Height=35,Width=35};
                    RadialGradientBrush rgb = new RadialGradientBrush();
                    rgb.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#FF0E980E"), 0.486));
                    rgb.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("White"), 0.978));
                    rgb.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("Black"), 0.007));
                    rgb.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("Black"), 0.009));

                    shapeToRender.Fill = rgb;

                    break;
                case SelectedShape.Rectangle:
                    shapeToRender = new Rectangle { Fill = Brushes.Red, Height = 35, Width = 35, RadiusX = 10, RadiusY = 10 };
                    break;
                case SelectedShape.Path:
                    Path pathToRender = new Path { Fill = Brushes.Orange, Stroke=Brushes.Blue,StrokeThickness=3 };
                    GeometryGroup geomGroup = new GeometryGroup();
                    geomGroup.Children.Add(new EllipseGeometry { Center = new Point(20, 10), RadiusX = 10, RadiusY = 15 });
                    geomGroup.Children.Add(new RectangleGeometry { Rect = new Rect(0, 0, 40, 20) });
                    pathToRender.Data = geomGroup;
                    shapeToRender = pathToRender;
                    break;
                default:
                    shapeToRender = new Line
                    {
                        Stroke = Brushes.Blue,
                        X1 = 10,
                        Y1 = 10,
                        X2 = 25,
                        Y2 = 25,
                        StrokeThickness = 10,
                        StrokeStartLineCap = PenLineCap.Triangle,
                        StrokeEndLineCap = PenLineCap.Round
                    };
                    break;
            }

            if (flipCave.IsChecked == true)
            {
                RotateTransform rt = new RotateTransform(-180);
                shapeToRender.RenderTransform = rt;
            }

            Canvas.SetLeft(shapeToRender, e.GetPosition(canvasDrawingArea).X);
            Canvas.SetTop(shapeToRender, e.GetPosition(canvasDrawingArea).Y);

            canvasDrawingArea.Children.Add(shapeToRender);
        }



        private void Click(object sender, RoutedEventArgs e)
        {
            switch((sender as RadioButton)?.Name.ToLower())
            {
                case "circleoption":
                    currentShape = SelectedShape.Circle;
                    break;
                case "rectoption":
                    currentShape = SelectedShape.Rectangle;
                    break;
                case "pathoption":
                    currentShape = SelectedShape.Path;
                    break;
                default:
                    currentShape = SelectedShape.Line;
                    break;
            }

        }

        private void canvasDrawingArea_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point p = e.GetPosition((Canvas)sender);

            HitTestResult result = VisualTreeHelper.HitTest(canvasDrawingArea, p);

            if(result != null)
            {
                canvasDrawingArea.Children.Remove(result.VisualHit as Shape);
            }
        }

        private void flipCanvas_Click(object sender, RoutedEventArgs e)
        {

            ToggleButton tb = (ToggleButton)sender;


            if (tb.IsChecked == true)
            {
                RotateTransform rt = new RotateTransform(-180);
                canvasDrawingArea.LayoutTransform = rt;
            }
            else canvasDrawingArea.LayoutTransform = null;


        }
    }
}
