using Newtonsoft.Json;

namespace VoteEase.DTO.ReadDTO
{
    public class ModelResult<T>
    {
        [JsonProperty("succeeded")]
        public bool Succeeded { get; set; }
        [JsonProperty("result")]
        public List<T> Result { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
