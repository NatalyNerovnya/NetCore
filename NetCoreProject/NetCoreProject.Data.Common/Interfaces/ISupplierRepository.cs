using NetCoreProject.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCoreProject.Data.Common.Interfaces
{
    public interface ISupplierRepository
    {
        Task<IEnumerable<Supplier>> GetSuppliersAsync();
    }
}
