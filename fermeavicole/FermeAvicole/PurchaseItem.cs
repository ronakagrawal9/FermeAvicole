//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FermeAvicole
{
    using System;
    using System.Collections.Generic;
    
    public partial class PurchaseItem
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PurchaseItem()
        {
            this.PurchaseTransactions = new HashSet<PurchaseTransaction>();
        }
    
        public int PurchaseItem_Id { get; set; }
        public string Item_Name { get; set; }
        public string Item_Description { get; set; }
        public string Image_Path { get; set; }
        public decimal Price { get; set; }
        public int PurchaseCategory_Id { get; set; }
    
        public virtual PurchaseCategory PurchaseCategory { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PurchaseTransaction> PurchaseTransactions { get; set; }
    }
}
