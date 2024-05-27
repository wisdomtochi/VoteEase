using Newtonsoft.Json;
using VoteEase.Domain.Entities.Core;

namespace VoteEase.DTO.WriteDTO
{
    public class AccreditedMemberDTO
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }
        [JsonProperty("member_Id")]
        public Guid MemberId { get; set; }
        [JsonProperty("member")]
        public Member Member { get; set; }
        [JsonProperty("date-added")]
        public DateTime DateAdded { get; set; }
    }
}
