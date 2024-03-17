﻿using Newtonsoft.Json;
using VoteEase.Domain.Entities.Core;

namespace VoteEase.DTO.WriteDTO
{
    public class VoteDTO
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }
        [JsonProperty("member")]
        public Member Member { get; set; }
        public DateTime DateCreated { get; set; }
        [JsonProperty("counsellor")]
        public Member Counsellor { get; set; }
        [JsonProperty("people's_warden")]
        public Member? PeoplesWarden { get; set; }
        [JsonProperty("synod_delegate")]
        public Member? SynodDelegate { get; set; }
    }
}
