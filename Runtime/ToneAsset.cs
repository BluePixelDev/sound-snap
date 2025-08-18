using UnityEngine;
using UnityEngine.Audio;

namespace BP.Aftertone
{
    /// <summary>
    /// Represents <see cref="AudioSource"/> properties to be used when playing audio.
    /// </summary>
    [CreateAssetMenu(fileName = "ToneAsset", menuName = "Aftertone/ToneAsset")]
    public sealed class ToneAsset : ScriptableObject
    {
        [SerializeField] private AudioResource resource;
        [SerializeField] private AudioMixerGroup mixerGroup;

        [Header("Audio Settings")]
        [SerializeField, Range(0, 256)] private int priority = 128;
        [SerializeField, Range(0f, 1f)] private float volume = 1f;
        [SerializeField, Range(0.5f, 3f)] private float pitch = 1f;
        [SerializeField, Range(-1f, 1f)] private float panStereo = 0f;
        [SerializeField, Range(0f, 1f)] private float spatialBlend = 0f;
        [SerializeField, Range(0f, 5f)] private float dopplerLevel = 1f;
        [SerializeField] private float minDistance = 1f;
        [SerializeField] private float maxDistance = 500f;
        [SerializeField] private AudioRolloffMode rolloffMode = AudioRolloffMode.Logarithmic;

        public AudioResource Resource => resource;
        public AudioMixerGroup MixerGroup => mixerGroup;
        public int Priority => priority;
        public float Volume => volume;
        public float Pitch => pitch;
        public float PanStereo => panStereo;
        public float SpatialBlend => spatialBlend;
        public float DopplerLevel => dopplerLevel;
        public float MinDistance => minDistance;
        public float MaxDistance => maxDistance;
        public AudioRolloffMode RolloffMode => rolloffMode;

        /// <summary>
        /// Copies properties from a <see cref="AudioSource"/> component.
        /// </summary>
        /// <param name="source">Properties copy target.</param>
        public void CopyFromSource(AudioSource source)
        {
            if (source == null)
            {
                Debug.LogError("Cannot copy properties from null source", this);
                return;
            }

            resource = source.resource;
            mixerGroup = source.outputAudioMixerGroup;
            priority = source.priority;
            volume = source.volume;
            pitch = source.pitch;
            panStereo = source.panStereo;
            spatialBlend = source.spatialBlend;
            dopplerLevel = source.dopplerLevel;
            minDistance = source.minDistance;
            maxDistance = source.maxDistance;
            rolloffMode = source.rolloffMode;
        }

        /// <summary>
        /// Applies stored properties to a target <see cref="AudioSource"/>
        /// </summary>
        /// <param name="source"></param>
        public void ApplyToSource(AudioSource source)
        {
            if (source == null)
            {
                Debug.LogError("Cannot apply properties to a null source", this);
                return;
            }

            source.resource = resource;
            source.outputAudioMixerGroup = mixerGroup;
            source.priority = priority;
            source.volume = volume;
            source.pitch = pitch;
            source.panStereo = panStereo;
            source.spatialBlend = spatialBlend;
            source.dopplerLevel = dopplerLevel;
            source.minDistance = minDistance;
            source.maxDistance = maxDistance;
            source.rolloffMode = rolloffMode;
        }
    }
}
