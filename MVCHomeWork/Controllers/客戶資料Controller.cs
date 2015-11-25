using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCHomeWork.Models;
using NPOI.XSSF.UserModel;
using System.IO;
using NPOI.HSSF.UserModel;

namespace MVCHomeWork.Controllers
{
    enum  欄位
    {
        姓名,
        統一編號,
        電話,
        傳真,
        地址
    }



    public class 客戶資料Controller : Controller
    {
        private 客戶資料Repository repo;
        public 客戶資料Controller()
        {
            repo = RepositoryHelper.Get客戶資料Repository();
        }


        public ActionResult Index()
        {
            ViewBag.category = repo.Get客戶分類List();
            return View();
        }

        [HttpPost]
        public FilePathResult Index(客戶資料 customer)
        {
            string fileName = Server.MapPath(@"~/Content/測試.xlsx");
           string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            var excel = new XSSFWorkbook();
            var sheet = excel.CreateSheet("測試");

          

            var sheetHead = sheet.CreateRow(0);
            sheetHead.CreateCell((int)欄位.姓名).SetCellValue("姓名111");
            sheetHead.CreateCell((int)欄位.統一編號).SetCellValue("統一編號");
            sheetHead.CreateCell((int)欄位.電話).SetCellValue("電話");
            sheetHead.CreateCell((int)欄位.傳真).SetCellValue("傳真");
            sheetHead.CreateCell((int)欄位.地址).SetCellValue("地址");
          

            var data = repo.All(customer).ToList();

            for (int i = 0; i <= data.Count - 1; i++)
            {
                var body = sheet.CreateRow(i + 1);
                body.CreateCell((int)欄位.姓名).SetCellValue(data[i].客戶名稱);
                body.CreateCell((int)欄位.統一編號).SetCellValue(data[i].統一編號);
                body.CreateCell((int)欄位.電話).SetCellValue(data[i].電話);
                body.CreateCell((int)欄位.傳真).SetCellValue(data[i].傳真);
                body.CreateCell((int)欄位.地址).SetCellValue(data[i].地址);
            
            }

            FileStream file = new FileStream(fileName, FileMode.Create);//產生檔案
            excel.Write(file);
            file.Close();
            return File(fileName, contentType, "D.xlsx");

        }

        


        public ActionResult IndexOrderBy(string orderTitle, bool flag)
        {

            return Json(repo.OrderBy(orderTitle, flag), JsonRequestBehavior.AllowGet);
        }

        // GET: 客戶資料/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = repo.GetById(id);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // GET: 客戶資料/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: 客戶資料/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email")] 客戶資料 客戶資料)
        {
            if (ModelState.IsValid)
            {
                repo.Add(客戶資料);
                return RedirectToAction("Index");
            }

            return View(客戶資料);
        }

        // GET: 客戶資料/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = repo.GetById(id);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // POST: 客戶資料/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id,FormCollection form)
            //[Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email")] 客戶資料 客戶資料)
        {

            客戶資料 data = repo.GetById(id);
            if (TryUpdateModel<客戶資料>(data, "客戶名稱,統一編號,電話,傳真,地址,Email".Split(',')))
            {
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            return View(data);          
        }

        // GET: 客戶資料/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = repo.GetById(id);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // POST: 客戶資料/Delete/5
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
