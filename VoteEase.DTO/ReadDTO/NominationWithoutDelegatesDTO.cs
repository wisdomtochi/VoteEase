﻿using Newtonsoft.Json;
using VoteEase.Domains.Entities;

namespace VoteEase.DTO.ReadDTO
{
    public class NominationWithoutDelegatesDTO
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }
        [JsonProperty("group_id")]
        [JsonIgnore]
        public Guid GroupId { get; set; }
        [JsonProperty("group")]
        public Group Group { get; set; }
        [JsonProperty("counsellors")]
        public CounsellorCategory Counsellors { get; set; }
        [JsonProperty("people's_warden")]
        public PeoplesWardenCategory? PeoplesWarden { get; set; }
    }
}
