using System;
using UnityEngine;

namespace ImprovedTimers
{
    /// <summary>
    /// Timer that counts up from zero to infinity.  Great for measuring durations.
    /// </summary>
    public class UpdateTicker : Timer
    {
        public Action OnTick;
        public UpdateTicker() : base(0) { }

        public override void Tick()
        {
            if (IsRunning)
            {
                CurrentTime += Time.deltaTime;
                OnTick?.Invoke();
            }
        }

        public override bool IsFinished => false;

    }
}