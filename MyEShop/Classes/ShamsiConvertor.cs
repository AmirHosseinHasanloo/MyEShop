using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Data.Entity.Infrastructure;

namespace MyEShop
{
    public static class ShamsiConvertorBase
    {
        public static string ToShamsi(this DateTime Value)
        {
            PersianCalendar pc = new PersianCalendar();
            return pc.GetYear(Value) + "/" + pc.GetMonth(Value).ToString("00") + "/" + pc.GetDayOfMonth(Value).ToString("00");
        }
    }

 
}