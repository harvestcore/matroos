using System.Text;

using Newtonsoft.Json;

using Matroos.Resources.Classes.Bots;
using Matroos.Resources.Classes.Workers;
using Matroos.Backend.Services.Interfaces;

namespace Matroos.Backend.Services;

public class CommunicationService : ICommunicationService
{
    /// <inheritdoc />
    public async Task<Worker> GetWorkerStatus(string remoteURL)
    {
        HttpClient client = new();

        try
        {
            HttpResponseMessage response = await client.GetAsync(new Uri(remoteURL));

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
        finally
        {
            client.Dispose();
        }

        return new();
    }

    /// <inheritdoc />
    public async Task AddBotToWorker(Worker worker, Bot bot)
    {
        HttpClient client = new();

        string json = JsonConvert.SerializeObject(bot);
        StringContent payload = new(json, Encoding.UTF8, "application/json");

        try
        {
            HttpResponseMessage response = await client.PostAsync(new Uri(worker.RemoteUrl), payload);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("AddBotToWorker");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
        finally
        {
            client.Dispose();
        }
    }

    /// <inheritdoc />
    public async Task UpdateBotInWorker(Worker worker, Bot bot)
    {
        HttpClient client = new();

        string json = JsonConvert.SerializeObject(bot);
        StringContent payload = new(json, Encoding.UTF8, "application/json");

        try
        {
            HttpResponseMessage response = await client.PutAsync(new Uri(worker.RemoteUrl), payload);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("UpdateBotInWorker");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
        finally
        {
            client.Dispose();
        }
    }

    /// <inheritdoc />
    public async Task DeleteBotFromWorker(Worker worker, Guid botId)
    {
        HttpClient client = new();

        try
        {
            HttpResponseMessage response = await client.DeleteAsync(new Uri(worker.RemoteUrl + botId));

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("DeleteBotFromWorker");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
        finally
        {
            client.Dispose();
        }
    }

    /// <inheritdoc />
    public async Task StartBotInWorker(Worker worker, Guid botId)
    {
        HttpClient client = new();

        try
        {
            HttpResponseMessage response = await client.GetAsync(new Uri($"{worker.RemoteUrl}start/{botId}"));

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("StartBotInWorker");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
        finally
        {
            client.Dispose();
        }
    }

    /// <inheritdoc />
    public async Task StopBotInWorker(Worker worker, Guid botId)
    {
        HttpClient client = new();

        try
        {
            HttpResponseMessage response = await client.GetAsync(new Uri($"{worker.RemoteUrl}stop/{botId}"));

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("StartBotInWorker");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
        finally
        {
            client.Dispose();
        }
    }
}
