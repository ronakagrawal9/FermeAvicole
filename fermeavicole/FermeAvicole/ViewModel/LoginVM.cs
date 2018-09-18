using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace FermeAvicole.ViewModel
{
    public class LoginVM
    {
        public int UserID { get; set; }
        public int Role_Id { get; set; }

        [Required(ErrorMessage = "Please Provide Username", AllowEmptyStrings = false)]
        public string Email_Id { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }       
        public string Account_Type { get; set; }
        public string Layout { get; set; }

        [Required(ErrorMessage = "Please provide password", AllowEmptyStrings = false)]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember on this computer")]
        public bool RememberMe { get; set; }
    }
}