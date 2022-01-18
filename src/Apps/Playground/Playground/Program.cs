using System;

namespace Playground
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var helloWorld = new Action(() => Console.WriteLine("Hello "));
            helloWorld += () => Console.WriteLine("World ");
            helloWorld += () => Console.WriteLine("& .Net");

            helloWorld();
        }
    }
}