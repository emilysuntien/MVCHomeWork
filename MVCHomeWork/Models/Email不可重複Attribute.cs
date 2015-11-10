using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCHomeWork.Models
{
    public class Email不可重複Attribute:DataTypeAttribute
    {
        public Email不可重複Attribute():base(DataType.Text)
        {
            ErrorMessage = "Email重複,請重新輸入";
        }

        public override bool IsValid(object value)
        {
            CustomerInfoEntities db = new CustomerInfoEntities();
            string str = Convert.ToString(value);

            if (db.客戶聯絡人.Where(p => p.Email.Equals(str)).Count() == 0)
            {
                return true;
            }

            return false;
        }
    }
}