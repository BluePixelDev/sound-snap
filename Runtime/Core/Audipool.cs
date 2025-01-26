using System.Runtime.CompilerServices;
using UnityEngine;

[assembly: InternalsVisibleTo("BP.Audipool.Tests")]
namespace BP.Audipool
{
    public sealed class Audipool
    {
        private static AudipoolRuntime instance;
        internal static AudipoolRuntime Instance
        {
            get
            {
                if (instance == null) Initialize();
                return instance;
            }
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            if (instance != null) return;

            var go = new GameObject("Audipool Runtime");
            GameObject.DontDestroyOnLoad(go);
            instance = go.AddComponent<AudipoolRuntime>();
        }

        //==== INTERNAL API ====
        internal static bool IsHandleValid(AudioHandle handle) => Instance.IsHandleValid(handle);
        internal static SourceSlot GetSlot(int i) => Instance.GetSlot(i);

        //==== PUBLIC API ====

        /// <summary> Reserves a slot for custom configuration. 
        /// Call <see cref="AudioHandle.Play"/> start. 
        /// Handles are automatically invalidated if they are not playing. 
        /// </summary>
        /// <returns>SourceHandle to chain commands.</returns>
        public static AudioHandle Get() => Instance.Get();

        /// <summary>
        /// Plays <see cref="AudioPreset"/>
        /// </summary>
        /// <returns>SourceHandle to chain commands.</returns>
        public static AudioHandle Play(AudioPreset asset) => Get().ApplyPreset(asset);
    }
}
