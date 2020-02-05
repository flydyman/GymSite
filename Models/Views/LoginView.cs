using System.ComponentModel.DataAnnotations;

namespace GymSite.Models.Views
{
    public class LoginView
    {
        [Required(ErrorMessage = "Login incorrect")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Password incorrect")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}