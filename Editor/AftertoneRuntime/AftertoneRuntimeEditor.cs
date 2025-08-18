using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace BP.Aftertone.Editor
{
    [CustomEditor(typeof(AftertoneRuntime))]
    public class AftertoneRuntimeEditor : UnityEditor.Editor
    {
        [SerializeField] private VisualTreeAsset treeAsset;
        public override VisualElement CreateInspectorGUI()
        {
            var runtime = (AftertoneRuntime)target;

            var root = new VisualElement();
            treeAsset.CloneTree(root);
            var poolSizeProgress = root.Q<ProgressBar>("progress-bar-pool-size");
            var usedCountProgress = root.Q<ProgressBar>("progress-bar-used-count");

            void UpdateUI()
            {
                float maxSize = Aftertone.Config.MaxPoolSize;
                float usagePercent = maxSize > 0 ? runtime.UsedCount / maxSize : 0f;
                float poolPercent = maxSize > 0 ? runtime.PoolSize / maxSize : 0f;

                poolSizeProgress.value = poolPercent;
                usedCountProgress.value = usagePercent;
                usedCountProgress.title = $"{runtime.UsedCount} used / {runtime.PoolSize} in pool / {maxSize} max";
            }

            root.RegisterCallback<AttachToPanelEvent>(evt =>
            {
                EditorApplication.update += UpdateUI;
            });

            root.RegisterCallback<DetachFromPanelEvent>(evt =>
            {
                EditorApplication.update -= UpdateUI;
            });

            UpdateUI();
            return root;
        }
    }
}
