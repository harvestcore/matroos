using System.Text;
using System.Text.Json;

using Matroos.Backend.Services.Interfaces;
using Matroos.Resources.Classes.Bots;
using Matroos.Resources.Classes.Workers;

namespace Matroos.Backend.Services;

public class CommunicationService : ICommunicationService
{
    /// <inheritdoc />
    public virtual async Task<HttpResponseMessage> Request(HttpMethod method, string uri, object? payload = null)
    {
        HttpClient client = new();
        HttpRequestMessage hrm = new(method, new Uri(uri));

        if (payload != null)
        {
            string json = JsonSerializer.Serialize(payload);
            StringContent stringContent = new(json, Encoding.UTF8, "application/json");
            hrm.Content = stringContent;
        }

        return await client.SendAsync(hrm);
    }

    /// <inheritdoc />
    public virtual async Task<Worker?> GetWorkerStatus(string remoteURL)
    {
        try
        {
            HttpResponseMessage response = await Request(HttpMethod.Get, remoteURL);

            if (response.IsSuccessStatusCode)
            {
                string output = await response.Content.ReadAsStringAsync();
                Worker? worker = JsonSerializer.Deserialize<Worker>(
                    output ?? "",
                    new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }
                );

                if (worker == null || !worker.RemoteUrl.Equals("update"))
                {
                    return null;
                }

                return worker;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return null;
        }

        return null;
    }

    /// <inheritdoc />
    public virtual async Task AddBotToWorker(Worker worker, Bot bot)
    {
        try
        {
            HttpResponseMessage response = await Request(HttpMethod.Post, worker.RemoteUrl, bot);

            if (response.IsSuccessStatusCode)
            {
                // To be handled.
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    /// <inheritdoc />
    public virtual async Task UpdateBotInWorker(Worker worker, Bot bot)
    {
        try
        {
            HttpResponseMessage response = await Request(HttpMethod.Put, worker.RemoteUrl, bot);

            if (response.IsSuccessStatusCode)
            {
                // To be handled.
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    /// <inheritdoc />
    public virtual async Task DeleteBotFromWorker(Worker worker, Guid botId)
    {
        try
        {
            HttpResponseMessage response = await Request(HttpMethod.Delete, worker.RemoteUrl + botId);

            if (response.IsSuccessStatusCode)
            {
                // To be handled.
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    /// <inheritdoc />
    public virtual async Task StartBotInWorker(Worker worker, Guid botId)
    {
        try
        {
            HttpResponseMessage response = await Request(HttpMethod.Get, $"{worker.RemoteUrl}start/{botId}");

            if (response.IsSuccessStatusCode)
            {
                // To be handled.
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    /// <inheritdoc />
    public virtual async Task StopBotInWorker(Worker worker, Guid botId)
    {
        try
        {
            HttpResponseMessage response = await Request(HttpMethod.Get, $"{worker.RemoteUrl}stop/{botId}");

            if (response.IsSuccessStatusCode)
            {
                // To be handled.
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}
