using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggerAplication
{
    internal class Result
    {
        private bool _status { get; set; }

        private string _message { get; set; } = string.Empty;

        public Result(bool status)
        {
            _status = status;
        }

        public Result(bool status, string message)
        {
            _message = message;

            _status = status;

            if (_status == false)
            {
                LoggerSingleton.GetInstance().Info(_message, LogType.Error);
            }
        }
    }
}
