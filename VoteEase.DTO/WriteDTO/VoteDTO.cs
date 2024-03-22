using Newtonsoft.Json;
using VoteEase.Domain.Entities.Core;
using VoteEase.Domain.Enums;

namespace VoteEase.DTO.WriteDTO
{
    public class VoteDTO
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }
        [JsonProperty("voter_id")]
        public Guid VoterId { get; set; }
        [JsonProperty("voter")]
        public Member Voter { get; set; }
        [JsonProperty("category_name")]
        public Category Category { get; set; }
        [JsonProperty("member_id")]
        public Guid MemberId { get; set; }
        [JsonProperty("member")]
        public Member Member { get; set; }
        public DateTime DateCreated { get; set; }

    }
}
