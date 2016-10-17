using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConstructionCompany.Models
{
    public class Material
    {
        public int MaterialID { get; set; }
        public string NameMaterial { get; set; }
        public string Packaging { get; set; }
        public string DescriptionMaterial { get; set; }
        public int PriceMaterial { get; set; }
        public virtual ICollection<Job> Jobs { get; set; }
        public Material()
        {
            Jobs = new List<Job>();

        }
    }
}