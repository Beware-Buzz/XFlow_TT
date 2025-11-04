using Core;
using Core.Rewards;

namespace Vip
{
    public class VipRewardDisplayer : IRewardDisplayer
    {
        public string GetRewardName() => "VIP";

        public string GetRewardAmount(PlayerData playerData)
        {
            return playerData.GetRewardModel<VipRewardData>().RewardData.Duration.ToString();
        }
    }
}