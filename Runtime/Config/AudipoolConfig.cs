using System.Runtime.CompilerServices;
using UnityEngine;

[assembly: InternalsVisibleTo("BP.Audipool.Editor")]
[assembly: InternalsVisibleTo("BP.Audipool.Tests")]
[assembly: InternalsVisibleTo("BP.Audipool.Editor.Tests")]

namespace BP.Audipool
{
    internal sealed class AudipoolConfig : ScriptableObject
    {
        public const string ConfigName = "AudipoolConfig";
        public const string ConfigPath = "Resources/";

        public const int InitialPoolSizeMin = 0;
        public const int PoolExpansionSizeMin = 1;
        public const int MaxPoolSizeMin = 2;
        public const int MaxAliveTimeMin = 1;

        [SerializeField, Min(InitialPoolSizeMin)] private int initialPoolSize = 15;
        [SerializeField, Min(PoolExpansionSizeMin)] private int poolExpansionSize = 5;
        [SerializeField, Min(MaxPoolSizeMin)] private int maxPoolSize = 50;
        [SerializeField, Min(MaxAliveTimeMin)] private float maxAliveTime = 60;

        public static int InitialPoolSize => Mathf.Max(0, Config.initialPoolSize);
        public static int PoolExpansionSize => Mathf.Max(1, Config.poolExpansionSize);
        public static int MaxPoolSize => Mathf.Max(2, Config.maxPoolSize);
        public static float MaxAliveTime => Mathf.Max(2, Config.maxAliveTime);

        private static AudipoolConfig config;
        public static AudipoolConfig Config
        {
            get
            {
                if (!config)
                    config = ConfigUtil.LoadOrCreateConfig(ConfigPath, ConfigName);

                return config;
            }
        }

        private void OnValidate()
        {
            initialPoolSize = Mathf.Max(InitialPoolSizeMin, initialPoolSize);
            poolExpansionSize = Mathf.Max(PoolExpansionSizeMin, poolExpansionSize);
            maxPoolSize = Mathf.Max(MaxPoolSizeMin, maxPoolSize);
            maxAliveTime = Mathf.Max(MaxAliveTimeMin, maxAliveTime);
        }
    }
}
