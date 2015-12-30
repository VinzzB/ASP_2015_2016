using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

//VB: ADDED RESOURCES EN CLASS VERPLAATST NAAR VIEWMODELS FOLDER.
namespace Asp_Mvc_2015_2016.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email", ResourceType = typeof(Resources.CultureResource))]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email", ResourceType = typeof(Resources.CultureResource))]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        //VB: Changed Login from Mail to Username (Which it actually is in the controller...)
        [Required]
        [Display(Name = "Username", ResourceType= typeof(Resources.CultureResource))]
      //  [EmailAddress]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(Resources.CultureResource))]
        public string Password { get; set; }

        [Display(Name = "RememberMe", ResourceType = typeof(Resources.CultureResource))]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        //VB: Added username on Registrationform
        [Required]
        [MinLength(4)]
        [Display(Name = "Username", ResourceType = typeof(Resources.CultureResource))]
        public string UserName { get; set; } 

        [Required]
        [EmailAddress]
        [Display(Name = "Email", ResourceType = typeof(Resources.CultureResource))]
        public string Email { get; set; }
        //VB: Added Voornaam, Achternaam on Registrationform
        [Required]
        [Display(Name = "Firstname", ResourceType = typeof(Resources.CultureResource))]
        public string Voornaam { get; set; }

        [Required]
        [Display(Name = "Lastname", ResourceType = typeof(Resources.CultureResource))]
        public string Achternaam { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6, ErrorMessageResourceName="ValidationLengthError", ErrorMessageResourceType= typeof(Resources.CultureResource))]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(Resources.CultureResource))]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "PasswordConfirm", ResourceType = typeof(Resources.CultureResource))]
        [Compare("Password", ErrorMessageResourceName="PasswordMatchError", ErrorMessageResourceType=typeof(Resources.CultureResource))]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email", ResourceType = typeof(Resources.CultureResource))]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6, ErrorMessageResourceName = "ValidationLengthError", ErrorMessageResourceType = typeof(Resources.CultureResource))]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(Resources.CultureResource))]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "PasswordConfirm", ResourceType = typeof(Resources.CultureResource))]
        [Compare("Password", ErrorMessageResourceName = "PasswordMatchError", ErrorMessageResourceType = typeof(Resources.CultureResource))]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email", ResourceType = typeof(Resources.CultureResource))]
        public string Email { get; set; }
    }
}
