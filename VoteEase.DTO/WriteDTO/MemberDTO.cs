using Newtonsoft.Json;

namespace VoteEase.DTO.WriteDTO
{
    public class MemberDTO
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("phone_number")]
        public string PhoneNumber { get; set; }
        [JsonProperty("is_accredited")]
        public bool IsAccredited { get; set; }
    }
}
