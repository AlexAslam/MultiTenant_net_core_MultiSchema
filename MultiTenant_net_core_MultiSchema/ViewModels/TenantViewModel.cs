using System.ComponentModel.DataAnnotations;

namespace MultiTenant_net_core_MultiSchema.ViewModels
{
    public class TenantViewModel
    {
        [Required]
        public string SubDomainName { get; set; }
        public string ConnectionStringName { get; set; }
    }
}
