using Microsoft.AspNetCore.Mvc;
using VoteEase.API.Helpers;
using VoteEase.Application.Votings;
using VoteEase.Domains.Entities;
using VoteEase.DTO.WriteDTO;

namespace VoteEase.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IMemberService memberService;

        public MemberController(IMemberService memberService)
        {
            this.memberService = memberService;
        }

        [HttpGet]
        [Route("get-all")]
        public async Task<IActionResult> GetMembers()
        {
            try
            {
                var members = await memberService.GetAllMembers();
                if (!members.Succeeded) return Ok(new JsonMessage<string>()
                {
                    Status = false,
                    ErrorMessage = members.Message
                });

                return Ok(new JsonMessage<MemberDTO>()
                {
                    Status = true,
                    Results = members.ListOfEntities
                });
            }
            catch (Exception e)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("get-member/{memberId}")]
        public async Task<IActionResult> GetMember([FromRoute] Guid memberId)
        {
            try
            {
                var member = await memberService.GetMember(memberId);
                if (!member.Succeeded) return Ok(new JsonMessage<string>()
                {
                    Status = false,
                    ErrorMessage = member.Message
                });

                return Ok(new JsonMessage<MemberDTO>()
                {
                    Status = true,
                    Result = member.Entity
                });
            }
            catch (Exception e)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("accredited-members")]
        public async Task<IActionResult> GetAccreditedMembeers()
        {
            try
            {
                var members = await memberService.GetAllAccreditedMembers();
                if (!members.Succeeded) return Ok(new JsonMessage<string>()
                {
                    Status = false,
                    ErrorMessage = members.Message
                });

                return Ok(new JsonMessage<MemberDTO>()
                {
                    Status = members.Succeeded,
                    Results = members.ListOfEntities,
                    SuccessMessage = members.Message
                });
            }
            catch (Exception e)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("create-new-member")]
        public async Task<IActionResult> AddNewMember([FromBody] Member member)
        {
            try
            {
                var newMember = await memberService.CreateNewMember(member);
                if (!newMember.Succeeded) return Ok(new JsonMessage<string>()
                {
                    Status = false,
                    ErrorMessage = newMember.Message
                });

                return Ok(new JsonMessage<MemberDTO>()
                {
                    Status = newMember.Succeeded,
                    SuccessMessage = newMember.Message
                });
            }
            catch (Exception e)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("update-member/{memberId}")]
        public async Task<IActionResult> UpdateMember([FromBody] Member member, [FromRoute] Guid memberId)
        {
            try
            {
                var newMemberDetails = await memberService.UpdateMember(member, memberId);
                if (!newMemberDetails.Succeeded) return Ok(new JsonMessage<string>()
                {
                    Status = false,
                    ErrorMessage = newMemberDetails.Message
                });

                return Ok(new JsonMessage<MemberDTO>()
                {
                    Status = newMemberDetails.Succeeded,
                    SuccessMessage = newMemberDetails.Message
                });
            }
            catch (Exception e)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("delete-member")]
        public async Task<IActionResult> DeleteMember([FromRoute] Guid memberId)
        {
            try
            {
                var member = await memberService.DeleteMember(memberId);
                if (!member.Succeeded) return Ok(new JsonMessage<string>()
                {
                    Status = false,
                    ErrorMessage = member.Message
                });

                return Ok(new JsonMessage<MemberDTO>()
                {
                    Status = member.Succeeded,
                    SuccessMessage = member.Message
                });
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
