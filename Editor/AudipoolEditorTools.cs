using UnityEditor;
using UnityEngine;

namespace BP.Audipool.Editor
{
    [InitializeOnLoad]
    public static class AudipoolEditorTools
    {
        static AudipoolEditorTools()
        {
            ConfigUtil.LoadOrCreateConfig(AudipoolConfig.ConfigPath, AudipoolConfig.ConfigName);
        }

        [MenuItem("CONTEXT/AudioSource/Convert To Audipool")]
        public static void BoxSource(MenuCommand command)
        {
            if (command.context is not AudioSource target)
                return;

            string path = EditorUtility.SaveFilePanelInProject("Save Asset", "Audipool", "asset", "");
            if (string.IsNullOrEmpty(path)) return;

            var newAsset = ScriptableObject.CreateInstance<AudioPreset>();
            newAsset.CopyFromSource(target);

            AssetDatabase.CreateAsset(newAsset, path);
            AssetDatabase.SaveAssets();

            var snapSource = target.gameObject.AddComponent<AudipoolSource>();
            snapSource.Asset = newAsset;
            target.clip = null;
            Object.DestroyImmediate(target);
        }

        [MenuItem("GameObject/Audio/Audipool Source")]
        public static void CreateAudipoolSource(MenuCommand menuCommand)
        {
            var go = new GameObject("Audipool Source", typeof(AudipoolSource));
            GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
            Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
            Selection.activeObject = go;
        }
    }
}
