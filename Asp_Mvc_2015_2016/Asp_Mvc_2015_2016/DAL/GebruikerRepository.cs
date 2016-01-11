using Asp_Mvc_2015_2016.Models;
//using Asp_Mvc_2015_2016.Models.DAL;
using Asp_Mvc_2015_2016.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Asp_Mvc_2015_2016.DAL
{
    public class GebruikerRepository : GenericRepository<Gebruiker, String>, IDisposable
    {

        private UserManager<Gebruiker> _userManager;
        private SignInManager<Gebruiker, String> _signInManager;
        public void Dispose()
        {
            if (_userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }
            if (_signInManager != null)
            {
                _signInManager.Dispose();
                _signInManager = null;
            }            
        }
        public GebruikerRepository(FacturatieDBContext context)
            : base(context)
        {  }
               
        public SignInManager<Gebruiker, String> SignInManager
        {
            get {
                if (_signInManager == null) {
                    _signInManager = new SignInManager<Gebruiker, String>(UserManager,
                    HttpContext.Current.GetOwinContext().Authentication);
                }
                return _signInManager; 
            }
        }

        public UserManager<Gebruiker> UserManager
        {
            get {
                if (_userManager == null) {
                    IUserStore<Gebruiker> userStore = new UserStore<Gebruiker>(context);
                    _userManager = new UserManager<Gebruiker>(userStore);
                }
                return _userManager; 
            }
        }
      

    }
}