using System.ComponentModel.DataAnnotations;

namespace LMS_SASS.Models;

public class LoginFormCredentialModel {
	[Display(Name = "Tên tài khoản")]
	public required string Username { get; set; }

	[Display(Name = "Mật khẩu")]
	public required string Password { get; set; }
}