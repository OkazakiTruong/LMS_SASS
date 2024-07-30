using LMS_SASS.Models;
using Microsoft.EntityFrameworkCore;

namespace LMS_SASS.Databases;

public class DatabaseContext : DbContext {

    public DbSet<UserModel> Users { get; set; }

	public DbSet<CourseModel> Courses { get; set; }

	public DbSet<CourseUserModel> CourseUsers { get; set; }
	public DbSet<AssignmentModel> Assignment { get; set; }
	public DbSet<MeetingModel> Meeting { get; set; }
	public DbSet<ActivityModel> Activities { get; set; }
	public DbSet<AssignmentSubmissionsModel> AssignmentSubmissions { get; set; }
	public DbSet<QuizModel> Quiz { get; set; }
	public DbSet<QuizQuestionModel> QuizQuestions { get; set; }
	public DbSet<QuizAttemptModel> QuizAttempts { get; set; }
	public DbSet<QuizAttemptDetailModel> QuizAttemptDetails { get; set; }

	public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
		
    }
}
