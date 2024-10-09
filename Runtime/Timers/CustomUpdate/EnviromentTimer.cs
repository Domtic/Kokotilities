using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ImprovedTimers;
public class EnviromentTimer : MonoBehaviour
{

    //since the world is always slowed server side, technically the player shouldnt acre about these
    public static Action OnTick;
    private SlowedTimerTicker updateTicker;

    private void CustomUpdate()
    {
        OnTick?.Invoke();
    }

    private void OnDestroy()
    {
        if (updateTicker != null)
        {
            updateTicker.OnTick -= CustomUpdate;
            updateTicker.Dispose();
        }
      
    }

    internal void StartSlowedTimer(float m_SlowedTimerScale)
    {
        updateTicker = new SlowedTimerTicker(m_SlowedTimerScale);
        updateTicker.OnTick += CustomUpdate;
        updateTicker.Start();
    }

    public SlowedTimerTicker GetTimer() => updateTicker;
}
