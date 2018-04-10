﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiTenant_net_core_MultiSchema.DataModels.Entities
{
    public class Tenant
    {
        public int Id { get; set; }
        public string SubDomainName { get; set; }
        public string ConnectionStringName { get; set; }
    }
}
