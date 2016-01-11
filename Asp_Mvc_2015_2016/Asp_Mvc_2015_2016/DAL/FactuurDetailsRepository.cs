using Asp_Mvc_2015_2016.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using Asp_Mvc_2015_2016.Models.DAL;
using Microsoft.AspNet.Identity;
namespace Asp_Mvc_2015_2016.DAL
{
    public class FactuurDetailsRepository : GenericRepository<FactuurDetails>
    {
        public FactuurDetailsRepository(FacturatieDBContext context)
            : base(context)
        { }

    }
}