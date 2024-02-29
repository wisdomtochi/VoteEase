using VoteEase.Domains.Entities;
using VoteEase.DTO.ReadDTO;
using VoteEase.DTO.WriteDTO;

namespace VoteEase.Mapper.Map
{
    public static class Map
    {
        public static ModelResult<T> GetModelResult<T>(T? entity, List<T>? listOfEntity, bool success, string message)
        {
            var model = new ModelResult<T>
            {
                Entity = entity,
                ListOfEntities = listOfEntity,
                Succeeded = success,
                Message = message
            };

            return model;
        }

        #region Member Section
        /// <summary>
        /// Method to convert the Member entity to the Member DTO
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static MemberDTO Member(Member source)
        {
            MemberDTO member = new()
            {
                Id = source.Id,
                Name = source.Name,
                PhoneNumber = source.PhoneNumber,
                IsAccredited = source.IsAccredited
            };

            return member;
        }

        /// <summary>
        /// Method to convert the List of Member entity to List of the Member DTO
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static List<MemberDTO> Member(IEnumerable<Member> source)
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
        #endregion

        #region Group Section
        /// <summary>
        /// Method to convert the Group entity to the Group DTO
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static GroupDTO Group(Group source)
        {
            GroupDTO group = new()
            {
                Id = source.Id,
                Name = source.Name,
                LeaderId = source.LeaderId,
                Leader = source.Leader
            };

            return group;
        }

        /// <summary>
        /// Method to convert the List of Group entity to List of the Group DTO
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static List<GroupDTO> Group(IEnumerable<Group> source)
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
        #endregion

        #region Nomination Section
        /// <summary>
        /// method that gets only the nominations
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static List<NominationDTO> Nomination(IEnumerable<Nomination> source)
        {
            List<NominationDTO> nominations = source.Select(x => new NominationDTO()
            {
                Counsellors = x.Counsellors,
                PeoplesWarden = x.PeoplesWarden,
                Delegates = x.Delegates
            }).ToList();

            return nominations;
        }

        #region Nominations Without Synod Delegates
        /// <summary>
        /// method to get one person and his nominations without synod delegates
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static NominationWithoutDelegatesDTO PeoplesNominationWithoutDelegates(Nomination source)
        {
            NominationWithoutDelegatesDTO nomination = new()
            {
                Id = source.Id,
                GroupId = source.GroupId,
                Group = source.Group,
                Counsellors = source.Counsellors,
                PeoplesWarden = source.PeoplesWarden
            };

            return nomination;
        }

        /// <summary>
        /// method to get the people that nominated and their nominations without synod delegates
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static List<NominationWithoutDelegatesDTO> PeoplesNominationWithoutDelegates(IEnumerable<Nomination> source)
        {
            List<NominationWithoutDelegatesDTO> peoplesNominations = source.Select(x => new NominationWithoutDelegatesDTO()
            {
                Id = x.Id,
                GroupId = x.GroupId,
                Group = x.Group,
                Counsellors = x.Counsellors,
                PeoplesWarden = x.PeoplesWarden
            }).ToList();

            return peoplesNominations;
        }
        #endregion

        #region Nominations With Synod Delegates
        /// <summary>
        /// method to get one person and his nominations with synod delegates
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static NominationDTOw PeoplesNomination(Nomination source)
        {
            NominationDTOw nomination = new()
            {
                Id = source.Id,
                GroupId = source.GroupId,
                Group = source.Group,
                Counsellors = source.Counsellors,
                PeoplesWarden = source.PeoplesWarden,
                Delegates = source.Delegates
            };

            return nomination;
        }

        /// <summary>
        /// method to get the people that nominated and their nominations with synod delegates
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static List<NominationDTOw> PeoplesNomination(IEnumerable<Nomination> source)
        {
            List<NominationDTOw> peoplesNominations = source.Select(x => new NominationDTOw()
            {
                Id = x.Id,
                GroupId = x.GroupId,
                Group = x.Group,
                Counsellors = x.Counsellors,
                PeoplesWarden = x.PeoplesWarden,
                Delegates = x.Delegates
            }).ToList();

            return peoplesNominations;
        }
        #endregion
        #endregion

        #region Vote Section
        /// <summary>
        /// Method to convert the Vote entity to the Vote DTO
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static VoteDTO Vote(Vote source)
        {
            VoteDTO vote = new()
            {
                Id = source.Id,
                Member = source.Member,
                Counsellor = source.Counsellor,
                PeoplesWarden = source.PeoplesWarden
            };

            return vote;
        }

        /// <summary>
        /// Method to convert the List of Vote entity to List of the Vote DTO
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static List<VoteDTO> Vote(IEnumerable<Vote> source)
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
        #endregion
    }
}
