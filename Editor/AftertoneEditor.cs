using UnityEditor;
using UnityEngine;

namespace BP.Aftertone.Editor
{
    [InitializeOnLoad]
    public static class AftertoneEditor
    {
        static AftertoneEditor()
        {
            AftertoneUtil.LoadOrCreateConfig(Aftertone.ConfigPath, Aftertone.ConfigName);
        }

        [MenuItem("CONTEXT/AudioSource/Convert To Aftertone")]
        public static void BoxSource(MenuCommand command)
        {
            if (command.context is not AudioSource target)
                return;

            string path = EditorUtility.SaveFilePanelInProject("Save Asset", "ToneAsset", "asset", "");
            if (string.IsNullOrEmpty(path)) return;

            var newAsset = ScriptableObject.CreateInstance<ToneAsset>();
            newAsset.CopyFromSource(target);

            AssetDatabase.CreateAsset(newAsset, path);
            AssetDatabase.SaveAssets();

            var snapSource = target.gameObject.AddComponent<AftertoneSource>();
            snapSource.Asset = newAsset;
            target.clip = null;
            Object.DestroyImmediate(target);
        }

        [MenuItem("GameObject/Audio/Aftertone Source")]
        public static void CreateAftertoneSource(MenuCommand menuCommand)
        {
            var go = new GameObject("Aftertone Source", typeof(AftertoneSource));
            GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
            Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
            Selection.activeObject = go;
        }
    }
}
