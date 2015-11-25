using System;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MVCHomeWork.Models
{   
	public  class 客戶銀行資訊Repository : EFRepository<客戶銀行資訊>, I客戶銀行資訊Repository
	{
        public  IQueryable<客戶銀行資訊> All(客戶銀行資訊 customerBankInfo)
        {

            IQueryable<客戶銀行資訊> data = this.All().Include(p => p.客戶資料).Where(x => x.是否刪除 == false);

            if (customerBankInfo.銀行名稱 != null) { data = this.All().Where(p => p.銀行名稱.Contains(customerBankInfo.銀行名稱)); }
            if (customerBankInfo.銀行代碼 != 0) { data = this.All().Where(p => p.銀行代碼.Equals(customerBankInfo.銀行代碼)); }
            if (customerBankInfo.帳戶名稱 != null) { data = this.All().Where(p => p.帳戶名稱.Contains(customerBankInfo.帳戶名稱)); }
            if (customerBankInfo.帳戶號碼 != null) { data = this.All().Where(p => p.帳戶號碼.Contains(customerBankInfo.帳戶號碼)); }

            return data;
        }

        public 客戶銀行資訊 GetByID(int? id)
        {
            return this.All().FirstOrDefault(p => p.Id == id);
        }

        public SelectList GetSelectList客戶資料()
        {
            return new SelectList(((CustomerInfoEntities)(this.UnitOfWork.Context)).客戶資料, "Id", "客戶名稱");
        }

        public SelectList GetSelectList客戶資料(int id)
        {
            return new SelectList(((CustomerInfoEntities)(this.UnitOfWork.Context)).客戶資料, "Id", "客戶名稱",id);
        }

        public override void Add(客戶銀行資訊 entity)
        {
            entity.是否刪除 = false;
            base.Add(entity);
            this.UnitOfWork.Commit();
        }

        public  void Delete(int id)
        {
            var data = this.All().FirstOrDefault(p => p.Id == id);
            data.是否刪除 = true;
            this.UnitOfWork.Commit();
        }
    }

	public  interface I客戶銀行資訊Repository : IRepository<客戶銀行資訊>
	{

	}
}