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
using System.Threading;
using System.IO;
using System.Drawing;

namespace DataParallelismWithForEach
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        CancellationTokenSource cancellationToken = new CancellationTokenSource();

        public MainWindow()
        {
            InitializeComponent();
        }



        private void cmdProcess_Click(object sender,EventArgs e)
        {
           Task.Factory.StartNew(()=> ProcessFiles());
        }

        private void ProcessFiles()
        {
            //использовать ParallelOptions для хранения CancellationToken
            ParallelOptions parallelOptions = new ParallelOptions();
            parallelOptions.CancellationToken = cancellationToken.Token;
            parallelOptions.MaxDegreeOfParallelism = Environment.ProcessorCount;

            string[] files = Directory.GetFiles(@"D:\Загрузки", "*.jpg", SearchOption.AllDirectories);

            string newDir = @".\ModifiedPictures";

            Directory.CreateDirectory(newDir);

            try
            {
                //Обработать данные изображения в параллельном режиме
                Parallel.ForEach(files, parallelOptions, fl => {

                    parallelOptions.CancellationToken.ThrowIfCancellationRequested();

                    string filename = System.IO.Path.GetFileName(fl);

                    using(Bitmap bitmap = new Bitmap(fl))
                    {
                        bitmap.RotateFlip(RotateFlipType.RotateNoneFlipNone);
                        bitmap.Save(System.IO.Path.Combine(newDir, filename));
                        
                        this.Dispatcher.Invoke(() =>
                        {
                            this.Title = $"Processing {filename} on thread {Thread.CurrentThread.ManagedThreadId}";
                        });

                    }
                });

                this.Dispatcher.Invoke(() =>
                {
                    this.Title = "All done!";
                });
            }
            catch (OperationCanceledException ex)
            {

                this.Dispatcher.Invoke(() => {
                    this.Title = ex.Message;
                });
            }


            ////Обработать данные в блокирующей манере
            //Parallel.ForEach(files, fl =>
            //{
            //    string filename = System.IO.Path.GetFileName(fl);

            //    using (Bitmap bitmap = new Bitmap(fl))
            //    {
            //        bitmap.RotateFlip(RotateFlipType.RotateNoneFlipNone);
            //        bitmap.Save(System.IO.Path.Combine(newDir, filename));

            //        //индентификатор потока, обрабатывающий текущее изображение
            //        //this.Title = $"Processing {filename} on thread {Thread.CurrentThread.ManagedThreadId}";
            //        this.Dispatcher.Invoke(() => {
            //            this.Title = $"Processing {filename} on thread {Thread.CurrentThread.ManagedThreadId}";
            //        });
            //    }
            //});


        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            cancellationToken.Cancel();
        }
    }

    
}
