using Asp_Mvc_2015_2016.Models;
using System;
using System.Collections.Generic;
namespace Asp_Mvc_2015_2016.DAL.Services
{
    public interface IUurRegistratieService
    {
        List<FactuurDetails> GefactureerdeFactuurDetails();
        List<FactuurDetails> NietGefactureerdeFactuurDetails();
    }
}
