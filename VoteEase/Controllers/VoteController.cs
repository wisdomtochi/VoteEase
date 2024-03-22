﻿using Microsoft.AspNetCore.Mvc;
using VoteEase.API.Helpers;
using VoteEase.Application.Error;
using VoteEase.Application.Votings;
using VoteEase.Domain.Entities.Core;
using VoteEase.DTO.ReadDTO;
using VoteEase.DTO.WriteDTO;

namespace VoteEase.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoteController : ControllerBase
    {
        private readonly IVoteService voteService;
        private readonly IErrorService errorService;

        public VoteController(IVoteService voteService, IErrorService errorService)
        {
            this.voteService = voteService;
            this.errorService = errorService;
        }

        [HttpGet]
        [Route("vote-result")]
        public async Task<IActionResult> VoteResult()
        {
            try
            {
                var voteResult = await voteService.CountVoteResult();
                return Ok(new JsonMessage<VoteResultDTO>
                {
                    Status = true,
                    Result = voteResult.Entity,
                    SuccessMessage = voteResult.Message
                });
            }
            catch (Exception e)
            {
                errorService.LogError(e);
                return StatusCode(500, "Internal Server Error");
            }
        }

        #region crud
        [HttpGet]
        [Route("votes/get-all")]
        public async Task<IActionResult> GetAllVotes()
        {
            try
            {
                var votes = await voteService.GetAllVotes();
                if (!votes.Succeeded) return Ok(new JsonMessage<string>()
                {
                    Status = false,
                    ErrorMessage = votes.Message
                });

                return Ok(new JsonMessage<VoteDTO>()
                {
                    Status = true,
                    Results = votes.ListOfEntities
                });
            }
            catch (Exception e)
            {
                errorService.LogError(e);
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet]
        [Route("vote/get/{voteId}")]
        public async Task<IActionResult> GetVote([FromRoute] Guid voteId)
        {
            try
            {
                var vote = await voteService.GetVote(voteId);
                if (!vote.Succeeded) return Ok(new JsonMessage<string>()
                {
                    Status = false,
                    ErrorMessage = vote.Message
                });

                return Ok(new JsonMessage<VoteDTO>()
                {
                    Status = true,
                    Result = vote.Entity
                });
            }
            catch (Exception e)
            {
                errorService.LogError(e);
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost]
        [Route("votes/vote")]
        public async Task<IActionResult> AddNewVote([FromBody] Vote vote)
        {
            try
            {
                var newVote = await voteService.CreateNewVote(vote);
                if (!newVote.Succeeded) return Ok(new JsonMessage<string>()
                {
                    Status = false,
                    ErrorMessage = newVote.Message
                });

                return Ok(new JsonMessage<VoteDTO>()
                {
                    Status = newVote.Succeeded,
                    SuccessMessage = newVote.Message
                });
            }
            catch (Exception e)
            {
                errorService.LogError(e);
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost]
        [Route("votes/update-vote/{voteId}")]
        public async Task<IActionResult> UpdateVote([FromBody] Vote vote, [FromRoute] Guid voteId)
        {
            try
            {
                var newVote = await voteService.UpdateVote(vote, voteId);
                if (!newVote.Succeeded) return Ok(new JsonMessage<string>()
                {
                    Status = false,
                    ErrorMessage = newVote.Message
                });

                return Ok(new JsonMessage<VoteDTO>()
                {
                    Status = newVote.Succeeded,
                    SuccessMessage = newVote.Message
                });
            }
            catch (Exception e)
            {
                errorService.LogError(e);
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost]
        [Route("votes/delete-vote/{voteId}")]
        public async Task<IActionResult> DeleteVote([FromRoute] Guid voteId)
        {
            try
            {
                var vote = await voteService.DeleteVote(voteId);
                if (!vote.Succeeded) return Ok(new JsonMessage<string>()
                {
                    Status = false,
                    ErrorMessage = vote.Message
                });

                return Ok(new JsonMessage<VoteDTO>()
                {
                    Status = vote.Succeeded,
                    SuccessMessage = vote.Message
                });
            }
            catch (Exception e)
            {
                errorService.LogError(e);
                return StatusCode(500, "Internal Server Error");
            }
        }
        #endregion
    }
}
