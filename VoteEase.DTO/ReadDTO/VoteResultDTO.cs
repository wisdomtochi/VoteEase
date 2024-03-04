namespace VoteEase.DTO.ReadDTO
{
    public class VoteResultDTO
    {

        public Dictionary<string, IEnumerable<MemberCount>> CounsellorVotes { get; set; }
        public Dictionary<string, IEnumerable<MemberCount>> PeoplesWardenVotes { get; set; }
        public Dictionary<string, IEnumerable<MemberCount>> SynodDelegateVotes { get; set; }
    }

    public class MemberCount
    {
        public string MemberName { get; set; }
        public int Count { get; set; }
    }
}
