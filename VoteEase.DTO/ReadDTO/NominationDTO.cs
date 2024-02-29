﻿using Newtonsoft.Json;
using VoteEase.Domains.Entities;

namespace VoteEase.DTO.ReadDTO
{
    public class NominationDTO
    {
        [JsonProperty("counsellors")]
        public CounsellorCategory Counsellors { get; set; }
        [JsonProperty("people's_warden")]
        public PeoplesWardenCategory? PeoplesWarden { get; set; }
        [JsonProperty("synod_delegates")]
        public DelegatesCategory? Delegates { get; set; }
    }
}
