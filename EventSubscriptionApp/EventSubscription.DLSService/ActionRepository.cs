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
        IDbaContextProvider _dbaContextProvider;

        public ActionRepository(IDbaContextProvider dbaContextProvider)
        {
            _dbaContextProvider = dbaContextProvider ?? throw new ArgumentNullException(nameof(dbaContextProvider));
        }

        public async Task Delete(Model.Action element)
        {
            using (var dba = _dbaContextProvider.CreateNewContext())
            {
                dba.Actions.Remove(element);
                await dba.SaveChangesAsync();
            }
        }

        public async Task<IList<Model.Action>> GetAll()
        {
            using (var dba = _dbaContextProvider.CreateNewContext())
            {
                return await dba.Actions.ToListAsync();                
            }
        }

        public async Task<IList<Model.Action>> GetAll(int page, int pageSize)
        {
            using (var dba = _dbaContextProvider.CreateNewContext())
            {
                return await dba.Actions
                    .Skip(pageSize * (page - 1))
                    .Take(pageSize)
                    .ToListAsync();
            }
        }

        public async Task<int> GetCount()
        {
            using (var dba = _dbaContextProvider.CreateNewContext())
            {
                return await dba.Actions.CountAsync();
            }
        }

        public async Task Insert(Model.Action element)
        {
            using (var dba = _dbaContextProvider.CreateNewContext())
            {
                dba.Actions.Add(element);
                await dba.SaveChangesAsync();
            }
        }

        public async Task Edit(Model.Action element)
        {
            using (var dba = _dbaContextProvider.CreateNewContext())
            {
                dba.Actions.Update(element);
                await dba.SaveChangesAsync();
            }
        }


    }
}
