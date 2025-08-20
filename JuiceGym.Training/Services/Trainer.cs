using JuiceGym.Core.Interfaces;
using JuiceGym.Core.Models;
using JuiceGym.Training.Models;
using Microsoft.ML;
using System.IO;
using System.Threading.Tasks;

namespace JuiceGym.Training.Services;

public class Trainer : ITrainer
{
    public async Task<string> TrainModelAsync(
        IEnumerable<ImageData> data,
        TrainingConfig config)
    {
        var dataView = ImageClassificationPipeline.PrepareData(data);
        var pipeline = ImageClassificationPipeline.BuildTrainingPipeline(
            "Resources/resnet50.onnx");

        /// <summary>
        /// Trains ML model using transfer learning
        /// </summary>
        // Execute training in background thread
        var model = await Task.Run(() => pipeline.Fit(dataView));
        var outputPath = "output/model.onnx";

        // Save as ONNX
        Directory.CreateDirectory("output");
        ImageClassificationPipeline.Context.Model.Save(
            model, 
            dataView.Schema, 
            outputPath);

        return outputPath;
    }
}
