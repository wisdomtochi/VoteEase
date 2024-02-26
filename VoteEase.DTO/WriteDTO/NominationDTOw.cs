using Newtonsoft.Json;
using VoteEase.Domains.Entities;

namespace VoteEase.DTO.WriteDTO
{
    public class NominationDTOw
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("group_id")]
        [JsonIgnore]
        public Guid GroupId { get; set; }
        [JsonProperty("group")]
        public Group Group { get; set; }
        [JsonProperty("counsellor_one")]
        public Member CounsellorOne { get; set; }
        [JsonProperty("counsellor_two")]
        public Member CounsellorTwo { get; set; }
        [JsonProperty("counsellor_three")]
        public Member CounsellorThree { get; set; }
        [JsonProperty("people's_warden")]
        public Member? PeoplesWarden { get; set; }
    }
}
