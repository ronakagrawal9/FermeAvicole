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
    
    public partial class Complain
    {
        public int Complain_Id { get; set; }
        public int Order_Id { get; set; }
        public int User_Id { get; set; }
        public System.DateTime Date { get; set; }
        public string Problem_Desc { get; set; }
    
        public virtual Order Order { get; set; }
        public virtual Registration Registration { get; set; }
    }
}
