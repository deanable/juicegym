using JuiceGym.Core.Models;

namespace JuiceGym.Core.Interfaces;

/// <summary>
/// Interface for training ML models
/// </summary>
public interface ITrainer
{
    /// <summary>
    /// Trains image classification model using transfer learning
    /// </summary>
    /// <param name="data">Training data</param>
    /// <param name="config">Training configuration</param>
    /// <returns>Path to exported ONNX model</returns>
    Task<string> TrainModelAsync(
        IEnumerable<ImageData> data,
        TrainingConfig config
    );
}
