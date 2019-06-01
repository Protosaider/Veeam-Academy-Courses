using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Multithreading
{
    internal sealed class CountDownEventExample : IDisposable
    {
        private const Int32 TestThreadNumber = 5;
        private readonly CMyCountdownEvent _countDownEvent = new CMyCountdownEvent(TestThreadNumber);

        public void Dispose()
        {
            _countDownEvent.Dispose();
        }

        public void ThreadRoutine(Int32 threadNumber)
        {
            Console.WriteLine("Thread#{0} started", threadNumber);

            Thread.Sleep(TimeSpan.FromSeconds(threadNumber));
            _countDownEvent.Signal();
            Console.WriteLine("Thread#{0} signal event", threadNumber);

            Console.WriteLine("Thread#{0} ended", threadNumber);
        }

        public void ThreadRoutineWait(Int32 threadNumber)
        {
            Console.WriteLine("Thread#{0}Wait started", threadNumber);

            Thread.Sleep(TimeSpan.FromSeconds(threadNumber));
            var res = _countDownEvent.Wait(TimeSpan.FromSeconds(threadNumber + 3));
            Console.WriteLine("Thread#{0}Wait after wait + {1}", threadNumber, res);

            Console.WriteLine("Thread#{0}Wait ended", threadNumber);
        }

        public void Run()
        {
            Console.WriteLine("CountDownEvent example");

            try
            {
                Thread[] threads = new Thread[TestThreadNumber + 2];

                threads[TestThreadNumber] = new Thread(() => ThreadRoutineWait(0));
                threads[TestThreadNumber + 1] = new Thread(() => ThreadRoutineWait(1));
                threads[TestThreadNumber].Start();
                threads[TestThreadNumber + 1].Start();

                for (Int32 i = 0; i < TestThreadNumber; ++i)
                {
                    Int32 copy = i;
                    threads[i] = new Thread(() => ThreadRoutine(copy));
                    threads[i].Start();
                }

                threads[TestThreadNumber].Join();
                threads[TestThreadNumber + 1].Join();

                Console.WriteLine("Main Thread: waiting for event");
                var res = _countDownEvent.Wait(TimeSpan.FromSeconds(6));
                Console.WriteLine("Main Thread: countdownEvent signaled + " + res);

                for (Int32 i = 0; i < TestThreadNumber + 2; ++i)
                {
                    threads[i].Join();
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }

            Console.WriteLine();
            Console.WriteLine("DONE");
            Console.ReadLine();
        }
    }
}
