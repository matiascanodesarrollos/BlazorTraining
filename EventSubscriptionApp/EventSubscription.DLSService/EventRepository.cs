using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventSubscription.Model;
using EventSubscription.DLSService.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EventSubscription.DLSService
{
    public class EventRepository : IRepository<Event>
    {
        IDbaContextProvider _dbaContextProvider;

        public EventRepository(IDbaContextProvider dbaContextProvider)
        {
            _dbaContextProvider = dbaContextProvider ?? throw new ArgumentNullException(nameof(dbaContextProvider));
        }

        public async Task Delete(Event element)
        {
            using (var dba = _dbaContextProvider.CreateNewContext())
            {
                dba.Events.Remove(element);
                await dba.SaveChangesAsync();
            }
        }

        public async Task<IList<Event>> GetAll()
        {
            using (var dba = _dbaContextProvider.CreateNewContext())
            {
                return await dba.Events.ToListAsync();                
            }
        }

        public async Task<IList<Event>> GetAll(int page, int pageSize)
        {
            using (var dba = _dbaContextProvider.CreateNewContext())
            {
                return await dba.Events
                    .Skip(pageSize * (page - 1))
                    .Take(pageSize)
                    .ToListAsync();
            }
        }

        public async Task<int> GetCount()
        {
            using (var dba = _dbaContextProvider.CreateNewContext())
            {
                return await dba.Events.CountAsync();
            }
        }

        public async Task Insert(Event element)
        {
            using (var dba = _dbaContextProvider.CreateNewContext())
            {
                dba.Events.Add(element);
                await dba.SaveChangesAsync();
            }
        }

        public async Task Edit(Event element)
        {
            using (var dba = _dbaContextProvider.CreateNewContext())
            {
                dba.Events.Update(element);
                await dba.SaveChangesAsync();
            }
        }


    }
}
