using System;
using Core;
using Core.Rewards;
using UnityEngine;

namespace Vip
{
    [Serializable]
    public class VipRewardActionAdd : IRewardAction
    {
        [SerializeField]
        private VipRewardData _amount;

        public bool CanExecute(PlayerData playerData)
        {
            return true;
        }
        
        public void Execute(PlayerData playerData)
        {
            var model = GetRewardModel(playerData);
            var data = model.RewardData;
            data.Duration += _amount.Duration;
            model.UpdateData(data);
        }

        private RewardModelAbstract<VipRewardData> GetRewardModel(PlayerData playerData)
        {
            return playerData.GetRewardModel<VipRewardData>();
        }
    }
}