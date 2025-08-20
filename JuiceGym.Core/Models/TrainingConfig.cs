using System.ComponentModel.DataAnnotations;

namespace JuiceGym.Core.Models;

/// <summary>
/// Configuration parameters for ML model training
/// </summary>
public record struct TrainingConfig(
    [Range(1, 1000)]
    int Epochs,

    [Range(1, 512)]
    int BatchSize
);

/// <summary>
/// Application configuration settings
/// </summary>
public class AppConfig
{
    public string DefaultImageDirectory { get; set; } = string.Empty;
    public string OutputDirectory { get; set; } = "output";
    public string ModelDirectory { get; set; } = "Resources";
    public string OnnxModelFile { get; set; } = "resnet50.onnx";
    public TrainingConfig DefaultTrainingConfig { get; set; } = new(10, 32);
    public bool EnableLogging { get; set; } = true;
    public int MaxConcurrency { get; set; } = Environment.ProcessorCount;
}
