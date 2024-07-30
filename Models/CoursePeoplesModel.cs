namespace LMS_SASS.Models;

public class CoursePeoplesModel {
	public required CourseModel Course { get; set; }

	public required List<CourseUserModel> CourseUsers { get; set; }
}
