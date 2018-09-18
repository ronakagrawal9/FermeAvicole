using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace FermeAvicole.ViewModel
{
    public class DoctorReportVM
    {
        public int DoctorReport_Id { get; set; }

        [Display(Name = "Account Type")]
        [Required]
        public int Doctor_Id { get; set; }

        [Display(Name = "Doctor Name")]
        public string Doctor_Name { get; set; }

        public int User_Id { get; set; }
        public string Remarks { get; set; }

        [Required]
        public int Item_Id { get; set; }

        [Display(Name = "Item Name")]
        public string Item_Name { get; set; }

        public List<SelectListItem> DDLItemList { get; set; }

        [Required]
        public string Description { get; set; }
        public List<DoctorReportVM> DoctorReportList { get; set; }

        [Required]

        [Display(Name = "Report date")]
        public DateTime Report_Date { get; set; }


        [DataType(DataType.Upload)]
        [Display(Name = "Upload Photo")]
       
        public HttpPostedFileBase ImageFile { get; set; }
        public string Image_Path { get; set; }


    }
}