using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace PersonalTaskManager.Web.Models.Account
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Vui lòng nhập tên đăng nhập.")]
        [Display(Name = "Tên đăng nhập")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Tên đăng nhập từ 3–50 ký tự.")]
        [RegularExpression(@"^[a-zA-Z0-9_]+$", ErrorMessage = "Chỉ dùng chữ, số và dấu gạch dưới.")]
        [Remote("IsUsernameAvailable", "Account", ErrorMessage = "Tên đăng nhập đã tồn tại.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập email.")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
        [Display(Name = "Email")]
        [StringLength(100)]
        [Remote("IsEmailAvailable", "Account", ErrorMessage = "Email đã được sử dụng.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu.")]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Mật khẩu từ 6–100 ký tự.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Vui lòng xác nhận mật khẩu.")]
        [DataType(DataType.Password)]
        [Display(Name = "Xác nhận mật khẩu")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Mật khẩu xác nhận không khớp.")]
        public string ConfirmPassword { get; set; }
    }
}
