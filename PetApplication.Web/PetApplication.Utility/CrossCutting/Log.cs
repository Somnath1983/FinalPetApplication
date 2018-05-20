using NLog;
using System;

namespace PetApplication.Utility
{
    public class Log
    {
        //Loggin to Nlog
        private static Logger logger = LogManager.GetCurrentClassLogger();

        //Elmah is set up access to anly local server
        //http://localhost:62195/elmah.axd
        public static void LogError(Exception exception)
        {
            //log exception here..
            logger.Error(Guid.NewGuid().ToString() + ":" + exception.InnerException + ":" + exception.StackTrace);
        }

        public static void LogError(string Message)
        {
            logger.Error(Message);
        }

        public static void LogAudit(string UserName, string IPAddress, string AreaAccessed, string Message)
        {
            logger.Info(Guid.NewGuid().ToString() + ":" + IPAddress + ":" + AreaAccessed + ":" + Message);
        }
    }
}
