namespace Core.Rewards
{
    public interface IRewardAction
    {
        public bool CanExecute(PlayerData currentBalance);
        public void Execute(PlayerData currentBalance);
    }
}