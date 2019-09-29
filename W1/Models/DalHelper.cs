using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace W1.Models {
    public class DalHelper {

        protected static string GetStringConexao() {
            return ConfigurationManager.ConnectionStrings["DATABASEICAR"].ConnectionString;
        }

        public static List<AdicionarVeiculo> GetAdicionarVeiculos() {
            List<AdicionarVeiculo> veiculos = new List<AdicionarVeiculo>();
            using (SqlConnection con = new SqlConnection(GetStringConexao())) {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM ADICIONARVEICULO", con)) {
                    using (SqlDataReader dr = cmd.ExecuteReader()) {
                        if (dr != null) {
                            while (dr.Read()) {
                                var veiculo = new AdicionarVeiculo();
                                veiculo.Placa = dr["Placa"].ToString();
                                veiculo.Modelo = dr["Modelo"].ToString();
                                veiculo.Kml = Convert.ToDouble(dr["KML"]);
                                veiculos.Add(veiculo);
                            }
                        }
                        return veiculos;

                    }
                }
            }
        }

        public static AdicionarVeiculo GetVeiculo(string Placa) {
            AdicionarVeiculo adicionarVeiculo = null;
            using (SqlConnection con = new SqlConnection(GetStringConexao())) {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM ADICIONARVEICULO WHERE PLACA=" + Placa, con)) {
                    using (SqlDataReader dr = cmd.ExecuteReader()) {
                        if (dr != null) {
                            while (dr.Read()) {
                                var veiculo = new AdicionarVeiculo();
                                veiculo.Placa = dr["Placa"].ToString();
                                veiculo.Modelo = dr["Modelo"].ToString();
                                veiculo.Kml = Convert.ToDouble(dr["KML"]);
                            }
                        }
                        return adicionarVeiculo;
                    }
                }
            }
        }

        public static int InsertVeiculo(AdicionarVeiculo adicionarVeiculo) {
            int reg = 0;
            using (SqlConnection con = new SqlConnection(GetStringConexao())) {
                string sql = "INSERT INTO ADICIONARVEICULO (PLACA,MODELO,KML) VALUES(@PLACA,@MODELO,@KML)";
                using (SqlCommand cmd = new SqlCommand(sql, con)) {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@PLACA", adicionarVeiculo.Placa);
                    cmd.Parameters.AddWithValue("@MODELO", adicionarVeiculo.Modelo);
                    cmd.Parameters.AddWithValue("@KML", adicionarVeiculo.Kml);

                    con.Open();
                    reg = cmd.ExecuteNonQuery();
                    con.Close();
                }

                return reg;
            }
        }

        public static int UpdateVeiculo(AdicionarVeiculo adicionarVeiculo) {
            int reg = 0;
            using (SqlConnection con = new SqlConnection(GetStringConexao())) {
                string sql = "UPDATE ADICIONARVEICULO SET PLACA=@PLACA, MODELO=@MODELO, KML=@KML";
                using (SqlCommand cmd = new SqlCommand(sql, con)) {

                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@PLACA", adicionarVeiculo.Placa);
                    cmd.Parameters.AddWithValue("@MODELO", adicionarVeiculo.Modelo);
                    cmd.Parameters.AddWithValue("@KML", adicionarVeiculo.Kml);

                    con.Open();
                    reg = cmd.ExecuteNonQuery();
                    con.Close();
                }

                return reg;
            }
        }

        public static int DeleteVeiculo(string Placa) {

            int reg = 0;
            using (SqlConnection con = new SqlConnection(GetStringConexao())) {

                string sql = "DELETE FROM ADICIONARVEICULO WHERE PLACA = " + Placa;
                using (SqlCommand cmd = new SqlCommand(sql, con)) {

                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@PLACA", Placa);

                    con.Open();
                    reg = cmd.ExecuteNonQuery();
                    con.Close();
                }
                return reg;
            }
            
        }

    }
}
    