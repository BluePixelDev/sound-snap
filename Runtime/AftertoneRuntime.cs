using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.Pool;

[assembly: InternalsVisibleTo("BP.Aftertone.Tests")]

namespace BP.Aftertone
{
    [AddComponentMenu(" ")]
    internal sealed class AftertoneRuntime : MonoBehaviour
    {
        private const string RuntimeName = "AftertoneRuntime";
        private const string ToneSourceNameTemplate = "ToneSource #{0}";

        private int currentToken = 0;

        private static AftertoneRuntime instance;
        internal static AftertoneRuntime Instance => instance != null ? instance : CreateRuntimeInstance();

        private readonly List<AudioSource> sourcesPool = new();
        private readonly ObjectPool<ToneHandleInternal> handlePool = new(() =>
        {
            return new ToneHandleInternal();
        }, actionOnRelease: (tar) =>
        {
            tar.Dispose();
        });

        private readonly List<ToneHandleInternal> activeHandles = new();

        public int PoolSize => sourcesPool.Count;
        public int UsedCount => activeHandles.Count;

        internal static void Initialize()
        {
            if (instance == null)
            {
                instance = CreateRuntimeInstance();
            }
        }
        private static AftertoneRuntime CreateRuntimeInstance()
        {
            var obj = new GameObject(RuntimeName);
            var runtime = obj.AddComponent<AftertoneRuntime>();
            return runtime;
        }

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this);
            }
            InitializePool();
        }

        private void LateUpdate()
        {
            for (int i = activeHandles.Count - 1; i >= 0; i--)
            {
                activeHandles[i].Update();
            }
        }

        private void InitializePool()
        {
            var config = Aftertone.Config;
            for (int i = 0; i < config.InitialPoolSize; i++)
                CreateNewSource();
        }

        private AudioSource GetAvailableInstance()
        {
            var config = Aftertone.Config;
            for (int i = sourcesPool.Count - 1; i >= 0; i--)
            {
                var source = sourcesPool[i];
                if (!source.gameObject.activeSelf)
                    return source;
            }

            if (PoolSize >= config.MaxPoolSize)
            {
                var recyclable = FindRecyclableInstance();
                if (recyclable != null)
                {
                    recyclable.Stop();
                    return recyclable;
                }
            }

            return ExtendPoolAndRetrieveInstance();
        }
        private AudioSource FindRecyclableInstance()
        {
            ToneHandleInternal candidate = null;
            float oldestStartTime = 0;

            foreach (var handle in activeHandles)
            {
                if (candidate == null || handle.StartTime < oldestStartTime)
                {
                    oldestStartTime = handle.StartTime;
                    candidate = handle;
                }
            }

            return candidate.Source;
        }
        private AudioSource ExtendPoolAndRetrieveInstance()
        {
            var config = Aftertone.Config;
            int remainingCapacity = config.MaxPoolSize - PoolSize;
            int toCreate = Mathf.Min(remainingCapacity, config.PoolExpansionSize);

            AudioSource first = null;
            for (int i = 0; i < toCreate; i++)
            {
                var source = CreateNewSource();
                if (i == 0)
                    first = source;
            }
            return first;
        }
        private AudioSource CreateNewSource()
        {
            var go = new GameObject(string.Format(ToneSourceNameTemplate, sourcesPool.Count));

#if UNITY_EDITOR
            go.transform.SetParent(transform);
#endif
            go.SetActive(false);

            var source = go.AddComponent<AudioSource>();
            sourcesPool.Add(source);
            return source;
        }

        public ToneHandle Play(ToneAsset asset)
        {
            var instance = GetAvailableInstance();
            if (instance == null)
            {
                Debug.LogWarning("No available audio source to play tone.");
                return default;
            }

            return RequestHandle(instance, asset);
        }
        private ToneHandle RequestHandle(AudioSource src, ToneAsset asset)
        {
            var handleInternal = handlePool.Get();
            activeHandles.Add(handleInternal);

            currentToken++;
            handleInternal.Reset(src, asset, currentToken);
            handleInternal.OnStop(() =>
            {
                activeHandles.Remove(handleInternal);
                handlePool.Release(handleInternal);
            });

            return new ToneHandle(handleInternal, currentToken);
        }

        private void OnGUI()
        {
            if (!Aftertone.Config.Debug || sourcesPool == null)
                return;

            float maxSize = Aftertone.Config.MaxPoolSize;
            float usagePercent = maxSize > 0 ? UsedCount / maxSize : 0f;
            float poolPercent = maxSize > 0 ? sourcesPool.Count / maxSize : 0f;

            GUILayout.BeginArea(new Rect(10, 10, 320, 170));
            GUILayout.Label("<b>Aftertone Debug</b>", new GUIStyle(EditorStyles.label) { richText = true, fontSize = 13 });

            Rect sliderRect = GUILayoutUtility.GetRect(280, 20);
            EditorGUI.DrawRect(sliderRect, new Color(0.15f, 0.15f, 0.15f));

            Rect poolFillRect = new(sliderRect.x, sliderRect.y, sliderRect.width * poolPercent, sliderRect.height);
            EditorGUI.DrawRect(poolFillRect, new Color(0.3f, 0.6f, 1.0f));

            Rect usedFillRect = new(sliderRect.x, sliderRect.y, sliderRect.width * usagePercent, sliderRect.height);
            EditorGUI.DrawRect(usedFillRect, Color.green);

            GUIStyle centeredStyle = new(GUI.skin.label)
            {
                alignment = TextAnchor.MiddleCenter,
                normal = { textColor = Color.white },
                fontStyle = FontStyle.Bold
            };
            GUI.Label(sliderRect, $"{UsedCount} used / {sourcesPool.Count} in pool / {maxSize} max", centeredStyle);
            GUILayout.EndArea();
        }
    }
}
