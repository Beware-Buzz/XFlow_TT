using Core;
using Core.Rewards;

namespace Location
{
    public class LocationRewardDisplayer : IRewardDisplayer
    {
        public string GetRewardName() => "Локация";

        public string GetRewardAmount(PlayerData playerData)
        {
            return playerData.GetRewardModel<LocationRewardData>().RewardData.Place;
        }
    }
}