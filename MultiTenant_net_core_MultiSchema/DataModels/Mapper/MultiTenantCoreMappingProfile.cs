using AutoMapper;
using MultiTenant_net_core_MultiSchema.DataModels.Entities;
using MultiTenant_net_core_MultiSchema.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiTenant_net_core_MultiSchema.DataModels.Mapper
{
    public class MultiTenantCoreMappingProfile : Profile
    {
        public MultiTenantCoreMappingProfile()
        {
            CreateMap<Tenant, TenantViewModel>().ReverseMap();
            CreateMap<Employee, EmployeeViewModel>().ReverseMap();
        }
    }
}
