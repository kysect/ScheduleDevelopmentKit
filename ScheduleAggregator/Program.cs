using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ScheduleAggregator.Printers;

namespace ScheduleAggregator
{
    static class Program
    {
        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            string input = Console.ReadLine();

            if (string.IsNullOrEmpty(input))
            {
                List<String> groups = GetTmpGroups().ToList();
                Execute(new ConsolePrinter(), groups);
            }
            else
            {
                String[] groups = File.ReadAllLines(input);
                Execute(new ConsolePrinter(), groups);
            }
        }

        public static void Execute(IPrinter printer, IEnumerable<String> groups)
        {
            var provider = new ScheduleItemProvider(groups.ToList(), printer);

            printer.Print("\t\tLectures");
            provider.PrintLecture();

            printer.Print("\n\n\t\tPractice");
            provider.PrintPractice();
        }

        private static IEnumerable<String> GetTmpGroups()
        {
            yield return "M3101";
        }
    }
}
