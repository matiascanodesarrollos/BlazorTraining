using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EventSubscription.DLSService.RepositoryProviders.Tests
{
    [TestClass()]
    public class ActionRepositoryProviderTests
    {
        [TestMethod()]
        public void CreateRepository_ShouldCreateCorrectType()
        {
            ActionRepositoryProvider actionRepositoryProvider = new ActionRepositoryProvider();
            Assert.IsTrue(actionRepositoryProvider.CreateRepository(null) is ActionRepository);
        }
    }
}