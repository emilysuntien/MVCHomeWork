using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCHomeWork.Models;
using Omu.ValueInjecter;

namespace MVCHomeWork.Controllers
{
    public class 客戶聯絡人Controller : Controller
    {
        private 客戶聯絡人Repository repo;
        public 客戶聯絡人Controller()
        {
            repo = RepositoryHelper.Get客戶聯絡人Repository();
        }

        [ChildActionOnly]
        public ActionResult GetInfo(int 客戶Id)
        {
            ViewBag.職稱 = repo.GetTitle();
            return View(repo.All(客戶Id).ToList());

        }

        [HttpPost]
        public ActionResult GetInfo(int? id, FormCollection form)
        {

            IList< 客戶聯絡人> data = new List<客戶聯絡人>();

            TryUpdateModel<IList<客戶聯絡人>>(data, "data","職稱,電話,手機".Split(','));
            foreach(var x in data)
            {
                var dbItem = repo.GetById(x.Id);

                dbItem.InjectFrom(x);

            }

            repo.UnitOfWork.Commit();
            return View(data);
        }

        public ActionResult Index()
        {
            ViewBag.職稱 = repo.GetTitle();
            return View(repo.All().ToList());
        }

        [HttpPost]
        public ActionResult Index(客戶聯絡人 customerContact)
        {
            ViewBag.職稱 = repo.GetTitle();
            return View(repo.All(customerContact).ToList());
           
        }

        // GET: 客戶聯絡人/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = repo.GetById(id);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Create
        public ActionResult Create()
        {
            ViewBag.客戶Id = repo.GetSelectList客戶資料();
            return View();
        }

        // POST: 客戶聯絡人/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話")] 客戶聯絡人 客戶聯絡人)
        {
            if (ModelState.IsValid)
            {
                repo.Add(客戶聯絡人);
                return RedirectToAction("Index");
               
            }

            ViewBag.客戶Id = repo.GetSelectList客戶資料( 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = repo.GetById(id);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            ViewBag.客戶Id = repo.GetSelectList客戶資料( 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // POST: 客戶聯絡人/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int?id, FormCollection from)
        {
            客戶聯絡人 data = repo.GetById(id);
            if (TryUpdateModel<客戶聯絡人>(data, "客戶Id,職稱,姓名,Email,手機,電話".Split(',')))
            {
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            
            ViewBag.客戶Id = repo.GetSelectList客戶資料(data.客戶Id);
            return View(data);
        }

        // GET: 客戶聯絡人/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = repo.GetById(id);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }

        // POST: 客戶聯絡人/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            repo.Delete(id);
            return RedirectToAction("Index");
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
