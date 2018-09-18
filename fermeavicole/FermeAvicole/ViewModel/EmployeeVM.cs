using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace FermeAvicole.ViewModel
{
    public class EmployeeVM
    {
        public int Employee_Id { get; set; }

        [Display(Name = "Accout Type")]
        [Required]
        public int Role_Id { get; set; }

        [Display(Name = "Role Name")]
        public string Role_Name { get; set; }

        [Display(Name = "Email Id")]
        [DataType(DataType.EmailAddress)]
        [Required]
        [RegularExpression(@"\A(?:[a-z!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z",
        ErrorMessage = "Please enter correct email address")]
        public string Email_Id { get; set; }

        [Required]
        [RegularExpression("([a-z]|[A-Z]|[0-9]|[\\W]){4}[a-zA-Z0-9\\W]{3,11}", ErrorMessage = "Invalid password format")]
        public string Password { get; set; }

        [Display(Name = "First Name")]
        [Required]
        public string First_Name { get; set; }

        [Display(Name = "Last Name")]
        [Required]
        public string Last_Name { get; set; }

        public List<EmployeeVM>EmployeeList { get; set; }


        [Display(Name = "Contact Number")]
        [StringLength(10, ErrorMessage = "Contact number must be 10 digit number")]
        [Required]
        [MaxLength(10)]
        [MinLength(10)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "UPRN must be numeric")]
        public string Contact_Number { get; set; }

        [DataType(DataType.Upload)]
        [Display(Name = "Upload Photo")]
        [Required(ErrorMessage = "Please choose file to upload.")]
        public HttpPostedFileBase ImageFile { get; set; }
        public string Image_Path { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [RegularExpression(@"^\d{6}(-\d{4})?$", ErrorMessage = "Please Enter Valid Postal Code.")]
        public int PinCode { get; set; }
    }
}