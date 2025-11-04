using Core.Rewards;

namespace Store
{
    public class BundleModel
    {
        public string TitleText;
        public IRewardAction[] Cost;
        public IRewardAction[] Prize;
    }
}