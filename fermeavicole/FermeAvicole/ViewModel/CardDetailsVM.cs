using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FermeAvicole.ViewModel
{
    public class CardDetailsVM
    {
        public int Card_Id{ get; set; }

        [Display(Name = "Card Id")]
        [Required]
        public int User_Id { get; set; }

        [Required]
        public string Card_HolderName { get; set; }

        public DateTime Expiry_Date { get; set; }

        [Display(Name = "Expiry date")]
        [Required]
        public int Card_Number { get; set; }

        
    }
}