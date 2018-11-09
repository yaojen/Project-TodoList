using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TodoList.Models;
using System.Net;

namespace TodoList.Controllers
{
    public class IndexController : Controller
    {
        private Database1Entities _db = new Database1Entities();
        // GET: Index

        public ActionResult Index()
        {
            var workItems = _db.WorkItem.ToList();

            return View(workItems);
        }


        public ActionResult Create()
        {

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include ="Subject,CreateDate,Finished,UserId,Memo")]WorkItem workitem)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _db.WorkItem.Add(workitem);
                    _db.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            return View(workitem);
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
        [Route(Name ="Edit")]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (ModelState.IsValid)
            {
                var workitem = _db.WorkItem.Find(id);

                if (TryUpdateModel(workitem,new string[] { }))
                {
                    try
                    {
                        _db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                        
                    }
                }
            }

            return View();
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

            return RedirectToAction("Index");

        }
    }
}