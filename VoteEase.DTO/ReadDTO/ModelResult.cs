using Newtonsoft.Json;

namespace VoteEase.DTO.ReadDTO
{
    public class ModelResult<T>
    {
        [JsonProperty("succeeded")]
        public bool Succeeded { get; set; }
        [JsonProperty("list_of_entities")]
        public List<T>? ListOfEntities { get; set; }
        [JsonProperty("entity")]
        public T? Entity { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
