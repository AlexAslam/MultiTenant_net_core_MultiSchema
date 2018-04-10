using MultiTenant_net_core_MultiSchema.DataModels.Entities;
using System.Collections.Generic;

namespace MultiTenant_net_core_MultiSchema.DataModels.Repository
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> getAllEmployees();
        Employee getEmployeeById(int id);
        void saveAll();
        void addEntity(Employee newEmployee);
    }
}