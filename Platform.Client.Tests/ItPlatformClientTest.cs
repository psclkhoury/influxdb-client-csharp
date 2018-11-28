using InfluxData.Platform.Client.Client;
using InfluxData.Platform.Client.Domain;
using NUnit.Framework;
using Task = System.Threading.Tasks.Task;

namespace Platform.Client.Tests
{
    public class ItPlatformClientTest : AbstractItClientTest
    {
        [Test]
        public async Task Health()
        {
            Health health = await PlatformClient.Health();

            Assert.IsNotNull(health);
            Assert.IsTrue(health.IsHealthy());
            Assert.AreEqual("howdy y'all", health.Message);
        }

        [Test]
        public async Task NotRunningInstance()
        {
            PlatformClient clientNotRunning = PlatformClientFactory.Create("http://localhost:8099");
            Health health = await clientNotRunning.Health();

            Assert.IsNotNull(health);
            Assert.IsFalse(health.IsHealthy());
            Assert.AreEqual("Connection refused", health.Message);

            await clientNotRunning.Close();
        }
    }
}