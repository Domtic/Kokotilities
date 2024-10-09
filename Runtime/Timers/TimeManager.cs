using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace ImprovedTimers
{
    public static class TimerManager
    {
        static readonly List<Timer> timers = new();

        private static bool SystemPaused = false;
        public static void RegisterTimer(Timer timer) => timers.Add(timer);
        public static void DeregisterTimer(Timer timer) => timers.Remove(timer);

        public static void UpdateTimers()
        {
            if (SystemPaused)
                return;

            foreach (var timer in new List<Timer>(timers))
            {
                timer.Tick();
            }
        }

        public static void Clear() => timers.Clear();
        public static void PauseAllTimers() => SystemPaused = true;
        public static void UnPauseAllTimers() => SystemPaused = false;
    }

}