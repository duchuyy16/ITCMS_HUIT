using System.ComponentModel.DataAnnotations;

namespace ITCMS_HUIT.Client.Models
{
    public class ChangePassword
    {
        [Required(ErrorMessage = "User Name is required")]
        public string? Username { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "CurrentPassword is required")]
        public string? CurrentPassword { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "NewPassword is required")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$",
            ErrorMessage = "NewPassword must contain at least 8 characters, including at least one letter, one number, and one special character.")]
        public string? NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "ConfirmNewPassword is required")]
        [Compare("NewPassword", ErrorMessage = "ConfirmNewPassword must match NewPassword.")]
        public string? ConfirmNewPassword { get; set; }
    }
}
