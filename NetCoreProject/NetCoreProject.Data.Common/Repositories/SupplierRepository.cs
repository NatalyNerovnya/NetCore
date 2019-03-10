using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NetCoreProject.Common;
using NetCoreProject.Data.Common.Interfaces;
using NetCoreProject.Data.EFModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCoreProject.Data.Common.Repositories
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly NorthwindContext _context;
        private readonly IMapper _mapper;

        public SupplierRepository(NorthwindContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Supplier>> GetSuppliersAsync()
        {
            var suppliers = await _context.Suppliers.ToListAsync();
            return _mapper.Map<IEnumerable<Supplier>>(suppliers);
        }
    }
}
