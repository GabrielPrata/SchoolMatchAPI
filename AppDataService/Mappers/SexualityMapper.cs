using AppDataService.Data.DTO;
using AppDataService.Model.SqlModels;

namespace AppDataService.Mappers
{
    internal static class SexualityMapper
    {
        internal static SexualityDTO ToDto(SqlSexualityData sqlData)
        {
            var model = new SexualityDTO
            {
                // SQL fields
                Id = sqlData.Id,
                Nome = sqlData.Nome,
                Descricao = sqlData.Descricao,
            };

            return model;
        }

        internal static SqlSexualityData ToSqlModel(this SexualityDTO dto)
        {
            var model = new SqlSexualityData
            {
                // SQL DTO fields
                Id = dto.Id,
                Nome = dto.Nome,
                Descricao = dto.Descricao,

            };

            return model;
        }
    }
}
