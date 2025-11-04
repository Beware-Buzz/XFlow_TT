using System;
using Core;
using Core.Rewards;
using UnityEngine;

namespace Gold
{
    [Serializable]
    public class GoldRewardActionAdd : IRewardAction
    {
        [SerializeField]
        private GoldRewardData _amount;

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

        private RewardModelAbstract<GoldRewardData> GetRewardModel(PlayerData playerData)
        {
            return playerData.GetRewardModel<GoldRewardData>();
        }
    }
}