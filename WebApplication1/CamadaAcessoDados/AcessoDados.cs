using CamadaDTO;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System;

namespace CamadaAcessoDados
{
    // TODO: Separar em classes de Curso e Aluno, cada uma com sua reponsabilidade; reutilizar código, remover repetição
    public class AcessoDados
    {
        public IEnumerable<CursoDTO> ListarCursos()
        {
            using (var conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["cursosConnection"].ConnectionString))
            {
                var query = " SELECT * FROM Curso ";
                var command = new SqlCommand(query, conexao);

                conexao.Open();

                using (var dataReader = command.ExecuteReader())
                {
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            yield return new CursoDTO
                            {
                                Id = dataReader.GetInt32(dataReader.GetOrdinal("Id")),
                                NomeCurso = dataReader.GetString(dataReader.GetOrdinal("NomeCurso")),
                                ValorCurso = dataReader.GetDecimal(dataReader.GetOrdinal("ValorCurso"))
                            };
                        }
                    }
                }
            }
        }

        public CursoDTO RetornarDetalheCurso(int idCurso)
        {
            using (var conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["cursosConnection"].ConnectionString))
            {
                var query = " SELECT * FROM Curso WHERE Id = @IdCurso ";
                var command = new SqlCommand(query, conexao);
                command.Parameters.Add(new SqlParameter("@IdCurso", idCurso));

                conexao.Open();

                using (var dataReader = command.ExecuteReader())
                {
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            return new CursoDTO
                            {
                                Id = dataReader.GetInt32(dataReader.GetOrdinal("Id")),
                                NomeCurso = dataReader.GetString(dataReader.GetOrdinal("NomeCurso")),
                                ValorCurso = dataReader.GetDecimal(dataReader.GetOrdinal("ValorCurso"))
                            };
                        }
                    }

                    return null;
                }
            }
        }

        public bool InserirCurso(CursoDTO curso)
        {
            using (var conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["cursosConnection"].ConnectionString))
            {
                var insert = " INSERT INTO Curso (NomeCurso,ValorCurso) VALUES (@Nome, @Valor) ";

                var command = new SqlCommand(insert, conexao);
                command.Parameters.Add(new SqlParameter("@Nome", curso.NomeCurso));
                command.Parameters.Add(new SqlParameter("@Valor", curso.ValorCurso));

                conexao.Open();

                var linhasAtingidas = command.ExecuteNonQuery();

                return linhasAtingidas > 0;
            }
        }

        public bool AlterarCurso(CursoDTO curso)
        {
            using (var conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["cursosConnection"].ConnectionString))
            {
                var update = " UPDATE Curso SET NomeCurso = @Nome, ValorCurso = @Valor WHERE Id = @IdCurso ";

                var command = new SqlCommand(update, conexao);
                command.Parameters.Add(new SqlParameter("@Nome", curso.NomeCurso));
                command.Parameters.Add(new SqlParameter("@Valor", curso.ValorCurso));
                command.Parameters.Add(new SqlParameter("@IdCurso", curso.Id));

                conexao.Open();

                var linhasAtingidas = command.ExecuteNonQuery();

                return linhasAtingidas > 0;
            }
        }

        public void DeletarCurso(int idCurso)
        {
            using (var conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["cursosConnection"].ConnectionString))
            {
                var delete = " DELETE FROM Curso WHERE Id = @IdCurso ";

                var command = new SqlCommand(delete, conexao);
                command.Parameters.Add(new SqlParameter("@IdCurso", idCurso));

                conexao.Open();

                command.ExecuteNonQuery();
            }
        }



        public IEnumerable<AlunoDTO> ListarAlunosCurso(int idCurso)
        {
            using (var conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["cursosConnection"].ConnectionString))
            {
                var query = " SELECT * FROM Aluno WHERE IdCurso = @IdCurso ";
                var command = new SqlCommand(query, conexao);
                command.Parameters.Add(new SqlParameter("@IdCurso", idCurso));

                conexao.Open();

                using (var dataReader = command.ExecuteReader())
                {
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            yield return new AlunoDTO
                            {
                                Id = dataReader.GetInt32(dataReader.GetOrdinal("Id")),
                                NomeAluno = dataReader.GetString(dataReader.GetOrdinal("NomeAluno")),
                                EmailAluno = dataReader.GetString(dataReader.GetOrdinal("EmailAluno")),
                                IdCurso = dataReader.GetInt32(dataReader.GetOrdinal("Id"))
                            };
                        }
                    }
                }
            }
        }

        public AlunoDTO RetornarDetalheAluno(int idAluno)
        {
            using (var conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["cursosConnection"].ConnectionString))
            {
                var query = " SELECT * FROM Aluno WHERE Id = @IdAluno ";
                var command = new SqlCommand(query, conexao);
                command.Parameters.Add(new SqlParameter("@IdAluno", idAluno));

                conexao.Open();

                using (var dataReader = command.ExecuteReader())
                {
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            return new AlunoDTO
                            {
                                Id = dataReader.GetInt32(dataReader.GetOrdinal("Id")),
                                NomeAluno = dataReader.GetString(dataReader.GetOrdinal("NomeAluno")),
                                EmailAluno = dataReader.GetString(dataReader.GetOrdinal("EmailAluno")),
                                IdCurso = dataReader.GetInt32(dataReader.GetOrdinal("IdCurso"))
                            };
                        }
                    }

                    return null;
                }
            }
        }

        public bool InserirAluno(AlunoDTO aluno)
        {
            using (var conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["cursosConnection"].ConnectionString))
            {
                var insert = " INSERT INTO Aluno (NomeAluno,EmailAluno,IdCurso) VALUES (@Nome, @Email, @IdCurso) ";

                var command = new SqlCommand(insert, conexao);
                command.Parameters.Add(new SqlParameter("@Nome", aluno.NomeAluno));
                command.Parameters.Add(new SqlParameter("@Email", aluno.EmailAluno));
                command.Parameters.Add(new SqlParameter("@IdCurso", aluno.IdCurso));

                conexao.Open();

                var linhasAtingidas = command.ExecuteNonQuery();

                return linhasAtingidas > 0;
            }
        }

        public bool AlterarAluno(AlunoDTO aluno)
        {
            using (var conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["cursosConnection"].ConnectionString))
            {
                var update = " UPDATE Aluno SET NomeAluno = @Nome, EmailAluno = @Email WHERE Id = @IdAluno ";

                var command = new SqlCommand(update, conexao);
                command.Parameters.Add(new SqlParameter("@Nome", aluno.NomeAluno));
                command.Parameters.Add(new SqlParameter("@Email", aluno.EmailAluno));
                command.Parameters.Add(new SqlParameter("@IdAluno", aluno.Id));

                conexao.Open();

                var linhasAtingidas = command.ExecuteNonQuery();

                return linhasAtingidas > 0;
            }
        }

        public void DeletarAluno(int idAluno)
        {
            using (var conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["cursosConnection"].ConnectionString))
            {
                var delete = " DELETE FROM Aluno WHERE Id = @IdAluno ";

                var command = new SqlCommand(delete, conexao);
                command.Parameters.Add(new SqlParameter("@IdAluno", idAluno));

                conexao.Open();

                command.ExecuteNonQuery();
            }
        }
    }
}
