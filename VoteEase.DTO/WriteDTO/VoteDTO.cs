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
        public Guid NominationId { get; set; }
        [JsonProperty("voter")]
        public Nomination VotedPerson { get; set; }
        [JsonProperty("member_id")]
        public Guid MemberId { get; set; }
        [JsonProperty("member")]
        public Member Voter { get; set; }
        [JsonProperty("category_name")]
        public Category Category { get; set; }
        public DateTime DateCreated { get; set; }

    }
}
