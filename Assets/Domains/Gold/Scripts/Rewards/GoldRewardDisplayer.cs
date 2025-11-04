using Core;
using Core.Rewards;

namespace Gold
{
    public class GoldRewardDisplayer : IRewardDisplayer
    {
        public string GetRewardName() => "Золото";

        public string GetRewardAmount(PlayerData playerData)
        {
            return playerData.GetRewardModel<GoldRewardData>().RewardData.Amount.ToString();
        }
    }
}