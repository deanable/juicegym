using JuiceGym.Core.Interfaces;
using JuiceGym.Core.Models;
using JuiceGym.Core.Services;

namespace JuiceGym.Tests;

public class UnitTest1
{
    [Fact]
    public void ConfigurationService_ShouldReturnDefaultConfig()
    {
        // Arrange
        var configService = new ConfigurationService();

        // Act
        var config = configService.GetConfiguration();

        // Assert
        Assert.NotNull(config);
        Assert.Equal("output", config.OutputDirectory);
        Assert.Equal("resnet50.onnx", config.OnnxModelFile);
        Assert.Equal(10, config.DefaultTrainingConfig.Epochs);
        Assert.Equal(32, config.DefaultTrainingConfig.BatchSize);
    }

    [Fact]
    public void ConfigurationService_ShouldUpdateConfig()
    {
        // Arrange
        var configService = new ConfigurationService();
        var newConfig = new AppConfig
        {
            OutputDirectory = "custom_output",
            OnnxModelFile = "custom_model.onnx",
            DefaultTrainingConfig = new TrainingConfig(20, 64)
        };

        // Act
        configService.UpdateConfiguration(newConfig);
        var retrievedConfig = configService.GetConfiguration();

        // Assert
        Assert.Equal("custom_output", retrievedConfig.OutputDirectory);
        Assert.Equal("custom_model.onnx", retrievedConfig.OnnxModelFile);
        Assert.Equal(20, retrievedConfig.DefaultTrainingConfig.Epochs);
        Assert.Equal(64, retrievedConfig.DefaultTrainingConfig.BatchSize);
    }

    [Fact]
    public void ServiceContainer_ShouldResolveConfigurationService()
    {
        // Arrange
        var container = ServiceContainer.CreateDefault();

        // Act
        var configService = container.GetService<IConfigurationService>();

        // Assert
        Assert.NotNull(configService);
        Assert.IsType<ConfigurationService>(configService);
    }

    [Fact]
    public void TrainingConfig_ShouldCreateValidConfig()
    {
        // Arrange & Act
        var config = new TrainingConfig(100, 128);

        // Assert
        Assert.Equal(100, config.Epochs);
        Assert.Equal(128, config.BatchSize);
    }

    [Fact]
    public void ConfigurationService_ShouldHandleNullConfig()
    {
        // Arrange
        var configService = new ConfigurationService();

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => configService.UpdateConfiguration(null!));
    }
}