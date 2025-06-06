﻿using AccountService.Data.DTO;
using AccountService.Model.MongoModels;
using AccountService.Model.SqlModels;

namespace AccountService.Mappers
{
    internal static class UserMapper
    {
        internal static UserDataDTO ToDto(SqlUserData sqlData, MongoUserData mongoData)
        {
            var model = new UserDataDTO
            {
                // SQL fields

                IdUsuario = sqlData.IdUsuario,
                Nome = sqlData.NomeUsuario,
                Sobrenome = sqlData.SobrenomeUsuario,
                EmailUsuario = sqlData.EmailUsuario,
                SenhaUsuario = sqlData.SenhaUsuario,
                UsuarioVerificado = sqlData.UsuarioVerificado,
                Curso = new CourseDTO(sqlData.CursoUsuario, mongoData.Curso.CourseName),
                Genero = new GenderDTO(sqlData.UsuarioGenero, mongoData.Genero.GenderName),
                UsuarioPreferencia = sqlData.UsuarioPreferenciaGenero,
                UsuarioCreatedAt = sqlData.UsuarioCreatedAt,
                UsuarioEditedAt = sqlData.UsuarioEditedAt,
                BlocosUsuario = sqlData.BlocosUsario,
                BlocoPrincipal = sqlData.BlocoPrincipal,

                // Mongo fields
                MongoId = mongoData._id,
                Sexualidade = mongoData.Sexualidade,
                Bio = mongoData.Bio,
                DataNascimento = mongoData.DataNascimento,
                Cidade = mongoData.Cidade,
                UserAbout = mongoData.SobreUsuario,
                Semestre = mongoData.Semestre,
               
                Interesses = mongoData.Interesses,
                SpotifyMusicData = mongoData.SpotifyMusicData,
            };

            return model;
        }

        internal static SqlUserData ToSqlModel(this UserDataDTO dto)
        {
            var model = new SqlUserData
            {
                // SQL DTO fields
                NomeUsuario = dto.Nome,
                SobrenomeUsuario = dto.Sobrenome,
                EmailUsuario = dto.EmailUsuario,
                SenhaUsuario = dto.SenhaUsuario,
                UsuarioVerificado = dto.UsuarioVerificado,
                CursoUsuario = dto.Curso.CourseId,
                UsuarioGenero = dto.Genero.GenderId,
                UsuarioPreferenciaGenero = dto.UsuarioPreferencia,
                UsuarioCreatedAt = dto.UsuarioCreatedAt,
                UsuarioEditedAt = dto.UsuarioEditedAt,
                BlocosUsario = dto.BlocosUsuario,
                BlocoPrincipal = dto.BlocoPrincipal
            };

            return model;
        }

        internal static MongoUserData ToMongoModel(this UserDataDTO dto)
        {
            var model = new MongoUserData
            {
                // Mongo DTO fields
                Nome = dto.Nome,
                Sobrenome = dto.Sobrenome,
                Curso = dto.Curso,
                BlocoPrincipal = dto.BlocoPrincipal,
                BlocosSecundarios = dto.BlocosUsuario,
                Sexualidade = dto.Sexualidade,
                Genero = dto.Genero,
                Bio = dto.Bio,
                DataNascimento = dto.DataNascimento,
                Cidade = dto.Cidade,
                SobreUsuario = dto.UserAbout,
                Semestre = dto.Semestre,
                Interesses = dto.Interesses,
                CriadoEm = dto.UsuarioCreatedAt,
                ModificadoEm = dto.UsuarioEditedAt,
                SpotifyMusicData = dto.SpotifyMusicData,
            };

            return model;
        }
    }
}
