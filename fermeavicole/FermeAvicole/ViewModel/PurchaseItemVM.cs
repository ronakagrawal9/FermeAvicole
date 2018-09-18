using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FermeAvicole.ViewModel
{
    public class PurchaseItemVM
    {
        public int PurchaseItem_Id { get; set; }

        [Display(Name = "Item Name")]
        [Required]
        public string Item_Name { get; set; }

        [Required]
        public int PurchaseCategory_Id { get; set; }

        [Display(Name = "Item Price")]
        [Required]
        public decimal Item_Price { get; set; }

        [Display(Name = "Category Name")]

        public string Category_Name { get; set; }

        [Display(Name = "Item Description")]
        [Required]
        public string Item_Description { get; set; }

        [Display(Name = "Item Image")]

        [DataType(DataType.Upload)]
        public HttpPostedFileBase Item_Image { get; set; }

        public string Image_Path { get; set; }

        public List<CategoryVM> CateGoryList { get; set; }

        public List<ItemVM> ItemList { get; set; }
        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public decimal Total_Amount { get; set; }

    }
}