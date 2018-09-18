using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FermeAvicole.ViewModel
{
    public class StoreVM
    {
        public int Store_Id { get; set; }

        public int User_Id { get; set; }
        [Required]
        public  String Latitude { get; set; }
        [Required]
        public string Longitude { get; set; }
        [Display(Name = "Store Name")]
        [Required]
        public string Store_Name { get; set; }
        [Required]
        public string Description{ get; set; }
    }
}