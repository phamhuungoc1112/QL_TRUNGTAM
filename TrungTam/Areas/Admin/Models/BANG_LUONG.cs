//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TrungTam.Areas.Admin.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class BANG_LUONG
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BANG_LUONG()
        {
            this.BUOI_HOC = new HashSet<BUOI_HOC>();
            this.NGOAI_GIO = new HashSet<NGOAI_GIO>();
        }
    
        public System.Guid MA_LOAI_LUONG { get; set; }
        public string TEN_LOAI { get; set; }
        public Nullable<int> SO_LUONG_MIN { get; set; }
        public Nullable<int> SO_LUONG_MAX { get; set; }
        public Nullable<decimal> DON_GIA { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BUOI_HOC> BUOI_HOC { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NGOAI_GIO> NGOAI_GIO { get; set; }
    }
}
