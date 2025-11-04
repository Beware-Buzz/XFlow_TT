using System;
using Core;
using Core.Rewards;
using UnityEngine;

namespace Vip
{
    public class VipRewardActionRemove: IRewardAction
    {
        [SerializeField]
        private VipRewardData _amount;

        public bool CanExecute(PlayerData playerData)
        {
            var model = GetRewardModel(playerData);
            return model.RewardData.Duration - _amount.Duration >= TimeSpan.Zero;
        }

        public void Execute(PlayerData playerData)
        {
            var model = GetRewardModel(playerData);
            var data = model.RewardData;
            data.Duration -= _amount.Duration;
            model.UpdateData(data);
        }

        private RewardModelAbstract<VipRewardData> GetRewardModel(PlayerData playerData)
        {
            return playerData.GetRewardModel<VipRewardData>();
        }
    }
}