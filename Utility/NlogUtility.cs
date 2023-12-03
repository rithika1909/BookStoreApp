using NLog;
using System;

namespace Utility
{
    public class NlogUtility
    {
        Logger logger = LogManager.GetCurrentClassLogger();
        public void LogInfo(string message)
        {
            logger.Info(message);
        }
        public void LogDebug(string message)
        {
            logger.Debug(message);
        }
        public void LogWarn(string message)
        {
            logger.Warn(message);
        }
        public void LogError(string message)
        {
            logger.Error(message);
        }
    }
}
