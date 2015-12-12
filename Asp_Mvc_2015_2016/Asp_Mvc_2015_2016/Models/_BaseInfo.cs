using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
namespace Asp_Mvc_2015_2016.Models
{
    public abstract class _BaseInfo
    {
        public int Id { get; set; }
        [Column(name: "CreatedBy")]
        public virtual Gebruiker CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }

        //History for last change. 
        [Column(name: "EditedBy")]
        public virtual Gebruiker EditedBy { get; set; }
        public DateTime? EditedOn { get; set; }

        public _BaseInfo()
        {
        //    CreatedBy = HttpContext.Current.User
            CreatedOn = DateTime.Now;
        }
    }


}