using Newtonsoft.Json;
using VoteEase.Domain.Entities.Core;

namespace VoteEase.DTO.ReadDTO
{
    public class NominationWithoutDelegatesDTO
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }
        [JsonProperty("group-Id")]
        [JsonIgnore]
        public Guid GroupId { get; set; }
        [JsonProperty("group")]
        public Group Group { get; set; }
        [JsonProperty("date-added")]
        public DateTime DateAdded { get; set; }
        [JsonProperty("counsellors")]
        public CounsellorCategory Counsellors { get; set; }
        [JsonProperty("people's_warden")]
        public PeoplesWardenCategory? PeoplesWarden { get; set; }
    }
}
