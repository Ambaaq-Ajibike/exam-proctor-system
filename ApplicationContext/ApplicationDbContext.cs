using exam_proctor_system.Models.Entities;
using Microsoft.EntityFrameworkCore;
namespace exam_proctor_system.ApplicationContext
{

	public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
	{

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{

			modelBuilder.Entity<User>().HasData(
				new User
				{
					Email = "ajibikeabulqayyum04@gmail.com",
					Password = "ambaaq",
					FaceId = "id",
					Role = Role.Admin
				});
		}

		public DbSet<Exam> Exams { get; set; }
		public DbSet<Question> Questions { get; set; }
		public DbSet<Option> Options { get; set; }
		public DbSet<Candidate> Candidates { get; set; }
		public DbSet<Result> Results { get; set; }
		public DbSet<User> Users { get; set; }
	}
}