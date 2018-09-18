using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FermeAvicole.ViewModel
{
    public class PaymentTransactionVM
    {
        public int PaymentTransaction_Id { get; set; }

        public DateTime Date { get; set; }

        public int Order_Id { get; set; }

        public int User_Id { get; set; }

        public decimal Amount { get; set; }

        public string Payment_Mode { get; set; }
    }
}