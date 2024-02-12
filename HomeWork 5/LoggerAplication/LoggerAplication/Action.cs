using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggerAplication
{
    internal class Action
    {
        public static Result StartMethod()
        {
            LoggerSingleton.GetInstance().Info("Satrt Method", LogType.Info);

            return new Result(true);
        }

        public static Result SkippedLogicInMethod()
        {
            LoggerSingleton.GetInstance().Info("Skipped Logic In Method", LogType.Warning);

            return new Result(true);
        }

        public static Result IBrokeALogic()
        {
            LoggerSingleton.GetInstance().Info("I Broke A Logic", LogType.Error);

            return new Result(false, "Action failed by a reason:");
        }
    }
}
