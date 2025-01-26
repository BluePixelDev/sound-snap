using NUnit.Framework;
using UnityEngine;

namespace BP.Audipool.Tests
{
    public sealed class AudioSourceDataTests
    {
        private GameObject testObject;
        private AudioSource source;

        [SetUp]
        public void SetUp()
        {
            testObject = new GameObject("TestAudioObject");
            source = testObject.AddComponent<AudioSource>();
        }

        [TearDown]
        public void TearDown()
        {
            Object.DestroyImmediate(testObject);
        }

        [Test]
        public void CopyFromSource_CorrectlyStoresValues()
        {
            // Arrange
            source.priority = 10;
            source.volume = 0.5f;
            source.pitch = 2.0f;
            source.spatialBlend = 1.0f;
            source.minDistance = 5f;
            source.rolloffMode = AudioRolloffMode.Linear;

            // Act
            var settings = new AudioSourceData(source);

            // Assert
            Assert.AreEqual(10, settings.Priority);
            Assert.AreEqual(0.5f, settings.Volume);
            Assert.AreEqual(2.0f, settings.Pitch);
            Assert.AreEqual(1.0f, settings.SpatialBlend);
            Assert.AreEqual(5f, settings.MinDistance);
            Assert.AreEqual(AudioRolloffMode.Linear, settings.RolloffMode);
        }

        [Test]
        public void ApplyToSource_CorrectlyUpdatesSource()
        {
            // Arrange
            var settings = new AudioSourceData(source)
            {
                Priority = 50,
                Volume = 0.25f,
                Pitch = 1.5f,
                SpatialBlend = 0.5f
            };

            // Act
            settings.ApplyToSource(source);

            // Assert
            Assert.AreEqual(50, source.priority);
            Assert.AreEqual(0.25f, source.volume);
            Assert.AreEqual(1.5f, source.pitch);
            Assert.AreEqual(0.5f, source.spatialBlend);
        }

        [Test]
        public void Setters_ClampValuesCorrecty()
        {
            // Arrange
            var settings = new AudioSourceData(source)
            {
                // Act
                Volume = 5.0f,     // Should clamp to 1.0
                Pitch = 0.1f,      // Should clamp to 0.5
                Priority = 500,    // Should clamp to 256
                PanStereo = -10f  // Should clamp to -1.0
            };

            // Assert
            Assert.AreEqual(1.0f, settings.Volume);
            Assert.AreEqual(0.5f, settings.Pitch);
            Assert.AreEqual(256, settings.Priority);
            Assert.AreEqual(-1.0f, settings.PanStereo);
        }

        [Test]
        public void ApplyToSource_NullSource_DoesNotThrow()
        {
            // Arrange
            var settings = new AudioSourceData(source);

            // Act & Assert
            Assert.DoesNotThrow(() => settings.ApplyToSource(null));
        }
    }
}
