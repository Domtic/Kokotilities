using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ImprovedTimers;
public class GameInitializer : MonoBehaviour
{

    private StopWatchTimer BootstrapTimer;
    private bool DataLoaded = false;
    [SerializeField]
    private string SceneToLoad;
    private bool GameDataLoaded { get { return DataLoaded; } set {

            DataLoaded = value;
            if(value)
            {
                BootstrapTimer.Stop();
            }
        } }
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        BootstrapTimer = new StopWatchTimer();
        BootstrapTimer.OnTimerStart += StartLoading;
        BootstrapTimer.OnTimerStop += LoadMainMenu;
        BootstrapTimer.Start();
        //Timing.RunCoroutine(_InitStages());
    }

    private void OnDestroy()
    {
        if(BootstrapTimer != null)
        {
            BootstrapTimer.OnTimerStart -= StartLoading;
            BootstrapTimer.OnTimerStop -= LoadMainMenu;
            BootstrapTimer.Dispose();
        }
    }

    private void LoadMainMenu()
    {
        Debug.Log("Phase 2: Loading Main menu, LOADING TIME:" + BootstrapTimer.CurrentTime);
        BootstrapTimer.OnTimerStart -= StartLoading;
        BootstrapTimer.OnTimerStop -= LoadMainMenu;
        BootstrapTimer.Dispose();
        TransitionCurtain.Instance.BeginCurtainTransition(SceneToLoad);

    }

    private void StartLoading()
    {
        //Data loading
        //for now its  a fake "loading"
        Debug.Log( "Phase 1: Loading game data");

        Action OnGameDataFinishedLoading = () => GameDataLoaded = true;
        //DataPersistentManager.OnFinishedLoading += OnGameDataFinishedLoading;
        // DataPersistentManager.Instance.OnInitGameLoading();

        //instatly proc the event that triggers the next step
        OnGameDataFinishedLoading?.Invoke();

    }

}
