using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImprovedTimers
{
    /// <summary>
    /// Timer that executes every X seconds
    /// </summary>
    public class RepeaterTimer : Timer
    {
        public float TickEverySecond { get; private set; }
        float timeThreshold;

        public RepeaterTimer(float _tickEveryXSeconds) : base(_tickEveryXSeconds) {
            SetTimeThreshold(_tickEveryXSeconds);
        }

        public Action OnTreshHoldReached;
        public override bool IsFinished => false;

        public override void Tick()
        {
            if (!IsRunning)
                return;

            CurrentTime += Time.deltaTime;
            timeThreshold += Time.deltaTime;

            if (timeThreshold >= TickEverySecond)
            {
                CurrentTime -= timeThreshold;
                timeThreshold = 0;
                OnTreshHoldReached?.Invoke();
            }
        }

        void SetTimeThreshold(float ticksPerSecond)
        {
            TickEverySecond = ticksPerSecond;
            timeThreshold = 0;
        }
    }

}