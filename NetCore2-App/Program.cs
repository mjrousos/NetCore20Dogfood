using System;
using System.Linq;
using NetStandard2_Libray;

namespace NetCore2_App
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine("Serializing DateTime.Now with the binary formatter...");
            var bytes = Serializer.Serialize(DateTime.Now);
            Console.WriteLine($"Bytes: {string.Join(" ", bytes.Select(b => b.ToString("X2")))}");
        }
    }
}
