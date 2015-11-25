using System;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MVCHomeWork.Models
{   
	public  class 客戶聯絡人Repository : EFRepository<客戶聯絡人>, I客戶聯絡人Repository
	{
        public  IQueryable<客戶聯絡人> All(客戶聯絡人 customerContact)
        {
            IQueryable<客戶聯絡人> data = this.All().Include(客 => 客.客戶資料).Where(p => p.是否刪除 == false);

            if (customerContact.職稱 != null) { data = this.All().Where(p => p.職稱.Contains(customerContact.職稱)); }
            if (customerContact.姓名 != null) { data = this.All().Where(p => p.姓名.Contains(customerContact.姓名)); }
            if (customerContact.手機 != null) { data = this.All().Where(p => p.手機.Contains(customerContact.手機)); }
            if (customerContact.電話 != null) { data = this.All().Where(p => p.電話.Contains(customerContact.電話)); }
            return data;
        }

        public  IQueryable<客戶聯絡人> All(int id)
        {
            return this.All().Where(p=>p.客戶Id == id);
        }

        public 客戶聯絡人 GetById(int? id)
        {
            return this.All().FirstOrDefault(p => p.Id == id);
        }

        public override void Add(客戶聯絡人 entity)
        {
            entity.是否刪除 = false;
            base.Add(entity);
            this.UnitOfWork.Commit();
        }

        public void Delete(int id)
        {
            var data = this.All().FirstOrDefault(p => p.Id == id);
            data.是否刪除 = true;
            this.UnitOfWork.Commit();
        }

        public SelectList GetSelectList客戶資料()
        {
            return new SelectList(((CustomerInfoEntities)(this.UnitOfWork.Context)).客戶資料, "Id", "客戶名稱");
        }

        public SelectList GetSelectList客戶資料(int id)
        {
            return new SelectList(((CustomerInfoEntities)(this.UnitOfWork.Context)).客戶資料, "Id", "客戶名稱", id);
        }

        public SelectList GetTitle() {
            return new SelectList(this.All().Select(p => p.職稱).Distinct());
        }

    }

	public  interface I客戶聯絡人Repository : IRepository<客戶聯絡人>
	{

	}
}