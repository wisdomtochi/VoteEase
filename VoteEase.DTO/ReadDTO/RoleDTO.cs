using Newtonsoft.Json;

namespace VoteEase.DTO.ReadDTO
{
    public class RoleDTO
    {
        [JsonProperty("Id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("users_in_role")]
        public List<UsersInRoleDTO> UsersInRole { get; set; }
    }

    public class UsersInRoleDTO
    {
        [JsonProperty("user_id")]
        public Guid Id { get; set; }
        [JsonProperty("name")]
        public string FullName { get; set; }
    }
}
