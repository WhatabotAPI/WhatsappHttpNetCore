using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        var client = new WhatabotHTTPClient();
        await client.SendMessage("Hello from .NET Core!");
    }
}

class WhatabotHTTPClient
{
    private readonly string apiKey = "YOUR_API_KEY";
    private readonly string phone = "YOUR_PHONE_NUMBER";
    private readonly string url = "https://api.whatabot.io/Whatsapp/RequestSendMessage";
    private readonly HttpClient client = new HttpClient();

    public async Task SendMessage(string text)
    {
        var data = new {
            ApiKey = apiKey,
            Text = text,
            Phone = phone
        };

        var content = new StringContent(JsonSerializer.Serialize(data), System.Text.Encoding.UTF8, "application/json");

        try
        {
            var response = await client.PostAsync(url, content);
            response.EnsureSuccessStatusCode();
            Console.WriteLine("Message sent successfully");
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Failed to send message: {ex.Message}");
        }
    }
}