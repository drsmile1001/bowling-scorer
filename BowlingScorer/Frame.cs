namespace BowlingScorer
{
    public class Frame
    {
        public const int MAX_FRAME = 10;
        public int Number { get; private set; }

        public int? FirstRoll { get; private set; }

        public int? SecordRoll { get; private set; }

        public int? ThirdRoll { get; private set; }

        public Frame(int number)
        {
            Number = number;
        }

        public bool IsRollComplate
        {
            get
            {
                if (FirstRoll == null) return false;
                if (Number != MAX_FRAME)
                {
                    return IsStrike || FirstRoll.HasValue && SecordRoll.HasValue;
                }
                else
                {
                    if (IsStrike || IsSpire)
                    {
                        return SecordRoll.HasValue && ThirdRoll.HasValue;
                    }
                    else
                    {
                        return SecordRoll.HasValue;
                    }
                }
            }
        }

        public void Roll(int pins)
        {
            if (IsRollComplate)
            {
                throw new InvalidOperationException();
            }

            if(FirstRoll == null)
            {
                FirstRoll = pins;
                return;
            }

            if(SecordRoll == null)
            {
                SecordRoll = pins;
                return;
            }

            if(ThirdRoll == null)
            {
                ThirdRoll = pins;
                return;
            }

            throw new InvalidOperationException();
        }

        public IEnumerable<int> Rolls
        {
            get
            {
                if (FirstRoll.HasValue)
                    yield return FirstRoll.Value;
                if (SecordRoll.HasValue)
                    yield return SecordRoll.Value;
                if (ThirdRoll.HasValue)
                    yield return ThirdRoll.Value;
            }
        }

        public bool IsStrike => FirstRoll == Game.MAX_PINS;
        public bool IsSpire => FirstRoll.HasValue 
            && SecordRoll.HasValue 
            && FirstRoll.Value + SecordRoll.Value == Game.MAX_PINS;
    }
}