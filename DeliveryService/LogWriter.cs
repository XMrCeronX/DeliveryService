using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService
{
    public class LogWriter
    {
        public static void Log(string logMessage)
        {
            string dir = Directory.GetCurrentDirectory();
            try
            {
                using (StreamWriter w = File.AppendText(System.IO.Path.Combine(dir, "log.txt")))
                {
                    Log(logMessage, w);
                }
            }
            catch (Exception ex)
            {
            }
        }

        public static void Log(string logMessage, TextWriter txtWriter)
        {
            try
            {
                txtWriter.WriteLine($"[{DateTime.Now.ToString(Order.DATE_TIME_FORMAT)}] [LogWriter] [Log]: {logMessage}");
            }
            catch (Exception ex)
            {
            }
        }
    }
}
