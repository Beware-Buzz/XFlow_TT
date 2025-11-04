using System;
using Core.Rewards;
using UnityEngine;

namespace Vip
{
    [Serializable]
    public class VipRewardData : IRewardData, ISerializationCallbackReceiver
    {
        [SerializeField]
        private string durationString;
        
        public TimeSpan Duration;
        
        public void OnBeforeSerialize()
        {
            durationString = Duration.ToString();
        }

        public void OnAfterDeserialize()
        {
            if (!TimeSpan.TryParse(durationString, out Duration))
            {
                Duration = TimeSpan.Zero;
            }
        }
    }
}