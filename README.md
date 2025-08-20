# JuiceGym - Image Classification Training Application

JuiceGym is a .NET 8 application for training custom image classification models using transfer learning with ML.NET. The application loads images with EXIF metadata tags, processes them, and trains models that can be exported as ONNX files.

## Features

- 🖼️ **Image Loading**: Loads images from directories with support for JPEG and PNG formats
- 🏷️ **EXIF Metadata Processing**: Extracts tags and categories from image EXIF metadata
- 🤖 **Transfer Learning**: Uses ResNet50 ONNX model for transfer learning
- ⚡ **Parallel Processing**: Efficient parallel image processing with progress tracking
- 🎯 **Model Export**: Exports trained models as ONNX files for deployment
- 🖥️ **Multiple UIs**: Console application and WinForms GUI support
- ⚙️ **Configuration Management**: Flexible configuration system
- 🧪 **Unit Testing**: Comprehensive test coverage for core functionality

## Project Structure

```
JuiceGym.sln
├── JuiceGym.Core/           # Core models, interfaces, and services
├── JuiceGym.Data/           # Data loading and processing logic
├── JuiceGym.Training/       # ML training pipeline and services
├── JuiceGym.UI/             # Console user interface
├── JuiceGym.UI.WinForms/    # Windows Forms GUI
├── JuiceGym.Tests/          # Unit tests
└── README.md
```

## Prerequisites

- .NET 8.0 SDK
- Windows 10/11 (for WinForms UI)
- Images with EXIF metadata containing XPSubject tags

## Quick Start

### Console Application
```bash
# Navigate to the UI project
cd JuiceGym.UI

# Train a model with default settings
dotnet run -- --path "C:\path\to\images" --epochs 10

# Train with custom batch size
dotnet run -- --path "C:\path\to\images" --epochs 20 --batch-size 64
```

### WinForms Application
```bash
cd JuiceGym.UI.WinForms
dotnet run
```

## Configuration

The application uses a flexible configuration system. Default settings:
- **Epochs**: 10
- **Batch Size**: 32
- **Output Directory**: `output/`
- **Model File**: `resnet50.onnx`
- **Concurrency**: Based on processor count

## Testing

Run unit tests:
```bash
dotnet test
```

## Architecture

### Core Components
- **IDataLoader**: Interface for loading and processing image data
- **ITrainer**: Interface for training ML models
- **IConfigurationService**: Interface for configuration management
- **AppConfig**: Application configuration model
- **TrainingConfig**: ML training parameters

### Services
- **ConfigurationService**: Default configuration implementation
- **ServiceContainer**: Simple dependency injection container
- **DataLoader**: EXIF metadata extraction and image processing
- **Trainer**: ML.NET training pipeline implementation

## Recent Improvements

✅ **Phase 1 (Core Fixes)**
- Fixed missing ImageSharp dependencies
- Completed ONNX model loading in ML pipeline
- Standardized TrainingConfig usage
- Added progress tracking and logging

✅ **Phase 2 (Reliability & Quality)**
- Implemented configuration management system
- Added dependency injection container
- Created comprehensive unit tests
- Enhanced error handling and validation

## Building

```bash
# Build all projects
dotnet build

# Build specific project
dotnet build JuiceGym.UI
```

## Contributing

1. Fork the repository
2. Create a feature branch
3. Add tests for new functionality
4. Ensure all tests pass
5. Submit a pull request

## License

This project is licensed under the MIT License.
