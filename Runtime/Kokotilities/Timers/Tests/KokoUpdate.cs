using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ImprovedTimers;
using System;

public class KokoUpdate : MonoBehaviour
{
    private UpdateTicker customUpdate;

    private void Awake()
    {
        customUpdate = new UpdateTicker();
        customUpdate.OnTick += CustomUpdate;
        customUpdate.Start();
    }

    private void OnDestroy()
    {
        customUpdate.Dispose();
    }
    
    public void Pause()
    {
        customUpdate.Pause();
    }

    public void Stop()
    {
        customUpdate.Stop();
    }

    public void Resume()
    {
        customUpdate.Resume();
    }

    private void CustomUpdate()
    {

    }
}
