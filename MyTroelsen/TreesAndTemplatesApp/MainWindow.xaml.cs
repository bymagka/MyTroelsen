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
using System.Reflection;
using System.Xml;
using System.Windows.Markup;

namespace TreesAndTemplatesApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string dataToShow { get; set; }
        private Control currControl = null;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnShowLogicalTree_Click(object sender, RoutedEventArgs e)
        {
            dataToShow = string.Empty;
            BuildLogicalTree(0, this);

            txtDisplayArea.Text = dataToShow;
        }

        private void BuildLogicalTree(int depth, object obj)
        {
            dataToShow += new string(' ', depth) + obj.GetType().Name + "\n";

            if (!(obj is DependencyObject)) return;

            foreach(var child in LogicalTreeHelper.GetChildren((DependencyObject)obj))
            {
                BuildLogicalTree(depth+5, child);
            }


        }

        private void btnShowVisualTree_Click(object sender, RoutedEventArgs e)
        {
            dataToShow = string.Empty;
            BuildVisualTree(0, this);
            txtDisplayArea.Text = dataToShow;
        }

        private void BuildVisualTree(int depth, DependencyObject obj)
        {
            dataToShow += new string(' ', depth) + obj.GetType().Name + "\n";

            for(int i = 0;i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                BuildVisualTree(depth + 1,VisualTreeHelper.GetChild(obj,i));
            }
        }

        private void btnTemplate_Click(object sender, RoutedEventArgs e)
        {
            dataToShow = string.Empty;
            ShowTemplate();
            txtDisplayArea.Text = dataToShow;
        }

        private void ShowTemplate()
        {
            if(currControl != null)
            {
                stackTemplatePanel.Children.Remove(currControl);
            }

            try
            {
                Assembly asm = Assembly.Load("PresentationFramework, Version=4.0.0.0," + "Culture=neutral, PublicKeyToken=31bf3856ad364e35");
                currControl = (Control)asm.CreateInstance(txtFullName.Text);
                currControl.Height = 200;
                currControl.Width = 200;
                currControl.Margin = new Thickness(5);

                stackTemplatePanel.Children.Add(currControl);

                var xmlSettings = new XmlWriterSettings() { Indent = true };

                StringBuilder sb = new StringBuilder();

                var xmlWriter = XmlWriter.Create(sb, xmlSettings);

                XamlWriter.Save(currControl.Template, xmlWriter);

                dataToShow = sb.ToString();


            }
            catch (Exception ex)
            {

                dataToShow = ex.Message;
            }
        }
    }
}
