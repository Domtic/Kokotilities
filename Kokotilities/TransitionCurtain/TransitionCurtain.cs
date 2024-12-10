using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using PrimeTween;
using NaughtyAttributes;
using ImprovedTimers;
    
public class TransitionCurtain : SingletonAsComponent<TransitionCurtain>
{
    public static TransitionCurtain Instance { get { return (TransitionCurtain)_Instance; } }
    public const float MAX_FADE_VALUE = 1.0f;
    public const float MIN_FADE_VALUE = 0.0f;

    public static Action OnTransitionScreenOff;
    public static Action OnAnySceneLoaded;
    [Header("Configuration")]
    [SerializeField]
    private TweenSettings _settings;
    [SerializeField]
    private bool _DoFakeDuration;
    [SerializeField, EnableIf(EConditionOperator.And, "_DoFakeDuration")]
    private float _FakeLoadTimeDuration;
    [SerializeField]
    private float _AdviceDuration;
    [SerializeField]
    private float _DotsAnimDuration;

    [Header("UI")]
    [SerializeField]
    private Image m_loadingProgress;
    [SerializeField]
    private TMP_Text m_LoadingText;
    [SerializeField]
    private TMP_Text m_AdviceText;
    [SerializeField]
    private Image m_BackgroundImage;
    [SerializeField]
    private CanvasGroup m_CanvasGroup;

    private Canvas m_CanvasRef;

    private UpdateTicker _LoadingSceneTimer;

    private void Awake()
    {
        m_CanvasRef = GetComponent<Canvas>();
        m_CanvasGroup.alpha = 0.99f;
    }

    public void BeginCurtainTransition(string _sceneName, Sprite _BackgroundImg = null)
    {
        m_BackgroundImage.sprite = null;
        m_BackgroundImage.color = Color.black;
        m_CanvasGroup.interactable = true;
        m_CanvasGroup.blocksRaycasts = true;
        m_AdviceText.text = "";
        m_LoadingText.text = "Loading . . .";


         Tween.Alpha(m_CanvasGroup, 1 , _settings.duration, _settings.ease).OnComplete(() => {
            m_BackgroundImage.sprite = _BackgroundImg;
            if (m_BackgroundImage.sprite != null)
                m_BackgroundImage.color = Color.white;

            _LoadingSceneTimer = new UpdateTicker();
            _LoadingSceneTimer.OnTimerStop += SceneFinishedLoading;
            _LoadingSceneTimer.OnTick += LoadingProgress;
            Action onSceneFinishedLoading = () => _LoadingSceneTimer.Stop();
            GameCordinator.Instance.GoToScene(_sceneName, onSceneFinishedLoading);
            _LoadingSceneTimer.Start();
          
            StartCoroutine(AnimLoadingText());
            StartCoroutine(ChangAdviceText());
        });
    }

    private void LoadingProgress()
    {
        m_loadingProgress.fillAmount = GameCordinator.Instance.LoadingData;
    }

    private void SceneFinishedLoading()
    {
        OnAnySceneLoaded?.Invoke();
        Tween.Alpha(m_CanvasGroup, 0, _settings.duration, _settings.ease).OnComplete(() => {
            _LoadingSceneTimer.Dispose();
            OnTransitionScreenOff?.Invoke();
            m_CanvasGroup.interactable = false;
            m_CanvasGroup.blocksRaycasts = false;
            Debug.Log("Game loop starts here");
            StopAllCoroutines();
        });
    }


    IEnumerator AnimLoadingText()
    {
        byte dotsCounter = 1;
        string dots = "";
        while (true)
        {
            dots += ". ";
            m_LoadingText.text = "Loading " + dots;
            yield return new WaitForSeconds(_DotsAnimDuration);

            dotsCounter++;
            if (dotsCounter > 3)
            {
                dotsCounter = 1;
                dots = "";
            }

        }
    }

    IEnumerator ChangAdviceText()
    {
        while (true)
        {
            m_AdviceText.text = "--Tips here";
            yield return new WaitForSeconds(_AdviceDuration);
           Debug.Log("Changing tip");
        }
    }

    public void ForceCurtainRemoval()
    {
        if(_LoadingSceneTimer != null)
            _LoadingSceneTimer.Dispose();

        OnTransitionScreenOff?.Invoke();
        m_CanvasGroup.interactable = false;
        m_CanvasGroup.blocksRaycasts = false;
        m_CanvasGroup.alpha = 0;

        StopAllCoroutines();
    }

    private void OnDestroy()
    {
        if(_LoadingSceneTimer != null)
        {
            _LoadingSceneTimer.OnTimerStop -= SceneFinishedLoading;
            _LoadingSceneTimer.OnTick -= LoadingProgress;
            _LoadingSceneTimer.Dispose();
        }
    }

    public int SetSortingOrder { set { m_CanvasRef.sortingOrder = value; } }
    
}