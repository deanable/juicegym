using System;
using JuiceGym.Core.Interfaces;
using JuiceGym.Core.Models;
using JuiceGym.Data.Utils;
using SixLabors.ImageSharp.Metadata.Profiles.Exif;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Concurrent;
using System.IO;

namespace JuiceGym.Data
{
    public class DataLoader : IDataLoader
    {
        public async Task<IEnumerable<ImageData>> LoadImagesAsync(string directoryPath)
        {
            FilePathValidator.ValidateDirectory(directoryPath);

            var supportedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var files = Directory.EnumerateFiles(
                directoryPath, "*", SearchOption.AllDirectories)
                .Where(f => supportedExtensions.Contains(
                    Path.GetExtension(f), StringComparer.OrdinalIgnoreCase))
                .ToList();

            var imageData = new ConcurrentBag<ImageData>();
            var uniqueTags = new ConcurrentDictionary<string, uint>();
            uint currentLabel = 0;

            await Parallel.ForEachAsync(files, async (file, token) =>
            {
                try
                {
                    using var image = await Image.LoadAsync(file);
                    if (image.Metadata.ExifProfile?.TryGetValue(
                        ExifTag.XPSubject, out IExifValue<string>? tagsValue) ?? false)
                    {
                        var tags = tagsValue?.Value?.Split([';', ','], StringSplitOptions.TrimEntries)
                            .Where(t => !string.IsNullOrEmpty(t))
                            .ToArray();

                        if (tags != null)
                        {
                            var labels = tags.Select(tag =>
                            {
                                if (!uniqueTags.TryGetValue(tag, out var label))
                                {
                                    label = Interlocked.Increment(ref currentLabel);
                                    uniqueTags.TryAdd(tag, label);
                                }
                                return label;
                            }).ToArray();

                            imageData.Add(new ImageData(file, tags, labels));
                        }
                        else
                        {
                            Console.WriteLine($"No EXIF tags found in {file}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Unsupported format: {file}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Error processing {file}: {ex.Message}");
                }
            });

            return imageData.OrderBy(d => d.ImagePath).ToList();
        }
    }
}
