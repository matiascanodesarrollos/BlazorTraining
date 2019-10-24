using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using EventSubscription.DLSService.Context;
using EventSubscription.DLSServiceTests;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EventSubscription.DLSService.Tests
{
    [TestClass()]
    public class ActionRepositoryTests
    {
        ActionRepository _instance;
        DbaContext _dbaContext;
        IList<Model.Action> _actionList;
        Model.ActionKind _actionKind;
        Model.Subscription _subcription;

        [TestInitialize]
        public void Setup()
        {
            _dbaContext = InMemoryDBAProvider.GetDBAContext();
            _actionKind = new Model.ActionKind { Id = 1, Description = "Description 1" };
            _dbaContext.ActionKinds.Add(_actionKind);

            _subcription = new Model.Subscription { Id = 1 };
            _dbaContext.Subscriptions.Add(_subcription);

            _actionList = new List<Model.Action>
            {
                new Model.Action()
                {
                    Id = 1,
                    Date = DateTime.Now.AddDays(1)
                },
                new Model.Action()
                {
                    Id = 2,
                    Date = DateTime.Now.AddDays(2)
                },
                new Model.Action()
                {
                    Id = 3,
                    Date = DateTime.Now.AddDays(1)
                },
                new Model.Action()
                {
                    Id = 4,
                    Date = DateTime.Now.AddDays(2)
                },
            };

            foreach (var action in _actionList)
            {
                _dbaContext.Actions.Add(action);
            }

            _dbaContext.SaveChanges();
            _instance = new ActionRepository(_dbaContext);
        }

        [TestMethod()]
        public async Task Delete_ShouldRemoveFromDatabase()
        {
            int id = 1;
            //Assert existence before deleting
            Assert.IsTrue(await _dbaContext.Actions.AnyAsync(x => x.Id == id));
            Model.Action actionToDelete = await _dbaContext.Actions.FirstAsync(x => x.Id == id);
            await _instance.Delete(actionToDelete);
            //Assert non-existence after deleting
            Assert.IsFalse(await _dbaContext.Actions.AnyAsync(x => x.Id == id));
        }

        [TestMethod()]
        public async Task GetAll_ShouldReturn()
        {
            var result = await _instance.GetAll();

            for (int i = 0; i < _actionList.Count; i++)
            {
                Assert.IsTrue(_actionList[i].Equals(result[i]));
            }
        }

        [TestMethod()]
        public async Task GetAll_Pagesize1_Page3_ShouldReturn()
        {
            var page = 3;
            var pageSize = 1;
            var result = await _instance.GetAll(page, pageSize);
            var expectedList = _actionList
                .Skip(pageSize * (page - 1))
                .Take(pageSize)
                .ToList();

            for (int i = 0; i < expectedList.Count(); i++)
            {
                Assert.IsTrue(expectedList[i].Equals(result[i]));
            }
        }

        [TestMethod()]
        public async Task GetAll_Pagesize2_Page1_ShouldReturn()
        {
            var page = 1;
            var pageSize = 2;
            var result = await _instance.GetAll(page, pageSize);
            var expectedList = _actionList
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
            Assert.AreEqual(_actionList.Count, result);
        }

        [TestMethod()]
        public async Task Insert_ShouldInsertToDatabase()
        {
            int expectedId = _actionList.Count + 1;
            await _instance.Insert(new Model.Action() { Id = expectedId, Date = DateTime.Now, Kind = _actionKind, Subscription = _subcription });
            //Assert non-existence after deleting
            Assert.IsTrue(await _dbaContext.Actions.AnyAsync(x => x.Id == expectedId));
        }

        [TestMethod()]
        public async Task Insert_ShouldThowException_SameId()
        {
            await Assert
                .ThrowsExceptionAsync<InvalidOperationException>(() =>
                _instance.Insert(
                    new Model.Action() { Id = 1, Date = DateTime.Now })
                );
        }

        [TestMethod()]
        public async Task Insert_ShouldThowException_NonExistentActionKind()
        {
            await Assert
                .ThrowsExceptionAsync<InvalidOperationException>(() =>
                _instance.Insert(
                    new Model.Action() { Id = 1, Date = DateTime.Now, Kind = new Model.ActionKind { Id = 2 } })
                );
        }

        [TestMethod()]
        public async Task Insert_ShouldThowException_NonExistentSubscription()
        {
            await Assert
                .ThrowsExceptionAsync<InvalidOperationException>(() =>
                _instance.Insert(
                    new Model.Action() { Id = 1, Date = DateTime.Now, Subscription = new Model.Subscription { Id = 2 } })
                );
        }

        [TestMethod()]
        public async Task Edit_ShouldEditDatabase()
        {
            int id = 1;
            DateTime newDate = DateTime.Today;
            var elementToEdit = await _dbaContext.Actions.FindAsync(id);
            elementToEdit.Date = newDate;
            await _instance.Edit(elementToEdit);

            var editedElement = await _dbaContext.Actions.FindAsync(id);
            Assert.AreEqual(newDate, editedElement.Date);

        }

        [TestMethod()]
        public async Task Edit_ShouldThowException_NonExistentActionKind()
        {
            int id = 1;
            var elementToEdit = await _dbaContext.Actions.FindAsync(id);
            elementToEdit.Kind = new Model.ActionKind { Id = 2 };

            await Assert
                .ThrowsExceptionAsync<DbUpdateConcurrencyException>(() =>
                _instance.Edit(elementToEdit));
        }

        [TestMethod()]
        public async Task Edit_ShouldThowException_NonExistentSubscription()
        {
            int id = 1;
            var elementToEdit = await _dbaContext.Actions.FindAsync(id);
            elementToEdit.Subscription = new Model.Subscription { Id = 2 };

            await Assert
                .ThrowsExceptionAsync<DbUpdateConcurrencyException>(() =>
                _instance.Edit(elementToEdit));
        }


    }
}