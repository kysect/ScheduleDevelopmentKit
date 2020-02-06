using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScheduleAggregator
{
    static class Program
    {
        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            ScheduleItemProvider provider = new ScheduleItemProvider(GetTmpGroups().ToList());
            
            Console.WriteLine("\t\tLectures");
            provider.PrintLecture();
            
            Console.WriteLine("\n\n\t\tPractice");
            provider.PrintPractice();
        }

        private static IEnumerable<String> GetTmpGroups()
        {
            yield return "M3101";

        }
    }
}
