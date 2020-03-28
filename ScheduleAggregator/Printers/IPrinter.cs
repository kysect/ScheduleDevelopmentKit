using System;
using System.IO;

namespace ScheduleAggregator.Printers
{
    public interface IPrinter
    {
        void Print(string message);
    }

    public class ConsolePrinter : IPrinter
    {
        public void Print(string message)
        {
            Console.WriteLine(message);
        }
    }

    public class FilePrinter : IPrinter
    {
        private readonly string _filePath;

        public FilePrinter(string filePath)
        {
            _filePath = filePath;
        }

        public void Print(string message)
        {
            File.AppendAllText(_filePath, $"{message}\n");
        }
    }
}