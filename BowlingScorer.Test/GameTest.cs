using FluentAssertions;
using Xunit;

namespace BowlingScorer.Test;

public class GameTest
{
    [Fact]
    public void 查詢局分數_未完成失誤局_跳出例外()
    {
        var game = new Game();
        game.Roll(2);

        var action = () => game.GetFrameScore(1);

        action.Should().Throw<FrameNotCompleteException>();
    }

    [Fact]
    public void 查詢局分數_已完成失誤局_得到分數()
    {
        var game = new Game();
        game.Roll(2);
        game.Roll(6);

        var score = game.GetFrameScore(1);

        score.Should().Be(8);
    }

    [Fact]
    public void 查詢局分數_未完成計分的補中局_跳出例外()
    {
        var game = new Game();
        game.Roll(2);
        game.Roll(8);

        var action = () => game.GetFrameScore(1);

        action.Should().Throw<ScoringNotComplateException>();
    }

    [Fact]
    public void 查詢局分數_已完成計分的補中局_得到分數()
    {
        var game = new Game();
        game.Roll(2);
        game.Roll(8);
        game.Roll(8);

        var score = game.GetFrameScore(1);

        score.Should().Be(18);
    }

    [Fact]
    public void 查詢局分數_未完成計分的全倒局_跳出例外()
    {
        var game = new Game();
        game.Roll(10);

        var action = () => game.GetFrameScore(1);

        action.Should().Throw<ScoringNotComplateException>();
    }

    [Fact]
    public void 查詢局分數_未完成計分的全倒局2_跳出例外()
    {
        var game = new Game();
        game.Roll(10);
        game.Roll(1);

        var action = () => game.GetFrameScore(1);

        action.Should().Throw<ScoringNotComplateException>();
    }

    [Fact]
    public void 查詢局分數_完成計分的全倒局_得到分數()
    {
        var game = new Game();
        game.Roll(10);
        game.Roll(1);
        game.Roll(2);

        var score = game.GetFrameScore(1);

        score.Should().Be(13);
    }
}