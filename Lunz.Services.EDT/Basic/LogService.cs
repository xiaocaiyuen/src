using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lunz.Services.EDT
{
    /// <summary>
    /// 处理 Log 的 Service 接口
    /// </summary>
    public interface ILogService
    {
        void Info(string message);
        void Debug(string message);
        void Warn(string message);
        void Error(string message);
        void Fatal(string message);
    }

    public class LogService:ServiceBase,ILogService
    {
        public static string LoggerName = string.Empty;
        private static ILog _log;  
        public static ILog log
        {
            get
            {
                if (string.Empty.Equals(LoggerName))
                {
                    LoggerName = "Default";
                }
                if (_log == null)
                {
                    //从配置文件中读取Logger对象  
                    _log = log4net.LogManager.GetLogger(LoggerName);
                }
                else
                {
                    if (_log.Logger.Name != LoggerName)
                    {
                        _log = log4net.LogManager.GetLogger(LoggerName);
                    }
                }
                return _log;
            }
        }

        public void Info(string message)
        {
            if (log.IsInfoEnabled)
            {
                log.Info(message);
            }  
        }

        public void Info(string loggerName,string message)
        {
            LoggerName = loggerName;
            if (log.IsInfoEnabled)
            {
                log.Info(message);
            }
        }

        public void Debug(string message)
        {
            if (log.IsDebugEnabled)
            {
                log.Debug(message);
            }
        }

        public void Debug(string loggerName, string message)
        {
            LoggerName = loggerName;
            if (log.IsDebugEnabled)
            {
                log.Debug(message);
            }
        }

        public void Warn(string message)
        {
            if (log.IsWarnEnabled)
            {
                log.Warn(message);
            }
        }

        public void Warn(string loggerName, string message)
        {
            LoggerName = loggerName;
            if (log.IsWarnEnabled)
            {
                log.Warn(message);
            }
        }

        public void Error(string message)
        {
            if (log.IsErrorEnabled)
            {
                log.Error(message);
            }
        }

        public void Error(string loggerName, string message)
        {
            LoggerName = loggerName;
            if (log.IsErrorEnabled)
            {
                log.Error(message);
            }
        }

        public void Fatal(string message)
        {
            if (log.IsFatalEnabled)
            {
                log.Fatal(message);
            }
        }

        public void Fatal(string loggerName, string message)
        {
            LoggerName = loggerName;
            if (log.IsFatalEnabled)
            {
                log.Fatal(message);
            }
        }
    }
}
