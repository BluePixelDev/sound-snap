using System;
using UnityEngine;

namespace BP.Audipool
{
    internal struct SourceSlot
    {
        public AudioSource source;
        public int generation;
        public float startTime;
        public Action onCompleteAction;

        public readonly void Play()
        {
            if (source != null)
            {
                source.Play();
            }
        }

        public void Stop()
        {
            if (source != null)
            {
                source.Stop();
                source.gameObject.SetActive(false);
            }

            onCompleteAction?.Invoke();
            onCompleteAction = null;
        }

        internal readonly bool IsActive => source.gameObject.activeSelf;
        internal readonly bool IsPlaying => source.isPlaying;
        internal readonly float TimeAlive => Time.time - startTime;

        internal readonly void SetPosition(Vector3 position) => source.transform.position = position;
    }

    [DefaultExecutionOrder(-100)]
    internal sealed class AudipoolRuntime : MonoBehaviour
    {
        private SourceSlot[] slots = Array.Empty<SourceSlot>();

        private void Awake()
        {
            ExpandPool(AudipoolConfig.InitialPoolSize);
        }

        public AudioHandle Get()
        {
            int index = GetAvailableSlotIndex();
            if (index == -1) return default;

            ref var slot = ref slots[index];
            slot.generation = (slot.generation == int.MaxValue) ? 1 : slot.generation + 1;

            slot.startTime = Time.time;
            slot.onCompleteAction = null;
            slot.source.gameObject.SetActive(true);

            return new AudioHandle(index, slot.generation);
        }

        private void LateUpdate()
        {
            for (int i = 0; i < slots.Length; i++)
            {
                var slot = slots[i];
                if (!slot.IsActive) continue;

                if (!slot.IsPlaying || slot.TimeAlive > AudipoolConfig.MaxAliveTime)
                    slot.Stop();
            }
        }

        private int GetAvailableSlotIndex()
        {
            for (int i = 0; i < slots.Length; i++)
                if (!slots[i].source.gameObject.activeSelf) return i;

            if (slots.Length < AudipoolConfig.MaxPoolSize)
            {
                int oldSize = slots.Length;
                ExpandPool(AudipoolConfig.PoolExpansionSize);
                return oldSize;
            }

            int oldest = 0;
            for (int i = 1; i < slots.Length; i++)
                if (slots[i].startTime < slots[oldest].startTime) oldest = i;

            slots[oldest].Stop();
            return oldest;
        }

        private void ExpandPool(int count)
        {
            int current = slots.Length;
            int next = Mathf.Min(current + count, AudipoolConfig.MaxPoolSize);
            if (next <= current) return;

            Array.Resize(ref slots, next);
            for (int i = current; i < next; i++)
            {
                var go = new GameObject($"Source #{i}");
                go.transform.SetParent(transform);
                go.SetActive(false);
                slots[i].source = go.AddComponent<AudioSource>();
            }
        }

        internal bool IsHandleValid(AudioHandle handle)
        {
            var index = handle.slotIndex;
            var generation = handle.generation;
            return index >= 0 && index < slots.Length && slots[index].generation == generation;
        }

        internal SourceSlot GetSlot(int i) => slots[i];
    }
}
