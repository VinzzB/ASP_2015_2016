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
using Asp_Mvc_2015_2016.ViewModels;
using Microsoft.AspNet.Identity;

namespace Asp_Mvc_2015_2016.Controllers
{
    public class KlantController : BaseController
    {
        private IUnitOfWork unitOfWork;
        //KlantRepository repo;
        //private FacturatieDBContext db = new FacturatieDBContext();

        public KlantController(IUnitOfWork uow)
        {
            //repo = new KlantRepository();
            unitOfWork = uow;
        }

        // GET: Klant
        public ActionResult Index()
        {
            //return View(repo.GetAll());
            //return View(db.Klanten.ToList());
            return View(unitOfWork.KlantRepository.GetAll());
            
        }

        // GET: Klant/Details/5
        public ActionResult Details(int id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Klant klant = db.Klanten.Find(id);
            Klant klant = unitOfWork.KlantRepository.GetById(id);
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
        public ActionResult Create([Bind(Include = "Id,Ondernemingsnr,NaamBedrijf, StraatNr, Postcode, Plaats")] Klant klant)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.KlantRepository.Add(klant);
                unitOfWork.Save();
                //db.Klanten.Add(klant);
                //db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(klant);
        }

        // GET: Klant/Edit/5
        public ActionResult Edit(int id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Klant klant = db.Klanten.Find(id);
            Gebruiker gebruiker = unitOfWork.GebruikerRepository.GetById(HttpContext.User.Identity.GetUserId());
            Klant klant = unitOfWork.KlantRepository.GetById(id);
            if (klant == null)
            {
                return HttpNotFound();
            }
            return View(new EditKlantViewModels()
            {
                klant = klant,
                AvailableDepartments = gebruiker.Departementen.ToList().ConvertAll(d => new SelectListItem()//<GebruikerDep> moet geconverteerd wrdn naar <selectLisItem> voor listbox en dus mr er ook een waarde wrdn toegekend aan de text en value
                        {
                            Value = d.DepartementId.ToString(),
                            Text = d.Departement.Code + " - " + d.Departement.Omschrijving
                        }
                        ),
                SelectedDepartments = klant.Departementen.ToList().ConvertAll(d => d.Departement.Id.ToString())
            });
        }

        // POST: Klant/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken] //tegen spoofing, checken dat data van onze form binnenkomt
        public ActionResult Edit( EditKlantViewModels viewModel) 
        {
            if (ModelState.IsValid)
            {
                Klant k = unitOfWork.KlantRepository.GetById(viewModel.klant.Id); //zie hidden field form model.klant.Id
                TryUpdateModel(k, "klant"); //verwijzing naar 'viewModel.klant' en update het db model met de nieuwe formdata uit het viewmodel.
               //EntityState.Modified wordt ook meegegeven via tryupdate
                SetDepartments(k, viewModel.SelectedDepartments);
                unitOfWork.Save();
                //db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        public void SetDepartments(Klant klant, List<String> departmentIds)
        {
            if (klant.Departementen != null)
            {
                foreach (DepartementKlant item in klant.Departementen.Reverse())
                {
                    //als huidige dep id van klant niet voorkomt in geselecteerde list, moet die terug gedeletet worden
                    if (!departmentIds.Contains(item.DepartementId.ToString()))
                    {
                        unitOfWork.DepartementKlantRepository.Delete(item.Id);
                    }
                }
            }
            //insert new Departements.
            foreach (String d in departmentIds)
            {
                if (klant.Departementen == null || !klant.Departementen.Any(p => p.DepartementId == int.Parse(d)))
                {
                    Departement dep = unitOfWork.DepartementRepository.GetById(int.Parse(d)); // db.Departementen.Find(int.Parse(d));
                    DepartementKlant dk = new DepartementKlant() { Departement = dep, Klant = klant };
                    dep.Klanten.Add(dk);
                    //gebr.Departementen.Add(dg);
                    //unitOfWork.KlantRepository.Update(klant);
                }
            }
        }

        // GET: Klant/Delete/5
        public ActionResult Delete(int id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            Klant klant = unitOfWork.KlantRepository.GetById(id);
            //Klant klant = db.Klanten.Find(id);
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
            //Klant klant = db.Klanten.Find(id);
            Klant klant = unitOfWork.KlantRepository.GetById(id);
            //db.Klanten.Remove(klant);
            unitOfWork.KlantRepository.Delete(id);
            //db.SaveChanges();
            unitOfWork.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
                unitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
