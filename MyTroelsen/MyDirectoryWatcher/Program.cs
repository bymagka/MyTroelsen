using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MyDirectoryWatcher
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** The Amazing File Watcher App*****");

            FileSystemWatcher watcher = new FileSystemWatcher();

            try
            {
                watcher.Path = @"D:\Загрузки";
            }
            catch (ArgumentException ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadLine();
                return;
            }


            //Цели наблюдения
            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;

            watcher.Filter = "*.txt";

            //Добавить обработчики событий 
            watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.Created += new FileSystemEventHandler(OnChanged);
            watcher.Deleted += new FileSystemEventHandler(OnChanged);
            watcher.Renamed += new RenamedEventHandler(OnRenamed);

            //Начать наблюдение
            watcher.EnableRaisingEvents = true;

            //Ожидать завершение программы
            Console.WriteLine("Press q to quit");

            while (Console.Read()!= 'q')
                return;
        }

        private static void OnRenamed(object sender, RenamedEventArgs e)
        {
            Console.WriteLine("File {0} renamed to {1}!", e.OldFullPath, e.FullPath);
        }

        private static void OnChanged(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine("File: {0} {1}!", e.FullPath, e.ChangeType);
        }
    }
}
