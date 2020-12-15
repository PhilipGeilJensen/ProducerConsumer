using System;
using System.Collections.Generic;
using System.Threading;

namespace ProducerConsumer
{
    class Program
    {
        static Queue<int> products = new Queue<int>();
        static void Main(string[] args)
        {
            Thread p = new Thread(Producer);
            Thread c = new Thread(Consumer);
            p.Start();
            c.Start();
        }

        static void Producer()
        {
            while (true)
            {
                Thread.Sleep(1000);
                lock (products)
                {
                    while (products.Count == 3)
                    {
                        Console.WriteLine("Producer waits...");
                        Monitor.Wait(products);
                    }

                    products.Enqueue(0);
                    Console.WriteLine("Producer added a product");
                    Monitor.PulseAll(products);
                }
            }
        }

        static void Consumer()
        {
            while (true)
            {
                Thread.Sleep(5000);
                lock (products)
                {
                    while (products.Count == 0)
                    {
                        Console.WriteLine("Consumer waits...");
                        Monitor.Wait(products);
                    }

                    products.Dequeue();
                    Console.WriteLine("Consumer took a product");
                    Monitor.PulseAll(products);
                }
            }
        }
    }
}
