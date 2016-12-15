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
    public class MaterialsController : Controller
    {
        private ConstructionCompanyContext db = new ConstructionCompanyContext();

        // GET: Materials
        [Authorize]
        public ActionResult Index(int? page, string filter, string MaterialFind = "")
        {
            if (MaterialFind == "")
            {
                MaterialFind = filter;
            }
            else
            {
                page = 1;
            }
            ViewBag.CurrentFilter = MaterialFind;
            if (MaterialFind != null)
            {
                var materials = from n in db.Materials
                               where n.NameMaterial.StartsWith(MaterialFind)
                               select n;
                return View(materials.ToList().ToPagedList(page ?? 1, 20));
            }
            else
            {
                var materials = from n in db.Materials
                               select n;
                return View(materials.ToList().ToPagedList(page ?? 1, 20));
            }
        }

        // GET: Materials/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Material material = db.Materials.Find(id);
            if (material == null)
            {
                return HttpNotFound();
            }
            return View(material);
        }

        // GET: Materials/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Materials/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaterialID,NameMaterial,Packaging,DescriptionMaterial,PriceMaterial")] Material material)
        {
            if (ModelState.IsValid)
            {
                db.Materials.Add(material);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(material);
        }

        // GET: Materials/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Material material = db.Materials.Find(id);
            if (material == null)
            {
                return HttpNotFound();
            }
            return View(material);
        }

        // POST: Materials/Edit/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaterialID,NameMaterial,Packaging,DescriptionMaterial,PriceMaterial")] Material material)
        {
            if (ModelState.IsValid)
            {
                db.Entry(material).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(material);
        }

        // GET: Materials/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Material material = db.Materials.Find(id);
            if (material == null)
            {
                return HttpNotFound();
            }
            return View(material);
        }

        // POST: Materials/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Material material = db.Materials.Find(id);
            db.Materials.Remove(material);
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
