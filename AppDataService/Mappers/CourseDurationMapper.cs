using AppDataService.Data.DTO;
using AppDataService.Model.SqlModels;

namespace AppDataService.Mappers
{
    internal static class CourseDurationMapper
    {
        internal static CourseDurationDTO ToDto(SqlCourseDuration sqlData)
        {
            var model = new CourseDurationDTO
            {
                // SQL fields

               CourseDuration = sqlData.TotalPeriodosCurso


            };

            return model;
        }

        internal static SqlCourseDuration ToSqlModel(this CourseDurationDTO dto)
        {
            var model = new SqlCourseDuration
            {
                // SQL DTO fields
                TotalPeriodosCurso = dto.CourseDuration,

            };

            return model;
        }
    }
}
