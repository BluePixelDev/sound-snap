using System;
using UnityEngine;
using UnityEngine.Audio;

namespace BP.Audipool
{
    public readonly struct AudioHandle
    {
        internal readonly int slotIndex;
        internal readonly int generation;

        internal AudioHandle(int index, int gen)
        {
            slotIndex = index;
            generation = gen;
        }

        public bool IsValid => Audipool.IsHandleValid(this);

        private bool TryGetSlot(out SourceSlot slot)
        {
            if (IsValid)
            {
                slot = Audipool.GetSlot(slotIndex);
                return true;
            }

            slot = default;
            return false;
        }

        public AudioHandle Play()
        {
            if (TryGetSlot(out var slot))
                slot.Play();

            return this;
        }

        public void Stop()
        {
            if (TryGetSlot(out var slot))
                slot.Stop();
        }

        public AudioHandle SetPosition(Vector3 position)
        {
            if (TryGetSlot(out var slot))
                slot.SetPosition(position);
            return this;
        }

        public AudioHandle ApplyPreset(AudioPreset asset)
        {
            if (asset == null) return this;

            if (TryGetSlot(out var slot))
                asset.ApplyToSource(slot.source);
            return this;
        }

        public AudioHandle SetResource(AudioResource resource)
        {
            if (TryGetSlot(out var slot))
                slot.source.resource = resource;
            return this;
        }

        public AudioHandle ApplySourceData(AudioSourceData settings)
        {
            if (TryGetSlot(out var slot))
                settings.ApplyToSource(slot.source);
            return this;
        }

        public AudioHandle SetPitch(float pitch)
        {
            if (TryGetSlot(out var slot))
                slot.source.pitch = pitch;
            return this;
        }

        public AudioHandle SetVolume(float volume)
        {
            if (TryGetSlot(out var slot))
                slot.source.volume = volume;
            return this;
        }

        public AudioHandle OnComplete(Action action)
        {
            if (TryGetSlot(out var slot))
                slot.onCompleteAction += action;
            return this;
        }
    }
}
