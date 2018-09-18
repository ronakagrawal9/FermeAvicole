using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FermeAvicole.ViewModel
{
    public class SupplierVM
    {
        public int Supplier_Id { get; set; }

        [Display(Name ="Supplier Name")]
        [Required]
        public string Supplier_Name { get; set; }

        [Required]
        public string Address { get; set; }

        public List<SupplierVM> SupplierList { get; set; }

        [Required]
        /*[StringLength(6,ErrorMessage ="Pincode only allow 6 digit")]*/
        public int Pincode { get; set; }

        [Display(Name = "Contact Number")]
        [Required]
        public string Contact_Number { get; set; }

        [Required]
        public string Email_Id { get; set; }
    }
}