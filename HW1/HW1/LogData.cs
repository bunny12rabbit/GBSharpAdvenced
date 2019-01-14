using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW1
{
    public delegate void Log(string LogMsg);
    /// <summary>
    /// Ведение журнала событий
    /// </summary>
    class LogData
    {
        /// <summary>
        /// отображение и запись логов
        /// </summary>
        /// <param name="msg">получаемая строка</param>               
        public void LogConsoleWrite(string msg)
        {
            Console.WriteLine(msg);
            using (var sw = new StreamWriter("Log.txt", true))
            {
                sw.WriteLine(msg);
            }
        }
    }
}
