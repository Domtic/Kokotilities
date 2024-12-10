using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImprovedAudio
{
    public class SoundBuilder
    {
        readonly AudioManager audioManager;
        SoundData soundData;
        Vector3 position = Vector3.zero;
        bool randomPitch;

        public SoundBuilder(AudioManager audioManager)
        {
            this.audioManager = audioManager;
        }

        public SoundBuilder WithSoundData(SoundData soundData)
        {
            this.soundData = soundData;
            return this;
        }

        public SoundBuilder WithPosition(Vector3 position)
        {
            this.position = position;
            return this;
        }

        public SoundBuilder WithRandomPitch()
        {
            this.randomPitch = true;
            return this;
        }

        public void Play()
        {
            if (!audioManager.CanPlaySound(soundData)) return;

            SoundEmitter soundEmitter = audioManager.Get();
            soundEmitter.Initialize(soundData);
            soundEmitter.transform.position = position;
            soundEmitter.transform.parent = AudioManager.Instance.transform;

            if(randomPitch)
            {
                soundEmitter.WithRandomPitch();
            }

            if(soundData.frequentSound)
            {
                audioManager.frequentSoundeEmitters.Enqueue(soundEmitter);
            }
            soundEmitter.Play();
        }
    }

}