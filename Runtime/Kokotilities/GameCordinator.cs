using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;
using NaughtyAttributes;
using ImprovedTimers;

public class GameCordinator : SingletonAsComponent<GameCordinator>
{
    public static GameCordinator Instance { get { return (GameCordinator)_Instance; } }

    public static Action<string> OnSceneChanged;
    public static Action OnInitializeControllers;
    public static Action OnAwakeTimerDone;

    private float CustomWaitTime = 0.1f;
    private bool IsSceneBeingLoaded;
    private bool IsSceneLoaded;
    private string CurrentSceneToLoad;
    private Action CurrentCallBack;
    private UpdateTicker LoadingProcess;
    private AsyncOperation loadOperation;
    public string GetCurrentScene()
    {
        return SceneManager.GetActiveScene().name;
    }

    public void GoToScene(string _sceneName, Action _callback = null)
    {
        LoadingProcess = new UpdateTicker();
        LoadingProcess.OnTick += LoadingSceneProcess;
        CurrentSceneToLoad = _sceneName;
        CurrentCallBack = _callback;
        IsSceneLoaded = false;
        IsSceneBeingLoaded = false;
        CustomWaitTime = 0.1f;
        LoadingProcess.Start();
        //Timing.RunCoroutine(LoadingSceneProcess(_sceneName, _callback));
    }



    private void LoadingSceneProcess()
    {
        if(!IsSceneBeingLoaded)
        {
            loadOperation = SceneManager.LoadSceneAsync(CurrentSceneToLoad);
            SceneManager.sceneLoaded += OnSceneLoaded;
            IsSceneBeingLoaded = true;
        }

      
        if(IsSceneLoaded)
        {
            CustomWaitTime -= Time.deltaTime;
            if(CustomWaitTime <= 0)
            {
                OnSceneFinishedLoading();
            }
        }

    }

    private void OnSceneFinishedLoading()
    {

        OnAwakeTimerDone?.Invoke();
        //Remove listeners
        LoadingProcess.OnTick -= LoadingSceneProcess;
        SceneManager.sceneLoaded -= OnSceneLoaded;
        LoadingProcess.Stop();
        LoadingProcess.Dispose();

        CurrentCallBack?.Invoke();

    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        IsSceneLoaded = true;
        OnSceneChanged?.Invoke(CurrentSceneToLoad);
        Debug.Log( "Scene loaded succesfully");
        Debug.Log( "start call sent");
        //Main systems that are not dependant of others should be subscribed to this one
        OnInitializeControllers?.Invoke();
    }

    public float LoadingData { get {

                return Mathf.Clamp01(loadOperation.progress / 1f);
        } }
}
