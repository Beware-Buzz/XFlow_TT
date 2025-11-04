using UnityEngine;

namespace Core.Rewards
{
    [CreateAssetMenu(menuName = "Scriptable Objects/RewardActionHolder")]
    public class RewardActionHolder : ScriptableObject
    {
        [SerializeReference] public IRewardAction RewardAction;
    }
}