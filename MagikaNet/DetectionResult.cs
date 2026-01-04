using System.Text.Json.Serialization;

namespace MagikaNet;

public class DetectionResult
{
    [JsonPropertyName("status")]
    public required string Status { get; set; }

    [JsonPropertyName("type")]
    public required string FileType { get; set; }

    [JsonPropertyName("value")]
    public required ValueData Value { get; set; }
}

public class ValueData
{
    [JsonPropertyName("output")]
    public required OutputData Output { get; set; }

    [JsonPropertyName("score")]
    public required double Score { get; set; }
}

public class OutputData
{
    [JsonPropertyName("label")]
    public required string Label { get; set; }

    [JsonPropertyName("mime_type")]
    public required string MimeType { get; set; }

    [JsonPropertyName("group")]
    public required string Group { get; set; }

    [JsonPropertyName("description")]
    public required string Description { get; set; }

    [JsonPropertyName("extensions")]
    public required List<string> Extensions { get; set; }

    [JsonPropertyName("is_text")]
    public required bool IsText { get; set; }
}