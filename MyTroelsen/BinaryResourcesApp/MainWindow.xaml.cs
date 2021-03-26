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

namespace BinaryResourcesApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<BitmapImage> Images = new List<BitmapImage>();
        int currentIndex = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            try
            {
                Images.Add(new BitmapImage(new Uri(@"/Images/ball.jpg",UriKind.Relative)));
                Images.Add(new BitmapImage(new Uri(@"/Images/me.jpg", UriKind.Relative)));
                Images.Add(new BitmapImage(new Uri(@"/Images/mem.jpg", UriKind.Relative)));

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ooooops");
            };

            myImage.Source = Images[currentIndex];

        }

        private void btnPreviousPage_Click(object sender, RoutedEventArgs e)
        {
            if (--currentIndex < 0)
            {
                currentIndex = Images.Count - 1;
            };

            myImage.Source = Images[currentIndex];
        }

        private void btnNextPage_Click(object sender, RoutedEventArgs e)
        {
            if (++currentIndex >= Images.Count)
            {
                currentIndex = 0;
        }; 

            myImage.Source = Images[currentIndex];
        }
    }
}
