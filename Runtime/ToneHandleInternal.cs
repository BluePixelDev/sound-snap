using System;
using UnityEngine;

namespace BP.Aftertone
{
    internal sealed class ToneHandleInternal
    {
        public AudioSource Source { get; private set; }
        public int TokenID { get; private set; }
        public float StartTime { get; private set; }
        public bool IsPaused { get; private set; }

        private event Action OnCompleteAction;
        private event Action OnStoppedAction;

        private Transform trackTarget;
        private Vector3 trackOffset;

        internal void Reset(AudioSource source, ToneAsset asset, int token)
        {
            Source = source;
            TokenID = token;
            StartTime = Time.time;
            IsPaused = false;

            trackTarget = null;
            trackOffset = Vector3.zero;

            OnCompleteAction = null;
            OnStoppedAction = null;

            asset.ApplyToSource(Source);
            source.gameObject.SetActive(true);
            source.transform.position = Vector3.zero;
            source.Play();
        }

        internal void Dispose()
        {
            Source.Stop();
            Source.gameObject.SetActive(false);
            Source = null;

            TokenID = -1;
            trackTarget = null;

            OnCompleteAction = null;
            OnStoppedAction = null;
        }

        internal void Update()
        {
            if (IsPaused || Source == null) return;

            if (trackTarget != null || trackTarget.Equals(null))
                Source.transform.position = trackTarget.TransformPoint(trackOffset);

            if (!Source.isPlaying)
            {
                OnCompleteAction?.Invoke();
                Stop();
            }
        }

        public void Stop()
        {
            Source.Stop();
            OnStoppedAction?.Invoke();
        }

        public void Pause()
        {
            if (IsPaused || Source == null) return;
            Source.Pause();
            IsPaused = true;
        }

        public void Resume()
        {
            if (!IsPaused || Source == null) return;
            Source.UnPause();
            IsPaused = false;
        }

        public void At(Vector3 position)
        {
            if (Source == null) return;

            Source.transform.position = position;

            if (trackTarget != null)
                trackOffset = trackTarget.position - position;
        }

        public void At(Transform transform) => At(transform.position);

        public void Track(Transform target)
        {
            if (target == null || Source == null) return;

            trackTarget = target;
            trackOffset = Source.transform.position - target.position;
        }

        public void OnComplete(Action callback) => OnCompleteAction += callback;
        public void OnStop(Action callback) => OnStoppedAction += callback;
    }
}
