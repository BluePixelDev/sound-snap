using System.IO;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

[assembly: InternalsVisibleTo("BP.Aftertone.Editor")]
namespace BP.Aftertone
{
    internal static class AftertoneUtil
    {
        public static AftertoneConfig LoadOrCreateConfig(string configPath, string configName)
        {
            var config = Resources.Load<AftertoneConfig>(configName);
            if (config == null)
            {
                config = ScriptableObject.CreateInstance<AftertoneConfig>();
                SaveConfigAsset(config, configPath, configName);
            }
            return config;
        }

        private static void SaveConfigAsset(AftertoneConfig config, string configPath, string configName)
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
