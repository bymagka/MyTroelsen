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

namespace WpfStyles
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            lstStyles.Items.Add("GrowingButton");
            lstStyles.Items.Add("BasicControl");
            lstStyles.Items.Add("TiltButton");
       
        }



        private void lstStyles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var currStyle = (Style)TryFindResource(lstStyles.SelectedValue);

            if (currStyle == null) return;
            else btnStyles.Style = currStyle;
        }
    }
}
