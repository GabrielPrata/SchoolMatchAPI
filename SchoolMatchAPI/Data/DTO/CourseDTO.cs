﻿namespace AccountService.Data.DTO
{
    public class CourseDTO
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }

        public CourseDTO(int courseId, string courseName)
        {
            CourseId = courseId;
            CourseName = courseName;
        }

        
    }
}
