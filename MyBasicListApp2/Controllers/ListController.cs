using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyBasicListApp2.Models.DB;
using MyBasicListApp2.Models.EntityManager;
using System.Data.Entity;
using System.Net;

namespace MyBasicListApp2.Controllers
{
    public class ListController : Controller
    {
        // GET: List
        public ActionResult Index()
        {
            return View();
        }

        #region ToDolIst View and Post for AddToList
        public ActionResult ToDoList()
        {
            return View();
        }


        // In the post I pass in the viewmodel that the view will use to collect information
        [HttpPost]
        public ActionResult ToDoList(tblBasicList LVM)
        {
            if (ModelState.IsValid)
            {
                ListManager LM = new ListManager();
                // Addtolist is a method belonging to ListManager and can only be called if ListManager 
                // object is created in the current context.
                LM.AddToList(LVM);
                return RedirectToAction("MyToDoList", "Home");

            }
            return View("MyToDoList", "Home");
        }
        #endregion

        #region Delete and DeleteConfirm actions
        public ActionResult Delete(int? id)
        {
            using (MyBasicListAppEntities db = new MyBasicListAppEntities())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                // Fetch the table row to display in the view
                tblBasicList tbl = db.tblBasicLists.Find(id);
                if (tbl == null)
                {
                    return HttpNotFound();
                }
                return View(tbl);
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            using (MyBasicListAppEntities db = new MyBasicListAppEntities())
            {
                // Find the row based on id from form (primary key)
                tblBasicList tbl = db.tblBasicLists.Find(id);
                // remove the whole row at that id
                db.tblBasicLists.Remove(tbl);
                db.SaveChanges();
            }
            return RedirectToAction("MyToDoList", "Home");
        }
        #endregion

        #region Edit methods

        // For Edit method:
        // Create the action for getting the view with the specified table row to be editted
        //              This involves passing in the primary key from the selection by the user in the form



        public ActionResult Edit(int? id)
        {
            using (MyBasicListAppEntities db = new MyBasicListAppEntities())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                tblBasicList tbl = db.tblBasicLists.Find(id);
                if (tbl == null)
                {
                    return HttpNotFound();
                }
                return View(tbl);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BasicListID, InputDate, InputItem, InputImportance, ItemCompletion")] tblBasicList BasicList)
        {
            using (MyBasicListAppEntities db = new MyBasicListAppEntities())
            {
                if(ModelState.IsValid)
                {
                    db.Entry(BasicList).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("MyToDoList", "Home");
                }
                return View(BasicList);
            }
        }

        #endregion
    }
}