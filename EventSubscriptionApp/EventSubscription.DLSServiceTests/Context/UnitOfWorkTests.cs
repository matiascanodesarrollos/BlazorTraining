using EventSubscription.DLSService;
using EventSubscription.DLSService.Context;
using EventSubscription.DLSService.RepositoryProviders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventSubscription.DLSServiceTests.Context
{
    [TestClass]
    public class UnitOfWorkTests
    {
        DbaContext _dbaContext;
        IEnumerable<Model.Event> _eventList;
        IEnumerable<Model.Action> _actiontList;
        IEnumerable<Model.ActionKind> _actiontKindList;
        IEnumerable<Model.Subscription> _subscriptionList;

        Mock<IEventRepositoryProvider> _eventRepositoryProvider;
        Mock<IActionRepositoryProvider> _actionRepositoryProvider;
        UnitOfWork _instance;

        [TestInitialize]
        public void Setup()
        {
            _dbaContext = InMemoryDBAProvider.GetDBAContext();
            _eventRepositoryProvider = new Mock<IEventRepositoryProvider>();
            _actionRepositoryProvider = new Mock<IActionRepositoryProvider>();

            #region Events

            _eventList = new List<Model.Event>
            {
                new Model.Event()
                {
                    Id = 1,
                    Description = "Description 1",
                    Title = "Title 1",
                    Message = "Message 1",
                    Date = DateTime.Now.AddDays(1),
                },
                new Model.Event()
                {
                    Id = 2,
                    Description = "Description 2",
                    Title = "Title 2",
                    Message = "Message 2",
                    Date = DateTime.Now.AddDays(2),
                },
                new Model.Event()
                {
                    Id = 3,
                    Description = "Description 3",
                    Title = "Title 3",
                    Message = "Message 3",
                    Date = DateTime.Now.AddDays(3),
                },
                new Model.Event()
                {
                    Id = 4,
                    Description = "Description 4",
                    Title = "Title 4",
                    Message = "Message 4",
                    Date = DateTime.Now.AddDays(4),
                },
            };

            foreach (var ev in _eventList)
            {
                _dbaContext.Events.Add(ev);
            }

            #endregion

            #region ActionKinds

            _actiontKindList = new List<Model.ActionKind>
            {
                new Model.ActionKind()
                {
                    Id = 1,
                    Description = "Action Kind 1",
                },
                new Model.ActionKind()
                {
                    Id = 2,
                    Description = "Action Kind 2",
                },
            };

            foreach (var ak in _actiontKindList)
            {
                _dbaContext.ActionKinds.Add(ak);
            }

            #endregion

            #region Subscriptions

            _subscriptionList = new List<Model.Subscription>
            {
                new Model.Subscription()
                {
                    Id = 1,                
                },
                new Model.Subscription()
                {
                    Id = 2,
                },
            };

            foreach (var s in _subscriptionList)
            {
                _dbaContext.Subscriptions.Add(s);
            }

            #endregion

            #region Actions

            _actiontList = new List<Model.Action>
            {
                new Model.Action()
                {
                    Id = 1,
                    Date = DateTime.Now.AddDays(1),
                    Kind = _actiontKindList.First(),
                    Subscription = _subscriptionList.First(),
                },
                new Model.Action()
                {
                    Id = 2,
                    Date = DateTime.Now.AddDays(2),
                    Kind = _actiontKindList.First(),
                },
                new Model.Action()
                {
                    Id = 3,
                    Date = DateTime.Now.AddDays(3),
                    Subscription = _subscriptionList.First(),
                },
                new Model.Action()
                {
                    Id = 4,
                    Date = DateTime.Now.AddDays(4),
                },
            };

            foreach (var a in _actiontList)
            {
                _dbaContext.Actions.Add(a);
            }

            #endregion

            _dbaContext.SaveChanges();

            _eventRepositoryProvider.Setup(x => x.CreateRepository(_dbaContext)).Returns(new EventRepository(_dbaContext));
            _actionRepositoryProvider.Setup(x => x.CreateRepository(_dbaContext)).Returns(new ActionRepository(_dbaContext));
            _instance = new UnitOfWork(_dbaContext, _eventRepositoryProvider.Object, _actionRepositoryProvider.Object);
        }

        [TestCleanup]
        public void Dispose()
        {
            _instance.Dispose();
        }

        [TestMethod]
        public async Task Remove_Action_ShouldDo()
        {
            int idToRemove = 1;
            _instance.Actions.Remove(await _instance.Actions.GetAsync(idToRemove));
            await _instance.SaveAsync();
            var databaseActions = await _instance.Actions.GetAllAsync();
            Assert.IsFalse(databaseActions.Any(x => x.Id == idToRemove));
        }

        [TestMethod]
        public async Task RemoveRange_Event_ShouldDo()
        {
            int expectedCount = 0;
            _instance.Events.RemoveRange(await _instance.Events.GetAllAsync());
            await _instance.SaveAsync();
            Assert.AreEqual(expectedCount, await _instance.Events.GetCountAsync());
        }

        [TestMethod]
        public async Task Add_Event_ShouldGenerateId()
        {
            int expectedCount = _eventList.Count() + 1;
            _instance.Events.Add(new Model.Event{ Date = DateTime.Now, Description = "New description",Message = "New message", Title = "New title" });
            await _instance.SaveAsync();
            Assert.AreEqual(expectedCount, await _instance.Events.GetCountAsync());
        }

        [TestMethod]
        public async Task AddRange_Action_ShouldGenerateId()
        {
            IEnumerable<Model.Action> actiontListToInsert = new List<Model.Action>
            {
                new Model.Action()
                {
                    Date = DateTime.Now.AddDays(1),
                    Kind = _actiontKindList.First(),
                    Subscription = _subscriptionList.First(),
                },
                new Model.Action()
                {
                    Date = DateTime.Now.AddDays(2),
                    Kind = _actiontKindList.First(),
                },
                new Model.Action()
                {
                    Date = DateTime.Now.AddDays(3),
                    Subscription = _subscriptionList.First(),
                },
            };
            int expectedCount = _actiontList.Count() + actiontListToInsert.Count();
            _instance.Actions.AddRange(actiontListToInsert);
            await _instance.SaveAsync();
            Assert.AreEqual(expectedCount, await _instance.Actions.GetCountAsync());
        }

        [TestMethod]
        public async Task Update_Event_ShoudDo()
        {
            int id = 1;
            string expectedNewDescription = "New desc";

            var ev = await _instance.Events.GetAsync(id);
            ev.Description = expectedNewDescription;
            await _instance.SaveAsync();

            ev = await _instance.Events.GetAsync(id);
            Assert.AreEqual(expectedNewDescription, ev.Description);
        }
    }
}
