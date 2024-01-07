using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITCMS_HUIT.DTO
{

    public class ChangePassword
    {
        [Required(ErrorMessage = "Username is required")]
        public string? Username { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "CurrentPassword is required")]
        public string? CurrentPassword { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "NewPassword is required")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$",
            ErrorMessage = "Mật khẩu mới phải chứa ít nhất 8 ký tự, bao gồm ít nhất một chữ cái, một số và một ký tự đặc biệt.")]
        public string? NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "ConfirmNewPassword is required")]
        [Compare("NewPassword", ErrorMessage = "Xác nhận mật khẩu mới phải khớp với Mật khẩu mới.")]
        public string? ConfirmNewPassword { get; set; }
    }
}
