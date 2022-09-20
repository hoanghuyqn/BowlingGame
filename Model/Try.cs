namespace bowlingGame.Model
{
    public class Try
    {
        #region PROPERTIES
        public int tryScore { get; set; } = 0;
        #endregion

        #region CONSTRUTOR
        public Try()
        {
        }
        #endregion

        #region IMPLEMENTATION FUNCTION
        public void Roll(int noOfPins)
        {
            var vRandom = new Random();

            if (noOfPins > 0)
            {
                tryScore = vRandom.Next(0, noOfPins);
            }
        }
        #endregion
    }
}
