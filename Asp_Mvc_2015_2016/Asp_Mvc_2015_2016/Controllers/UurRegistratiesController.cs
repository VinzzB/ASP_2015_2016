using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Asp_Mvc_2015_2016.Models;
using Asp_Mvc_2015_2016.Models.DAL;
using Asp_Mvc_2015_2016.DAL;
using Asp_Mvc_2015_2016.DAL.Services;
using Asp_Mvc_2015_2016.ViewModels;

namespace Asp_Mvc_2015_2016.Controllers
{
    public class UurRegistratiesController : BaseController
    {
        private IUnitOfWork uow;
        private IUurRegistratieService service;

        public UurRegistratiesController(IUnitOfWork uow, IUurRegistratieService service)
        {
            this.uow = uow;
            this.service = service;
        }

        //
        // GET: /UurRegistraties/
        public ViewResult Index()
        {
            return View(uow.UurRegistratieRepository.DbSet.ToList());
        }

        public ViewResult Create(int DetailId)
        {
            FactuurDetails fd = uow.FactuurDetailsRepository.GetById(DetailId);
            return View(new UurRegistratieViewModel()
            {                
                UurRegistratie = new UurRegistratie() { FactuurDetailId = DetailId, FactuurDetail = fd},
                AvailableTypeWerk = uow.TypeWerkRepository.GetAll().ConvertAll(p => new SelectListItem() { Value = p.Id.ToString(), Text = p.WerkType })
            });
        }



        ////
        //// POST: /UurRegistraties/Create
        [HttpPost]
        public ActionResult Create(UurRegistratieViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                uow.UurRegistratieRepository.Add(viewModel.UurRegistratie);
                uow.Save();
                return RedirectToAction("details", new { controller = "FactuurDetails", id = viewModel.UurRegistratie.FactuurDetailId });
            }
            
            return View(viewModel);
        }

        //
        // GET: /UurRegistraties/Details/5
        public ViewResult Details(int id)
        {
            UurRegistratie uurregistratie = uow.UurRegistratieRepository.GetById(id);
            return View(new UurRegistratieViewModel()
            {
                UurRegistratie = uurregistratie,
                AvailableTypeWerk = uow.TypeWerkRepository.GetAll().ConvertAll(p => new SelectListItem()
                {
                    Value = p.Id.ToString(),
                    Text = p.WerkType
                })
            });
        }
        
        ////
        //// GET: /UurRegistraties/Edit/5
        public ActionResult Edit(int id)
        {
            UurRegistratie uurregistratie = uow.UurRegistratieRepository.GetById(id);
            return View(new UurRegistratieViewModel()
            {
                UurRegistratie = uurregistratie,
                AvailableTypeWerk = uow.TypeWerkRepository.GetAll().ConvertAll(p => new SelectListItem() { 
                    Value = p.Id.ToString(),
                    Text = p.WerkType
                })
            });
        }

        ////
        //// POST: /UurRegistraties/Edit/5
        [HttpPost]
        public ActionResult Edit(UurRegistratieViewModel viewmodel)
        {
            if (ModelState.IsValid)
            {
                var ur = uow.UurRegistratieRepository.GetById(viewmodel.UurRegistratie.Id);
                TryUpdateModel(ur, "UurRegistratie");
                uow.Save();
                return RedirectToAction("Details", new { controller = "FactuurDetails", id = ur.FactuurDetailId });
            }            
            return View(viewmodel);
        }

        ////
        //// GET: /UurRegistraties/Delete/5
        public ActionResult Delete(int id)
        {
            UurRegistratie uurregistratie = uow.UurRegistratieRepository.GetById(id);
            return View(uurregistratie);
        }

        ////
        //// POST: /UurRegistraties/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            UurRegistratie uurregistratie = uow.UurRegistratieRepository.GetById(id);
            if (uurregistratie.Factuur == null) {
                uow.UurRegistratieRepository.Delete(id);
                uow.Save();
            }
            return RedirectToAction("UnbilledList", new { controller = "FactuurDetails"});
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                uow.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}