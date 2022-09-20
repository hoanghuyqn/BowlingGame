using bowlingGame.Const;

namespace bowlingGame.Model
{
    public class Frame
    {

        #region PROPERTIES
        public List<Try> tries { get; set; }
        public int remainingPin { get; set; } = GameConstants.Pin; // Set default
        public int frameScore { get; set; }
        public bool isSpare { get; set; } = false;
        public bool isSpike { get; set; } = false;

        #endregion

        #region CONSTRUCTOR
        public Frame(int nTry)
        {
            tries = new List<Try>(nTry);
            for (int i = 0; i < nTry; i++)
            {
                tries.Add(new Try());
            }
            frameScore = 0;
        }
        #endregion

        #region IMPLEMENT FUNCTION
        public void Roll(int nTry)
        {
            try
            {
                if (nTry < tries.Count())
                {
                    tries[nTry].Roll(remainingPin);
                    //if (frameScore <= 10) frameScore += tries[nTry].tryScore
                    frameScore += tries[nTry].tryScore;
                    remainingPin = remainingPin - tries[nTry].tryScore;

                    // Checking current roll is Spike or Spare after rolling
                    IsSpike(nTry);
                }

                IsSpare(nTry);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region PRIVATE METHODS
        private void IsSpare(int nTry)
        {
            if (frameScore == 10 && nTry == 1)
                isSpare = true;
        }

        private void IsSpike(int nTry)
        {
            if (frameScore == 10 && nTry == 0)
                isSpike = true;
        }
        #endregion
    }
}
