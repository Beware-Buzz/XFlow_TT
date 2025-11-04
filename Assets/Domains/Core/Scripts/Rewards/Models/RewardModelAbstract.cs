using System;

namespace Core.Rewards
{
    public class RewardModelAbstract<T> : IRewardModel
    {
        public T RewardData { get; private set; }
        public event Action DataChanged;

        public RewardModelAbstract(T rewardData)
        {
            RewardData = rewardData;
        }

        public void UpdateData(T rewardData)
        {
            RewardData = rewardData;
            DataChanged?.Invoke();
        }
    }
}