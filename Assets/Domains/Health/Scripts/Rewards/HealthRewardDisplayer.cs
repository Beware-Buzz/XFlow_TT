using Core;
using Core.Rewards;

namespace Health
{
    public class HealthRewardDisplayer : IRewardDisplayer
    {
        public string GetRewardName() => "Жизни";

        public string GetRewardAmount(PlayerData playerData)
        {
            return playerData.GetRewardModel<HealthRewardData>().RewardData.Amount.ToString();
        }
    }
}