using System;
using System.IO;

namespace JuiceGym.Data.Utils;

/// <summary>
/// Provides file path validation utilities
/// </summary>
public static class FilePathValidator
{
    /// <summary>
    /// Validates directory existence
    /// </summary>
    /// <param name="path">Directory path to validate</param>
    /// <exception cref="ArgumentNullException">When path is null</exception>
    /// <exception cref="DirectoryNotFoundException">When directory doesn't exist</exception>
    public static void ValidateDirectory(string path)
    {
        if (path == null) throw new ArgumentNullException(nameof(path));
        if (!Directory.Exists(path))
            throw new DirectoryNotFoundException($"Invalid directory: {path}");
    }
}
