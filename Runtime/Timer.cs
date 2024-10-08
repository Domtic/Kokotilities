using System;
using UnityEngine;

namespace ImprovedTimers
{
    public abstract class Timer: IDisposable
    {
        private bool dissposed;
        ~Timer()
        {
            Dispose(false);
        }

        //Call dispose to ensure deregistration of the timer from the timerManager
        //when the consumer is done with the timer or being destoryed
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        
        protected virtual void Dispose(bool dispossing) {
            if (dissposed)
                return;
            if (dispossing)
                TimerManager.DeregisterTimer(this);

            dissposed = true;
        }
        public float CurrentTime { get; protected set; }
        public bool IsRunning { get; private set; }

        protected float m_InitialTime;

        public float Progress => Mathf.Clamp(CurrentTime / m_InitialTime, 0, 1);

        public Action OnTimerStart = delegate { };
        public Action OnTimerStop = delegate { };

        protected Timer(float value)
        {
            m_InitialTime = value;
        }

        public void Start()
        {
            CurrentTime = m_InitialTime;
            if(!IsRunning)
            {
                IsRunning = true;
                TimerManager.RegisterTimer(this);
                OnTimerStart?.Invoke();
            }
        }

        public void Stop()
        {
            if(IsRunning)
            {
                IsRunning = false;
                TimerManager.DeregisterTimer(this);
                OnTimerStop?.Invoke();
            }
        }

        public abstract void Tick();
        public abstract bool IsFinished { get; }

        public void Resume() => IsRunning = true;
        public void Pause() => IsRunning = false;

        public virtual void Reset() => CurrentTime = m_InitialTime;
        public virtual void Reset(float newTime)
        {
            m_InitialTime = newTime;
            Reset();
        }


    }
}