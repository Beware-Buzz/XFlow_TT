namespace Core.Rewards
{
    public interface IRewardDisplayer
    {
        public string GetRewardName();
        public string GetRewardAmount(PlayerData playerData);
    }
}