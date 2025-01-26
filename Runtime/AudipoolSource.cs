using UnityEngine;

namespace BP.Audipool
{
    [AddComponentMenu("Audipool/Source")]
    public class AudipoolSource : MonoBehaviour
    {
        [SerializeField] private AudioPreset toneAsset;
        [SerializeField] private bool follow;
        [SerializeField] private bool playOnAwake;

        private AudioHandle audioHandle;

        public AudioPreset Asset
        {
            get => toneAsset;
            set => toneAsset = value;
        }

        public bool PlayOnStart
        {
            get => playOnAwake;
            set => playOnAwake = value;
        }

        private void Awake()
        {
            if (playOnAwake)
            {
                Play();
            }
        }

        /// <summary>
        /// Plays the tone associated with the current <see cref="AudioPreset"/>.
        /// Stops any previously playing tone.
        /// </summary>
        public void Play()
        {
            audioHandle.Stop();
            PlaySource(Asset);
        }

        private void Update()
        {
            if (follow && audioHandle.IsValid)
            {
                audioHandle.SetPosition(transform.position);
            }
        }

        /// <summary>
        /// Plays the tone once without storing a handle or stopping previous playback.
        /// </summary>
        public void PlayOneShot() => PlaySource(Asset);

        private void PlaySource(AudioPreset preset)
        {
            audioHandle = Audipool.Get()
                .ApplyPreset(preset)
                .SetPosition(transform.position)
                .Play();
        }

        /// <summary>
        /// Stops the currently playing tone, if any.
        /// </summary>
        public void Stop() => audioHandle.Stop();
    }
}
