using System;
using System.Linq;

namespace NetCore2_App
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Serializing DateTime.Now with a NetStandard 2.0 library...");
            var bytes = NetStandard2_Libray.Serializer.Serialize(DateTime.Now);
            Console.WriteLine($"Bytes: {string.Join(" ", bytes.Select(b => b.ToString("X2")))}");
            Console.WriteLine();

            Console.WriteLine("Serializing DateTime.Now with a NetFX 4.5 library...");
        //    bytes = Serializer.Serialize(DateTime.Now);
            Console.WriteLine($"Bytes: {string.Join(" ", bytes.Select(b => b.ToString("X2")))}");
            Console.WriteLine();

            Console.WriteLine();
            Console.WriteLine("- Done -");
        }
    }
}
