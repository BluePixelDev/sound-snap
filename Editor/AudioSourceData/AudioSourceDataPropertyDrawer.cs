using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace BP.Audipool.Editor
{
    [CustomPropertyDrawer(typeof(AudioSourceData))]
    internal class AudioSourceDataPropertyDrawer : PropertyDrawer
    {
        [SerializeField] private VisualTreeAsset treeAsset;
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var root = new VisualElement();
            treeAsset.CloneTree(root);
            return root;
        }
    }
}
