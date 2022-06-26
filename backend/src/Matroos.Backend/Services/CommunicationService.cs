using System.Text;

using Matroos.Backend.Services.Interfaces;
using Matroos.Resources.Classes.Bots;
using Matroos.Resources.Classes.Workers;

using Newtonsoft.Json;

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
            string json = JsonConvert.SerializeObject(payload);
            StringContent stringContent = new(json, Encoding.UTF8, "application/json");
            hrm.Content = stringContent;
        }

        return await client.SendAsync(hrm);
    }

    /// <inheritdoc />
    public virtual async Task<Worker> GetWorkerStatus(string remoteURL)
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
    public virtual async Task AddBotToWorker(Worker worker, Bot bot)
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
    public virtual async Task UpdateBotInWorker(Worker worker, Bot bot)
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
    public virtual async Task DeleteBotFromWorker(Worker worker, Guid botId)
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
    public virtual async Task StartBotInWorker(Worker worker, Guid botId)
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
    public virtual async Task StopBotInWorker(Worker worker, Guid botId)
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
