using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace BP.Audipool.Editor
{
    [CustomEditor(typeof(AudioPreset))]
    public class AudioPresetEditor : UnityEditor.Editor
    {
        [SerializeField] private VisualTreeAsset dropzoneAsset;
        [SerializeField] private VisualTreeAsset treeAsset;

        private AudioPreset asset;
        private AudioSource previewSource;

        private void OnEnable()
        {
            asset = (AudioPreset)target;
            EditorApplication.update += EditorUpdate;
        }

        private void OnDestroy()
        {
            DestroyPreviewSource();
            EditorApplication.update -= EditorUpdate;

        }

        private void EditorUpdate()
        {
            if (previewSource && !previewSource.isPlaying)
            {
                DestroyPreviewSource();
            }
        }

        public override VisualElement CreateInspectorGUI()
        {
            var root = new VisualElement();
            treeAsset.CloneTree(root);
            SetupButtons(root);
            SetupDropzone(root);
            return root;
        }

        public void SetupButtons(VisualElement root)
        {
            var previewButton = root.Q<Button>("preview-button");

            previewButton.clicked += () =>
            {
                PreviewAudio();
            };

            var createButton = root.Q<Button>("create-button");
            createButton.clicked += () =>
            {
                var newSourceObject = new GameObject("New Source");
                var audioSource = newSourceObject.AddComponent<AudioSource>();
                EditorGUIUtility.PingObject(audioSource);
                Selection.activeObject = newSourceObject;
                asset.ApplyToSource(audioSource);
            };
        }

        public void SetupDropzone(VisualElement root)
        {
            var dropzoneArea = root.Q("dropzone-area");
            var dropzoneLabel = root.Q<Label>("dropzone-label");
            var defaultLabelText = dropzoneLabel.text;

            dropzoneArea.RegisterCallback<DragUpdatedEvent>(evt =>
            {
                DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
            });

            dropzoneArea.RegisterCallback<DragEnterEvent>(evt =>
            {
                dropzoneLabel.text = "Drop here!";
            });

            dropzoneArea.RegisterCallback<DragLeaveEvent>(evt =>
            {
                dropzoneLabel.text = defaultLabelText;
            });

            dropzoneArea.RegisterCallback<DragPerformEvent>((callback) =>
            {
                dropzoneLabel.text = defaultLabelText;
                DragAndDrop.AcceptDrag();
                foreach (var obj in DragAndDrop.objectReferences)
                {
                    if (obj is GameObject gameObject)
                    {
                        CopyAudioSource(gameObject);
                        return;
                    }
                }
            });
        }
        public void CopyAudioSource(GameObject gameObject)
        {
            if (gameObject.TryGetComponent(out AudioSource audioSource))
            {
                Undo.RecordObject(target, "Copied audio source");
                asset.CopyFromSource(audioSource);
            }
        }

        private void DestroyPreviewSource()
        {
            if (previewSource)
            {
                DestroyImmediate(previewSource.gameObject);
                previewSource = null;
            }
        }
        private void PreviewAudio()
        {
            DestroyPreviewSource();
            GameObject previewGO = new("AudioPreview")
            {
                hideFlags = HideFlags.HideAndDontSave
            };

            AudioSource audioSource = previewGO.AddComponent<AudioSource>();
            asset.ApplyToSource(audioSource);
            audioSource.spatialBlend = 0;

            audioSource.Play();
        }
    }
}