using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace ImprovedAudio
{
    public class AudioManager : SingletonAsComponent<AudioManager>
    {
        public static AudioManager Instance { get { return (AudioManager)_Instance; } }

        private IObjectPool<SoundEmitter> _soundEmitterPool;
        public readonly List<SoundEmitter> activeSoundeEmitters = new();
        public readonly Queue<SoundEmitter>frequentSoundeEmitters = new();

        [SerializeField]
        private SoundEmitter soundEmitterPrefab;
        [SerializeField]
        private bool collectionCheck = true;
        [SerializeField]
        private int defaultCapacity = 10;
        [SerializeField]
        private int maxPoolSize = 100;
        [SerializeField]
        private int maxSoundInstances = 30;

        private void Start()
        {
            InitializePool();
        }

        public SoundBuilder CreateSound() => new SoundBuilder(this);

        public bool CanPlaySound(SoundData Data)
        {
            if (!Data.frequentSound) return true;

            if(frequentSoundeEmitters.Count >= maxSoundInstances && frequentSoundeEmitters.TryDequeue(out var soundEmitter))
            {
                try
                {
                    soundEmitter.Stop();
                    return true;
                }
                catch
                {
                    Debug.Log("Sound emitter is already released");
                }
                return false;
            }

            return true;
            //return !Counts.TryGetValue(Data, out var count) || count < maxSoundInstances;
        }
        public SoundEmitter Get() { return _soundEmitterPool.Get(); }
        public void ReturnToPool(SoundEmitter soundEmitter) { _soundEmitterPool.Release(soundEmitter); }

        private void InitializePool()
        {
            _soundEmitterPool = new ObjectPool<SoundEmitter>(
                 CreatedSoundEmitter,
                 OnTakeFromPool,
                 OnReturnedToPool,
                 OnDestrouPoolObject,
                 collectionCheck,
                 defaultCapacity,
                 maxPoolSize);
        }

        private void OnDestrouPoolObject(SoundEmitter obj)
        {
            Destroy(obj.gameObject);
        }
        private void OnReturnedToPool(SoundEmitter obj)
        {
            obj.gameObject.SetActive(false);
            activeSoundeEmitters.Remove(obj);
        }
        private void OnTakeFromPool(SoundEmitter obj)
        {
            obj.gameObject.SetActive(true);
            activeSoundeEmitters.Add(obj);
        }
        private SoundEmitter CreatedSoundEmitter()
        {
            var soundEmitter = Instantiate(soundEmitterPrefab);
            soundEmitter.gameObject.SetActive(false);
            return soundEmitter;
        }
    }

}
