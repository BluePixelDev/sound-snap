using UnityEngine;

namespace BP.Aftertone
{
    /// <summary>
    /// An Aftertone componenent for playing audio using a <see cref="ToneAsset"/>.
    /// </summary>
    [AddComponentMenu("Aftertone/Aftertone Source")]
    public class AftertoneSource : MonoBehaviour
    {
        [SerializeField] private ToneAsset toneAsset;
        [SerializeField] private bool follow;
        [SerializeField] private bool playOnStart;

        private ToneHandle toneHandle;

        public ToneAsset Asset
        {
            get => toneAsset;
            set => toneAsset = value;
        }

        public bool PlayOnStart
        {
            get => playOnStart;
            set => playOnStart = value;
        }

        private void Start()
        {
            if (playOnStart)
            {
                Play();
            }
        }

        /// <summary>
        /// Plays the tone associated with the current <see cref="Asset"/>.
        /// Stops any previously playing tone.
        /// </summary>
        public void Play()
        {
            toneHandle.Stop();
            toneHandle = PlaySource(Asset);
        }

        /// <summary>
        /// Plays the tone once without storing a handle or stopping previous playback.
        /// </summary>
        public void PlayOneShot() => PlaySource(Asset);

        private ToneHandle PlaySource(ToneAsset asset)
        {
            var handle = Aftertone.Play(asset).At(transform);
            if (follow)
                handle.Track(transform);
            return handle;
        }

        /// <summary>
        /// Stops the currently playing tone, if any.
        /// </summary>
        public void Stop() => toneHandle.Stop();
    }
}
