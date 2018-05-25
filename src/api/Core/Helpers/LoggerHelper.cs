using System.Collections.Generic;
using System.Linq;
using Serilog;
using Serilog.Events;

namespace Portal.Apis.Core.Helpers
{
    public class LoggerHelper
    {
        public static void SetupSerilog()
        {
            // Configure Serilog
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel
            .Information()
            .WriteTo.RollingFile("logs/log-{Date}.txt", LogEventLevel.Information) // Uncomment if logging required on text file
            // .WriteTo.Seq("http://localhost:5341/")
            .CreateLogger();
        }
    }
}