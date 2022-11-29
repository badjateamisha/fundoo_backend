using Microsoft.Extensions.Logging;
using NLog;

namespace FundooNotesApp
{
    public class NLog
    {
        public Logger logger = LogManager.GetCurrentClassLogger();
        public void LogInfo(string input)
        {
            logger.Info(input);
        }
    }
}
