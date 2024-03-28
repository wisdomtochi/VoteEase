using VoteEase.Application.Votings;
using VoteEase.Data_Access.Interface;
using VoteEase.Domain.Entities.Core;
using VoteEase.DTO.ReadDTO;
using VoteEase.Mapper.Map;
using Timer = System.Timers.Timer;

namespace VoteEase.Infrastructure.Votings
{
    public class MessageService : IMessageService
    {
        private readonly IGenericRepository<MemberPasscode> memberPasscodeGenericRepository;
        static DateTime setElectionDate;
        static Timer timer;

        public MessageService(IGenericRepository<MemberPasscode> memberPasscodeGenericRepository)
        {
            this.memberPasscodeGenericRepository = memberPasscodeGenericRepository;
        }

        #region Schedule Election Date
        public static ModelResult<string> ScheduleElectionDate(DateTime targetDate)
        {
            setElectionDate = targetDate;

            TimeSpan timeUntilTargetDate = setElectionDate - DateTime.UtcNow;

            timer = new Timer(timeUntilTargetDate.TotalMilliseconds);

            timer.Elapsed += (sender, e) =>
            {
                timer.Stop();
            };

            timer.Start();

            return Map.GetModelResult<string>(null, null, true, $"Election date has been set to {targetDate}");
        }
        #endregion

        #region Update Election Date
        public static ModelResult<string> UpdateElectionDate(DateTime newTargetDate)
        {
            timer.Stop();

            setElectionDate = newTargetDate;

            TimeSpan timeUntilTargetDate = setElectionDate - DateTime.UtcNow;

            timer = new Timer(timeUntilTargetDate.TotalMilliseconds);

            timer.Elapsed += (sender, e) =>
            {
                timer.Stop();
            };

            timer.Start();

            return Map.GetModelResult<string>(null, null, true, $"Election date has been updated to {newTargetDate}");
        }
        #endregion

        #region Create Passcode
        public static string CreatePassCode()
        {
            Random randomNumbers = new();
            string randomString = string.Join("", Enumerable.Range(0, 6).Select(_ => randomNumbers.Next(10)));

            return randomString;
        }
        #endregion
    }
}
