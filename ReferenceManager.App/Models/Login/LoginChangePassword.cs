using System.ComponentModel.DataAnnotations;

namespace ReferenceManager.App.Models.Login
{
    public class LoginChangePassword
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Nueva Contraseña")]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmacion Contraseña")]
        [Compare("NewPassword")]
        public string Password { get; set; }
    }
}
