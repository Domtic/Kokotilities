using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class AnimationEvent 
{
    public string eventName;
    public UnityEvent OnAnimationEvent;
}
