using UnityEngine;

namespace BP.Audipool
{
    /// <summary>
    /// Represents <see cref="AudioSource"/> properties to be used when playing audio.
    /// </summary>
    [CreateAssetMenu(fileName = "New Audio Preset", menuName = "Audipool/Preset")]
    public sealed class AudioPreset : ScriptableObject
    {
        [SerializeField] internal AudioSourceData data = new();

        /// <summary>
        /// Copies properties from a <see cref="AudioSource"/> component.
        /// </summary>
        /// <param name="source">Properties copy target.</param>
        public void CopyFromSource(AudioSource source) => data.CopyFromSource(source);

        /// <summary>
        /// Applies stored properties to a target <see cref="AudioSource"/>
        /// </summary>
        /// <param name="source"></param>
        public void ApplyToSource(AudioSource source) => data.ApplyToSource(source);
    }
}
