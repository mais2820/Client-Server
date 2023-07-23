﻿using API.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.DTOs.EducationDto
{
    public class EducationDto
    {
        public Guid Guid { get; set; }
        public string Major { get; set; }
        public string Degree { get; set; }
        public float Gpa { get; set; }
        public Guid UniversityGuid { get; set; }

        public static implicit operator Education(EducationDto educationDto)
        {
            return new Education
            {
                Guid = educationDto.Guid,
                Major = educationDto.Major,
                Degree = educationDto.Degree,
                Gpa = educationDto.Gpa,
                UniversityGuid = educationDto.UniversityGuid,
                ModifiedDate = DateTime.Now
            };
        }

        public static explicit operator EducationDto(Education education)
        {
            return new EducationDto
            {
                Guid = education.Guid,
                Major = education.Major,
                Degree = education.Degree,
                Gpa = education.Gpa,
                UniversityGuid = education.UniversityGuid
            };
        }
    }
}
