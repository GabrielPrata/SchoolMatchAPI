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
                SexualityId = sqlData.Id,
                SexualityName = sqlData.Nome,
                SexualityDescription = sqlData.Descricao,
            };

            return model;
        }

        internal static SqlSexualityData ToSqlModel(this SexualityDTO dto)
        {
            var model = new SqlSexualityData
            {
                // SQL DTO fields
                Id = dto.SexualityId,
                Nome = dto.SexualityName,
                Descricao = dto.SexualityDescription,

            };

            return model;
        }
    }
}
