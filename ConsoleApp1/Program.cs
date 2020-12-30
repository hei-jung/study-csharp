using System;

namespace ConsoleApp1
{
    class Program
    {
        static void temp(string name)
        {
            Console.WriteLine("name: {0}", name);
        }

        static void Main(string[] args)
        {
            int sum = 0;

            Action act0 = () => Console.WriteLine("name: act0");

        }
    }
}
