using Asp_Mvc_2015_2016.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
namespace Asp_Mvc_2015_2016.DAL.Services
{
    public interface IUurRegistratieService
    {
        List<SelectListItem> GetGebruikerKlanten();
        List<FactuurDetails> GefactureerdeFactuurDetails();
        List<FactuurDetails> NietGefactureerdeFactuurDetails();
        FactuurDetails GetFactuurDetail(int id);
    }
}
