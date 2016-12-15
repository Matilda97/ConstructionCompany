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
    public class JobsController : Controller
    {
        private ConstructionCompanyContext db = new ConstructionCompanyContext();

        // GET: Jobs
        [Authorize]
        public ActionResult Index(int? page, string filter, string JobFind = "")
        {
            if (JobFind == "")
            {
                JobFind = filter;
            }
            else
            {
                page = 1;
            }
            ViewBag.CurrentFilter = JobFind;
            if (JobFind != null)
            {
                var jobs = from n in db.Jobs.Include(j => j.Material)
                           where n.NameJob.StartsWith(JobFind)
                               select n;
                return View(jobs.ToList().ToPagedList(page ?? 1, 20));
            }
            else
            {
                var jobs = from n in db.Jobs.Include(j => j.Material)
                           select n;
                return View(jobs.ToList().ToPagedList(page ?? 1, 20));
            }
        }

        // GET: Jobs/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = db.Jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // GET: Jobs/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.MaterialID = new SelectList(db.Materials, "MaterialID", "NameMaterial");
            return View();
        }

        // POST: Jobs/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "JobID,NameJob,DescriptionJob,PriceJob,MaterialID")] Job job)
        {
            if (ModelState.IsValid)
            {
                db.Jobs.Add(job);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaterialID = new SelectList(db.Materials, "MaterialID", "NameMaterial", job.MaterialID);
            return View(job);
        }

        // GET: Jobs/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = db.Jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaterialID = new SelectList(db.Materials, "MaterialID", "NameMaterial", job.MaterialID);
            return View(job);
        }

        // POST: Jobs/Edit/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "JobID,NameJob,DescriptionJob,PriceJob,MaterialID")] Job job)
        {
            if (ModelState.IsValid)
            {
                db.Entry(job).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaterialID = new SelectList(db.Materials, "MaterialID", "NameMaterial", job.MaterialID);
            return View(job);
        }

        // GET: Jobs/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = db.Jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // POST: Jobs/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Job job = db.Jobs.Find(id);
            db.Jobs.Remove(job);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult ListOfJobs(int? page, DateTime? DateFind, DateTime? filter4, string filter1 = "", string JobFind = "", string filter2 = "", string PriceFind = "", string filter3 = "", string CustomerFind = "")
        {
            if (JobFind == "" && PriceFind == "" && CustomerFind == "" && DateFind == null)
            {
                JobFind = filter1;
                PriceFind = filter2;
                CustomerFind = filter3;
                DateFind = filter4;
            }
            else
            {
                page = 1;
            }
            ViewBag.CurrentFilter1 = JobFind;
            ViewBag.CurrentFilter2 = PriceFind;
            ViewBag.CurrentFilter3 = CustomerFind;
            ViewBag.CurrentFilter4 = DateFind;

            
            var list = from m in db.Orders.Include(o => o.Brigade).Include(o => o.Customer).Include(o => o.Job)
                       select m;
            if (JobFind != "")
                list = list.Include(o => o.Brigade).Include(o => o.Customer).Include(o => o.Job).Where(m => m.Job.NameJob.StartsWith(JobFind));
            if (PriceFind != "")
                list = list.Include(o => o.Brigade).Include(o => o.Customer).Include(o => o.Job).Where(m => m.Job.PriceJob.ToString().StartsWith(PriceFind));
            if (CustomerFind != "")
                list = list.Include(o => o.Brigade).Include(o => o.Customer).Include(o => o.Job).Where(m => m.Customer.FullName.StartsWith(CustomerFind));
            if (DateFind != null)
                list = list.Include(o => o.Brigade).Include(o => o.Customer).Include(o => o.Job).Where(m => m.DateEnd == DateFind);
            return View(list.ToList().ToPagedList(page ?? 1, 20)); ;
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
