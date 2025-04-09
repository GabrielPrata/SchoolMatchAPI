using AppDataService.Data.DTO;
using AppDataService.Model.SqlModels;

namespace AppDataService.Mappers
{
    internal static class BlocksMapper
    {
        internal static BlocksDTO ToDto(SqlBlockData sqlData)
        {
            var model = new BlocksDTO
            {
                // SQL fields
                //BlocoFaculdade = sqlData.BlocoFaculdade,
                IdBloco = sqlData.IdBloco,
                NomeBloco = sqlData.NomeBloco,
            };

            return model;
        }

        internal static SqlBlockData ToSqlModel(this BlocksDTO dto)
        {
            var model = new SqlBlockData
            {
                // SQL DTO fields
                //BlocoFaculdade = dto.BlocoFaculdade,
                IdBloco = dto.IdBloco,
                NomeBloco = dto.NomeBloco

            };

            return model;
        }
    }
}
