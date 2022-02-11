namespace BowlingScorer;
public class Game
{
    public const int MIN_PINS = 1;
    public const int MAX_PINS = 10;
    private readonly List<Frame> _frames = new();

    public Game()
    {
        _frames.Add(CurrentFrame);
    }

    public Frame CurrentFrame { get; private set; } = new(1);

    public bool IsComplate { get; private set; }

    private void NextFrame()
    {
        CurrentFrame = new(CurrentFrame.Number + 1);
        _frames.Add(CurrentFrame);
    }

    public void Roll(int downPins)
    {
        if (IsComplate)
            throw new InvalidOperationException();
        CurrentFrame.Roll(downPins);
        if (CurrentFrame.IsRollComplate)
        {
            if (CurrentFrame.Number < Frame.MAX_FRAME)
                NextFrame();
            else
                IsComplate = true;
        }
    }

    public int GetFrameScore(int frame)
    {
        var found = _frames.SingleOrDefault(x => x.Number == frame);
        if(found == null) throw new InvalidOperationException();
        if (!found.IsRollComplate) throw new FrameNotCompleteException();
        if (found.IsStrike || found.IsSpire)
        {
            var scoringPins = GetRollPinsBeforeFrame(frame).Take(3).ToArray();
            if (scoringPins.Length != 3) throw new ScoringNotComplateException();
            return scoringPins.Sum();
        }
        else
        {
            return found.Rolls.Sum();
        }
    }

    public IEnumerable<int> GetRollPinsBeforeFrame(int frame)
    {
        while (true)
        {
            var found = _frames.SingleOrDefault(x => x.Number == frame);
            if (found == null) break;
            foreach (var roll in found.Rolls)
            {
                yield return roll;
            }
            frame++;
        }
    }
}

public class FrameNotCompleteException : Exception { }

public class ScoringNotComplateException : Exception { }