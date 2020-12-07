namespace Rewards
{
    public class RewardManager : Singleton<RewardManager>
    {
        public Reward[] allReward;

        private void Awake()
        {
            CreateSingleton();
        }
    }
}