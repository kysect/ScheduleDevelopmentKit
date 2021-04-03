using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ScheduleDevelopmentKit.Core.ScheduleItemProviders;
using ScheduleDevelopmentKit.Printers;

namespace ScheduleDevelopmentKit
{
    static class Program
    {
        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            string input = Console.ReadLine();

            string[] groups = string.IsNullOrEmpty(input)
                ? GetTmpGroups().ToArray()
                : File.ReadAllLines(input);

            Execute(new ConsolePrinter(), groups);
        }

        public static void Execute(IPrinter printer, IEnumerable<string> groups)
        {
            var provider = new ApiScheduleItemProvider(groups.ToList(), new List<int>());

            printer.Print("\t\tLectures");
            printer.PrintLecture(provider);

            printer.Print("\n\n\t\tPractice");
            printer.PrintPractice(provider);
        }

        private static IEnumerable<string> GetTmpGroups()
        {
            yield return "M3101";
            yield return "M3405";
        }
    }
}
