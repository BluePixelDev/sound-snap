using System.Runtime.CompilerServices;
using UnityEngine;

[assembly: InternalsVisibleTo("BP.Aftertone.Editor")]

namespace BP.Aftertone
{
    public static class Aftertone
    {
        internal const string ConfigName = "AftertoneConfig";
        internal const string ConfigPath = "Resources/";

        private static AftertoneConfig snapConfig;

        /// <summary>
        /// Gets the active Aftertone configuration.
        /// Loaded automatically from Resources folder, created if missing.
        /// </summary>
        public static AftertoneConfig Config
        {
            get
            {
                if (!snapConfig)
                {
                    snapConfig = AftertoneUtil.LoadOrCreateConfig(ConfigPath, ConfigName);
                }

                return snapConfig;
            }
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void InitializeOnLoad()
        {
            snapConfig = AftertoneUtil.LoadOrCreateConfig(ConfigPath, ConfigName);
            AftertoneRuntime.Initialize();
        }

        /// <summary>
        /// Plays the specified tone asset at the world origin with default priority.
        /// </summary>
        /// <param name="asset">Tone asset to play.</param>
        /// <returns>Handle to control the playing tone instance, or null if failed.</returns>
        public static ToneHandle Play(ToneAsset asset) => AftertoneRuntime.Instance.Play(asset);
    }
}
