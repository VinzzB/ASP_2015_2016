﻿using Asp_Mvc_2015_2016.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;
namespace Asp_Mvc_2015_2016.DAL.Services
{
    public class UurRegistratieService : IUurRegistratieService
    {

        IUnitOfWork uow;

        public UurRegistratieService(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        //public List<UurRegistratie> getUnbilledInvoices(int klantId) {
        //    return uow.UurRegistratieRepository.DbSet.Where(p => p.FactuurDetail.KlantId == klantId && p.TeFactureren && p.FactuurId == null).ToList();
        //}

        //public List<FactuurDetails> getUnbilledInvoiceDetails(int klantId) {
        //    var res = uow.FactuurDetailsRepository.DbSet.Where(p => p.uurRegistratie.Any(u => u.TeFactureren && u.FactuurId == null) && p.KlantId == klantId).ToList();
        //}

        public List<SelectListItem> AllAvailableTypenWerk() {
            return uow.TypeWerkRepository.DbSet
                .Where(p => p.GeldigVanaf >= DateTime.Today)
                .ToList()
                .ConvertAll(p => new SelectListItem()
                {
                    Value = p.Id.ToString(),
                    Text = p.WerkType
                });
        }

        public FactuurDetails GetFactuurDetail(int id) {
            return uow.FactuurDetailsRepository.AllIncluding(p => p.uurRegistratie).SingleOrDefault(p => p.Id == id);
        }

        public List<SelectListItem> GetGebruikerKlanten() {
            return uow.KlantRepository.GetKlanten(HttpContext.Current.User.Identity.GetUserId())
                .ToList().ConvertAll(p => new SelectListItem() { Value = p.Id.ToString(), Text = p.NaamBedrijf }); 
        }

        public List<FactuurDetails> GefactureerdeFactuurDetails()
        {
            var res = uow.FactuurDetailsRepository.DbSet
                .Include("uurRegistratie")
                .Where(p => p.uurRegistratie.Any(ur => ur.FactuurId != null));
            return filterByRole(res);
        }

        public List<FactuurDetails> NietGefactureerdeFactuurDetails()
        {
            var res = uow.FactuurDetailsRepository.DbSet
                .Include("uurRegistratie")
                .Where(p =>  p.uurRegistratie.Count == 0 || p.uurRegistratie.Any(ur => ur.FactuurId == null));
            return filterByRole(res);
        }

        private List<FactuurDetails> filterByRole(IEnumerable<FactuurDetails> listToFilter) {
            string uid = HttpContext.Current.User.Identity.GetUserId();
            if (HttpContext.Current.User.IsInRole("Systeem Administrator")) {
                return listToFilter.ToList();
            } else if (HttpContext.Current.User.IsInRole("Departement Admnistrator")) {
                return listToFilter.Where(p => p.CreatedBy.Departementen.Any(d => d.GebruikerId == uid)).ToList();                 
            } if (HttpContext.Current.User.IsInRole("Gebruiker")) {
                return listToFilter.Where(p => p.CreatedById == uid).ToList();
            }
            return new List<FactuurDetails>();
        }
    }
}