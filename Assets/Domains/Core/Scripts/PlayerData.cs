using System;
using System.Collections.Generic;
using Core.Rewards;

namespace Core
{
    public class PlayerData
    {
        public event Action DataChanged;
        
        private Dictionary<Type, IRewardData> _rewardsStorage = new ();
        private Dictionary<Type, IRewardModel> _rewardModels = new ();

        public void Init()
        {
            LoadRewards();
        }

        public RewardModelAbstract<T> GetRewardModel<T>() where T: class, IRewardData, new()
        {
            if (_rewardModels.TryGetValue(typeof(T), out var rewardModel))
            {
                return rewardModel as RewardModelAbstract<T>;
            }

            var model = new RewardModelAbstract<T>(GetReward<T>());
            model.DataChanged += RaiseDataChanged;
            _rewardModels.Add(typeof(T), model);
            return model;
        }
        
        public void Dispose()
        {
            foreach (var rewardModel in _rewardModels.Values)
            {
                rewardModel.DataChanged -= RaiseDataChanged;
            }
        }

        private void RaiseDataChanged()
        {
            DataChanged?.Invoke();
        }

        private void LoadRewards()
        {
            //_rewards = new List<IRewardData>();
        }

        private T GetReward<T>() where T : class, IRewardData, new()
        {
            if (_rewardsStorage.TryGetValue(typeof(T), out var reward))
            {
                return reward as T;
            }

            var newReward = new T();
            _rewardsStorage[typeof(T)] = newReward;
            return newReward;
        }
    }
}