using System;
using System.IO;
using System.Threading.Tasks;

using VanityMonKeyGenerator;
using static VanityMonKeyGenerator.Requests;

namespace CLI
{
    class Program
    {
        private const string gitBookUri = "vanitymonkeygenerator.gitbook.io";
        private const ulong reportInterval = 10000;
        private static ulong iterations = 0;
        static void Main()
        {
            Console.WriteLine("Welcome to the Vanity MonKey Generator!");
            Config config;
            string response;

            try
            {
                config = new Config();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Press \"Enter\" to exit.");
                Console.ReadLine();
                return;
            }

            if (!config.IsOpened)
            {
                do
                {
                    Console.WriteLine("Configuration file seems to be missing. Want to generate one? [y]/n");
                    response = Console.ReadLine().ToLower();
                    if (response == "y" ||  response == "")
                    {
                        Config.CreateSettingsFile();
                        Console.WriteLine("Configuration file generated. Please restart the application after" +
                            $" creating your MonKey.\nYou can visit {gitBookUri} for more information.");
                        Console.WriteLine("Press \"Enter\" to exit.");
                        Console.ReadLine();
                        return;
                    }
                    else if (response == "n")
                    {
                        Console.WriteLine("Press \"Enter\" to exit.");
                        Console.ReadLine();
                        return;
                    }
                } while (true);              
            }

            Console.WriteLine("Configuration loaded successfully.");
            do
            {
                Console.WriteLine("Start MonKey search? [y]/n");
                response = Console.ReadLine();
                if (response == "y" || response == "")
                {
                    break;
                }
                else if (response == "n")
                {
                    Console.WriteLine("Press \"Enter\" to exit.");
                    Console.ReadLine();
                    return;
                }
            } while (true);

            Console.WriteLine("Starting the MonKey search...");
            Result result = Task.Run(
                    () => SearchMonKeys(
                        new System.Threading.CancellationToken(),
                        config.Accessories,
                        config.RequestAmount,
                        delegate (Progress progress) { ReportProgress(progress); }
                    )
                ).Result;

            if (result != null)
            {
                Console.WriteLine($"Found MonKey after {result.Iterations:#,#} MonKeys.");
                Console.WriteLine($"The MonKey address is: {result.MonKey.Address}");
                Console.WriteLine($"The MonKey seed is: {result.MonKey.Seed}");
                if (config.LogData)
                {
                    StreamWriter resultsFile = new StreamWriter("results.txt");
                    resultsFile.WriteLine($"Address: {result.MonKey.Address}");
                    resultsFile.WriteLine($"Seed: {result.MonKey.Seed}");
                    resultsFile.Close();
                }
                Console.WriteLine("Press \"Enter\" to exit.");
                Console.ReadLine();
            }
        }
        
        static void ReportProgress(Progress progress)
        {
            iterations += progress.Iterations;
            if (iterations > reportInterval)
            {
                Console.WriteLine($"Searched {progress.Iterations - iterations} more MonKeys");
            }
        }
    }
}
