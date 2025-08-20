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
