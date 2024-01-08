using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggerAplication
{
    /// <summary>
    /// Have one method. Method generate 3 random situation for loglist: info, warning, error
    /// </summary>
    internal class Starter
    {
        public static void Run()
        {
            for (int i = 0; i <= 100; i++)
            {

                switch (new Random().Next(1, 4))
                {
                    case 1:
                        {
                            Action.StartMethod();

                            break;
                        }

                    case 2:
                        {
                            Action.SkippedLogicInMethod();

                            break;
                        }

                    case 3:
                        {
                            Action.IBrokeALogic();

                            break;
                        }
                }

                Thread.Sleep(200);
            }

            SaveLog.AskSaveLogToFile();
        }
    }
}
