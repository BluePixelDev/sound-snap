using NUnit.Framework;
using UnityEngine;

namespace BP.Aftertone.Tests
{
    public class ToneAssetTests
    {

        [Test(Description = "Tests if properties are correctly copied from source to asset")]
        public void CopyFromSource_CopiesAudioSourcePropertiesCorrectly()
        {
            var obj = new GameObject("TestObject");
            var source = obj.AddComponent<AudioSource>();

            source.volume = 0.75f;
            source.pitch = 1.25f;
            source.dopplerLevel = 1.15f;

            var asset = ScriptableObject.CreateInstance<ToneAsset>();
            asset.CopyFromSource(source);

            AssertSourceAndAssetPropertiesMatch(source, asset);

            // Clean up
            Object.DestroyImmediate(asset);
            Object.DestroyImmediate(obj);
        }

        [Test(Description = "Tests when we copy from source and apply to another that copied properties are correct on asset and the target source")]
        public void CopyToSource_CopiesToneAssetPropertiesCorrectly()
        {
            var obj = new GameObject("TestObject");
            var sourceA = obj.AddComponent<AudioSource>();
            var sourceB = obj.AddComponent<AudioSource>();

            sourceA.volume = 0.75f;
            sourceA.pitch = 1.25f;
            sourceA.dopplerLevel = 1.15f;

            var asset = ScriptableObject.CreateInstance<ToneAsset>();
            asset.CopyFromSource(sourceA);
            AssertSourceAndAssetPropertiesMatch(sourceA, asset);

            asset.ApplyToSource(sourceB);
            AssertSourcesPropertiesMatch(sourceA, sourceB);

            // Cleanup
            Object.DestroyImmediate(asset);
            Object.DestroyImmediate(obj);
        }

        [Test(Description = "Tests if ToneAsset has correct default values that match those of default AudioSource.")]
        public void DefaultProperties_AssetHasCorrectDefaultProperties()
        {
            var obj = new GameObject("TestObject");
            var source = obj.AddComponent<AudioSource>();

            var asset = ScriptableObject.CreateInstance<ToneAsset>();
            AssertSourceAndAssetPropertiesMatch(source, asset);
        }

        //==== UTILITIES ====
        public void AssertSourceAndAssetPropertiesMatch(AudioSource source, ToneAsset asset)
        {
            Assert.AreEqual(source.resource, asset.Resource, "Resource should match.asset");
            Assert.AreEqual(source.outputAudioMixerGroup, asset.MixerGroup, "Mixer group should match asset.");
            Assert.AreEqual(source.priority, asset.Priority, "Priority should match asset.");
            Assert.AreEqual(source.volume, asset.Volume, "Volume should match asset.");
            Assert.AreEqual(source.pitch, asset.Pitch, "Pitch should match asset.");
            Assert.AreEqual(source.panStereo, asset.PanStereo, "Pan Stereo should match asset.");
            Assert.AreEqual(source.spatialBlend, asset.SpatialBlend, "Spatial blend should match asset.");
            Assert.AreEqual(source.dopplerLevel, asset.DopplerLevel, "Dopler level should match asset.");
            Assert.AreEqual(source.minDistance, asset.MinDistance, "Min Distance should match asset.");
            Assert.AreEqual(source.maxDistance, asset.MaxDistance, "Max Distance should match asset.");
            Assert.AreEqual(source.rolloffMode, asset.RolloffMode, "Rolloff Mode should match asset.");
        }

        public void AssertSourcesPropertiesMatch(AudioSource source, AudioSource asset)
        {
            Assert.AreEqual(source.resource, asset.resource, "Resource should match.asset");
            Assert.AreEqual(source.outputAudioMixerGroup, asset.outputAudioMixerGroup, "Mixer group should match asset.");
            Assert.AreEqual(source.priority, asset.priority, "Priority should match asset.");
            Assert.AreEqual(source.volume, asset.volume, "Volume should match asset.");
            Assert.AreEqual(source.pitch, asset.pitch, "Pitch should match asset.");
            Assert.AreEqual(source.panStereo, asset.panStereo, "Pan Stereo should match asset.");
            Assert.AreEqual(source.spatialBlend, asset.spatialBlend, "Spatial blend should match asset.");
            Assert.AreEqual(source.dopplerLevel, asset.dopplerLevel, "Dopler level should match asset.");
            Assert.AreEqual(source.minDistance, asset.minDistance, "Min Distance should match asset.");
            Assert.AreEqual(source.maxDistance, asset.maxDistance, "Max Distance should match asset.");
            Assert.AreEqual(source.rolloffMode, asset.rolloffMode, "Rolloff Mode should match asset.");
        }
    }
}
