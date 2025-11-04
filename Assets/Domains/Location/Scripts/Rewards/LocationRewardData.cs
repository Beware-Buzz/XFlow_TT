using System;
using Core.Rewards;

namespace Location
{
    [Serializable]
    public class LocationRewardData : IRewardData
    {
        public const string DEFAULT = "Нигде";
        public string Place = DEFAULT;
    }
}