using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace WebAPI.Models
{
    public class LembreteDallHelper
    {
        protected static string GetStringConexao()
        {
            return ConfigurationManager.ConnectionStrings["DATABASEICAR"].ConnectionString;
        }

        public static List<Lembrete> GetLembretes()
        {
            List<Lembrete> _lembretes = new List<Lembrete>();
            using (SqlConnection con = new SqlConnection(GetStringConexao()))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Lembrete", con))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr != null)
                        {
                            while (dr.Read())
                            {
                                var lembrete = new Lembrete();
                                lembrete.id = Convert.ToInt32(dr["id"]);
                                lembrete.login = dr["login"].ToString();
                                lembrete.lembrete = dr["lembrete"].ToString();
                                _lembretes.Add(lembrete);
                            }
                        }
                        return _lembretes;

                    }
                }
            }
        }

        public static Lembrete GetLembrete(int id)
        {
            Lembrete lembrete = null;
            using (SqlConnection con = new SqlConnection(GetStringConexao()))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Lembrete WHERE id=" + id, con))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr != null)
                        {
                            while (dr.Read())
                            {
                                lembrete = new Lembrete();
                                lembrete.id = Convert.ToInt32(dr["id"]);
                                lembrete.login = dr["login"].ToString();
                                lembrete.lembrete = dr["lembrete"].ToString();
                            }
                        }
                        return lembrete;
                    }
                }
            }
        }

        public static int InsertLembrete(Lembrete lembrete)
        {
            int reg = 0;
            using (SqlConnection con = new SqlConnection(GetStringConexao()))
            {
                string sql = "INSERT INTO Lembrete (login, lembrete) VALUES(@login, @lembrete)";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@login", lembrete.login);
                    cmd.Parameters.AddWithValue("@lembrete", lembrete.lembrete);

                    con.Open();
                    reg = cmd.ExecuteNonQuery();
                    con.Close();
                }
                return reg;
            }
        }

        public static int UpdateLembrete(Lembrete lembrete)
        {
            int reg = 0;
            using (SqlConnection con = new SqlConnection(GetStringConexao()))
            {
                string sql = "UPDATE Lembrete SET login=@login, lembrete=@lembrete WHERE id = " + lembrete.id;
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@id", lembrete.id);
                    cmd.Parameters.AddWithValue("@login", lembrete.login);
                    cmd.Parameters.AddWithValue("@lembrete", lembrete.lembrete);

                    con.Open();
                    reg = cmd.ExecuteNonQuery();
                    con.Close();
                }
                return reg;
            }
        }

        public static int DeleteLembrete(int id)
        {
            int reg = 0;
            using (SqlConnection con = new SqlConnection(GetStringConexao()))
            {
                string sql = "DELETE FROM Lembrete WHERE id = " + id;
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {

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
