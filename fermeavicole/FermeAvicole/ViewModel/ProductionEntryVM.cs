using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace FermeAvicole.ViewModel
{
    public class ProductionEntryVM
    {
        public int ProductionEntry_Id { get; set; }

        [Display(Name = "Account Type")]
        [Required]
        public int Item_Id { get; set; }

        [Display(Name = "Item Name")]
        public string Item_Name { get; set; }

        public int User_Id { get; set; }
       


        [Required]
        public int Quantity { get; set; }
        public List<ProductionEntryVM> ProductionEntryList { get; set; }

        [Required]

        [Display(Name = "date")]
        public DateTime Date { get; set; }
        public ItemVM SalesItem { get; set; }


    }
}