using System;
using System.Collections.Generic;
using System.Linq;
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
                .ForMember(dest => dest.Professor, opt => opt.MapFrom(e => e.CoursesAcademicStaff.AcademicStaff));


            CreateMap<ExamSeason, ExamHistoryResponse>();
            CreateMap<AcademicStaff, AcademicStaffResponse>();
            CreateMap<GradeStatus, GradeStatusResponse>();
            CreateMap<Semester, SemesterResponse>();
            CreateMap<CourseCategory, CourseCategoryResponse>();
            CreateMap<Specialization, SpecializationResponse>();
        }

    }
}