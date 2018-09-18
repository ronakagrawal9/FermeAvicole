using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FermeAvicole.ViewModel
{
    public class CategoryVM
    {
        public int Category_Id  { get; set; }

        [Display(Name ="Category Name")]
        [Required]
        public string Category_Name { get; set; }

        public string Data_Filter { get; set; }
    }
}