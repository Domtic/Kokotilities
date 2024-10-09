using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImprovedTimers
{
    public class SlowedTimerTicker : Timer
    {
        public Action OnTick;
        private float slowFactor = 20f;
        public SlowedTimerTicker(float _slowFactor) : base(0)
        {
            slowFactor = _slowFactor;
        }

        private float timer = 0;
        private float slowedDeltaTime = 0;

        public float SlowedDeltaTime { get => slowedDeltaTime; }
        public float TimeSinceLastTick { get; private set; } = 0f;
        public float TimeUntilNextTick => (Time.fixedDeltaTime * slowFactor) - TimeSinceLastTick;
        public override void Tick()
        {
            if (IsRunning)
            {
                timer += Time.deltaTime;
                TimeSinceLastTick += Time.deltaTime;
                while (timer >= Time.fixedDeltaTime * slowFactor)
                {

                    slowedDeltaTime = Time.fixedDeltaTime * slowFactor;
                    timer -= Time.fixedDeltaTime * slowFactor;
                    TimeSinceLastTick = 0f;
                    CurrentTime -= Time.fixedDeltaTime * slowFactor;
                    OnTick?.Invoke();
                }
            }
        }

        public override bool IsFinished => false;

    }     
}
