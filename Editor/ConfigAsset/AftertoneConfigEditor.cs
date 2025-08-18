using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace BP.Aftertone.Editor
{
    [CustomEditor(typeof(AftertoneConfig))]
    public class AftertoneConfigEditor : UnityEditor.Editor
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
