using NetCoreProject.Common;
using NetCoreProject.Data.Common.Interfaces;
using NetCoreProject.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCoreProject.Domain.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;

        public SupplierService(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public async Task<IEnumerable<Supplier>> GetSuppliersAsync()
        {
            return await _supplierRepository.GetSuppliersAsync();
        }
    }
}
