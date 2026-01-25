using UnityEngine;
using UnityEngine.Audio;

namespace BP.Audipool
{
    [System.Serializable]
    public sealed class AudioSourceData
    {
        [SerializeField] private AudioResource resource;
        [SerializeField] private AudioMixerGroup mixerGroup;

        [Header("Audio Settings")]
        [SerializeField, Range(0, 256)] private int priority = 128;
        [SerializeField, Range(0f, 1f)] private float volume = 1f;
        [SerializeField, Range(0.5f, 3f)] private float pitch = 1f;
        [SerializeField, Range(-1f, 1f)] private float panStereo = 0f;
        [SerializeField, Range(0f, 1f)] private float spatialBlend = 0f;

        [Header("3D Settings")]
        [SerializeField, Range(0f, 5f)] private float dopplerLevel = 1f;
        [SerializeField, Range(0f, 360)] private float spread = 0;
        [SerializeField] private AudioRolloffMode volumeRolloff = AudioRolloffMode.Logarithmic;
        [SerializeField] private float minDistance = 1f;
        [SerializeField] private float maxDistance = 500f;

        public AudioResource Resource => resource;
        public AudioMixerGroup MixerGroup => mixerGroup;
        public int Priority
        {
            get => priority;
            set => priority = Mathf.Clamp(value, 0, 256);
        }
        public float Volume
        {
            get => volume;
            set => volume = Mathf.Clamp(value, 0, 1);
        }
        public float Pitch
        {
            get => pitch;
            set => pitch = Mathf.Clamp(value, 0.5f, 3f);
        }
        public float PanStereo
        {
            get => panStereo;
            set => panStereo = Mathf.Clamp(value, -1, 1);
        }
        public float SpatialBlend
        {
            get => spatialBlend;
            set => spatialBlend = Mathf.Clamp(value, 0, 1);
        }
        public float DopplerLevel
        {
            get => dopplerLevel;
            set => dopplerLevel = Mathf.Clamp(value, 0, 5);
        }
        public float Spread
        {
            get => spread;
            set => spread = Mathf.Clamp(value, 0, 360);
        }
        public AudioRolloffMode RolloffMode => volumeRolloff;
        public float MinDistance => minDistance;
        public float MaxDistance => maxDistance;

        public AudioSourceData()
        {
            priority = 128;
            volume = 1f;
            panStereo = 0f;
            spatialBlend = 0f;
            dopplerLevel = 1f;
            spread = 360;
            volumeRolloff = AudioRolloffMode.Logarithmic;
            minDistance = 1f;
            maxDistance = 500f;
        }

        public AudioSourceData(AudioSource source)
        {
            CopyFromSource(source);
        }

        public void CopyFromSource(AudioSource source)
        {
            if (source == null)
            {
                Debug.LogError("Cannot copy properties from null source");
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
            spread = source.spread;
            volumeRolloff = source.rolloffMode;
            minDistance = source.minDistance;
            maxDistance = source.maxDistance;
        }

        public void ApplyToSource(AudioSource source)
        {
            if (source == null)
            {
                Debug.LogError("Cannot apply properties to a null source");
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
            source.spread = spread;
            source.rolloffMode = volumeRolloff;
            source.minDistance = minDistance;
            source.maxDistance = maxDistance;
        }
    }
}
