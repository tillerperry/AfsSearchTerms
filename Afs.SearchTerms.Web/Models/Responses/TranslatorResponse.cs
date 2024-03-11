using Newtonsoft.Json;

namespace Afs.SearchTerms.Web.Models.Responses;
#nullable disable
public class TranslatorResponse
{
    [JsonProperty("success")]
    public Success Success { get; set; }

    [JsonProperty("contents")]
    public Contents Contents { get; set; }
}

public class Contents
{
    [JsonProperty("translated")]
    public string Translated { get; set; }

    [JsonProperty("text")]
    public string Text { get; set; }

    [JsonProperty("translation")] 
    public string Translation { get; set; }
}

public class Success
{
    [JsonProperty("total")] 
    public int Total { get; set; }
}