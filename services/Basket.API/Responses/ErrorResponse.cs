using Newtonsoft.Json;

namespace Basket.API.Responses;

public class ErrorResponse : ApiResponse<ErrorResponse>
{
    [JsonProperty("traceId")]
    public string TraceId { get; set; }
    [JsonProperty("errors")]
    public List<Error> Errors { get; set; }
    
}

public class Error
{
    [JsonProperty("source")]
    public string Source { get; set; }
    [JsonProperty("message")]
    public string Message { get; set; }
}