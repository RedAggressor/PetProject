using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggerAplication
{
    public enum LogType
    {
        Info = 1,
        Warning,
        Error
    }

    internal class LoggerSingleton
    {
        private DateTime _logTime { get; set; }

        private string _messageText { get; set; } = string.Empty;

        private LogType _logType;

        private StringBuilder _logMessage = new StringBuilder();

        private LoggerSingleton() { }

        private static LoggerSingleton _loggerSingletonInstance = null;

        public static LoggerSingleton GetInstance()
        {
            if (_loggerSingletonInstance == null)
            {
                _loggerSingletonInstance = new LoggerSingleton();
            }

            return _loggerSingletonInstance;
        }

        public void Info(string message, LogType logType)
        {
            _logTime = DateTime.Now;

            _logType = logType;

            _messageText = message;

            DisplayLogToConsole();

            WritteToLoglist();
        }

        private void DisplayLogToConsole() => Console.WriteLine($"{_logTime} : {_logType} : {_messageText}");

        public string ShowLoglist() => _logMessage.ToString();

        private void WritteToLoglist() => _logMessage.Append($"{_logTime} : {_logType} : {_messageText}\n");
    }
}
