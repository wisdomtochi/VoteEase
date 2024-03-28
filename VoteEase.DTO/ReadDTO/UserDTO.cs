using Newtonsoft.Json;

namespace VoteEase.DTO.ReadDTO
{
    public class UserDTO
    {
        [JsonProperty("user_id")]
        public Guid Id { get; set; }
        [JsonProperty("first_name")]
        public string FirstName { get; set; }
        [JsonProperty("last_name")]
        public string LastName { get; set; }
        [JsonProperty("phone_number")]
        public string PhoneNumber { get; set; }
        [JsonProperty("passcode")]
        public string Passcode { get; set; }
        [JsonProperty("date_joined")]
        public DateTime DateJoined { get; set; }
    }
}
