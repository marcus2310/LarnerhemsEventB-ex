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
    
    public partial class order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public order()
        {
            this.packageorderdetails = new HashSet<packageorderdetail>();
        }
    
        public int orderID { get; set; }
        public Nullable<System.DateTime> orderdate { get; set; }
        public string deliveryadress { get; set; }
        public Nullable<System.DateTime> eventdate { get; set; }
        public Nullable<int> fk_customer_id { get; set; }
        public Nullable<int> fk_campaigncode_id { get; set; }
        public string approved { get; set; }
        public Nullable<int> totalprice { get; set; }
        public string sent { get; set; }
        public string zipcode { get; set; }
        public string town { get; set; }
    
        public virtual campaigncode campaigncode { get; set; }
        public virtual customer customer { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<packageorderdetail> packageorderdetails { get; set; }
    }
}
