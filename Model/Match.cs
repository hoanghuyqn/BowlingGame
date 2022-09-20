using bowlingGame.Const;

namespace bowlingGame.Model
{
    public class Match
    {
        #region PROPERITY
        public List<Frame> frames { get; set; }
        public int matchScore { get; set; }
        #endregion

        #region CONSTRUCTOR
        public Match(int nFrame)
        {
            frames = new List<Frame>(nFrame);
            for (int i = 0; i < nFrame; i++)
            {
                if (nFrame < GameConstants.Frame)
                    frames.Add(new Frame(GameConstants.Tries));
                else
                    frames.Add(new Frame(GameConstants.LastFrameTries));
            }

            matchScore = 0;
        }
        #endregion

        #region IMPLEMENT FUNCTION
        public void Roll(int nFrame, int nTry)
        {
            try
            {
                if (nFrame < frames.Count() && nTry < 2)
                {
                    frames[nFrame].Roll(nTry);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Play()
        {
            try
            {
                int nFrame = 0;
                while (nFrame < frames.Count())
                {
                    if (nFrame < 9)
                        NormalFrameRoll(nFrame);
                    else
                        LastFrameRoll(nFrame);

                    nFrame++;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Run in normal frame
        /// </summary>
        /// <param name="nFrame"></param>
        public void NormalFrameRoll(int nFrame)
        {
            var nTry = 0;
            var currentFrame = frames[nFrame];

            while (nTry < 2)
            {
                Roll(nFrame, nTry);
                if (frames[nFrame].isSpike)
                    break;
                nTry++;
            }
            matchScore += frames[nFrame].frameScore;

            if (nFrame > 0)
            {
                var previousFrame = frames[nFrame - 1];
                if (previousFrame.isSpare)
                {
                    previousFrame.frameScore += currentFrame.tries[0].tryScore;
                    matchScore += currentFrame.tries[0].tryScore;
                }

                if (previousFrame.isSpike)
                {
                    previousFrame.frameScore += currentFrame.frameScore;
                    matchScore += currentFrame.frameScore;
                }

                //special case if there are 2 previous spike
                if (nFrame > 1)
                {
                    var previousOfPreviousFrame = frames[nFrame - 2];
                    if (previousOfPreviousFrame.isSpike)
                    {
                        previousOfPreviousFrame.frameScore += currentFrame.frameScore;
                        matchScore += currentFrame.frameScore;
                    }
                }
            }
        }

        /// <summary>
        /// Run in last frame
        /// </summary>
        /// <param name="nFrame"></param>
        public void LastFrameRoll(int nFrame)
        {
            var nTry = 0;
            var currentFrame = frames[nFrame];
            bool isSpikeInLastFrame = false;

            SPIKE_IN_LAST_FRAME:
            while (nTry < GameConstants.Tries)
            {
                Roll(nFrame, nTry);
                nTry++;
            }
            matchScore += frames[nFrame].frameScore;

            if (nFrame > 0)
            {
                var previousFrame = frames[nFrame - 1];

                #region update frame score if the previous frame is Spare
                if (previousFrame.isSpare)
                {
                    previousFrame.frameScore += currentFrame.tries[0].tryScore;
                    matchScore += currentFrame.tries[0].tryScore;

                    Roll(nFrame, 2);
                    matchScore += frames[nFrame].frameScore;
                }
                #endregion

                #region update frame score if there are 1 previous spike
                if (previousFrame.isSpike)
                {
                    previousFrame.frameScore += currentFrame.frameScore;
                    matchScore += currentFrame.frameScore;
                }
                #endregion

                #region update frame score if there are 2 previous spike
                if (nFrame > 1)
                {
                    var previousOfPreviousFrame = frames[nFrame - 2];
                    if (previousOfPreviousFrame.isSpike)
                    {
                        if (!isSpikeInLastFrame)
                            previousOfPreviousFrame.frameScore += currentFrame.frameScore;

                        matchScore += currentFrame.frameScore;
                        if (!isSpikeInLastFrame)
                        {
                            isSpikeInLastFrame = true;
                            goto SPIKE_IN_LAST_FRAME;
                        }
                    }
                }
                #endregion
            }
        }
        #endregion
    }
}
