using System;
using System.Threading.Tasks;
using ObserverPatternDemo.Models;

namespace ObserverPatternDemo.Services
{
    public class TenantService
    {
        public TenantService()
        {
        }

        public async Task CreateTenant(Order order)
        {
            Console.WriteLine("Create Tenant");
            
        }
    }
}