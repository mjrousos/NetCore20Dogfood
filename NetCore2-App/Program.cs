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
            Console.WriteLine();

            Console.WriteLine("Testing use of a NetFX package with NetCore 2.0");
            Console.WriteLine("Using RestSharp to send an HTTP request to Microsoft.com/net/core");
            var client = new RestSharp.RestClient();
            client.BaseUrl = new Uri("http://www.microsoft.com");
            var request = new RestSharp.RestRequest();
            request.Resource = "net/core";
            var response = client.Execute(request);
            Console.WriteLine($"Received a {response.StatusDescription} response with {response.ContentLength} bytes of content.");

            Console.WriteLine();
            Console.WriteLine("- Done -");
        }
    }
}
