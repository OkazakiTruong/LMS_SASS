using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using LMS_SASS.Databases;
using Microsoft.EntityFrameworkCore;

namespace LMS_SASS.Models;

[Table("Users")]
[PrimaryKey(nameof(Id))]
[Index(nameof(Username), IsUnique = true)]
public class UserModel {

	public const string ROLE_USER = "user";

	public const string ROLE_TEACHER = "teacher";

	public const string ROLE_ADMIN = "admin";

	[Key]
	public int Id { get; set; }

	[Display(Name = "Tên tài khoản")]
	[DataType(DataType.Text)]
	[MaxLength(32)]
	public required string Username { get; set; }

	[Display(Name = "Họ tên")]
	[DataType(DataType.Text)]
	[MaxLength(32)]
	public required string Name { get; set; }

	[Display(Name = "Mật khẩu")]
	[DataType(DataType.Password)]
	[MaxLength(128)]
	[JsonIgnore]
	public string? Password { get; set; }

	[Display(Name = "Ngày sinh")]
	[DataType(DataType.DateTime)]
	public required DateTime DOB { get; set; }

	[Display(Name = "Số điện thoại")]
	[DataType(DataType.PhoneNumber)]
	[MaxLength(14)]
	public required string Phone { get; set; }

	[Display(Name = "Email")]
	[DataType(DataType.EmailAddress)]
	[MaxLength(32)]
	public required string Email { get; set; }

	[Display(Name = "Địa chỉ")]
	[DataType(DataType.Text)]
	[MaxLength(128)]
	public required string Address { get; set; }

	[DataType(DataType.Text)]
	[MaxLength(8)]
	public required string Role { get; set; } = ROLE_USER;

	[DataType(DataType.DateTime)]
	public DateTime Created { get; set; }
}
