using EventSubscription.DLSService.Context;
using EventSubscription.DLSServiceTests;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventSubscription.DLSService.Tests
{
    [TestClass()]
    public class EventRepositoryTests
    {
        EventRepository _instance;
        DbaContext _dbaContext;
        IList<Model.Event> _eventList;

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

            foreach(var ev in _eventList)
            {
                _dbaContext.Events.Add(ev);
            }

            _dbaContext.SaveChanges();
            _instance = new EventRepository(_dbaContext);
        }

        [TestMethod()]
        public async Task Delete_ShouldRemoveFromDatabase()
        {
            int id = 1;
            //Assert existence before deleting
            Assert.IsTrue(await _dbaContext.Events.AnyAsync(x => x.Id == id));
            Model.Event eventToDelete = await _dbaContext.Events.FirstAsync(x => x.Id == id);
            await _instance.Delete(eventToDelete);
            //Assert non-existence after deleting
            Assert.IsFalse(await _dbaContext.Events.AnyAsync(x => x.Id == id));
        }

        [TestMethod()]
        public async Task GetAll_ShouldReturn()
        {
            var result = await _instance.GetAll();
            
            for(int i = 0; i < _eventList.Count; i++)
            {
                Assert.IsTrue(_eventList[i].Equals(result[i]));
            }
        }

        [TestMethod()]
        public async Task GetAll_Pagesize1_Page2_ShouldReturn()
        {
            var page = 2;
            var pageSize = 1;
            var result = await _instance.GetAll(page, pageSize);
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
            var result = await _instance.GetAll(page, pageSize);
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
            var result = await _instance.GetCount();
            //Assert non-existence after deleting
            Assert.AreEqual(_eventList.Count, result);
        }

        [TestMethod()]
        public async Task Insert_ShouldInsertToDatabase()
        {
            int expectedId = _eventList.Count + 1;
            await _instance.Insert(new Model.Event() { Id = expectedId, Description = "Description X", Message = "Message X", Title = "Title X", Date = DateTime.Now });
            //Assert non-existence after deleting
            Assert.IsTrue(await _dbaContext.Events.AnyAsync(x => x.Id == expectedId));
        }

        [TestMethod()]
        public async Task Insert_ShouldThowException_SameId()
        {
            await Assert
                .ThrowsExceptionAsync<InvalidOperationException>(() => 
                _instance.Insert(
                    new Model.Event() { Id = 1, Description = "Description X", Message = "Message X", Title = "Title X", Date = DateTime.Now })
                );            
        }

        [TestMethod()]
        public async Task Edit_ShouldEditDatabase()
        {
            int id = 1;
            string newDescription = "New description";
            var elementToEdit = await _dbaContext.Events.FindAsync(id);
            elementToEdit.Description = newDescription;
            await _instance.Edit(elementToEdit);

            var editedElement = await _dbaContext.Events.FindAsync(id);
            Assert.AreEqual(newDescription, editedElement.Description);

        }

    }
}