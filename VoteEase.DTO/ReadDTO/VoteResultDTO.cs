using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using VoteEase.Domains.Entities;

namespace VoteEase.DTO.ReadDTO
{
    public class VoteResultDTO
    {
        [JsonProperty("member_name")]
        public Member Member { get; set; }
        [JsonProperty("number_of_votes")]
        public IEnumerable<int> Count { get; set; }
    }
}
