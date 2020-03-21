using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace WebAPI.Models
{
    public class CombustivelDallHelper
    {
        protected static string GetStringConexao()
        {
            return ConfigurationManager.ConnectionStrings["DATABASEICAR"].ConnectionString;
        }

        public static List<Combustivel> GetCombustiveis()
        {
            List<Combustivel> _combustiveis = new List<Combustivel>();
            using (SqlConnection con = new SqlConnection(GetStringConexao()))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Combustivel", con))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr != null)
                        {
                            while (dr.Read())
                            {
                                var combustivel = new Combustivel();
                                combustivel.id = Convert.ToInt32(dr["id"]);
                                combustivel.login = dr["login"].ToString();
                                combustivel.etanol = float.Parse(dr["etanol"].ToString());
                                combustivel.diesel = float.Parse(dr["diesel"].ToString());
                                combustivel.gasolina = float.Parse(dr["gasolina"].ToString());
                                combustivel.outro = float.Parse(dr["outro"].ToString());
                                _combustiveis.Add(combustivel);
                            }
                        }
                        return _combustiveis;

                    }
                }
            }
        }

        public static Combustivel GetCombustivel(int id)
        {
            Combustivel combustivel = null;
            using (SqlConnection con = new SqlConnection(GetStringConexao()))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Combustivel WHERE id=" + id, con))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr != null)
                        {
                            while (dr.Read())
                            {
                                combustivel = new Combustivel();
                                combustivel.id = Convert.ToInt32(dr["id"]);
                                combustivel.login = dr["login"].ToString();
                                combustivel.etanol = float.Parse(dr["etanol"].ToString());
                                combustivel.diesel = float.Parse(dr["diesel"].ToString());
                                combustivel.gasolina = float.Parse(dr["gasolina"].ToString());
                                combustivel.outro = float.Parse(dr["outro"].ToString());
                            }
                        }
                        return combustivel;
                    }
                }
            }
        }

        public static int InsertCombustivel(Combustivel combustivel)
        {
            int reg = 0;
            using (SqlConnection con = new SqlConnection(GetStringConexao()))
            {
                string sql = "INSERT INTO Combustivel (login, etanol, diesel, gasolina, outro) VALUES(@login, @etanol, @diesel, @gasolina, @outro)";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@login", combustivel.login);
                    cmd.Parameters.AddWithValue("@etanol", combustivel.etanol);
                    cmd.Parameters.AddWithValue("@diesel", combustivel.diesel);
                    cmd.Parameters.AddWithValue("@gasolina", combustivel.gasolina);
                    cmd.Parameters.AddWithValue("@outro", combustivel.outro);

                    con.Open();
                    reg = cmd.ExecuteNonQuery();
                    con.Close();
                }
                return reg;
            }
        }

        public static int UpdateCombustivel(Combustivel combustivel)
        {
            int reg = 0;
            using (SqlConnection con = new SqlConnection(GetStringConexao()))
            {
                string sql = "UPDATE Combustivel SET login=@login, diesel=@diesel, etanol=@etanol, gasolina=@gasolina, outro=@outro";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@id", combustivel.id);
                    cmd.Parameters.AddWithValue("@etanol", combustivel.etanol);
                    cmd.Parameters.AddWithValue("@diesel", combustivel.diesel);
                    cmd.Parameters.AddWithValue("@gasolina", combustivel.gasolina);
                    cmd.Parameters.AddWithValue("@outro", combustivel.outro);

                    con.Open();
                    reg = cmd.ExecuteNonQuery();
                    con.Close();
                }
                return reg;
            }
        }

        public static int DeleteCombustivel(int id)
        {
            int reg = 0;
            using (SqlConnection con = new SqlConnection(GetStringConexao()))
            {
                string sql = "DELETE FROM Combustivel WHERE id = " + id;
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
    