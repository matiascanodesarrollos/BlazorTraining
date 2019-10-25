using EventSubscription.DLSService;
using EventSubscription.DLSService.Context;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventSubscription.DLSServiceTests
{
    [TestClass]
    public class RepositoryTests
    {
        DbaContext _dbaContext;
        IEnumerable<Model.Event> _eventList;
        Repository<Model.Event> _instance;

        [TestInitialize]
        public void Setup()
        {
            _dbaContext = InMemoryDBAProvider.GetDBAContext();

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

            _dbaContext.SaveChanges();
            _instance = new Repository<Model.Event>(_dbaContext);
        }

        [TestCleanup]
        public void Dispose()
        {
            _dbaContext.Dispose();
        }

        [TestMethod()]
        public async Task GetAll_ShouldReturn()
        {
            var result = (await _instance.GetAllAsync()).ToList();
            var events = _eventList.ToList();

            for (int i = 0; i < events.Count(); i++)
            {
                Assert.IsTrue(events[i].Equals(result[i]));
            }
        }

        [TestMethod()]
        public async Task GetAll_Pagesize1_Page2_ShouldReturn()
        {
            var page = 2;
            var pageSize = 1;
            var result = (await _instance.GetAllAsync(page, pageSize)).ToList();
            var expectedList = _eventList
                .Skip(pageSize * (page - 1))
                .Take(pageSize)
                .ToList();

            for (int i = 0; i < expectedList.Count(); i++)
            {
                Assert.IsTrue(expectedList[i].Equals(result[i]));
            }
        }

        [TestMethod()]
        public async Task GetAll_Pagesize3_Page1_ShouldReturn()
        {
            var page = 1;
            var pageSize = 3;
            var result = (await _instance.GetAllAsync(page, pageSize)).ToList();
            var expectedList = _eventList
                .Skip(pageSize * (page - 1))
                .Take(pageSize)
                .ToList();

            for (int i = 0; i < expectedList.Count(); i++)
            {
                Assert.IsTrue(expectedList[i].Equals(result[i]));
            }
        }

        [TestMethod()]
        public async Task GetCount_ShouldReturn()
        {
            var result = await _instance.GetCountAsync();
            //Assert non-existence after deleting
            Assert.AreEqual(_eventList.Count(), result);
        }

        [TestMethod()]
        public void Insert_ShouldThowException_SameId()
        {
            Assert
                .ThrowsException<InvalidOperationException>(() =>
                _instance.Add(
                    new Model.Event() { Id = 1, Description = "Description X", Message = "Message X", Title = "Title X", Date = DateTime.Now })
                );
        }
        
    }
}
