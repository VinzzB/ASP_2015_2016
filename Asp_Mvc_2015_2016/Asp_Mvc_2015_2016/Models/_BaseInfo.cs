using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Asp_Mvc_2015_2016.Models
{
    public abstract class _BaseInfo
    {
        public Gebruiker CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }

        //History for last change. 
        public Gebruiker EditedBy { get; set; }
        public DateTime EditedOn { get; set; }

        //public _BaseInfo()
        //{
        //    CreatedBy = HttpContext.Current.User
        //}
    }


}