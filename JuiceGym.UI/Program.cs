using JuiceGym.Core.Interfaces;
using JuiceGym.Core.Models;
using JuiceGym.Core.Services;
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
                // Initialize services
                var container = ServiceContainer.CreateDefault();
                var configService = container.GetService<IConfigurationService>();
                var appConfig = configService.GetConfiguration();

                // Parse arguments
                string? path = null;
                int epochs = appConfig.DefaultTrainingConfig.Epochs;
                int batchSize = appConfig.DefaultTrainingConfig.BatchSize;

                for (int i = 0; i < args.Length; i++)
                {
                    if (args[i] == "--path" && i + 1 < args.Length)
                        path = args[i + 1];
                    if (args[i] == "--epochs" && i + 1 < args.Length)
                        epochs = int.Parse(args[i + 1]);
                    if (args[i] == "--batch-size" && i + 1 < args.Length)
                        batchSize = int.Parse(args[i + 1]);
                }

                if (path == null)
                {
                    Console.WriteLine("Usage: dotnet run --path <dir> [--epochs <num>] [--batch-size <num>]");
                    Console.WriteLine($"Default configuration: {epochs} epochs, {batchSize} batch size");
                    return;
                }

                if (!Directory.Exists(path))
                    throw new DirectoryNotFoundException($"Invalid path: {path}");

                // Validate inputs
                if (epochs < 1 || epochs > 1000)
                    throw new ArgumentException("Epochs must be between 1 and 1000");
                if (batchSize < 1 || batchSize > 512)
                    throw new ArgumentException("Batch size must be between 1 and 512");

                // Create output directory
                Directory.CreateDirectory(appConfig.OutputDirectory);

                // Load data
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Loading images from {path}");
                var dataLoader = new DataLoader();
                var data = await dataLoader.LoadImagesAsync(path);

                if (!data.Any())
                {
                    Console.WriteLine("No images with valid EXIF data found.");
                    return;
                }

                // Train model
                var trainer = new Trainer();
                var config = new TrainingConfig(epochs, batchSize);
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Starting training...");
                var modelPath = await trainer.TrainModelAsync(data, config);

                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Training completed successfully!");
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Model saved to: {modelPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[{DateTime.Now:HH:mm:ss}] Error: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.Error.WriteLine($"[{DateTime.Now:HH:mm:ss}] Details: {ex.InnerException.Message}");
                }
            }
        }
    }
}
