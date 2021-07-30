using System;
using System.Net;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace VanityMonKeyGenerator
{
    public static class Requests
    {
        private const int MaxRequestCount = 32;

        public static Result SearchMonKeys(CancellationToken cancellationToken, List<string> requestedAccessories,
            int monKeyAmount, Action<Progress> reportFunction)
        {
            ServicePointManager.DefaultConnectionLimit = MaxRequestCount * 2;
            HttpClient client = new HttpClient();
            List<Task<List<MonKey>>> tasks = new List<Task<List<MonKey>>>();
            ulong expectation = Accessories.GetMonKeyRarity(requestedAccessories);
            ulong iterations = 0;

            for (int i = 0; i < MaxRequestCount; i++)
            {
                tasks.Add(GetMonKeysAsync(client, monKeyAmount));
            }

            while (!cancellationToken.IsCancellationRequested)
            {
                Task.WaitAny(tasks.ToArray());

                for (int i = 0; i < tasks.Count; i++)
                {
                    try
                    {
                        if (tasks[i].IsCompleted)
                        {
                            ulong localIteration = 0;
                            foreach (MonKey monKey in tasks[i].Result)
                            {
                                if (Accessories.AccessoriesMatching(requestedAccessories, monKey.Accessories))
                                {
                                    return new Result(monKey, iterations + localIteration);
                                }
                                localIteration++;
                            }
                            tasks.RemoveAt(i);
                            tasks.Add(GetMonKeysAsync(client, monKeyAmount));
                            iterations += (ulong)monKeyAmount;
                            reportFunction(new Progress(expectation, iterations));
                        }
                        else
                        {
                            continue;
                        }
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
            }
            return null;
        }

        private static async Task<List<MonKey>> GetMonKeysAsync(HttpClient client, int amount)
        {
            Dictionary<string, MonKey> monKeyDictionary = new Dictionary<string, MonKey>();

            for (int i = 0; i < amount; i++)
            {
                MonKey monKey = new MonKey();
                monKeyDictionary.Add(monKey.Address, monKey);
            }
            var content = new StringContent("{\"addresses\":" + JsonSerializer.Serialize(monKeyDictionary.Keys) + "}");
            var response = await client.PostAsync("http://monkey.banano.cc/api/v1/monkey/dtl", content);
            var results = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(await response.Content.ReadAsStringAsync());
            foreach (var result in results)
            {
                monKeyDictionary[result.Key].Accessories = Accessories.ObtainedAccessories(result.Value);
            }

            return monKeyDictionary.Values.ToList();
        }
    }
    public class Progress
    {
        public ulong Expectation;
        public ulong Iterations;

        public Progress(ulong expectation, ulong iterations)
        {
            Expectation = expectation;
            Iterations = iterations;
        }
    }

    public class Result
    {
        public MonKey MonKey;
        public ulong Iterations;

        public Result(MonKey monKey, ulong iterations)
        {
            MonKey = monKey;
            Iterations = iterations;
        }
    }
}
