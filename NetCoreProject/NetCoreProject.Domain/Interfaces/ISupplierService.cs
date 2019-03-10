using NetCoreProject.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCoreProject.Domain.Interfaces
{
    public interface ISupplierService
    {
        Task<IEnumerable<Supplier>> GetSuppliersAsync();
    }
}
