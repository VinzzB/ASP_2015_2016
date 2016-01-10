using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Asp_Mvc_2015_2016.Models;

namespace Asp_Mvc_2015_2016.DAL
{
    public class GebruikerDepartementRepository : GenericRepository<DepartementGebruiker>
    {
        public GebruikerDepartementRepository(FacturatieDBContext context)
            : base(context)
        {
            
        }
    }
}