using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConstructionCompany.Models
{
    public class Customer
    {
        public int CustomerID { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public int Telephone { get; set; }
        public string PassportData { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public Customer()
        {
            Orders = new List<Order>();

        }
    }
}