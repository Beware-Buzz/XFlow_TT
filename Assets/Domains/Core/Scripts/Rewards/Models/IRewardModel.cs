using System;

namespace Core.Rewards
{
    public interface IRewardModel
    {
        public event Action DataChanged;
    }
}