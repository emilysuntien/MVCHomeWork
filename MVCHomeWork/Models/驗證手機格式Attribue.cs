using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCHomeWork.Models
{
    public class 驗證手機格式Attribute:DataTypeAttribute
    {
        public 驗證手機格式Attribute():base(DataType.Text)
        {
            this.ErrorMessage = "手機號碼格式錯誤";
        }

        public override bool IsValid(object value)
        {
            string str = Convert.ToString(value);          

            if (str.Contains("-")) {
                var splitStr = str.Split('-');
                if (splitStr[0].Length == 4 && splitStr[1].Length == 6) { return true; }
            }

            return false;
        }
    }
}