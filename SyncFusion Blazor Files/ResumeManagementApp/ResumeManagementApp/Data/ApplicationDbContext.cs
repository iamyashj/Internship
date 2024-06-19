using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ResumeManagementApp.Entities;
using static ResumeManagementApp.Entities.Role;

namespace ResumeManagementApp.Data
{
	public class ApplicationDbContext : IdentityDbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}



		public DbSet<JobPosting> JobPostings { get; set; }

		public DbSet<JobApplication> JobApplications { get; set; }

		public DbSet<UserProfile> UserProfiles { get; set; }

		public DbSet<Resume> Resumes { get; set; }

		public DbSet<Role> Roles { get; set; }



		public DbSet<SeedData> SeedData { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<JobPosting>(entity =>
			{
				entity.HasKey(e => e.Id);
				entity.Property(e => e.Title).IsRequired().HasMaxLength(100);
				entity.Property(e => e.Description).IsRequired().HasMaxLength(500);
				entity.Property(e => e.Location).IsRequired().HasMaxLength(100);
				entity.Property(e => e.PostedDate).IsRequired();
				entity.Property(e => e.ExpirationDate).IsRequired();
				entity.Property(e => e.IsClosed).IsRequired();
				entity.HasMany(e => e.JobApplications).WithOne(e => e.JobPosting).HasForeignKey(e => e.JobPostingId);
			});

			modelBuilder.Entity<JobApplication>(entity =>
			{
				entity.HasKey(e => e.Id);
				entity.Property(e => e.ApplicationDate).IsRequired();
				entity.HasOne(e => e.JobPosting).WithMany(e => e.JobApplications).HasForeignKey(e => e.JobPostingId);
				entity.HasOne(e => e.UserProfile).WithMany(e => e.JobApplications).HasForeignKey(e => e.UserProfileId);
			});

			modelBuilder.Entity<UserProfile>(entity =>
			{
				entity.HasKey(e => e.Id);
				entity.Property(e => e.FirstName).IsRequired().HasMaxLength(50);
				entity.Property(e => e.LastName).IsRequired().HasMaxLength(50);
				entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
				entity.HasMany(e => e.JobApplications).WithOne(e => e.UserProfile).HasForeignKey(e => e.UserProfileId);
				entity.HasMany(e => e.Resumes).WithOne(e => e.UserProfile).HasForeignKey(e => e.UserProfileId);
				entity.HasMany(e => e.UserRoles).WithOne(e => e.UserProfile).HasForeignKey(e => e.UserId);
			});

			modelBuilder.Entity<Resume>(entity =>
			{
				entity.HasKey(e => e.Id);
				entity.Property(e => e.Title).IsRequired().HasMaxLength(100);
				entity.Property(e => e.FileName).IsRequired().HasMaxLength(100);
				entity.Property(e => e.FileContent).IsRequired();
				entity.HasOne(e => e.UserProfile).WithMany(e => e.Resumes).HasForeignKey(e => e.UserProfileId);
			});

			modelBuilder.Entity<Role>(entity =>
			{
				entity.HasKey(e => e.Id);
				entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
				entity.HasMany(e => e.UserRoles).WithOne(e => e.Role).HasForeignKey(e => e.RoleId);
			});

			modelBuilder.Entity<UserRole>(entity =>
			{
				entity.HasKey(e => new { e.UserId, e.RoleId });
				entity.HasOne(e => e.UserProfile).WithMany(e => e.UserRoles).HasForeignKey(e => e.UserId);
				entity.HasOne(e => e.Role).WithMany(e => e.UserRoles).HasForeignKey(e => e.RoleId);
			});
			modelBuilder.Entity<SeedData>(entity =>
			{
				entity.HasKey(e => e.Id);
				entity.Property(e => e.EntityName).IsRequired().HasMaxLength(100);
				entity.Property(e => e.SeedJson).IsRequired();
			});
		}
	}
} 


