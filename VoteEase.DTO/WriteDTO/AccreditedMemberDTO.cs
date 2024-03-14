using Newtonsoft.Json;

namespace VoteEase.DTO.WriteDTO
{
    public class AccreditedMemberDTO
    {
        [JsonProperty("member_Id")]
        public Guid MemberId { get; set; }
        [JsonProperty("date-added")]
        public DateTime DateAdded { get; set; }
    }
}
