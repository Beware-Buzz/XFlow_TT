using System;
using Core;
using Core.Rewards;
using UnityEngine;

namespace Location
{
    [Serializable]
    public class LocationRewardActionSet : IRewardAction
    {
        [SerializeField]
        private LocationRewardData _data;

        public bool CanExecute(PlayerData playerData)
        {
            return true;
        }
        
        public void Execute(PlayerData playerData)
        {
            var model = GetRewardModel(playerData);
            var data = model.RewardData;
            data.Place = _data.Place;
            model.UpdateData(data);
        }

        private RewardModelAbstract<LocationRewardData> GetRewardModel(PlayerData playerData)
        {
            return playerData.GetRewardModel<LocationRewardData>();
        }
    }
}