using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FermeAvicole.ViewModel
{
    public class PurchaseCategoryVM
    {
        public int PurchaseCategory_Id { get; set; }

        [Display(Name = "Category Name")]
        [Required]
        public string Category_Name { get; set; }
    }
}