using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MVCHomeWork.Models
{   
	public  class 客戶相關資訊Repository : EFRepository<客戶相關資訊>, I客戶相關資訊Repository
	{
        public  IQueryable<客戶相關資訊> All(string CategoryList)
        {
            if (CategoryList == null || CategoryList=="") { return base.All(); }
            return base.All().Where(p => p.客戶分類 == CategoryList); 
        }

        public 客戶相關資訊 GetByID(int ?id)
        {
            return this.All().FirstOrDefault(p => p.Id == id);
        }

        public SelectList GetCategory()
        {
            return new SelectList(this.All().Select(p => p.客戶分類).Distinct());
        }
    }

	public  interface I客戶相關資訊Repository : IRepository<客戶相關資訊>
	{

	}
}