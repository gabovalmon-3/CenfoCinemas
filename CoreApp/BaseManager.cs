using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CoreApp
{
    public class BaseManager
    {
        protected void ManageException(Exception exception)
        {

            var logDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
            var logFile = Path.Combine(logDir, "errors.log");

            try
            {
                Directory.CreateDirectory(logDir);
                var entry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} | {exception.GetType().Name}: {exception.Message}{Environment.NewLine}";
                File.AppendAllText(logFile, entry);
            }
            catch
            {

            }


            throw new ApplicationException("Se produjo un problema interno. Por favor, inténtalo de nuevo más tarde.");
        }
    }
}