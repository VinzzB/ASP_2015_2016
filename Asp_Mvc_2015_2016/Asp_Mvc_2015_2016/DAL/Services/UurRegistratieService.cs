using Asp_Mvc_2015_2016.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Asp_Mvc_2015_2016.DAL.Services
{
    public class UurRegistratieService : IUurRegistratieService
    {

        IUnitOfWork uow;

        public UurRegistratieService(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public List<FactuurDetails> GefactureerdeFactuurDetails()
        {
            //var res = from fd in uow.FactuurDetailsRepository.DbSet
            //          join ur in uow.UurRegistratieRepository.DbSet on fd.Id equals ur.FactuurDetailId
            //          where ur.FactuurId != null
            //          select fd;
            var res = uow.FactuurDetailsRepository.DbSet
                .Include("uurRegistratie")
                .Where(p => p.uurRegistratie.Count(ur => ur.FactuurId != null) > 0)
                .ToList();
            return res;
        }

        public List<FactuurDetails> NietGefactureerdeFactuurDetails()
        {
            //var res = (from fd in uow.FactuurDetailsRepository.DbSet.Include("uurRegistratie")
            //          join ur in uow.UurRegistratieRepository.DbSet.DefaultIfEmpty() on fd.Id equals ur.FactuurDetailId into tmp_ur //join met de tabel Rollen
            //          from fdur in tmp_ur.DefaultIfEmpty() //for outerjoin!
            //          where fdur.FactuurId == null
            //          select fd).ToList();
            var res = uow.FactuurDetailsRepository.DbSet
                .Include("uurRegistratie")
                .Where(p => p.uurRegistratie.Count == 0 || p.uurRegistratie.Count(ur => ur.FactuurId == null) > 0)
                .ToList();
            return res;
        }

    }
}