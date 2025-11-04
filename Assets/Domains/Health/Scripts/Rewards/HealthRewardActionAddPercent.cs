using System;
using Core;
using Core.Rewards;
using UnityEngine;

namespace Health
{
    [Serializable]
    public class HealthRewardActionAddPercent: IRewardAction
    {
        [SerializeField]
        private int _percent;

        public bool CanExecute(PlayerData playerData)
        {
            return true;
        }

        public void Execute(PlayerData playerData)
        {
            var model = GetRewardModel(playerData);
            var data = model.RewardData;
            data.Amount += data.Amount * _percent / 100;
            model.UpdateData(data);
        }

        private RewardModelAbstract<HealthRewardData> GetRewardModel(PlayerData playerData)
        {
            return playerData.GetRewardModel<HealthRewardData>();
        }
    }
}