using Newtonsoft.Json;
using VoteEase.Domains.Entities;

namespace VoteEase.DTO.WriteDTO
{
    public class GroupDTO
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("leader_id")]
        [JsonIgnore]
        public Guid LeaderId { get; set; }
        [JsonProperty("group_leader")]
        public Member Leader { get; set; }
    }
}
