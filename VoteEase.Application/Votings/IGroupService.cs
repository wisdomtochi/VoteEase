﻿using VoteEase.Domains.Entities;
using VoteEase.DTO.ReadDTO;
using VoteEase.DTO.WriteDTO;

namespace VoteEase.Application.Votings
{
    public interface IGroupService
    {
        Task<ModelResult<GroupDTO>> GetGroup(Guid id);
        Task<ModelResult<GroupDTO>> GetAllGroups();
        Task<ModelResult<string>> CreateNewGroup(Group group);
        Task<ModelResult<string>> UpdateGroupDetails(Group group, Guid groupId);
        Task<ModelResult<string>> DeleteGroup(Guid id);
    }
}
