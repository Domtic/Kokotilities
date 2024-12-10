using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ImprovedAudio;
using NaughtyAttributes;
public class TestAudio : MonoBehaviour
{
    public SoundData audio;

    [Button("Play test audio",EButtonEnableMode.Playmode)]
    public void InstantiateAudio()
    { 
        AudioManager.Instance.CreateSound().WithSoundData(audio).WithRandomPitch().WithPosition(Vector3.zero).Play(); 
    }
}
