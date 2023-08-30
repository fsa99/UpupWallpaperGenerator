namespace MakeUpupResources.Helper
{
    using NLog;

    public class LogManager
    {
        private static LogManager _instance;
        private static Logger _logger;

        private LogManager()
        {
            _logger = NLog.LogManager.GetCurrentClassLogger();
        }

        public static LogManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new LogManager();
                }
                return _instance;
            }
        }

        // 通用的日志记录方法
        public void Log(NLog.LogLevel level, string message)
        {
            _logger.Log(level, message);
        }
    }


}
