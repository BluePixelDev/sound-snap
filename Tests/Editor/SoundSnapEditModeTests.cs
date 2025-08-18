using NUnit.Framework;

namespace BP.Aftertone.Editor.Tests
{
    public class SoundSnapEditModeTests
    {
        [Test]
        public void Config_IsLoadedOrCreated()
        {
            var config = Aftertone.Config;
            Assert.IsNotNull(config, "SnapConfig should not be null.");
        }

        [Test]
        public void ConfigValues_RespectMinimums()
        {
            var config = Aftertone.Config;

            Assert.GreaterOrEqual(config.InitialPoolSize, 0);
            Assert.GreaterOrEqual(config.PoolExpansionSize, 1);
            Assert.GreaterOrEqual(config.MaxPoolSize, 10);
        }
    }
}
