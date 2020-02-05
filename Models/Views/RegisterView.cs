using System.ComponentModel.DataAnnotations;

namespace GymSite.Models.Views
{
    public class RegisterView
    {
        [Required(ErrorMessage = "Last name is not set")]
        public string LastName {get;set;}
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Login incorrect")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Password incorrect")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password is not confirmed")]
        public string CPassword { get; set; }
    }
}