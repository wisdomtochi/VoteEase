using Newtonsoft.Json;
using VoteEase.Domain.Entities.Core;

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
        public DateTime DateCreated { get; set; }
        [JsonProperty("counsellors")]
        public CounsellorCategory Counsellors { get; set; }
        [JsonProperty("people's_warden")]
        public PeoplesWardenCategory? PeoplesWarden { get; set; }
        [JsonProperty("synod_delegates")]
        public DelegatesCategory? Delegates { get; set; }
    }
}
