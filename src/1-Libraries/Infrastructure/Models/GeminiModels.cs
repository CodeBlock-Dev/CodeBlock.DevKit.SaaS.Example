using System.Text.Json.Serialization;

namespace HeyItIsMe.Infrastructure.Models;

// Response models for Gemini 2.5 Flash Image API
public class GeminiImageResponse
{
    [JsonPropertyName("candidates")]
    public GeminiImageCandidate[] Candidates { get; set; }
}

public class GeminiImageCandidate
{
    [JsonPropertyName("content")]
    public GeminiImageContent Content { get; set; }
    
    [JsonPropertyName("finishReason")]
    public string FinishReason { get; set; }
    
    [JsonPropertyName("finishMessage")]
    public string FinishMessage { get; set; }
}

public class GeminiImageContent
{
    [JsonPropertyName("parts")]
    public GeminiImagePart[] Parts { get; set; }
}

public class GeminiImagePart
{
    [JsonPropertyName("text")]
    public string Text { get; set; }
    
    [JsonPropertyName("inlineData")]
    public GeminiInlineData InlineData { get; set; }
}

public class GeminiInlineData
{
    [JsonPropertyName("mimeType")]
    public string MimeType { get; set; }
    
    [JsonPropertyName("data")]
    public string Data { get; set; }
}

// Response models for Gemini 1.5 Flash text API
public class GeminiTextResponse
{
    [JsonPropertyName("candidates")]
    public GeminiTextCandidate[] Candidates { get; set; }
}

public class GeminiTextCandidate
{
    [JsonPropertyName("content")]
    public GeminiTextContent Content { get; set; }
}

public class GeminiTextContent
{
    [JsonPropertyName("parts")]
    public GeminiTextPart[] Parts { get; set; }
}

public class GeminiTextPart
{
    [JsonPropertyName("text")]
    public string Text { get; set; }
}
