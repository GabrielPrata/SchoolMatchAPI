using AppDataService.Data.DTO;
using AppDataService.Model.SqlModels;

namespace AppDataService.Mappers
{
    internal static class InterestsMapper
    {
        internal static InterestsDTO ToDto(SqlInterestsData sqlData)
        {
            var model = new InterestsDTO
            {
                // SQL fields
                Id = sqlData.Id,
                Nome = sqlData.Nome,
            };

            return model;
        }

        internal static SqlInterestsData ToSqlModel(this InterestsDTO dto)
        {
            var model = new SqlInterestsData
            {
                // SQL DTO fields
                Id = dto.Id,
                Nome = dto.Nome,

            };

            return model;
        }
    }
}
