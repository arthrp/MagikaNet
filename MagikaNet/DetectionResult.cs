using System.Text.Json.Serialization;

namespace MagikaNet;

public class DetectionResult
{
    [JsonPropertyName("status")]
    public string Status { get; set; }

    [JsonPropertyName("type")]
    public string FileType { get; set; }

    [JsonPropertyName("value")]
    public ValueData Value { get; set; }
}

public class ValueData
{
    [JsonPropertyName("output")]
    public OutputData Output { get; set; }

    [JsonPropertyName("score")]
    public double Score { get; set; }
}

public class OutputData
{
    [JsonPropertyName("label")]
    public string Label { get; set; }

    [JsonPropertyName("mime_type")]
    public string MimeType { get; set; }

    [JsonPropertyName("group")]
    public string Group { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("extensions")]
    public List<string> Extensions { get; set; }

    [JsonPropertyName("is_text")]
    public bool IsText { get; set; }
}