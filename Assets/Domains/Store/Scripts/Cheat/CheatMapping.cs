using System;
using Core.Rewards;
using UnityEngine;

namespace Store
{
    [Serializable]
    public class CheatMapping
    {
        [SerializeReference] public IRewardAction RewardAction;
        [SerializeReference] public IRewardDisplayer RewardDisplayer;
    }
}