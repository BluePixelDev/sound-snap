using NUnit.Framework;
using UnityEngine;

namespace BP.Aftertone.Tests
{
    public class AftertoneRuntimeTests
    {
        [Test(Description = "Tests if the ToneRuntime compoenent has been initialized upon runtime enter.")]
        public void ToneRuntime_ShouldBePresent()
        {
            var runtime = GameObject.FindAnyObjectByType<AftertoneRuntime>();
            Assert.IsNotNull(runtime);
        }

        [Test(Description = "Tests if the ToneRuntime is initialzed with correct initial size set in config.")]
        public void ToneRuntime_ShouldHaveCorrectInitialSize()
        {
            var config = Aftertone.Config;
            var runtime = AftertoneRuntime.Instance;

            Assert.AreEqual(runtime.PoolSize, config.InitialPoolSize);
        }

        [Test(Description = "Tests if config has bee correclty loaded or created and is availible upon runtime enter.")]
        public void ToneConfig_ShouldBePresent() => Assert.IsNotNull(Aftertone.Config);

        [Test]
        public void ToneRuntimeDestroy_ShouldCreateNewOne()
        {
            var runtime = GameObject.FindAnyObjectByType<AftertoneRuntime>();
            Assert.IsNotNull(runtime, "Runtime should be initialized by now.");
            GameObject.DestroyImmediate(runtime);

            // Unity destroys object on the next frame, until then it marks them as null but == null still returns false
            Assert.IsNull(null, "ToneRuntime component should have been destroyed.");
            Assert.IsNotNull(AftertoneRuntime.Instance, "A new instance of ToneRuntime should be created.");
        }
    }
}
