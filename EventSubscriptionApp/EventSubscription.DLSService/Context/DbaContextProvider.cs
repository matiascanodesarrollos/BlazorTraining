using System;
using System.Collections.Generic;
using System.Text;

namespace EventSubscription.DLSService.Context
{
    public class DbaContextProvider : IDbaContextProvider
    {
        public IDbaContext CreateNewContext()
        {
            return new DbaContext();
        }
    }
}
