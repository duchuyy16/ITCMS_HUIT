using System.ComponentModel.DataAnnotations;

namespace ITCMS_HUIT.Client.Models
{
    public class RegisterModel
    {
		[Required(ErrorMessage = "User Name is required")]
		public string? Username { get; set; }

		[EmailAddress]
		[Required(ErrorMessage = "Email is required")]
		public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
            ErrorMessage = "Mật khẩu phải chứa ít nhất một chữ cái viết thường, một chữ cái viết hoa, một chữ số và một ký tự đặc biệt.")]
        public string? Password { get; set; }
    }
}
