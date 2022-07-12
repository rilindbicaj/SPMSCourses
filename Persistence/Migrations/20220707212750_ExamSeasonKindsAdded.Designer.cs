﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Persistence;

namespace Persistence.Migrations
{
    [DbContext(typeof(SPMSCoursesContext))]
    [Migration("20220707212750_ExamSeasonKindsAdded")]
    partial class ExamSeasonKindsAdded
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Domain.AcademicStaff", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("AcademicStaff");
                });

            modelBuilder.Entity("Domain.Course", b =>
                {
                    b.Property<int>("CourseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CourseCategoryId")
                        .HasColumnType("int");

                    b.Property<string>("CourseCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CourseName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Ects")
                        .HasColumnType("int");

                    b.Property<int>("SemesterId")
                        .HasColumnType("int");

                    b.Property<int>("SpecializationId")
                        .HasColumnType("int");

                    b.HasKey("CourseId");

                    b.HasIndex("CourseCategoryId");

                    b.HasIndex("SemesterId");

                    b.HasIndex("SpecializationId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("Domain.CourseCategory", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CategoryName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryId");

                    b.ToTable("CourseCategories");
                });

            modelBuilder.Entity("Domain.Courses_AcademicStaff", b =>
                {
                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<Guid>("AcademicStaffId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("LectureRoleId")
                        .HasColumnType("int");

                    b.HasKey("CourseId", "AcademicStaffId");

                    b.HasIndex("AcademicStaffId");

                    b.HasIndex("LectureRoleId");

                    b.ToTable("CoursesAcademicStaff");
                });

            modelBuilder.Entity("Domain.ExamSeason", b =>
                {
                    b.Property<int>("ExamSeasonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Faculty")
                        .HasColumnType("int");

                    b.Property<int?>("SeasonKindId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("StatusId")
                        .HasColumnType("int");

                    b.HasKey("ExamSeasonId");

                    b.HasIndex("SeasonKindId");

                    b.HasIndex("StatusId");

                    b.ToTable("ExamSeasons");
                });

            modelBuilder.Entity("Domain.ExamSeasonKind", b =>
                {
                    b.Property<int>("ExamSeasonKindId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ExamSeasonKindName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ExamSeasonKindId");

                    b.ToTable("ExamSeasonKinds");
                });

            modelBuilder.Entity("Domain.ExamSeasonStatus", b =>
                {
                    b.Property<int>("StatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("StatusName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StatusId");

                    b.ToTable("ExamSeasonStatuses");
                });

            modelBuilder.Entity("Domain.Grade", b =>
                {
                    b.Property<int>("GradeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<Guid>("AcademicStaffId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DateGraded")
                        .HasColumnType("datetime2");

                    b.Property<int>("ExamSeasonId")
                        .HasColumnType("int");

                    b.Property<int>("StatusId")
                        .HasColumnType("int");

                    b.Property<Guid?>("StudentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Value")
                        .HasColumnType("int");

                    b.HasKey("GradeId");

                    b.HasIndex("ExamSeasonId");

                    b.HasIndex("StatusId");

                    b.HasIndex("StudentId");

                    b.HasIndex("CourseId", "AcademicStaffId");

                    b.ToTable("Grades");
                });

            modelBuilder.Entity("Domain.GradeStatus", b =>
                {
                    b.Property<int>("StatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("StatusName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StatusId");

                    b.ToTable("GradeStatuses");
                });

            modelBuilder.Entity("Domain.LectureRole", b =>
                {
                    b.Property<int>("LectureRoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("RoleName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LectureRoleId");

                    b.ToTable("LectureRoles");
                });

            modelBuilder.Entity("Domain.Semester", b =>
                {
                    b.Property<int>("SemesterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("SemesterName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SemesterId");

                    b.ToTable("Semesters");
                });

            modelBuilder.Entity("Domain.Specialization", b =>
                {
                    b.Property<int>("SpecializationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("FacultyId")
                        .HasColumnType("int");

                    b.Property<int>("ParentSpecializationId")
                        .HasColumnType("int");

                    b.Property<string>("SpecializationName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SpecializationId");

                    b.HasIndex("ParentSpecializationId");

                    b.ToTable("Specializations");
                });

            modelBuilder.Entity("Domain.Student", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("FileNumber")
                        .HasColumnType("int");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("Domain.Course", b =>
                {
                    b.HasOne("Domain.CourseCategory", "CourseCategory")
                        .WithMany("Courses")
                        .HasForeignKey("CourseCategoryId")
                        .HasConstraintName("FK_Courses_CourseCategories")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Semester", "Semester")
                        .WithMany("Courses")
                        .HasForeignKey("SemesterId")
                        .HasConstraintName("FK_Courses_Semesters")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Specialization", "Specialization")
                        .WithMany("Courses")
                        .HasForeignKey("SpecializationId")
                        .HasConstraintName("FK_Courses_Specializations")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CourseCategory");

                    b.Navigation("Semester");

                    b.Navigation("Specialization");
                });

            modelBuilder.Entity("Domain.Courses_AcademicStaff", b =>
                {
                    b.HasOne("Domain.AcademicStaff", "AcademicStaff")
                        .WithMany("CoursesAcademicStaff")
                        .HasForeignKey("AcademicStaffId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Course", "Course")
                        .WithMany("CoursesAcademicStaff")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.LectureRole", "LectureRole")
                        .WithMany("CoursesAcademicStaves")
                        .HasForeignKey("LectureRoleId")
                        .HasConstraintName("FK_CoursesAcademicStaff_LectureRole")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AcademicStaff");

                    b.Navigation("Course");

                    b.Navigation("LectureRole");
                });

            modelBuilder.Entity("Domain.ExamSeason", b =>
                {
                    b.HasOne("Domain.ExamSeasonKind", "SeasonKind")
                        .WithMany("ExamSeasons")
                        .HasForeignKey("SeasonKindId")
                        .HasConstraintName("FK_ExamSeasons_ExamSeasonKinds");

                    b.HasOne("Domain.ExamSeasonStatus", "CurrentStatus")
                        .WithMany("ExamSeasons")
                        .HasForeignKey("StatusId")
                        .HasConstraintName("FK_ExamSeasons_ExamSeasonStatuses")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CurrentStatus");

                    b.Navigation("SeasonKind");
                });

            modelBuilder.Entity("Domain.Grade", b =>
                {
                    b.HasOne("Domain.ExamSeason", "ExamSeason")
                        .WithMany("Grades")
                        .HasForeignKey("ExamSeasonId")
                        .HasConstraintName("FK_Grades_ExamSeasons")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.GradeStatus", "Status")
                        .WithMany("Grades")
                        .HasForeignKey("StatusId")
                        .HasConstraintName("FK_Grades_GradeStatuses")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Student", "Student")
                        .WithMany("Grades")
                        .HasForeignKey("StudentId")
                        .HasConstraintName("FK_Grades_Students");

                    b.HasOne("Domain.Courses_AcademicStaff", "CoursesAcademicStaff")
                        .WithMany("Grades")
                        .HasForeignKey("CourseId", "AcademicStaffId")
                        .HasConstraintName("FK_Grades_CoursesAcademicStaff")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CoursesAcademicStaff");

                    b.Navigation("ExamSeason");

                    b.Navigation("Status");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("Domain.Specialization", b =>
                {
                    b.HasOne("Domain.Specialization", "ParentSpecialization")
                        .WithMany("ChildSpecializations")
                        .HasForeignKey("ParentSpecializationId")
                        .HasConstraintName("FK_Specializations_SpecializaitionsChild");

                    b.Navigation("ParentSpecialization");
                });

            modelBuilder.Entity("Domain.AcademicStaff", b =>
                {
                    b.Navigation("CoursesAcademicStaff");
                });

            modelBuilder.Entity("Domain.Course", b =>
                {
                    b.Navigation("CoursesAcademicStaff");
                });

            modelBuilder.Entity("Domain.CourseCategory", b =>
                {
                    b.Navigation("Courses");
                });

            modelBuilder.Entity("Domain.Courses_AcademicStaff", b =>
                {
                    b.Navigation("Grades");
                });

            modelBuilder.Entity("Domain.ExamSeason", b =>
                {
                    b.Navigation("Grades");
                });

            modelBuilder.Entity("Domain.ExamSeasonKind", b =>
                {
                    b.Navigation("ExamSeasons");
                });

            modelBuilder.Entity("Domain.ExamSeasonStatus", b =>
                {
                    b.Navigation("ExamSeasons");
                });

            modelBuilder.Entity("Domain.GradeStatus", b =>
                {
                    b.Navigation("Grades");
                });

            modelBuilder.Entity("Domain.LectureRole", b =>
                {
                    b.Navigation("CoursesAcademicStaves");
                });

            modelBuilder.Entity("Domain.Semester", b =>
                {
                    b.Navigation("Courses");
                });

            modelBuilder.Entity("Domain.Specialization", b =>
                {
                    b.Navigation("ChildSpecializations");

                    b.Navigation("Courses");
                });

            modelBuilder.Entity("Domain.Student", b =>
                {
                    b.Navigation("Grades");
                });
#pragma warning restore 612, 618
        }
    }
}
