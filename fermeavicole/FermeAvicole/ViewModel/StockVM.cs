using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FermeAvicole.ViewModel
{
    public class StockVM
    {
        
        public int Item_Id { get; set; }

        public string Item_Name { get; set; }

        public int Current_Stock { get; set; }

        
        
    }
}