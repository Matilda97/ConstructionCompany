using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConstructionCompany.Models
{
    public class Brigade
    {
        public int BrigadeID { get; set; }
        public string NameBrigade { get; set; }
        public string Brigadier { get; set; }
        public string Composition { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public Brigade()
        {
            Orders = new List<Order>();

        }

    }
}