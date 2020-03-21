using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace WebAPI.Models
{
    public class RelatorioDallHelper
    {
        protected static string GetStringConexao()
        {
            return ConfigurationManager.ConnectionStrings["DATABASEICAR"].ConnectionString;
        }

        public static List<Relatorio> GetRelatorios()
        {
            List<Relatorio> _relatorios = new List<Relatorio>();
            using (SqlConnection con = new SqlConnection(GetStringConexao()))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Relatorio", con))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr != null)
                        {
                            while (dr.Read())
                            {
                                var relatorio = new Relatorio();
                                relatorio.id = Convert.ToInt32(dr["id"]);
                                relatorio.carro = dr["carro"].ToString();
                                relatorio.endinicial = dr["endinicial"].ToString();
                                relatorio.endfinal = dr["endfinal"].ToString();
                                relatorio.datainicial = Convert.ToDateTime(dr["datainicial"]);
                                relatorio.datafinal = Convert.ToDateTime(dr["datafinal"]);
                                relatorio.kmpercorridos = float.Parse(dr["kmpercorridos"].ToString());
                                _relatorios.Add(relatorio);
                            }
                        }
                        return _relatorios;

                    }
                }
            }
        }

        public static Relatorio GetRelatorio(int id)
        {
            Relatorio relatorio = null;
            using (SqlConnection con = new SqlConnection(GetStringConexao()))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Relatorio WHERE id=" + id, con))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr != null)
                        {
                            while (dr.Read())
                            {
                                relatorio = new Relatorio();
                                relatorio.id = Convert.ToInt32(dr["id"]);
                                relatorio.carro = dr["carro"].ToString();
                                relatorio.endinicial = dr["endinicial"].ToString();
                                relatorio.endfinal = dr["endfinal"].ToString();
                                relatorio.datainicial = Convert.ToDateTime(dr["datainicial"]);
                                relatorio.datafinal = Convert.ToDateTime(dr["datafinal"]);
                                relatorio.kmpercorridos = float.Parse(dr["kmpercorridos"].ToString());
                            }
                        }
                        return relatorio;
                    }
                }
            }
        }

        public static int InsertRelatorio(Relatorio relatorio)
        {
            int reg = 0;
            using (SqlConnection con = new SqlConnection(GetStringConexao()))
            {
                string sql = "INSERT INTO Relatorio (carro, endinicial, endfinal, datainicial, datafinal, kmpercorridos) VALUES(@carro, @endinicial, @endfinal, @datainicial, @datafinal, @kmpercorridos)";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@carro", relatorio.carro);
                    cmd.Parameters.AddWithValue("@endinicial", relatorio.endinicial);
                    cmd.Parameters.AddWithValue("@endfinal", relatorio.endfinal);
                    cmd.Parameters.AddWithValue("@datainicial", relatorio.datainicial);
                    cmd.Parameters.AddWithValue("@datafinal", relatorio.datafinal);
                    cmd.Parameters.AddWithValue("@kmpercorridos", relatorio.kmpercorridos);

                    con.Open();
                    reg = cmd.ExecuteNonQuery();
                    con.Close();
                }
                return reg;
            }
        }

        public static int UpdateRelatorio(Relatorio relatorio)
        {
            int reg = 0;
            using (SqlConnection con = new SqlConnection(GetStringConexao()))
            {
                string sql = "UPDATE Relatorio SET carro=@carro, endinicial=@endinicial, endfinal=@endfinal, datainicial=@datainicial, datafinal=@datafinal, kmpercorridos=@kmpercorridos";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@id", relatorio.id);
                    cmd.Parameters.AddWithValue("@carro", relatorio.carro);
                    cmd.Parameters.AddWithValue("@endinicial", relatorio.endinicial);
                    cmd.Parameters.AddWithValue("@endfinal", relatorio.endfinal);
                    cmd.Parameters.AddWithValue("@datainicial", relatorio.datainicial);
                    cmd.Parameters.AddWithValue("@datafinal", relatorio.datafinal);
                    cmd.Parameters.AddWithValue("@kmpercorridos", relatorio.kmpercorridos);

                    con.Open();
                    reg = cmd.ExecuteNonQuery();
                    con.Close();
                }
                return reg;
            }
        }

        public static int DeleteRelatorio(int id)
        {
            int reg = 0;
            using (SqlConnection con = new SqlConnection(GetStringConexao()))
            {
                string sql = "DELETE FROM Relatorio WHERE id = " + id;
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
    