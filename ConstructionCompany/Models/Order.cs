using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConstructionCompany.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public int JobID { get; set; }
        public int BrigadeID { get; set; }
        public int PriceOrder { get; set; }
        [DataType(DataType.Date)]
        public System.DateTime DateStart { get; set; }
        [DataType(DataType.Date)]
        public System.DateTime DateEnd { get; set; }
        public bool Completion { get; set; }
        public bool Payment { get; set; }
        public string Senior { get; set; }
        //ссылка на вид работы
        public virtual Job Job { get; set; }
        //ссылка на заказчика
        public virtual Customer Customer { get; set; }
        //ссылка на бригаду
        public virtual Brigade Brigade { get; set; }
    }
}