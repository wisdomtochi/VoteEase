using VoteEase.Domain.Entities.Core;
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
                DateCreated = source.DateCreated,
                Group = source.Group
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
                DateCreated = x.DateCreated,
                Group = x.Group
            }).ToList();

            return members;
        }
        #endregion

        #region Accredited Members Section
        /// <summary>
        /// Method to convert the Accredited Member entity to Accredited Member DTO
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static AccreditedMemberDTO AccreditedMember(AccreditedMember source)
        {
            AccreditedMemberDTO member = new()
            {
                MemberId = source.MemberId,
                DateAdded = source.DateAdded
            };

            return member;
        }

        /// <summary>
        /// Method to convert the List of Accredited Member entity to List of Accredited Member DTO
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static List<AccreditedMemberDTO> AccreditedMember(IEnumerable<AccreditedMember> source)
        {
            List<AccreditedMemberDTO> members = source.Select(x => new AccreditedMemberDTO
            {
                MemberId = x.MemberId,
                DateAdded = x.DateAdded
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
                Leader = source.Leader,
                DateAdded = source.DateAdded
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
                LeaderId = x.LeaderId,
                DateAdded = x.DateAdded
            }).ToList();

            return groups;
        }
        #endregion

        #region Nomination Section
        /// <summary>
        /// Method to convert the Nomination entity to the Nomination DTO
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static NominationDTOw Nomination(Nomination source)
        {
            NominationDTOw nomination = new()
            {
                Id = source.Id,
                GroupId = source.GroupId,
                Group = source.Group,
                MemberId = source.MemberId,
                Member = source.Member,
                Category = source.Category,
                DateCreated = source.DateCreated
            };

            return nomination;
        }

        /// <summary>
        /// Method to convert the list of Nomination entity to list of Nomination DTO
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static List<NominationDTOw> Nomination(IEnumerable<Nomination> source)
        {
            List<NominationDTOw> peoplesNominations = source.Select(x => new NominationDTOw()
            {
                Id = x.Id,
                GroupId = x.GroupId,
                Group = x.Group,
                MemberId = x.MemberId,
                Member = x.Member,
                Category = x.Category,
                DateCreated = x.DateCreated
            }).ToList();

            return peoplesNominations;
        }
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
                VoterId = source.VoterId,
                Voter = source.Voter,
                MemberId = source.MemberId,
                Member = source.Member,
                Category = source.Category,
                DateCreated = source.DateCreated
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
                VoterId = x.VoterId,
                Voter = x.Voter,
                MemberId = x.MemberId,
                Member = x.Member,
                Category = x.Category,
                DateCreated = x.DateCreated
            }).ToList();

            return votes;
        }

        /// <summary>
        /// Method to get the vote result
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static VoteResultDTO VoteResult(VoteResultDTO source)
        {
            VoteResultDTO voteResult = new()
            {
                CounsellorVotes = source.CounsellorVotes,
                PeoplesWardenVotes = source.PeoplesWardenVotes,
                SynodDelegateVotes = source.SynodDelegateVotes
            };

            return voteResult;
        }
        #endregion

        #region Member's Group Section
        /// <summary>
        /// Method to convert the MemberInGroup entity to the MemberInGroup DTO
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static MemberInGroupDTOw MemberInGroup(MemberInGroup source)
        {
            MemberInGroupDTOw memberGroup = new()
            {
                MemberId = source.MemberId,
                Member = source.Member,
                GroupId = source.GroupId,
                Group = source.Group
            };

            return memberGroup;
        }

        /// <summary>
        /// Method to convert the List of MemberInGroup entity to List of the MemberInGroup DTO
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static List<MemberInGroupDTOw> MemberInGroup(IEnumerable<MemberInGroup> source)
        {
            List<MemberInGroupDTOw> members = source.Select(x => new MemberInGroupDTOw()
            {
                MemberId = x.MemberId,
                Member = x.Member,
                GroupId = x.GroupId,
                Group = x.Group
            }).ToList();

            return members;
        }
        #endregion
    }
}
