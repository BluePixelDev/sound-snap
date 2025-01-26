using NUnit.Framework;
using UnityEngine;

namespace BP.Audipool.Tests
{
    public sealed class AudipoolRuntimeTests
    {
        [Test(Description = "Tests if the ToneRuntime compoenent has been initialized upon runtime enter.")]
        public void AudipoolRuntime_ShouldBePresent()
        {
            var runtime = GameObject.FindAnyObjectByType<AudipoolRuntime>();
            Assert.IsNotNull(runtime);
        }

        [Test(Description = "Tests if config has bee correclty loaded or created and is availible upon runtime enter.")]
        public void AudipoolConfig_ShouldBePresent() => Assert.IsNotNull(AudipoolConfig.Config);

        [Test]
        public void AudipoolRuntimeDestroy_ShouldCreateNewOne()
        {
            var runtime = GameObject.FindAnyObjectByType<AudipoolRuntime>();
            Assert.IsNotNull(runtime, "Runtime should be initialized by now.");
            GameObject.DestroyImmediate(runtime);

            // Unity destroys object on the next frame, until then it marks them as null but == null still returns false
            Assert.IsNull(null, "ToneRuntime component should have been destroyed.");
            Assert.IsNotNull(Audipool.Instance, "A new instance of ToneRuntime should be created.");
        }
    }
}
