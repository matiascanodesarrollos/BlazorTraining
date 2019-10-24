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
        DbaContext _dbaContext;

        public EventRepository(DbaContext dbaContext)
        {
            _dbaContext = dbaContext ?? throw new ArgumentNullException(nameof(dbaContext));
        }

        public async Task Delete(Event element)
        {
            _dbaContext.Events.Remove(element);
            await _dbaContext.SaveChangesAsync();
        }

        public async Task<IList<Event>> GetAll()
        {
            return await _dbaContext.Events.ToListAsync(); 
        }

        public async Task<IList<Event>> GetAll(int page, int pageSize)
        {
            return await _dbaContext.Events
                    .Skip(pageSize * (page - 1))
                    .Take(pageSize)
                    .ToListAsync();
        }

        public async Task<int> GetCount()
        {
            return await _dbaContext.Events.CountAsync();
        }

        public async Task Insert(Event element)
        {
            _dbaContext.Events.Add(element);
             await _dbaContext.SaveChangesAsync();
        }

        public async Task Edit(Event element)
        {
            _dbaContext.Events.Update(element);
            await _dbaContext.SaveChangesAsync();
        }


    }
}
