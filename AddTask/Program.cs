using System;
using System.Threading;

namespace AddTask
{
    /*
    Використовуючи конструкції блокування, змініть останній приклад уроку таким чином, 
    щоб отримати можливість почергової роботи 3-х потоків.
     */
    class Program
    {
        static private int counter = 0;

        static private object block = new object();

        static private void Function1()
        {
            lock (block)
            {
                for (int i = 0; i < 50; ++i)
                {
                    Monitor.Wait(block, Timeout.Infinite);
                    Console.Write("1 ");
                    Console.WriteLine("{0} из потока {1}", ++counter, Thread.CurrentThread.GetHashCode());
                    Monitor.Pulse(block);
                }
            }
        }

        static private void Function2()
        {
            lock (block)
            {
                for (int i = 0; i < 50; ++i)
                {
                    Monitor.Wait(block, Timeout.Infinite);
                    Console.Write("2 ");
                    Console.WriteLine("{0} из потока {1}", ++counter, Thread.CurrentThread.GetHashCode());
                    Monitor.Pulse(block);
                }
            }
        }

        static private void Function3()
        {
            lock (block)
            {
                for (int i = 0; i < 50; ++i)
                {
                    Monitor.Pulse(block);
                    Monitor.Wait(block, Timeout.Infinite);
                    Console.Write("3 ");
                    Console.WriteLine("{0} из потока {1}", ++counter, Thread.CurrentThread.GetHashCode());
                }
            }
        }

        static void Main()
        {
            Thread[] threads = { new Thread(Function1), new Thread(Function2), new Thread(Function3) };

            for (int i = 0; i < threads.Length; i++)
            {
                threads[i].Start();
            }

            Console.ReadKey();
        }
    }
}

