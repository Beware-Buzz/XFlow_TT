using UnityEngine;

namespace Core.Rewards
{
    [CreateAssetMenu(menuName = "Scriptable Objects/BundleSettings")]
    public class BundleSettings : ScriptableObject
    {
        public string Text;
        public RewardActionHolder[] Cost;
        public RewardActionHolder[] Prize;
    }
}