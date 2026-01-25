using NUnit.Framework;

namespace BP.Audipool.Editor.Tests
{
    public class AudipoolConfigEditorTests
    {
        [Test]
        public void AudipoolConfig_Exists()
        {
            Assert.NotNull(AudipoolConfig.Config);
        }
    }
}
