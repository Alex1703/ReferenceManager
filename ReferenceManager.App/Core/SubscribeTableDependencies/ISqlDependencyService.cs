using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReferenceManager.App.Core.SubscribeTableDependencies
{
    public interface ISqlDependencyService
    {
        void SubscribeTableDependency();
    }
}
