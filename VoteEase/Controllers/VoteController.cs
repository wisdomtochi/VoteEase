using Microsoft.AspNetCore.Mvc;
using VoteEase.API.Helpers;
using VoteEase.Application.Votings;
using VoteEase.Domains.Entities;
using VoteEase.DTO.ReadDTO;
using VoteEase.DTO.WriteDTO;

namespace VoteEase.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoteController : ControllerBase
    {
        private readonly IVoteService voteService;

        public VoteController(IVoteService voteService)
        {
            this.voteService = voteService;
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
                throw;
            }
        }

        #region crud
        [HttpGet]
        [Route("get-all")]
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
                throw;
            }
        }

        [HttpGet]
        [Route("get-vote/{voteId}")]
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
                throw;
            }
        }

        [HttpPost]
        [Route("add-new-vote")]
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
                throw;
            }
        }

        [HttpPost]
        [Route("update-vote")]
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
                throw;
            }
        }

        [HttpPost]
        [Route("delete-vote")]
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
                throw;
            }
        }
        #endregion
    }
}
