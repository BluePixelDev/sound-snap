using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace BP.Audipool.Editor
{
    [CustomEditor(typeof(AudipoolRuntime))]
    public class AudipoolRuntimeEditor : UnityEditor.Editor
    {
        [SerializeField] private VisualTreeAsset treeAsset;
        public override VisualElement CreateInspectorGUI()
        {
            var root = new VisualElement();
            treeAsset.CloneTree(root);
            return root;
        }
    }
}
