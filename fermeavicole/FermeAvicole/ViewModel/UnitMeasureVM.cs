using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FermeAvicole.ViewModel
{
    public class UnitMeasureVM
    {

        [Display(Name = "Unit Measure Id")]
        public int UnitMeasure_Id { get; set; }

        [Display(Name = "Unit Measure Name")]
        [Required]
        public string UnitMeasure_Name { get; set; }
    }
}