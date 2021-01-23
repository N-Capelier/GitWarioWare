namespace RadioRTL
{
    /// <summary>
    /// Théo Valet
    /// </summary>

    namespace CrabGolf
    {
        public class DifficultyManager : TimedBehaviour
        {
            public ParrotBehaviour crabParrotBehavior;
            public CrabBehaviour normalCrabBehaviour;
            public float easySpeed;
            public float mediumSpeed;
            public float hardSpeed;
            public int easyCP;
            public int mediumCP;
            public int hardCP;
            public override void Start()
            {
                base.Start();

                switch (currentDifficulty)
                {
                    case Difficulty.EASY:
                        //insérer la logique quand le jeu est facile
                        normalCrabBehaviour.speed = easySpeed;
                        crabParrotBehavior.speed = easySpeed;
                        break;
                    case Difficulty.MEDIUM:
                        //insérer la logique quand le jeu est facile
                        normalCrabBehaviour.speed = mediumSpeed;
                        crabParrotBehavior.speed = mediumSpeed;
                        break;
                    case Difficulty.HARD:
                        //insérer la logique quand le jeu est facile
                        normalCrabBehaviour.speed = hardSpeed;
                        crabParrotBehavior.speed = hardSpeed;
                        break;
                }
            }
        }
    }
}
