using Newtonsoft.Json;
using VoteEase.Domain.Entities.Core;

namespace VoteEase.DTO.WriteDTO
{
    public class MemberInGroupDTOw
    {
        [JsonProperty("member_id")]
        public Guid MemberId { get; set; }
        [JsonProperty("member")]
        public Member Member { get; set; }
        [JsonProperty("group_id")]
        public Guid GroupId { get; set; }
        [JsonProperty("group")]
        public Group Group { get; set; }
    }
}
