using System;
using System.Collections.Generic;
using System.Linq;
using Application.Enums;
using Application.Events;
using Application.Responses;
using AutoMapper;
using Domain;

namespace Application.Core
{
    public class MappingProfiles : Profile
    {

        public MappingProfiles()
        {
            CreateMap<Course, CourseResponse>()
                .ForMember(response => response.Semester, opt => opt.MapFrom(course => course.Semester))
                .ForMember(dest => dest.Specialization, opt => opt.MapFrom(x => x.Specialization))
                .ForMember(dest => dest.CourseCategory, opt => opt.MapFrom(x => x.CourseCategory))
                .AfterMap((course, response, context) =>
                {
                    if(course.CoursesAcademicStaff == null) return;
                    response.Lecturers = context.Mapper.Map<ICollection<AcademicStaffResponse>>
                    (
                        course.CoursesAcademicStaff.Select(e => e.AcademicStaff)
                    );
                });

            CreateMap<Grade, GradeResponse>()
                .ForMember(dest => dest.Course, opt => opt.MapFrom(e => e.CoursesAcademicStaff.Course))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(e => e.Status))
                .ForMember(dest => dest.Professor, opt => opt.MapFrom(e => e.CoursesAcademicStaff.AcademicStaff))
                .ForMember(dest => dest.Student, opt => opt.MapFrom(e => e.Student));

            CreateMap<Grade, StudentGradedEvent>()
                .ForMember(dest => dest.StudentName, opt => opt.MapFrom(e => e.Student.FullName))
                .ForMember(dest => dest.To, opt => opt.MapFrom(e => new List<string> { e.Student.Email }))
                .ForMember(dest => dest.Course, opt => opt.MapFrom(e => e.CoursesAcademicStaff.Course.CourseName))
                .ForMember(dest => dest.Grade, opt => opt.MapFrom(e => e.Value))
                .ForMember(dest => dest.ProfessorName,
                    opt => opt.MapFrom(e => e.CoursesAcademicStaff.AcademicStaff.FullName))
                .AfterMap((a, b, c) =>
                {
                    b.EventType = BusEvents.StudentGraded.ToString();
                });


            CreateMap<ExamSeason, ExamHistoryResponse>();
            CreateMap<ExamSeason, ExamSeasonResponse>()
                .ForMember(dest =>dest.Status, opt => opt.MapFrom(e => e.CurrentStatus));
            CreateMap<AcademicStaff, AcademicStaffResponse>();
            CreateMap<GradeStatus, GradeStatusResponse>();
            CreateMap<Semester, SemesterResponse>();
            CreateMap<CourseCategory, CourseCategoryResponse>();
            CreateMap<Specialization, SpecializationResponse>();
            CreateMap<Student, StudentResponse>();
        }

    }
}