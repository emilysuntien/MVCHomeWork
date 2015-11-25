using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCHomeWork.Models;

namespace MVCHomeWork.Controllers
{
    public class 客戶相關資訊Controller : Controller
    {
       private 客戶相關資訊Repository repo;
        public 客戶相關資訊Controller()
        {
            this.repo = RepositoryHelper.Get客戶相關資訊Repository();
        }

        // GET: 客戶相關資訊
        public ActionResult Index(string CategoryList)
        {
           ViewBag.CategoryList= repo.GetCategory();
            return View(repo.All(CategoryList));
        }

        // GET: 客戶相關資訊/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            客戶相關資訊 客戶相關資訊 = repo.GetByID(id);
         
            if (客戶相關資訊 == null)
            {
                return HttpNotFound();
            }
            return View(客戶相關資訊);
        }

        // GET: 客戶相關資訊/Create
        public ActionResult Create()
        {
            return View();
        }

       

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repo.UnitOfWork.Context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
