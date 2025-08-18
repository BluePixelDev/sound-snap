using System;
using UnityEngine;

namespace BP.Aftertone
{
    /// <summary>
    /// A safe handle to a playing tone instance.
    /// </summary>
    public readonly struct ToneHandle
    {
        private readonly ToneHandleInternal handle;
        private readonly int tokenID;

        internal ToneHandle(ToneHandleInternal handle, int tokenID)
        {
            this.handle = handle;
            this.tokenID = tokenID;
        }

        /// <summary>
        /// Returns true if the handle is still valid (i.e. not disposed or reused).
        /// </summary>
        public bool IsValid => handle != null && handle.TokenID == tokenID;

        /// <summary>
        /// Stops playback of the tone and disposes the handle.
        /// </summary>
        public void Stop()
        {
            if (IsValid) handle.Stop();
        }

        /// <summary>
        /// Pauses playback of the tone if it's currently playing.
        /// </summary>
        public void Pause()
        {
            if (IsValid) handle.Pause();
        }

        /// <summary>
        /// Resumes playback of a paused tone.
        /// </summary>
        public void Resume()
        {
            if (IsValid) handle.Resume();
        }

        /// <summary>
        /// Sets the 3D position where the tone will be played from.
        /// </summary>
        /// <param name="position">The world position of the sound source.</param>
        /// <returns>The same <see cref="ToneHandle"/> instance for chaining.</returns>
        public ToneHandle At(Vector3 position)
        {
            if (IsValid) handle.At(position);
            return this;
        }

        /// <summary>
        /// Sets the 3D position based on the given transform.
        /// </summary>
        /// <param name="transform">A transform whose position will be used.</param>
        /// <returns>The same <see cref="ToneHandle"/> instance for chaining.</returns>
        public ToneHandle At(Transform transform)
        {
            if (IsValid) handle.At(transform);
            return this;
        }

        /// <summary>
        /// Tracks the position of a transform during playback.
        /// The tone source will follow the transform each frame.
        /// </summary>
        /// <param name="target">The transform to follow.</param>
        /// <returns>The same <see cref="ToneHandle"/> instance for chaining.</returns>
        public ToneHandle Track(Transform target)
        {
            if (IsValid) handle.Track(target);
            return this;
        }

        /// <summary>
        /// Registers a callback to be invoked when the tone finishes playing naturally.
        /// </summary>
        /// <param name="callback">The action to invoke on completion.</param>
        /// <returns>The same <see cref="ToneHandle"/> instance for chaining.</returns>
        public ToneHandle OnComplete(Action callback)
        {
            if (IsValid) handle.OnComplete(callback);
            return this;
        }

        /// <summary>
        /// Registers a callback to be invoked when the tone playback stops,
        /// either naturally when the audio finishes or manually when stopped.
        /// </summary>
        /// <param name="callback">The action to invoke on stop.</param>
        /// <returns>The same <see cref="ToneHandle"/> instance for chaining.</returns>
        public ToneHandle OnStop(Action callback)
        {
            if (IsValid) handle.OnStop(callback);
            return this;
        }

        public static implicit operator bool(ToneHandle h) => h.IsValid;
    }
}
