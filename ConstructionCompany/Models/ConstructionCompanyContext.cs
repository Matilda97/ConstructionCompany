using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ConstructionCompany.Models
{
    public class ConstructionCompanyContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public ConstructionCompanyContext() : base("name=ConstructionCompanyContext")
        {
        }

        public System.Data.Entity.DbSet<ConstructionCompany.Models.Job> Jobs { get; set; }

        public System.Data.Entity.DbSet<ConstructionCompany.Models.Material> Materials { get; set; }

        public System.Data.Entity.DbSet<ConstructionCompany.Models.Brigade> Brigades { get; set; }

        public System.Data.Entity.DbSet<ConstructionCompany.Models.Customer> Customers { get; set; }

        public System.Data.Entity.DbSet<ConstructionCompany.Models.Order> Orders { get; set; }
    }
}
