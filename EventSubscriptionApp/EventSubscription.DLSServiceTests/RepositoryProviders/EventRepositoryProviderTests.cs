using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EventSubscription.DLSService.RepositoryProviders.Tests
{
    [TestClass()]
    public class EventRepositoryProviderTests
    {
        [TestMethod()]
        public void CreateRepository_ShouldCreateCorrectType()
        {
            EventRepositoryProvider actionRepositoryProvider = new EventRepositoryProvider();
            Assert.IsTrue(actionRepositoryProvider.CreateRepository(null) is EventRepository);
        }
    }
}