using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace WebAPI.Models
{
    public class CadastroDallHelper
    {
        protected static string GetStringConexao()
        {
            return ConfigurationManager.ConnectionStrings["DATABASEICAR"].ConnectionString;
        }

        public static List<Cadastro> GetCadastros()
        {
            List<Cadastro> _cadastros = new List<Cadastro>();
            using (SqlConnection con = new SqlConnection(GetStringConexao()))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Cadastro", con))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr != null)
                        {
                            while (dr.Read())
                            {
                                var cadastro = new Cadastro();
                                cadastro.id = Convert.ToInt32(dr["id"]);
                                cadastro.login = dr["login"].ToString();
                                cadastro.senha = dr["senha"].ToString();
                                cadastro.nome = dr["nome"].ToString();
                                cadastro.email = dr["email"].ToString();
                                cadastro.cpf = dr["cpf"].ToString();
                                _cadastros.Add(cadastro);
                            }
                        }
                        return _cadastros;

                    }
                }
            }
        }

        public static Cadastro GetCadastro(int id)
        {
            Cadastro cadastro = null;
            using (SqlConnection con = new SqlConnection(GetStringConexao()))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Cadastro WHERE id=" + id, con))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr != null)
                        {
                            while (dr.Read())
                            {
                                cadastro = new Cadastro();
                                cadastro.id = Convert.ToInt32(dr["id"]);
                                cadastro.login = dr["login"].ToString();
                                cadastro.senha = dr["senha"].ToString();
                                cadastro.nome = dr["nome"].ToString();
                                cadastro.email = dr["email"].ToString();
                                cadastro.cpf = dr["cpf"].ToString();
                            }
                        }
                        return cadastro;
                    }
                }
            }
        }

        public static int InsertCadastro(Cadastro cadastro)
        {
            int reg = 0;
            using (SqlConnection con = new SqlConnection(GetStringConexao()))
            {
                string sql = "INSERT INTO Cadastro (login, senha, nome, email, cpf) VALUES(@login, @senha, @nome, @email, @cpf)";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@login", cadastro.login);
                    cmd.Parameters.AddWithValue("@senha", cadastro.senha);
                    cmd.Parameters.AddWithValue("@nome", cadastro.nome);
                    cmd.Parameters.AddWithValue("@email", cadastro.email);
                    cmd.Parameters.AddWithValue("@cpf", cadastro.cpf);

                    con.Open();
                    reg = cmd.ExecuteNonQuery();
                    con.Close();
                }
                return reg;
            }
        }

        public static int UpdateCadastro(Cadastro cadastro)
        {
            int reg = 0;
            using (SqlConnection con = new SqlConnection(GetStringConexao()))
            {
                string sql = "UPDATE Cadastro SET login=@login, senha=@senha, nome=@nome, email=@email, cpf=@cpf WHERE id = " + cadastro.id;
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@id", cadastro.id);
                    cmd.Parameters.AddWithValue("@login", cadastro.login);
                    cmd.Parameters.AddWithValue("@senha", cadastro.senha);
                    cmd.Parameters.AddWithValue("@nome", cadastro.nome);
                    cmd.Parameters.AddWithValue("@email", cadastro.email);
                    cmd.Parameters.AddWithValue("@cpf", cadastro.cpf);

                    con.Open();
                    reg = cmd.ExecuteNonQuery();
                    con.Close();
                }
                return reg;
            }
        }

        public static int DeleteCadastro(int id)
        {
            int reg = 0;
            using (SqlConnection con = new SqlConnection(GetStringConexao()))
            {
                string sql = "DELETE FROM Cadastro WHERE id = " + id;
                using (SqlCommand cmd = new SqlCommand(sql, con)) {

                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@id", id);

                    con.Open();
                    reg = cmd.ExecuteNonQuery();
                    con.Close();
                }
                return reg;
            }
            
        }

    }
}
    