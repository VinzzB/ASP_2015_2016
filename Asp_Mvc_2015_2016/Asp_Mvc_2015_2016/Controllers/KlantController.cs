using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Asp_Mvc_2015_2016.Models;

namespace Asp_Mvc_2015_2016.Controllers
{
    public class KlantController : CultureController
    {
        private FacturatieDBContext db = new FacturatieDBContext();

        // GET: Klant
        public ActionResult Index()
        {
            return View(db.Klanten.ToList());
        }

        // GET: Klant/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Klant klant = db.Klanten.Find(id);
            if (klant == null)
            {
                return HttpNotFound();
            }
            return View(klant);
        }

        // GET: Klant/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Klant/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Ondernemingsnr,NaamBedrijf")] Klant klant)
        {
            if (ModelState.IsValid)
            {
                db.Klanten.Add(klant);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(klant);
        }

        // GET: Klant/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Klant klant = db.Klanten.Find(id);
            if (klant == null)
            {
                return HttpNotFound();
            }
            return View(klant);
        }

        // POST: Klant/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Ondernemingsnr,NaamBedrijf")] Klant klant)
        {
            if (ModelState.IsValid)
            {
                db.Entry(klant).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(klant);
        }

        // GET: Klant/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Klant klant = db.Klanten.Find(id);
            if (klant == null)
            {
                return HttpNotFound();
            }
            return View(klant);
        }

        // POST: Klant/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Klant klant = db.Klanten.Find(id);
            db.Klanten.Remove(klant);
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
