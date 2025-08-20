using JuiceGym.Core.Interfaces;
using JuiceGym.Core.Models;
using JuiceGym.Training.Models;
using Microsoft.ML;
using System.IO;
using System.Threading.Tasks;
using System.Diagnostics;

namespace JuiceGym.Training.Services;

public class Trainer : ITrainer
{
    public async Task<string> TrainModelAsync(
        IEnumerable<ImageData> data,
        TrainingConfig config)
    {
        try
        {
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Starting model training...");
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Data items: {data.Count()}");
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Training configuration: {config.Epochs} epochs, batch size {config.BatchSize}");

            var stopwatch = Stopwatch.StartNew();

            var dataView = ImageClassificationPipeline.PrepareData(data);
            var pipeline = ImageClassificationPipeline.BuildTrainingPipeline(
                "Resources/resnet50.onnx");

            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Pipeline created. Starting training...");

            /// <summary>
            /// Trains ML model using transfer learning
            /// </summary>
            // Execute training in background thread
            var model = await Task.Run(() => pipeline.Fit(dataView));

            var outputPath = "output/model.onnx";
            Directory.CreateDirectory("output");

            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Training completed. Saving model...");

            ImageClassificationPipeline.Context.Model.Save(
                model,
                dataView.Schema,
                outputPath);

            stopwatch.Stop();
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Model saved to: {outputPath}");
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Training completed in {stopwatch.Elapsed.TotalSeconds:F2} seconds");

            return outputPath;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Error during training: {ex.Message}");
            throw new InvalidOperationException("Model training failed", ex);
        }
    }
}
