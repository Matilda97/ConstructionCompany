using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConstructionCompany.Models
{
    public class Job
    {
        public int JobID { get; set; }
        public string NameJob { get; set; }
        public string DescriptionJob { get; set; }
        public int PriceJob { get; set; }
        public int MaterialID { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public Job()
        {
            Orders = new List<Order>();

        }
        //ссылка на виды материала
        public virtual Material Material { get; set; }
    }
}