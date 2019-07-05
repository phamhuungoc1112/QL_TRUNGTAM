using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrungTam.Areas.Admin.Models;
namespace TrungTam.Function_Base
{
    public class BASE
    {
        QL_TRUNGTAMEntities1 db = new QL_TRUNGTAMEntities1();
        public void create_TAI_KHOAN(string tk, string hoten)
        {
            TAI_KHOAN tAI_KHOAN = new TAI_KHOAN();
            tAI_KHOAN.TAI_KHOAN1 = tk;
            tAI_KHOAN.MAT_KHAU = "123";
            db.TAI_KHOAN.Add(tAI_KHOAN);
            db.SaveChanges();
        }
        //----------------------------------------
        
    }
}