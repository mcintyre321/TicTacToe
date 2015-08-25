using System;

namespace TicTacToe.Tests
{
    class SelfAdvancingClock
    {
        private DateTimeOffset? _prev;

        public DateTimeOffset GetTime()
        {
            _prev = _prev?.AddSeconds(1) ?? DateTimeOffset.UtcNow;
            return _prev.Value;
        }
    }
}