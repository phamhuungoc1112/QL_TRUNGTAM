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
    
    public partial class THOI_KHOA_BIEU
    {
        public System.Guid MA_LOP { get; set; }
        public string THU { get; set; }
        public System.TimeSpan THOI_GIAN_BD { get; set; }
        public Nullable<System.TimeSpan> THOI_GIAN_KT { get; set; }
    
        public virtual LOP_HOC LOP_HOC { get; set; }
    }
}
