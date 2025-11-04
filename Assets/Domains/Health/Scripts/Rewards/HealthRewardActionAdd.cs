using System;
using Core;
using Core.Rewards;
using UnityEngine;

namespace Health
{
    [Serializable]
    public class HealthRewardActionAdd : IRewardAction
    {
        [SerializeField]
        private HealthRewardData _amount;

        public bool CanExecute(PlayerData playerData)
        {
            return true;
        }
        
        public void Execute(PlayerData playerData)
        {
            var model = GetRewardModel(playerData);
            var data = model.RewardData;
            data.Amount += _amount.Amount;
            model.UpdateData(data);
        }

        private RewardModelAbstract<HealthRewardData> GetRewardModel(PlayerData playerData)
        {
            return playerData.GetRewardModel<HealthRewardData>();
        }
    }
}