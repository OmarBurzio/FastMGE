using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juppiter.DL
{
    public class BaseManager
    {
        public ServiceManager serviceManager;

        public BaseManager(ServiceManager serviceManager)
        {
            this.serviceManager = serviceManager;
        }
    }
}
