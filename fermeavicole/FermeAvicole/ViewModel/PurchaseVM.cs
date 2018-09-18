using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FermeAvicole.ViewModel
{
    public class PurchaseVM
    {
        public int Purchase_Id { get; set; }

        [Display(Name = "Supplier Id")]
        [Required]
        public int Supplier_Id { get; set; }

        public List<PurchaseVM> PurchaseList { get; set; }

        [Display(Name = "Purchase Date")]
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Purchase_Date { get; set; }
        public decimal Tax { get; set; }

        [Display(Name = "Total Amount")]
        [Required]
        public decimal Total_amount { get; set; }

        [Display(Name = "Supplier Name")]
        public string Supplier_Name { get; set; }

        public PurchaseItemVM PurchaseItem { get; set; }

    }
}