using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace WebAPI.Models
{
    public class CarroDallHelper
    {
        protected static string GetStringConexao()
        {
            return ConfigurationManager.ConnectionStrings["DATABASEICAR"].ConnectionString;
        }

        public static List<Carro> GetCarros()
        {
            List<Carro> _carros = new List<Carro>();
            using (SqlConnection con = new SqlConnection(GetStringConexao()))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Carro", con))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr != null)
                        {
                            while (dr.Read())
                            {
                                var carro = new Carro();
                                carro.id = Convert.ToInt32(dr["id"]);
                                carro.placa = dr["placa"].ToString();
                                carro.modelo = dr["modelo"].ToString();
                                carro.dono = dr["dono"].ToString();
                                carro.tipocombustivel = dr["tipocombustivel"].ToString();
                                carro.kmatual = float.Parse(dr["kmatual"].ToString());
                                carro.kmlitro = float.Parse(dr["kmlitro"].ToString());
                                carro.status = dr["status"].ToString();
                                _carros.Add(carro);
                            }
                        }
                        return _carros;

                    }
                }
            }
        }

        public static Carro GetCarro(int id)
        {
            Carro carro = null;
            using (SqlConnection con = new SqlConnection(GetStringConexao()))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Carro WHERE id=" + id, con))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr != null)
                        {
                            while (dr.Read())
                            {
                                carro = new Carro();
                                carro.id = Convert.ToInt32(dr["id"]);
                                carro.placa = dr["placa"].ToString();
                                carro.modelo = dr["modelo"].ToString();
                                carro.dono = dr["dono"].ToString();
                                carro.tipocombustivel = dr["tipocombustivel"].ToString();
                                carro.kmatual = float.Parse(dr["kmatual"].ToString());
                                carro.kmlitro = float.Parse(dr["kmlitro"].ToString());
                                carro.status = dr["status"].ToString();
                            }
                        }
                        return carro;
                    }
                }
            }
        }

        public static int InsertCarro(Carro carro)
        {
            int reg = 0;
            using (SqlConnection con = new SqlConnection(GetStringConexao()))
            {
                string sql = "INSERT INTO Carro (placa, modelo, dono, tipocombustivel, kmatual, kmlitro, status) VALUES(@placa, @modelo, @dono, @tipocombustivel, @kmatual, @kmlitro, @status)";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@placa", carro.placa);
                    cmd.Parameters.AddWithValue("@modelo", carro.modelo);
                    cmd.Parameters.AddWithValue("@dono", carro.dono);
                    cmd.Parameters.AddWithValue("@tipocombustivel", carro.tipocombustivel);
                    cmd.Parameters.AddWithValue("@kmatual", carro.kmatual);
                    cmd.Parameters.AddWithValue("@kmlitro", carro.kmlitro);
                    cmd.Parameters.AddWithValue("@status", carro.status);

                    con.Open();
                    reg = cmd.ExecuteNonQuery();
                    con.Close();
                }
                return reg;
            }
        }

        public static int UpdateCarro(Carro carro)
        {
            int reg = 0;
            using (SqlConnection con = new SqlConnection(GetStringConexao()))
            {
                string sql = "UPDATE Carro SET placa=@placa, modelo=@modelo, dono=@dono, tipocombustivel=@tipocombustivel, kmatual=@kmatual, kmlitro=@kmlitro, status=@status WHERE id = " + carro.id;
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@id", carro.id);
                    cmd.Parameters.AddWithValue("@placa", carro.placa);
                    cmd.Parameters.AddWithValue("@modelo", carro.modelo);
                    cmd.Parameters.AddWithValue("@dono", carro.dono);
                    cmd.Parameters.AddWithValue("@tipocombustivel", carro.tipocombustivel);
                    cmd.Parameters.AddWithValue("@kmatual", carro.kmatual);
                    cmd.Parameters.AddWithValue("@kmlitro", carro.kmlitro);
                    cmd.Parameters.AddWithValue("@status", carro.status);

                    con.Open();
                    reg = cmd.ExecuteNonQuery();
                    con.Close();
                }
                return reg;
            }
        }

        public static int DeleteCarro(int id)
        {
            int reg = 0;
            using (SqlConnection con = new SqlConnection(GetStringConexao()))
            {
                string sql = "DELETE FROM Carro WHERE id = " + id;
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
    