//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LarnerhemsEvent.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class user
    {
        public int userID { get; set; }
        public string password { get; set; }
        public Nullable<int> fk_access_id { get; set; }
        public string username { get; set; }
    
        public virtual access access { get; set; }
    }
}
