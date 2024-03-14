using Newtonsoft.Json;
using VoteEase.Domains.Entities;

namespace VoteEase.DTO.WriteDTO
{
    public class GroupDTO
    {
        [JsonProperty("group_Id")]
        public Guid Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("leader_Id")]
        [JsonIgnore]
        public Guid LeaderId { get; set; }
        [JsonProperty("group-leader")]
        public Member Leader { get; set; }
        [JsonProperty("date-created")]
        public DateTime DateAdded { get; set; }
    }
}
