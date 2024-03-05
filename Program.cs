
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lab3
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var url = "https://geo.ipify.org/api/v2/country?apiKey=at_5R4aK50rnbtA1v8Xpy9u5poblcctx&ipAddress=8.8.8.8";
            var httpClient = new NeiHttpClient(url);
            var getResult = await httpClient.GetAsync();
            var parameters = new Dictionary<string, string>
            {
            { "ip", "8.8.8.8" }
            };
            var postResult = await httpClient.PostAsync(parameters);
            Console.WriteLine("GET Result:");
            Console.WriteLine($"Message: {getResult.Message}");
            Console.WriteLine($"Status Code: {getResult.StatusCode}");
            Console.WriteLine($"Data: {string.Join(", ", getResult.Information)}");

            Console.WriteLine();

            Console.WriteLine("POST Result:");
            Console.WriteLine($"Message: {postResult.Message}");
            Console.WriteLine($"Status Code: {postResult.StatusCode}");
            Console.WriteLine($"Data: {string.Join(", ", postResult.Information)}");
        }
    }
}
