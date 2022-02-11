using FluentAssertions;
using System;
using Xunit;

namespace BowlingScorer.Test
{
    public class FrameTest
    {
        [Fact]
        public void 計分格狀態_尚未投擲()
        {
            var frame = new Frame(1);

            frame.IsRollComplate.Should().BeFalse();
            frame.Rolls.Should().BeEquivalentTo(Array.Empty<int>());
            frame.IsStrike.Should().BeFalse();
            frame.IsSpire.Should().BeFalse();
        }

        [Fact]
        public void 計分格狀態_投擲一次半倒_未完成計分格()
        {
            var frame = new Frame(1);
            
            frame.Roll(1);

            frame.IsRollComplate.Should().BeFalse();
            frame.Rolls.Should().BeEquivalentTo(new[] { 1 });
            frame.IsStrike.Should().BeFalse();
            frame.IsSpire.Should().BeFalse();
        }

        [Fact]
        public void 計分格狀態_非10局投擲全倒_完成計分格()
        {
            var frame = new Frame(1);

            frame.Roll(10);

            frame.IsRollComplate.Should().BeTrue();
            frame.Rolls.Should().BeEquivalentTo(new[] { 10 });
            frame.IsStrike.Should().BeTrue();
            frame.IsSpire.Should().BeFalse();
        }

        [Fact]
        public void 計分格狀態_非10局投擲半倒後捕中_完成計分格()
        {
            var frame = new Frame(1);

            frame.Roll(1);
            frame.Roll(9);

            frame.IsRollComplate.Should().BeTrue();
            frame.Rolls.Should().BeEquivalentTo(new[] { 1, 9 });
            frame.IsStrike.Should().BeFalse();
            frame.IsSpire.Should().BeTrue();
        }

        [Fact]
        public void 計分格狀態_非10局投擲兩次半倒_完成計分格()
        {
            var frame = new Frame(1);

            frame.Roll(1);
            frame.Roll(2);

            frame.IsRollComplate.Should().BeTrue();
            frame.Rolls.Should().BeEquivalentTo(new[] { 1, 2 });
            frame.IsStrike.Should().BeFalse();
            frame.IsSpire.Should().BeFalse();
        }

        [Fact]
        public void 計分格狀態_10局投擲全倒_未完成計分格()
        {
            var frame = new Frame(10);
            
            frame.Roll(10);

            frame.IsRollComplate.Should().BeFalse();
            frame.Rolls.Should().BeEquivalentTo(new[] { 10 });
            frame.IsStrike.Should().BeTrue();
            frame.IsSpire.Should().BeFalse();
        }

        [Fact]
        public void 計分格狀態_10局投擲全倒後投1顆_未完成計分格()
        {
            var frame = new Frame(10);

            frame.Roll(10);
            frame.Roll(1);

            var result = frame.IsRollComplate;

            result.Should().BeFalse();
            frame.Rolls.Should().BeEquivalentTo(new[] { 10, 1 });
            frame.IsStrike.Should().BeTrue();
            frame.IsSpire.Should().BeFalse();
        }

        [Fact]
        public void 計分格狀態_10局投擲全倒後投2顆_完成計分格()
        {
            var frame = new Frame(10);

            frame.Roll(10);
            frame.Roll(1);
            frame.Roll(2);

            frame.IsRollComplate.Should().BeTrue();
            frame.Rolls.Should().BeEquivalentTo(new[] { 10, 1, 2 });
            frame.IsStrike.Should().BeTrue();
            frame.IsSpire.Should().BeFalse();
        }

        [Fact]
        public void 計分格狀態_10局投擲半倒後捕中_未完成計分格()
        {
            var frame = new Frame(10);

            frame.Roll(1);
            frame.Roll(9);

            frame.IsRollComplate.Should().BeFalse();
            frame.Rolls.Should().BeEquivalentTo(new[] { 1, 9 });
            frame.IsStrike.Should().BeFalse();
            frame.IsSpire.Should().BeTrue();
        }

        [Fact]
        public void 計分格狀態_10局投擲半倒後捕中再投_完成計分格()
        {
            var frame = new Frame(10);

            frame.Roll(1);
            frame.Roll(9);
            frame.Roll(9);

            frame.IsRollComplate.Should().BeTrue();
            frame.Rolls.Should().BeEquivalentTo(new[] { 1, 9, 9 });
            frame.IsStrike.Should().BeFalse();
            frame.IsSpire.Should().BeTrue();
        }

        [Fact]
        public void 計分格狀態_10局投擲半倒再半倒_完成計分格()
        {
            var frame = new Frame(10);

            frame.Roll(1);
            frame.Roll(2);

            frame.IsRollComplate.Should().BeTrue();
            frame.Rolls.Should().BeEquivalentTo(new[] { 1, 2 });
            frame.IsStrike.Should().BeFalse();
            frame.IsSpire.Should().BeFalse();
        }
    }
}
