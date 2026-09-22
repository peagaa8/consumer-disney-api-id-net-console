using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConsumerDisneyIdApi
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string apiUrl = "https://api.disneyapi.dev/character/423";

            using HttpClient client = new HttpClient();

            try
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();

                string jsonResponse = await response.Content.ReadAsStringAsync();

                using JsonDocument doc = JsonDocument.Parse(jsonResponse);
                JsonElement root = doc.RootElement;

                if (root.TryGetProperty("data", out JsonElement data))
                {
                    string nome = data.GetProperty("name").GetString();
                    string imagem = data.GetProperty("imageUrl").GetString();

                    Console.WriteLine("Nome:");
                    Console.WriteLine(nome);
                    Console.WriteLine("Imagem:");
                    Console.WriteLine(imagem);
                }
                else
                {
                    Console.WriteLine("Os dados da personagem não foram encontrados.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ocorreu um erro ao consumir a API: {e.Message}");
            }
        }
    }
}