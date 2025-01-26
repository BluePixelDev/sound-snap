using System.IO;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

[assembly: InternalsVisibleTo("BP.Audipool.Editor")]
namespace BP.Audipool
{
    internal static class ConfigUtil
    {
        public static AudipoolConfig LoadOrCreateConfig(string configPath, string configName)
        {
            var config = Resources.Load<AudipoolConfig>(configName);
            if (config == null)
            {
                config = ScriptableObject.CreateInstance<AudipoolConfig>();
                SaveConfigAsset(config, configPath, configName);
            }
            return config;
        }

        private static void SaveConfigAsset(AudipoolConfig config, string configPath, string configName)
        {
#if UNITY_EDITOR
            string fullFolderPath = Path.Combine("Assets", configPath).TrimEnd('/');
            if (!AssetDatabase.IsValidFolder(fullFolderPath))
            {
                Directory.CreateDirectory(fullFolderPath);
                AssetDatabase.Refresh();
            }

            string assetPath = Path.Combine(fullFolderPath, configName + ".asset");
            AssetDatabase.CreateAsset(config, assetPath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
#endif
        }
    }
}
