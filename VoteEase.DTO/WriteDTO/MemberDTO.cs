using Newtonsoft.Json;

namespace VoteEase.DTO.WriteDTO
{
    public class MemberDTO
    {
        [JsonProperty("member_Id")]
        public Guid Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("phone_number")]
        public string PhoneNumber { get; set; }
        [JsonProperty("group_Id")]
        [JsonIgnore]
        public Guid GroupId { get; set; }
        [JsonProperty("date_created")]
        public DateTime DateCreated { get; set; }
        [JsonProperty("group_name")]
        [JsonIgnore]
        public string GroupName { get; set; }
    }
}
