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
    public class BrigadesController : Controller
    {
        private ConstructionCompanyContext db = new ConstructionCompanyContext();

        // GET: Brigades
        public ActionResult Index()
        {
            return View(db.Brigades.ToList());
        }

        // GET: Brigades/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Brigade brigade = db.Brigades.Find(id);
            if (brigade == null)
            {
                return HttpNotFound();
            }
            return View(brigade);
        }

        // GET: Brigades/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Brigades/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BrigadeID,NameBrigade,Brigadier,Composition")] Brigade brigade)
        {
            if (ModelState.IsValid)
            {
                db.Brigades.Add(brigade);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(brigade);
        }

        // GET: Brigades/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Brigade brigade = db.Brigades.Find(id);
            if (brigade == null)
            {
                return HttpNotFound();
            }
            return View(brigade);
        }

        // POST: Brigades/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BrigadeID,NameBrigade,Brigadier,Composition")] Brigade brigade)
        {
            if (ModelState.IsValid)
            {
                db.Entry(brigade).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(brigade);
        }

        // GET: Brigades/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Brigade brigade = db.Brigades.Find(id);
            if (brigade == null)
            {
                return HttpNotFound();
            }
            return View(brigade);
        }

        // POST: Brigades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Brigade brigade = db.Brigades.Find(id);
            db.Brigades.Remove(brigade);
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
