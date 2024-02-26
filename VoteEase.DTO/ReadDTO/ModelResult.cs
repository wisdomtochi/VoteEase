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
        [JsonProperty("meta_data")]
        public MetaData MetaData { get; set; }
    }

    public class MetaData
    {
        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}
