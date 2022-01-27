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

        public static async Task<string> GetMonKeySvg(this MonKey monKey, int size)
        {
            HttpClient client = new HttpClient();
            return await client.GetStringAsync($"https://monkey.banano.cc/api/v1/monkey/{monKey.Address}?size={size}");
        }

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
                            ulong localIteration = 1;
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
                    catch
                    {
                        return null;
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

        public static string GetEstimatedTime(ulong iterations, ulong expectation, double elapsedSeconds)
        {
            double expectedSeconds = elapsedSeconds / iterations * expectation;
            double remainingSeconds = expectedSeconds - elapsedSeconds;
            if (remainingSeconds < 0)
            {
                return "Any time now";
            }
            else if (remainingSeconds > 3600)
            {
                return $"{(int)remainingSeconds / 3600} hours";
            }
            else if (remainingSeconds > 60)
            {
                return $"{(int)remainingSeconds / 60} minutes";
            }
            else
            {
                return $"{(int)remainingSeconds} seconds";
            }
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
