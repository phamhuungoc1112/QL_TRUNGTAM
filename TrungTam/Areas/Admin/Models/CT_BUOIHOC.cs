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
    
    public partial class CT_BUOIHOC
    {
        public System.Guid MA_BUOI { get; set; }
        public string MA_HS { get; set; }
        public string DIEM_DANH_HS { get; set; }
        public string NHAN_XET_GV { get; set; }
        public string BAI_TAP_VN { get; set; }
    
        public virtual BUOI_HOC BUOI_HOC { get; set; }
        public virtual HOC_SINH HOC_SINH { get; set; }
    }
}