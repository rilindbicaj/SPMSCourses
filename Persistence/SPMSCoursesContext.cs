using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class SPMSCoursesContext : DbContext
    {
        public DbSet<AcademicStaff> AcademicStaff { get; set; }
        
        public DbSet<Course> Courses { get; set; }
        
        public DbSet<Courses_AcademicStaff> CoursesAcademicStaff { get; set; }
        public DbSet<ExamSeason> ExamSeasons { get; set; }
        
        public DbSet<Grade>  Grades { get; set; }
        
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
        
        public DbSet<CourseCategory> CourseCategories { get; set; }
        
        public DbSet<GradeStatus> GradeStatuses { get; set; }
        
        public DbSet<ExamSeasonStatus> ExamSeasonStatuses { get; set; }
        
        public DbSet<LectureRole> LectureRoles { get; set; }
        
        public SPMSCoursesContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AcademicStaff>(entity =>
            {
                entity.HasKey(a => a.UserId);
            });

            modelBuilder.Entity<LectureRole>(entity =>
            {
                entity.HasKey(e => e.LectureRoleId);
            });

            modelBuilder.Entity<Courses_AcademicStaff>(entity =>
            {
                entity.HasKey(e => new { e.CourseId, e.AcademicStaffId });
                entity.HasOne(e => e.Course)
                    .WithMany(c => c.CoursesAcademicStaff)
                    .HasForeignKey(e => e.CourseId);
                entity.HasOne(e => e.AcademicStaff)
                    .WithMany(a => a.CoursesAcademicStaff)
                    .HasForeignKey(c => c.AcademicStaffId);
                entity.HasOne(e => e.LectureRole)
                    .WithMany(e => e.CoursesAcademicStaves)
                    .HasForeignKey(e => e.LectureRoleId)
                    .HasConstraintName("FK_CoursesAcademicStaff_LectureRole");
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasKey(e => e.CourseId);
                entity.HasOne(e => e.CourseCategory)
                    .WithMany(c => c.Courses)
                    .HasForeignKey(e => e.CourseCategoryId)
                    .HasConstraintName("FK_Courses_CourseCategories");
                entity.HasOne(e => e.Semester)
                    .WithMany(s => s.Courses)
                    .HasForeignKey(e => e.SemesterId)
                    .HasConstraintName("FK_Courses_Semesters");
                entity.HasOne(e => e.Specialization)
                    .WithMany(s => s.Courses)
                    .HasForeignKey(e => e.SpecializationId)
                    .HasConstraintName("FK_Courses_Specializations");
            });

            modelBuilder.Entity<ExamSeason>(entity =>
            {
                entity.HasKey(e => e.ExamSeasonId);
                entity.HasOne(e => e.CurrentStatus)
                    .WithMany(e => e.ExamSeasons)
                    .HasForeignKey(e => e.StatusId)
                    .HasConstraintName("FK_ExamSeasons_ExamSeasonStatuses");
            });

            modelBuilder.Entity<Grade>(entity =>
            {
                entity.HasKey(e => e.GradeId);
                entity.HasOne(e => e.Status)
                    .WithMany(e => e.Grades)
                    .HasForeignKey(e => e.StatusId)
                    .HasConstraintName("FK_Grades_GradeStatuses");
                entity.HasOne(e => e.ExamSeason)
                    .WithMany(e => e.Grades)
                    .HasForeignKey(e => e.ExamSeasonId)
                    .HasConstraintName("FK_Grades_ExamSeasons");
                entity.HasOne(e => e.CoursesAcademicStaff)
                    .WithMany(c => c.Grades)
                    .HasForeignKey(e => new {e.CourseId, e.AcademicStaffId})
                    .HasConstraintName("FK_Grades_CoursesAcademicStaff");
            });

            modelBuilder.Entity<Semester>(entity =>
            {
                entity.HasKey(e => e.SemesterId);
            });

            modelBuilder.Entity<Specialization>(entity =>
            {
                entity.HasKey(e => e.SpecializationId);
                entity.HasOne(e => e.ParentSpecialization)
                    .WithMany(e => e.ChildSpecializations)
                    .HasForeignKey(e => e.ParentSpecializationId)
                    .IsRequired(false)
                    .HasConstraintName("FK_Specializations_SpecializaitionsChild");
            });

            modelBuilder.Entity<CourseCategory>(entity =>
            {
                entity.HasKey(e => e.CategoryId);
            });
            
            modelBuilder.Entity<GradeStatus>(entity =>
            {
                entity.HasKey(e => e.StatusId);
            });
            
            modelBuilder.Entity<ExamSeasonStatus>(entity =>
            {
                entity.HasKey(e => e.StatusId);
            });

        }
    }
}