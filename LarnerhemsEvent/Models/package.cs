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
    
    public partial class package
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public package()
        {
            this.packageorderdetails = new HashSet<packageorderdetail>();
            this.prodpackdetails = new HashSet<prodpackdetail>();
        }
    
        public int packageID { get; set; }
        public string name { get; set; }
        public Nullable<int> price { get; set; }
        public Nullable<int> originalprice { get; set; }
        public string info { get; set; }
        public string moreinfo { get; set; }
        public string img { get; set; }
        public Nullable<int> fk_genre_id { get; set; }
    
        public virtual genre genre { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<packageorderdetail> packageorderdetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<prodpackdetail> prodpackdetails { get; set; }
    }
}
