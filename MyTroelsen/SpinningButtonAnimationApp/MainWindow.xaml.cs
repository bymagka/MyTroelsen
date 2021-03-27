using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Animation;

namespace SpinningButtonAnimationApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool isSpinning = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void spinner_OnClick(object sender, RoutedEventArgs e)
        {
            DoubleAnimation dblAnim = new DoubleAnimation { From = 1.0, To=0.0 };
            dblAnim.AutoReverse = true;
            dblAnim.RepeatBehavior = new RepeatBehavior(TimeSpan.FromSeconds(20));

            btnSpinner.BeginAnimation(Button.OpacityProperty,dblAnim);


        }

        private void spinner_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!isSpinning)
            {
                isSpinning = true;
                
                var dblAnim = new DoubleAnimation();
                dblAnim.Completed += (o, s) => { isSpinning = false; };

                dblAnim.Duration = new Duration(TimeSpan.FromSeconds(4));

                dblAnim.From = 0;
                dblAnim.To = 360;

                var rt = new RotateTransform();
                btnSpinner.RenderTransform = rt;

                rt.BeginAnimation(RotateTransform.AngleProperty, dblAnim);

            }
        }
    }
}
