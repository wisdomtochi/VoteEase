namespace VoteEase.API.Models
{
    public class AddUserToRoleViewModel
    {
        public List<string> UserId { get; set; }
        public List<string> RoleNames { get; set; }
    }

    public class IsUserInRoleViewModel
    {
        public string UserId { get; set; }
        public string RoleName { get; set; }
    }
}
