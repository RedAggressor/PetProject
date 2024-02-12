using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggerAplication
{
    internal class SaveLog
    {
        public static void AskSaveLogToFile()
        {
            bool checkOutRequest = true;

            while (checkOutRequest)
            {
                Console.WriteLine("Does save the log to txt file? Y/N");

                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.Y:
                        {
                            Console.WriteLine();

                            Console.WriteLine("Log saves to log.txt");

                            File.WriteAllText("log.txt", LoggerSingleton.GetInstance().ShowLoglist());

                            checkOutRequest = false;

                            break;
                        }
                    case ConsoleKey.N:
                        {
                            Console.WriteLine();

                            Console.WriteLine("Log doesn`t save to file");

                            checkOutRequest = false;

                            break;
                        }
                    default:
                        {
                            Console.WriteLine();

                            Console.WriteLine("You enter somthing else... repit");

                            checkOutRequest = true;

                            break;
                        }
                }
            }
        }
    }
}
