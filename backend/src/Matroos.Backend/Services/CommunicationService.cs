using System.Text;

using Newtonsoft.Json;

using Matroos.Resources.Classes.Bots;
using Matroos.Resources.Classes.Workers;
using Matroos.Backend.Services.Interfaces;

namespace Matroos.Backend.Services;

public class CommunicationService : ICommunicationService
{
    /// <summary>
    /// Perform a Http request.
    /// </summary>
    /// <param name="method">The request method.</param>
    /// <param name="uri">The request URI.</param>
    /// <param name="payload">The payload.</param>
    /// <returns>A task containing the <see cref="HttpResponseMessage"/>.</returns>
    private static async Task<HttpResponseMessage> Request(HttpMethod method, string uri, object? payload = null)
    {
        using HttpClient client = new();
        HttpRequestMessage hrm = new(method, new Uri(uri));

        if (payload != null)
        {
            string json = JsonConvert.SerializeObject(payload);
            StringContent stringContent = new(json, Encoding.UTF8, "application/json");
            hrm.Content = stringContent;
        }

        return await client.SendAsync(hrm);
    }

    /// <inheritdoc />
    public async Task<Worker> GetWorkerStatus(string remoteURL)
    {
        try
        {
            HttpResponseMessage response = await Request(HttpMethod.Get, remoteURL);

            if (response.IsSuccessStatusCode)
            {
                string output = await response.Content.ReadAsStringAsync();
                Worker? worker = JsonConvert.DeserializeObject<Worker>(output ?? "");

                return worker ?? new();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return new();
        }

        return new();
    }

    /// <inheritdoc />
    public async Task AddBotToWorker(Worker worker, Bot bot)
    {
        try
        {
            HttpResponseMessage response = await Request(HttpMethod.Post, worker.RemoteUrl, bot);

            if (response.IsSuccessStatusCode)
            {
                // To be handled.
                Console.WriteLine("AddBotToWorker");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    /// <inheritdoc />
    public async Task UpdateBotInWorker(Worker worker, Bot bot)
    {
        try
        {
            HttpResponseMessage response = await Request(HttpMethod.Put, worker.RemoteUrl, bot);

            if (response.IsSuccessStatusCode)
            {
                // To be handled.
                Console.WriteLine("UpdateBotInWorker");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    /// <inheritdoc />
    public async Task DeleteBotFromWorker(Worker worker, Guid botId)
    {
        try
        {
            HttpResponseMessage response = await Request(HttpMethod.Delete, worker.RemoteUrl + botId);

            if (response.IsSuccessStatusCode)
            {
                // To be handled.
                Console.WriteLine("DeleteBotFromWorker");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    /// <inheritdoc />
    public async Task StartBotInWorker(Worker worker, Guid botId)
    {
        try
        {
            HttpResponseMessage response = await Request(HttpMethod.Get, $"{worker.RemoteUrl}start/{botId}");

            if (response.IsSuccessStatusCode)
            {
                // To be handled.
                Console.WriteLine("StartBotInWorker");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    /// <inheritdoc />
    public async Task StopBotInWorker(Worker worker, Guid botId)
    {
        try
        {
            HttpResponseMessage response = await Request(HttpMethod.Get, $"{worker.RemoteUrl}stop/{botId}");

            if (response.IsSuccessStatusCode)
            {
                // To be handled.
                Console.WriteLine("StartBotInWorker");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}
