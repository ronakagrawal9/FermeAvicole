using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FermeAvicole.ViewModel
{
    public class ItemVM
    {
        public int Item_Id { get; set; }

        [Display(Name = "Expiry Date")]
        [Required]
        public string Expiry_Date { get; set; }

        [Display(Name ="Item Name")]
        [Required]
        public string Item_Name { get; set; }

        [Required]
        public int Category_Id { get; set; }

        [Display(Name = "Item Price")]
        [Required]
        public decimal Item_Price { get; set; }

        public string Doctor_Report_Path { get; set; }

        [Display(Name = "Category Name")]
      
        public string Category_Name { get; set; }

        public string Payment_Mode { get; set; }

        [Display(Name = "Item Description")]
        [Required]
        public string Item_Description { get; set; }

        [Display(Name = "Item Image")]
      
        [DataType(DataType.Upload)]
        public HttpPostedFileBase Item_Image { get; set; }

        public string Image_Path { get; set; }

        public List<CategoryVM> CateGoryList { get; set; }

        public List<ItemVM> ItemList { get; set; }

        public string Data_Filter { get; set; }

        public decimal Amount { get; set; }

        public decimal Total_Amount { get; set; }

        public int Quantity { get; set; }

        public string Layout { get; set; }


    }
}