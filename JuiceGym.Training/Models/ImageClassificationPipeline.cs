using JuiceGym.Core.Models;
using Microsoft.ML.Data;
using Microsoft.ML.Transforms.Image;
using Microsoft.ML.Transforms;
using Microsoft.ML.Transforms.Onnx;
using Microsoft.ML;
using System.IO;

namespace JuiceGym.Training.Models;

public class ImageClassificationPipeline
{
    public static MLContext Context { get; } = new();

    public static IDataView PrepareData(IEnumerable<ImageData> data)
    {
        return Context.Data.LoadFromEnumerable(data);
    }

    public static IEstimator<ITransformer> BuildTrainingPipeline(string onnxModelPath)
    {
        var pipeline = Context.Transforms.Conversion
            .MapValueToKey("Labels", "Label")
            .Append(Context.Transforms.LoadImages("ImagePath", "Image"))
            .Append(Context.Transforms.ResizeImages("Image", 224, 224))
            .Append(Context.Transforms.ExtractPixels("Image"))
            .Append(Context.Transforms.ApplyOnnxModel(
                modelFile: onnxModelPath,
                outputColumnNames: new[] { "BottleneckFeatures" },
                inputColumnNames: new[] { "Image" }))
            .Append(Context.MulticlassClassification.Trainers
                .SdcaMaximumEntropy(
                    labelColumnName: "Label",
                    featureColumnName: "BottleneckFeatures"));

        return pipeline;
    }
}
