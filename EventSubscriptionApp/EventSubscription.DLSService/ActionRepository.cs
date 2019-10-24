using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventSubscription.DLSService.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EventSubscription.DLSService
{
    public class ActionRepository : IRepository<Model.Action>
    {
        DbaContext _dbaContext;

        public ActionRepository(DbaContext dbaContext)
        {
            _dbaContext = dbaContext ?? throw new ArgumentNullException(nameof(dbaContext));
        }

        public async Task Delete(Model.Action element)
        {
            _dbaContext.Actions.Remove(element);
            await _dbaContext.SaveChangesAsync();
        }

        public async Task<IList<Model.Action>> GetAll()
        {
            return await _dbaContext.Actions.ToListAsync();
        }

        public async Task<IList<Model.Action>> GetAll(int page, int pageSize)
        {
            return await _dbaContext.Actions
                    .Skip(pageSize * (page - 1))
                    .Take(pageSize)
                    .ToListAsync();
        }

        public async Task<int> GetCount()
        {
            return await _dbaContext.Actions.CountAsync();
        }

        public async Task Insert(Model.Action element)
        {
            _dbaContext.Actions.Add(element);
             await _dbaContext.SaveChangesAsync();
        }

        public async Task Edit(Model.Action element)
        {
            _dbaContext.Actions.Update(element);
            await _dbaContext.SaveChangesAsync();
        }


    }
}
