using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace BP.Audipool.Editor
{
    [CustomEditor(typeof(AudipoolSource))]
    public class AudipoolSourceEditor : UnityEditor.Editor
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
