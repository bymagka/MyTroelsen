using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MyEBookReader
{
    class Program
    {
        static string theEBook;
        
        static void Main(string[] args)
        {
            GetEbook();

            Console.ReadLine();
        }

        static void GetEbook()
        {
            WebClient wc = new WebClient();

            wc.DownloadStringCompleted += (s, eArgs) =>
            {
                theEBook = eArgs.Result;
                Console.WriteLine("Downloading complete.");
                GetStats();

            };

            wc.DownloadStringAsync(new Uri("https://www.gutenberg.org/cache/epub/66936/pg66936.txt"));
        }

        private static void GetStats()
        {
            string[] words = theEBook.Split(new char[] {' ','\u000A',',','.',';',':','-','?','/' },StringSplitOptions.RemoveEmptyEntries);

            //10 чаще всего встречающихся
            string[] tenMostCommon = null;

            //самое длинное слово
            string longestWord = String.Empty;


            Parallel.Invoke(
                    () =>
                    {
                        tenMostCommon = FindTenMostCommon(words);
                    },
                    () =>
                    {
                        longestWord = FindLongestWord(words);
                    }

                );


            //Когда все задачи завершены, построить строку, показывающую всю статистику в окне сообщений.
            StringBuilder bookStats = new StringBuilder("Ten most common words are: \n");



            foreach(var s in tenMostCommon)
            {
                bookStats.AppendLine(s);

            }

            bookStats.AppendFormat("Longest world is: {0}", longestWord);
            bookStats.AppendLine();

            Console.WriteLine(bookStats.ToString(),"Book info");


        }

        private static string FindLongestWord(string[] words)
        {
            return words.Select(x => x).OrderByDescending(x => x.Length).FirstOrDefault();
        }

        private static string[] FindTenMostCommon(string[] words)
        {
            return words.Where(wd => wd.Length > 6)
                        .GroupBy(wd => wd)
                        .OrderBy(wd => wd.Count())
                        .Select(wd => wd.Key)
                        .Take(10)
                        .ToArray();

            
        }
    }
}
