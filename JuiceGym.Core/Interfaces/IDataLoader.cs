using JuiceGym.Core.Models;

namespace JuiceGym.Core.Interfaces;

/// <summary>
/// Interface for loading image data from directories
/// </summary>
public interface IDataLoader
{
    /// <summary>
    /// Loads images and metadata from specified directory
    /// </summary>
    /// <param name="directoryPath">Path to image directory</param>
    /// <returns>Collection of ImageData objects</returns>
    Task<IEnumerable<ImageData>> LoadImagesAsync(string directoryPath);
}
