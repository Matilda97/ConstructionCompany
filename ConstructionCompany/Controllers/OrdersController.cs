using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ConstructionCompany.Models;

namespace ConstructionCompany.Controllers
{
    public class OrdersController : Controller
    {
        private ConstructionCompanyContext db = new ConstructionCompanyContext();

        // GET: Orders
        public ActionResult Index()
        {
            var orders = db.Orders.Include(o => o.Brigade).Include(o => o.Customer).Include(o => o.Job);
            return View(orders.ToList());
        }

        // GET: Orders/Details/5
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
        public ActionResult Create()
        {
            ViewBag.BrigadeID = new SelectList(db.Brigades, "BrigadeID", "NameBrigade");
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "FullName");
            ViewBag.JobID = new SelectList(db.Jobs, "JobID", "NameJob");
            return View();
        }

        // POST: Orders/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
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
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
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
