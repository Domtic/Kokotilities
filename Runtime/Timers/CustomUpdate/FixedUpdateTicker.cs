using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImprovedTimers
{
    /// <summary>
    /// Fixed update timer that can be stopped
    /// </summary>
    public class FixedUpdateTicker : Timer
    {
        public Action OnTick;
        private readonly float slowFactor = 20f;
        public FixedUpdateTicker() : base(0) { }

        private float timer = 0;
        public override void Tick()
        {
            if (IsRunning)
            {
                timer += Time.deltaTime;

                while (timer >= Time.fixedDeltaTime *slowFactor)
                {
                    timer -= Time.fixedDeltaTime * slowFactor; 
                    CurrentTime -= Time.fixedDeltaTime * slowFactor; 
                    OnTick?.Invoke(); 
                }
            }
        }

        public override bool IsFinished => false;
    }
}
