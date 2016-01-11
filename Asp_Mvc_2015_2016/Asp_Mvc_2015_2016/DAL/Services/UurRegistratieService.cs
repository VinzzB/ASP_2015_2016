using Asp_Mvc_2015_2016.Models;
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
                .Where(p => p.uurRegistratie.Any(ur => ur.FactuurId == null));
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