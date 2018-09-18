using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FermeAvicole.ViewModel
{
    public class OrderVM
    {

        public int Order_Id { get; set; }

        public int User_Id { get; set; }

        public DateTime Date { get; set; }

        public decimal Total_Amount { get; set; }

        public int OrderStatus_Id { get; set; }

        public string Order_Status { get; set; }

        public string Customer_Name { get; set; }

        public List<OrderTransactionVM> OrderItems { get; set; }

        public PaymentTransactionVM PaymentTransaction { get; set; }

    }
}