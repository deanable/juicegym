using JuiceGym.Core.Interfaces;
using JuiceGym.Core.Models;
using JuiceGym.Data;
using JuiceGym.Training.Services;
using System.IO;

namespace JuiceGym.UI
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                // Parse arguments
                string? path = null;
                int epochs = 10;
                for (int i=0; i < args.Length; i++)
                {
                    if (args[i] == "--path" && i+1 < args.Length)
                        path = args[i+1];
                    if (args[i] == "--epochs" && i+1 < args.Length)
                        epochs = int.Parse(args[i+1]);
                }

                if (path == null)
                {
                    Console.WriteLine("Usage: dotnet run --path <dir> [--epochs <num>]");
                    return;
                }

                // Validate directory
                if (!Directory.Exists(path))
                    throw new DirectoryNotFoundException($"Invalid path: {path}");

                // Load data
                Console.WriteLine($"Loading images from {path}");
                var dataLoader = new DataLoader();
                var data = await dataLoader.LoadImagesAsync(path);

                // Train model
                var trainer = new Trainer();
                var config = new TrainingConfig(epochs, 32);
                Console.WriteLine("Starting training...");
                var modelPath = await trainer.TrainModelAsync(data, config);

                Console.WriteLine($"Model saved to: {modelPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
