using AccountService.Data.DTO;
using AccountService.Model.SqlModels;

namespace AccountService.Mappers
{
    internal static class CourseMapper
    {
        internal static CourseDataDTO ToDto(SqlCourseData sqlData)
        {
            var model = new CourseDataDTO
            {
                // SQL fields

                CourseId = sqlData.IdCurso,
                CourseName = sqlData.NomeCurso,
              

            };

            return model;
        }

        internal static SqlCourseData ToSqlModel(this CourseDataDTO dto)
        {
            var model = new SqlCourseData
            {
                // SQL DTO fields
                IdCurso = dto.CourseId,
                NomeCurso = dto.CourseName,

            };

            return model;
        }
    }
}
