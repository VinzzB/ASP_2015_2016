﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Asp_Mvc_2015_2016.Models;
using Asp_Mvc_2015_2016.ViewModels;
using Asp_Mvc_2015_2016.DAL;
using Microsoft.AspNet.Identity;
namespace Asp_Mvc_2015_2016.Controllers
{
    public class FactuurController : BaseController
    {
        //private FacturatieDBContext db = new FacturatieDBContext();
        private IUnitOfWork unitOfWork;

        public FactuurController(IUnitOfWork uow)
        {
            unitOfWork = uow;
        }

        // GET: Factuur
        public ActionResult Index()
        {
            return View(unitOfWork.FactuurRepository.GetAll());
        }

        // GET: Factuur/Details/5
        public ActionResult Details(int id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            Factuur factuur = unitOfWork.FactuurRepository.GetById(id);
           // var byDetails = factuur.Uurregistraties.GroupBy(p => p.FactuurDetail).Select(s => new { detail = s.Key, uren = s });

            
            if (factuur == null)
            {
                return HttpNotFound();
            }
            return View(factuur);
        }

        // GET: Factuur/Create
        public ActionResult Create()
        {
            string uid = User.Identity.GetUserId();
            CreateFactuurViewModel vm = new CreateFactuurViewModel()
            {            
                Factuur = new Factuur(),
                AvailableKlanten = unitOfWork.KlantRepository.GetKlanten(uid).ToList().ConvertAll(k => new SelectListItem()
                {
                    Value = k.Id.ToString(),
                    Text = k.NaamBedrijf
                })
            };

            return View(vm);
        }

        // POST: Factuur/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,FactuurJaar,FactuurNr,FactuurDatum,Totaal")] Factuur factuur)
            public ActionResult Create(CreateFactuurViewModel vm)
        {
            if (ModelState.IsValid)
            {


                // ->> is in Create view direct toegewezen aan factuur.klantId.
                ////updatemodel factuur
                //Factuur f = new Factuur() { 
                //    Klant = unitOfWork.KlantRepository.GetById(int.Parse(vm.SelectedKlant))
                //};
                //TryUpdateModel(f, "factuur");

                //get UurRegistraties...
                List<UurRegistratie> teFactureren = unitOfWork.UurRegistratieRepository.DbSet.Where(
                    p => p.FactuurDetail.KlantId == vm.Factuur.KlantId 
                        && p.TeFactureren 
                        && p.FactuurId == null
                        && (p.CreatedOn >= vm.FactuurVan && p.CreatedOn <= vm.FactuurTot)).ToList();
                if (teFactureren.Count() > 0)
                {
                    //set total
                    vm.Factuur.Totaal = teFactureren.Sum(p => p.Total);
                    //add to db (so we have an Id assigned to factuur)
                    unitOfWork.FactuurRepository.Add(vm.Factuur); //(vm.factuur);
                    vm.Factuur.Uurregistraties = new List<UurRegistratie>();
                    //now add the registraties to the factuur.                
                    teFactureren.ForEach(p => vm.Factuur.Uurregistraties.Add(p));

                    unitOfWork.Save();
                    //TempData["Message"] = f.FactuurNr; 
                    return RedirectToAction("Details", new { id = vm.Factuur.Id });
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Er zijn geen registraties om te factureren in deze periode voor de opgegeven klant.");                    
                }
            }

            string uid = User.Identity.GetUserId();
            vm.AvailableKlanten = unitOfWork.KlantRepository.GetKlanten(uid).ToList().ConvertAll(k => new SelectListItem()
                {
                    Value = k.Id.ToString(),
                    Text = k.NaamBedrijf
                });
            return View(vm);
        }

        
     
        public PartialViewResult _GetTeFacturerenForKlant(int kId)
        {
            //CreateFactuurViewModel vm = new CreateFactuurViewModel(){vm.SelectedKlant = }
            //int klantId = int.Parse(kId);
            Klant k = unitOfWork.KlantRepository.GetById(kId);
            List<UurRegistratie> teFactureren = unitOfWork.UurRegistratieRepository.DbSet.Where(p => p.FactuurDetail.KlantId == kId && p.TeFactureren && p.FactuurId == null).ToList();  
            return PartialView("_GetTeFacturerenForKlant", teFactureren);
        }
        

        // GET: Factuur/Edit/5
        public ActionResult Edit(int id)
        {
            Factuur factuur = unitOfWork.FactuurRepository.GetById(id);
            if (factuur == null)
            {
                return HttpNotFound();
            }
            return View(factuur);
        }

        // POST: Factuur/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FactuurJaar,FactuurNr,FactuurDatum,Totaal")] Factuur factuur)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(factuur).State = EntityState.Modified;
                unitOfWork.FactuurRepository.context.Entry(factuur).State = EntityState.Modified;
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(factuur);
        }

        //// GET: Factuur/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    //if (id == null)
        //    //{
        //    //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    //}
        //    Factuur factuur = unitOfWork.FactuurRepository.GetById(id);
        //    if (factuur == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(factuur);
        //}

        //// POST: Factuur/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Factuur factuur = unitOfWork.FactuurRepository.GetById(id);
        //    unitOfWork.FactuurRepository.Delete(id); 
        //    unitOfWork.Save();
        //    return RedirectToAction("Index");
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                unitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
