using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Asp_Mvc_2015_2016.Models;
//using Asp_Mvc_2015_2016.Models.DAL;

namespace Asp_Mvc_2015_2016.DAL
{
    public class RolesRepository : GenericRepository<IdentityRole>
    {
        public RolesRepository(FacturatieDBContext context)
            : base(context)
        { }

        public List<String> AllRolesAsStringList() {
            return DbSet.ToList().ConvertAll(p => p.Name); 
        }

    }
}