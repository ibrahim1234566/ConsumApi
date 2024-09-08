namespace ConsumApi
{
using System.Text;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Text.Json;
    internal class Program
    {
        private static readonly HttpClient client = new HttpClient();

        static async Task Main(string[] args)
        {
            // 1. API URL
            string apiUrl = "https://jsonplaceholder.typicode.com/todos/1";
            string filePath = "todoData.json"; // File path for saving the serialized data

            // 2. Initialize HttpClient for API calls
            using (HttpClient client = new HttpClient())
            {
                // 3. Fetch the data from the API
                HttpResponseMessage response = await client.GetAsync(apiUrl);
                string apiResponse = await response.Content.ReadAsStringAsync();

                // 4. Deserialize the JSON response into a C# object with case-insensitive option
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                 Data Data = JsonSerializer.Deserialize<Data>(apiResponse, options);

                // 5. Output the deserialized object to the console
                Console.WriteLine("Deserialized Object:");
                Console.WriteLine($"UserId: {Data.UserId}");
                Console.WriteLine($"Id: {Data.Id}");
                Console.WriteLine($"Title: {Data.Title}");
                Console.WriteLine($"Completed: {Data.Completed}");

                // 6. Serialize the object back into JSON
                string jsonString = JsonSerializer.Serialize(Data, new JsonSerializerOptions { WriteIndented = true });

                // 7. Write the serialized JSON to a file
                File.WriteAllText(filePath, jsonString);

                Console.WriteLine($"\nSerialized JSON data written to {filePath}");
            }
        }
    }
}
