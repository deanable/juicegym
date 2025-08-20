using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JuiceGym.Core.Interfaces;
using JuiceGym.Core.Models;
using JuiceGym.Data.Utils;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Metadata.Profiles.Exif;

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
            int processedCount = 0;
            int errorCount = 0;

            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Processing {files.Count} images...");

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

                        if (tags != null && tags.Length > 0)
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
                            Interlocked.Increment(ref processedCount);
                        }
                        else
                        {
                            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] No EXIF tags found in {Path.GetFileName(file)}");
                            Interlocked.Increment(ref errorCount);
                        }
                    }
                    else
                    {
                        Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] No EXIF profile found in {Path.GetFileName(file)}");
                        Interlocked.Increment(ref errorCount);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Error processing {Path.GetFileName(file)}: {ex.Message}");
                    Interlocked.Increment(ref errorCount);
                }
            });

            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Data loading completed:");
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] - Successfully processed: {processedCount} images");
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] - Errors: {errorCount} images");
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] - Unique tags found: {uniqueTags.Count}");

            if (processedCount == 0)
            {
                throw new InvalidOperationException("No valid images found with EXIF tags. Ensure images have proper metadata.");
            }

            return imageData.OrderBy(d => d.ImagePath).ToList();
        }
    }
}
