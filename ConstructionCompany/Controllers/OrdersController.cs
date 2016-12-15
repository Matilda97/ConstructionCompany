using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ConstructionCompany.Models;
using PagedList;
using PagedList.Mvc;

namespace ConstructionCompany.Controllers
{
    public class OrdersController : Controller
    {
        private ConstructionCompanyContext db = new ConstructionCompanyContext();

        // GET: Orders
        [Authorize]
        public ActionResult Index(int? page, string filter1 = "", string SeniorOrderFind = "", string filter2 = "", string MaterialFind = "")
        {
            if (SeniorOrderFind == "" && MaterialFind == "")
            {
                SeniorOrderFind = filter1;
                MaterialFind = filter2;
            }
            else
            {
                page = 1;
            }
            ViewBag.CurrentFilter1 = SeniorOrderFind;
            ViewBag.CurrentFilter2 = MaterialFind;

            var orders = from n in db.Orders.Include(o => o.Brigade).Include(o => o.Customer).Include(o => o.Job)
                     select n;
            if (SeniorOrderFind != "")
                orders = orders.Include(o => o.Brigade).Include(o => o.Customer).Include(o => o.Job).Where(n => n.Senior.StartsWith(SeniorOrderFind));
            if (MaterialFind != "")
                orders = orders.Include(o => o.Brigade).Include(o => o.Customer).Include(o => o.Job).Where(n => n.Job.Material.NameMaterial.StartsWith(MaterialFind));
            return View(orders.ToList().ToPagedList(page ?? 1, 20));

        }

        // GET: Orders/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.BrigadeID = new SelectList(db.Brigades, "BrigadeID", "NameBrigade");
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "FullName");
            ViewBag.JobID = new SelectList(db.Jobs, "JobID", "NameJob");
            return View();
        }

        // POST: Orders/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderID,CustomerID,JobID,BrigadeID,PriceOrder,DataStartAndEnd,Completion,Payment,Senior")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BrigadeID = new SelectList(db.Brigades, "BrigadeID", "NameBrigade", order.BrigadeID);
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "FullName", order.CustomerID);
            ViewBag.JobID = new SelectList(db.Jobs, "JobID", "NameJob", order.JobID);
            return View(order);
        }

        // GET: Orders/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.BrigadeID = new SelectList(db.Brigades, "BrigadeID", "NameBrigade", order.BrigadeID);
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "FullName", order.CustomerID);
            ViewBag.JobID = new SelectList(db.Jobs, "JobID", "NameJob", order.JobID);
            return View(order);
        }

        // POST: Orders/Edit/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderID,CustomerID,JobID,BrigadeID,PriceOrder,DataStartAndEnd,Completion,Payment,Senior")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BrigadeID = new SelectList(db.Brigades, "BrigadeID", "NameBrigade", order.BrigadeID);
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "FullName", order.CustomerID);
            ViewBag.JobID = new SelectList(db.Jobs, "JobID", "NameJob", order.JobID);
            return View(order);
        }

        // GET: Orders/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
