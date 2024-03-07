using Newtonsoft.Json;

namespace VoteEase.DTO.WriteDTO
{
    public class AdminDTOw
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }
        [JsonProperty("position")]
        public string Position { get; set; }
        [JsonProperty("email_address")]
        public string EmailAddress { get; set; }
    }
}
