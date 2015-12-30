using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
namespace Asp_Mvc_2015_2016.Models
{    
    /// <summary>Default _BaseInfo class (With an integer type as Id field</summary>
    public abstract class _BaseInfo : _BaseInfo<int> { }

    /// <summary>_BaseInfo class With custom type as Id field. The type is passed as typeparam.</summary>
    /// <typeparam name="T">The used type for the Id field.</typeparam>
    public abstract class _BaseInfo<T>
    {
        public T Id { get; set; }

        public string    CreatedById { get; set; }     
        public DateTime? CreatedOn   { get; set; }

        public string    EditedById { get; set; }
        public DateTime? EditedOn   { get; set; }


        /* No 'virtuals' allowed in abstract class! 
         * Navigation properties moeten in conrete classe gezet worden */
        //public virtual Gebruiker CreatedBy { get; set; }
        //public virtual Gebruiker EditedBy { get; set; }

        public _BaseInfo()
        {
            try //try needed for PM> Update-Database command. (Null exception on HttpContext...)
            {
                /* DEFAULTS (not sure if this is the correct way) */
                CreatedById = HttpContext.Current.User.Identity.GetUserId(); //Referenced in Microsoft.AspNet.Identity!
                CreatedOn = DateTime.Now;
            }
            catch (Exception)
            { }            
        }
    }


}