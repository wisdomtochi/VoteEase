using Newtonsoft.Json;
using VoteEase.Domain.Entities.Core;
using VoteEase.Domain.Enums;

namespace VoteEase.DTO.WriteDTO
{
    public class NominationDTOw
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }
        [JsonProperty("group_id")]
        [JsonIgnore]
        public Guid GroupId { get; set; }
        [JsonProperty("group")]
        public Group Group { get; set; }
        [JsonProperty("category_name")]
        public Category Category { get; set; }
        [JsonProperty("member_id")]
        public Guid MemberId { get; set; }
        [JsonProperty("member")]
        public Member Member { get; set; }
        [JsonProperty("date_created")]
        public DateTime DateCreated { get; set; }
    }
}
