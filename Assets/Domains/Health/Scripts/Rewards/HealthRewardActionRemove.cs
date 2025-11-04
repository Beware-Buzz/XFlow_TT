using Core;
using Core.Rewards;
using UnityEngine;

namespace Health
{
    public class HealthRewardActionRemove: IRewardAction
    {
        [SerializeField]
        private HealthRewardData _amount;

        public bool CanExecute(PlayerData playerData)
        {
            var model = GetRewardModel(playerData);
            return model.RewardData.Amount - _amount.Amount >= 0;
        }

        public void Execute(PlayerData playerData)
        {
            var model = GetRewardModel(playerData);
            var data = model.RewardData;
            data.Amount -= _amount.Amount;
            model.UpdateData(data);
        }

        private RewardModelAbstract<HealthRewardData> GetRewardModel(PlayerData playerData)
        {
            return playerData.GetRewardModel<HealthRewardData>();
        }
    }
}