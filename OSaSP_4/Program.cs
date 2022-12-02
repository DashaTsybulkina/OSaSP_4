using System.Reflection;

namespace OSaSP_4
{
    class Program
    {

        //F:\\Осисп\\lab4\\OSaSP_4\\test.txt

        private static string getPathToFile() {
            Console.WriteLine("Path to file:");
            string path = string.Empty, temp;
            Boolean isExists = false;
            do
            {
                temp = Console.ReadLine();
                if (File.Exists(temp))
                {
                    isExists = true;
                    path = temp;
                }
                else {
                    Console.WriteLine("Input Error!Enter the path to an existing file ");
                }
            }while (!isExists);
            return path;
        }

        private static string[] getFileContent(string path) {
            return File.ReadAllLines(path);
        }

        static void Main(string[] args)
        {
            int countThreads;
            string path;
            Console.WriteLine("Count of threads:");
            while (!int.TryParse(Console.ReadLine(), out countThreads))
            {
                Console.WriteLine("Input Error! Enter an integer");
            }
            path = getPathToFile();
            string[] lines = getFileContent(path);

            var sortLines = Sorter.sort(lines, countThreads);
            foreach (string line in sortLines)
            {
                Console.WriteLine(line);
            }
            Console.ReadLine();
        }
    }
}