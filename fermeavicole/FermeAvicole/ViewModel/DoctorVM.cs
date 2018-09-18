using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace FermeAvicole.ViewModel
{
    public class DoctorVM
    {
        [Required]
        [Display(Name = "Doctor Id")]
        public int Docter_id { get; set; }

        [Required]
        [Display(Name = "Doctor Name")]
        public string Doctor_Name { get; set; }
        

        [Required]
        [Display(Name = "Contact Number")]
        public int Contact_Number { get; set; }
        

        [Required]
        [Display(Name = "Email Id")]
        public String Email_Id { get; set; }
        


        [Required]
        [Display(Name = "Adress")]
        public string Address { get; set; }

    }
    
}