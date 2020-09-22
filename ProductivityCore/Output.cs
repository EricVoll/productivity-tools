using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityCore
{
    public static class Output
    {
        public static void SetLogDelegate(Action<string> log)
        {
            Log = log;
        }

        static Action<string> Log { get; set; } = Console.WriteLine;
    
        /// <summary>
        /// Writes the output to the specified delegate
        /// </summary>
        /// <param name="text"></param>
        public static void Write(string text)
        {
            Log(text);
        }
    }
}
