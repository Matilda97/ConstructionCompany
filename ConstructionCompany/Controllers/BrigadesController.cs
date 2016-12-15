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
    public class BrigadesController : Controller
    {
        private ConstructionCompanyContext db = new ConstructionCompanyContext();

        // GET: Brigades
        [Authorize]
        public ActionResult Index(int? page, string filter1 = "", string BrigadeFind = "", string filter2 = "", string BrigadierFind = "")
        {
            if (BrigadeFind == "" && BrigadierFind == "")
            {
                BrigadeFind = filter1;
                BrigadierFind = filter2;
            }
            else
            {
                page = 1;
            }
            ViewBag.CurrentFilter1 = BrigadeFind;
            ViewBag.CurrentFilter2 = BrigadierFind;

            var brigades = from n in db.Brigades
                           select n;
            if (BrigadeFind != "")
                brigades = brigades.Where(n => n.NameBrigade.StartsWith(BrigadeFind));
            if (BrigadierFind != "")
                brigades = brigades.Where(n => n.Brigadier.StartsWith(BrigadierFind));
            return View(brigades.ToList().ToPagedList(page ?? 1, 20));
        }

        // GET: Brigades/Details/5
        [Authorize]
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
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Brigades/Create
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Brigade brigade = db.Brigades.Find(id);
            db.Brigades.Remove(brigade);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult Brigade_sJobs(int? page, string filter = "", string JobFind = "")
        {
            if (JobFind == "")
            {
                JobFind = filter;
            }
            else
            {
                page = 1;
            }
            ViewBag.CurrentFilter1 = JobFind;

            var jobs = from n in db.Orders
                           select n;
            if (JobFind != "")
                jobs = jobs.Where(n => n.Job.NameJob.StartsWith(JobFind));
            return View(jobs.ToList().ToPagedList(page ?? 1, 20));
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
