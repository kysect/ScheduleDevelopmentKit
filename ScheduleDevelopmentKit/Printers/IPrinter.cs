using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Kysect.ItmoScheduleSdk.Models;
using ScheduleDevelopmentKit.Core.ScheduleItemProviders;

namespace ScheduleDevelopmentKit.Printers
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

    public static class PrinterExtensions
    {
        public static void Print(this IPrinter printer, IEnumerable<ScheduleItemModel> items)
        {
            var scheduleItems = items
                .Select(e => (e.DataDay, e.DataWeek, e.Group, e.StartTime, e.SubjectTitle, e.Teacher))
                .GroupBy(e => (e.DataDay, e.DataWeek))
                .ToList();

            foreach (var tuple in scheduleItems)
            {
                printer.Print($"\t\t{tuple.Key}");
                foreach (var valueTuple in tuple)
                {
                    printer.Print(valueTuple.ToString());
                }
            }
        }

        public static void PrintLecture(this IPrinter printer, IScheduleItemProvider provider)
        {
            Print(printer, provider.GetItems().Where(e => e.IsLecture()));
        }

        public static void PrintPractice(this IPrinter printer, IScheduleItemProvider provider)
        {
            Print(printer, provider.GetItems().Where(e => !e.IsLecture()));
        }
    }
}