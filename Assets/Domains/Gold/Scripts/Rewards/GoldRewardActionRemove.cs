using Core;
using Core.Rewards;
using UnityEngine;

namespace Gold
{
    public class GoldRewardActionRemove: IRewardAction
    {
        [SerializeField]
        private GoldRewardData _amount;

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

        private RewardModelAbstract<GoldRewardData> GetRewardModel(PlayerData playerData)
        {
            return playerData.GetRewardModel<GoldRewardData>();
        }
    }
}