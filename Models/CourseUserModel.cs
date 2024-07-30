using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LMS_SASS.Models;

[Table("CourseUsers")]
[PrimaryKey(nameof(Id))]
public class CourseUserModel {
	public const string ROLE_STUDENT = "student";

	public const string ROLE_TEACHER = "teacher";

	[Key]
	public int Id { get; set; }

	[ForeignKey(nameof(User))]
	public required int UserId { get; set; }

	public UserModel? User { get; set; }

	[ForeignKey(nameof(Course))]
	public required int CourseId { get; set; }

	public CourseModel? Course { get; set; }

	[DataType(DataType.Text)]
	[MaxLength(8)]
	public required string Role { get; set; } = ROLE_STUDENT;
}