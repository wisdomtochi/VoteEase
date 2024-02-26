using VoteEase.Domains.Entities;
using VoteEase.DTO.ReadDTO;
using VoteEase.DTO.WriteDTO;

namespace VoteEase.Mapper.Map
{
    public class Map
    {
        public static ModelResult<T> GetModelResult<T>(List<T> entity, bool success, string message, MetaData metaData)
        {
            var model = new ModelResult<T>
            {
                Result = entity,
                Succeeded = success,
                Message = message,
                MetaData = metaData
            };

            return model;
        }

        public static List<MemberDTO> Member(ICollection<Member> source)
        {
            List<MemberDTO> members = source.Select(x => new MemberDTO()
            {
                Id = x.Id,
                Name = x.Name,
                PhoneNumber = x.PhoneNumber,
                IsAccredited = x.IsAccredited
            }).ToList();

            return members;
        }

        public static List<GroupDTO> Group(ICollection<Group> source)
        {
            List<GroupDTO> groups = source.Select(x => new GroupDTO()
            {
                Id = x.Id,
                Name = x.Name,
                Leader = x.Leader,
                LeaderId = x.LeaderId
            }).ToList();

            return groups;
        }

        /// <summary>
        /// method to get the people that nominated and their nominations
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static List<NominationDTOw> PeoplesNomination(ICollection<Nomination> source)
        {
            List<NominationDTOw> peoplesNominations = source.Select(x => new NominationDTOw()
            {
                Id = x.Id,
                GroupId = x.GroupId,
                Group = x.Group,
                CounsellorOne = x.CounsellorOne,
                CounsellorTwo = x.CounsellorTwo,
                CounsellorThree = x.CounsellorThree,
                PeoplesWarden = x.PeoplesWarden
            }).ToList();

            return peoplesNominations;
        }

        /// <summary>
        /// method that gets only the nominations
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static List<NominationDTO> Nomination(ICollection<Nomination> source)
        {
            List<NominationDTO> nominations = source.Select(x => new NominationDTO()
            {
                CounsellorOne = x.CounsellorOne,
                CounsellorTwo = x.CounsellorTwo,
                CounsellorThree = x.CounsellorThree,
                PeoplesWarden = x.PeoplesWarden
            }).ToList();

            return nominations;
        }

        public static List<VoteDTO> Vote(ICollection<Vote> source)
        {
            List<VoteDTO> votes = source.Select(x => new VoteDTO()
            {
                Id = x.Id,
                Member = x.Member,
                Counsellor = x.Counsellor,
                PeoplesWarden = x.PeoplesWarden
            }).ToList();

            return votes;
        }
    }
}
