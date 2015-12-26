using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Asp_Mvc_2015_2016.Models;
using Asp_Mvc_2015_2016.DAL;

namespace Asp_Mvc_2015_2016.Controllers
{
    public class DepartementController : BaseController
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        //private FacturatieDBContext db = new FacturatieDBContext();

        // GET: Departement
        public ActionResult Index()
        {
            return View(unitOfWork.DepartementRepository.GetAll());
            //return View(db.Departementen.ToList());
        }

        // GET: Departement/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Departement departement = db.Departementen.Find(id);
            Departement departement = unitOfWork.DepartementRepository.GetById(id);
            if (departement == null)
            {
                return HttpNotFound();
            }
            return View(departement);
        }

        // GET: Departement/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Departement/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Code,Omschrijving")] Departement departement)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.DepartementRepository.Add(departement);
                unitOfWork.Save();
                //db.Departementen.Add(departement);
                //db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(departement);
        }

        // GET: Departement/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Departement departement = unitOfWork.DepartementRepository.GetById(id);
            //Departement departement = db.Departementen.Find(id);
            if (departement == null)
            {
                return HttpNotFound();
            }
            return View(departement);
        }

        // POST: Departement/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Code,Omschrijving")] Departement departement)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.DepartementRepository.context.Entry(departement).State = EntityState.Modified;
                unitOfWork.Save();
                //db.Entry(departement).State = EntityState.Modified;
                //db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(departement);
        }

        // GET: Departement/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Departement departement = unitOfWork.DepartementRepository.GetById(id);
            //Departement departement = db.Departementen.Find(id);
            if (departement == null)
            {
                return HttpNotFound();
            }
            return View(departement);
        }

        // POST: Departement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Departement departement = unitOfWork.DepartementRepository.GetById(id);
            //Departement departement = db.Departementen.Find(id);
            unitOfWork.DepartementRepository.Delete(id);
            //db.Departementen.Remove(departement);
            unitOfWork.Save();
            //db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                unitOfWork.Dispose();
                //db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
