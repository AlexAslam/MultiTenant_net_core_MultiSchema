using MultiTenant_net_core_MultiSchema.DataModels.Contexts;

namespace MultiTenant_net_core_MultiSchema.DataModels.Repository
{
    public interface IMigrationRepository
    {
        void AddMigration();
    }
}