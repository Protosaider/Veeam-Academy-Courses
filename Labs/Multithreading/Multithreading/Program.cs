using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Multithreading
{
    public static class CustomTimeouts
    {
        public static TimeSpan ShortTimePeriod = TimeSpan.FromSeconds(1);
        public static TimeSpan MiddleTimePeriod = TimeSpan.FromSeconds(5);
        public static TimeSpan InfiniteTimePeriod = new TimeSpan(0, 0, 0, 0, Timeout.Infinite);
    }

    class Program
    {
        static void Main(string[] args)
        {
            using (CountDownEventExample example = new CountDownEventExample())
            {
                example.Run();
            }
        }
    }
}
