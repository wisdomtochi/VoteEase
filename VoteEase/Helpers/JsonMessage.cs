using Newtonsoft.Json;

namespace VoteEase.API.Helpers
{
    public class JsonMessage<T>
    {
        [JsonProperty("result")]
        public T Result { get; set; }

        [JsonProperty("results")]
        public List<T> Results { get; set; }

        [JsonProperty("status")]
        public bool Status { get; set; }

        [JsonProperty("error_message")]
        public string ErrorMessage { get; set; }

        [JsonProperty("success_message")]
        public string SuccessMessage { get; set; }
    }
}
