using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FermeAvicole.ViewModel
{
    public class OrderTransactionVM
    {
        public int OrderTransaction_Id { get; set; }

        public int Order_Id { get; set; }

        public int Item_Id { get; set; }

        public string Item_Name { get; set; }


        public int Quantity { get; set; }

        public decimal Amount { get; set; }

        public decimal Total{ get; set; }

    }
}