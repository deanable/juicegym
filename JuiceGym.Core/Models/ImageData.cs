using System.ComponentModel.DataAnnotations;

namespace JuiceGym.Core.Models;

/// <summary>
/// Represents image metadata with path and extracted tags/categories
/// </summary>
/// <summary>
/// Represents image metadata with path, tags, and numerical labels
/// </summary>
public record ImageData(
    [Required] string ImagePath,
    string[] Tags,
    uint[] Labels
);
