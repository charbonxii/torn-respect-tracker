using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ApiCalls
{
    public class Product
    {
        public JObject members { get; set; }
        public string respect { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
    }
    class Program
    {
        static HttpClient client = new HttpClient();

        static void Main(string[] args)
        {
            RunAsync().Wait();


        }

        static async Task RunAsync()
        {
            String apiKey = "";
            String factionKey = "";
            client.BaseAddress = new Uri("https://api.torn.com/faction/{factionKey}?selections=basic&key={apiKey}");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                Product product = new Product();
                Uri url = new Uri("https://api.torn.com/faction/{{factionKey}?selections=basic&key={apiKey}");
                product = await GetProductAsync(url.PathAndQuery);
                string lines = product.respect;
                DateTime date1 = DateTime.Now;
                System.IO.File.AppendAllText(@"c:\\tornRespect.txt", "\r\n" + date1.ToString() + "," + lines);
                Console.WriteLine(lines);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        static async Task<Product> GetProductAsync(string path)
        {
            Product product = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                product = await response.Content.ReadAsAsync<Product>();
            }
            return product;
        }

    }
}