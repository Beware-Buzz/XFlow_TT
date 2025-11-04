using Core;
using Core.Rewards;

namespace Location
{
    public class LocationRewardActionSetDefault : IRewardAction
    {
        public bool CanExecute(PlayerData playerData)
        {
            return true;
        }

        public void Execute(PlayerData playerData)
        {
            var model = GetRewardModel(playerData);
            var data = model.RewardData;
            data.Place = LocationRewardData.DEFAULT;
            model.UpdateData(data);
        }

        private RewardModelAbstract<LocationRewardData> GetRewardModel(PlayerData playerData)
        {
            return playerData.GetRewardModel<LocationRewardData>();
        }
    }
}