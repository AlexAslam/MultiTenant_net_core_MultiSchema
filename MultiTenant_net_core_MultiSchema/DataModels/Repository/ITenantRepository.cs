using MultiTenant_net_core_MultiSchema.DataModels.Entities;
using System.Collections.Generic;

namespace MultiTenant_net_core_MultiSchema.DataModels.Repository
{
    public interface ITenantRepository
    {
        IEnumerable<Tenant> getAllTenants();
        Tenant getTenantById(int id);
        void saveAll();
        bool addEntity(Tenant newTenant);
    }
}