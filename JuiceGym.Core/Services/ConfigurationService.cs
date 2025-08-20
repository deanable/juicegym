using JuiceGym.Core.Interfaces;
using JuiceGym.Core.Models;

namespace JuiceGym.Core.Services;

/// <summary>
/// Default implementation of configuration management
/// </summary>
public class ConfigurationService : IConfigurationService
{
    private AppConfig _config;

    public ConfigurationService()
    {
        _config = new AppConfig();
        LoadDefaults();
    }

    private void LoadDefaults()
    {
        _config = new AppConfig
        {
            DefaultImageDirectory = string.Empty,
            OutputDirectory = "output",
            ModelDirectory = "Resources",
            OnnxModelFile = "resnet50.onnx",
            DefaultTrainingConfig = new TrainingConfig(10, 32),
            EnableLogging = true,
            MaxConcurrency = Environment.ProcessorCount
        };
    }

    public AppConfig GetConfiguration() => _config;

    public void UpdateConfiguration(AppConfig config)
    {
        _config = config ?? throw new ArgumentNullException(nameof(config));
    }
}