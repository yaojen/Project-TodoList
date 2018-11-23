using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TodoList.Models;
using System.Net;
using TodoList.ViewModel;
using TodoList.CommonClass;

namespace TodoList.Controllers
{
    public class ItemsController : Controller
    {
        private Database1Entities _db = new Database1Entities();
        // GET: Index

        public ActionResult Index()
        {
            var workItems = _db.WorkItem
                .Select(x => new ItemsIndexViewModel
                {
                    Id = x.Id,
                    CreateDate = x.CreateDate,
                    Finished = x.Finished,
                    Memo = x.Memo,
                    Subject = x.Subject
                })
                .ToList();

            return View(workItems);
        }


        public ActionResult Create()
        {

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ItemsCreateViewModel itemsCreateViewModel)
        {
            string message = "";
            try
            {
                if (ModelState.IsValid)
                {
                    var workItem = new WorkItem()
                    {
                        CreateDate = DateTime.Now,
                        Finished = itemsCreateViewModel.Finished,
                        Memo = itemsCreateViewModel.Memo,
                        Subject = itemsCreateViewModel.Subject,

                    };
                    _db.WorkItem.Add(workItem);
                    _db.SaveChanges();

                    message = "新增成功";
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                message = "新增失敗";
            }

            return RedirectToAction("Index").WithWarning(message);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }

            var workItem = _db.WorkItem.Find(id);

            if (workItem == null)
            {
                return HttpNotFound();
            }

            return View(workItem);
        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult EditPost(ItemsEditViewModel itemsEditViewModel)
        {
            string message = "";
            if (itemsEditViewModel.Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (ModelState.IsValid)
            {
                var workitem = _db.WorkItem.Find(itemsEditViewModel.Id);

                if (TryUpdateModel(workitem, new string[] { }))
                {
                    try
                    {
                        _db.SaveChanges();
                        message = "修改成功";
                        return RedirectToAction("Index").WithSuccess(message);
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");

                    }
                }
            }

            return RedirectToAction("Index").WithSuccess("修改成功") ;
        }



        public ActionResult Delete(int? id)
        {
            if (id == null)
                new HttpNotFoundResult();

            var item = _db.WorkItem.Find(id);

            if (item == null)
            {
                new HttpNotFoundResult();
            }

            _db.WorkItem.Remove(item);

            _db.SaveChanges();

            return RedirectToAction("Index").WithSuccess("刪除成功"); ;

        }
    }
}