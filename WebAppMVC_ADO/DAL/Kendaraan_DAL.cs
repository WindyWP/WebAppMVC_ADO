using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using WebAppMVC_ADO.Models;

namespace WebAppMVC_ADO.DAL
{
    public class Kendaraan_DAL
    {
        string conString = ConfigurationManager.ConnectionStrings["ConnectionStringADOMVC"].ToString();

        public List<Kendaraan_Model> GetKendaraans()
        {
            List<Kendaraan_Model> kendaraanList = new List<Kendaraan_Model>();

            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand comm = connection.CreateCommand();
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "sp_GetAllKendaraan";
                SqlDataAdapter sqlDA = new SqlDataAdapter(comm);
                DataTable dtKendaraan = new DataTable();

                connection.Open();
                sqlDA.Fill(dtKendaraan);
                connection.Close();

                foreach (DataRow dr in dtKendaraan.Rows)
                {
                    kendaraanList.Add(new Kendaraan_Model
                    {
                        KendaraanID = Convert.ToInt32(dr["KendaraanID"]),
                        KendaraanName = dr["KendaraanName"].ToString(),
                        Jumlah = Convert.ToInt32(dr["Jumlah"]),
                        Harga = Convert.ToInt32(dr["Harga"]),
                        Keterangan = dr["Keterangan"].ToString()
                    });
                }
            }

            return kendaraanList;
        }

        public bool InsertKendaraan(Kendaraan_Model kendaraan)
        {
            int id = 0;

            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand comm = new SqlCommand("sp_InsertKendaraan", connection);
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@KendaraanName", kendaraan.KendaraanName);
                comm.Parameters.AddWithValue("@Harga", kendaraan.Harga);
                comm.Parameters.AddWithValue("@Keterangan", kendaraan.Keterangan); 
                comm.Parameters.AddWithValue("@Jumlah", kendaraan.Jumlah);

                connection.Open();
                id = comm.ExecuteNonQuery();
                connection.Close();
            }

            if(id>0)
            {
                return true;
            }
            else
            {
                return false;
            }


        }

        public List<Kendaraan_Model> GetKendaraanID(int KendaraanID)
        {
            List<Kendaraan_Model> kendaraanList = new List<Kendaraan_Model>();

            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand comm = connection.CreateCommand();
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "sp_GetKendaraanByID";
                comm.Parameters.AddWithValue("@KendaraanID", KendaraanID);
                SqlDataAdapter sqlDA = new SqlDataAdapter(comm);
                DataTable dtKendaraan = new DataTable();

                connection.Open();
                sqlDA.Fill(dtKendaraan);
                connection.Close();

                foreach (DataRow dr in dtKendaraan.Rows)
                {
                    kendaraanList.Add(new Kendaraan_Model
                    {
                        KendaraanID = Convert.ToInt32(dr["KendaraanID"]),
                        KendaraanName = dr["KendaraanName"].ToString(),
                        Jumlah = Convert.ToInt32(dr["Jumlah"]),
                        Harga = Convert.ToInt32(dr["Harga"]),
                        Keterangan = dr["Keterangan"].ToString()
                    });
                }
            }

            return kendaraanList;
        }

        public bool UpdateKendaraan(Kendaraan_Model kendaraan)
        {
            int i = 0;

            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand comm = new SqlCommand("sp_UpdateKendaraan", connection);
                comm.CommandType = CommandType.StoredProcedure; 
                comm.Parameters.AddWithValue("@KendaraanID", kendaraan.KendaraanID);
                comm.Parameters.AddWithValue("@KendaraanName", kendaraan.KendaraanName);
                comm.Parameters.AddWithValue("@Harga", kendaraan.Harga);
                comm.Parameters.AddWithValue("@Keterangan", kendaraan.Keterangan);
                comm.Parameters.AddWithValue("@Jumlah", kendaraan.Jumlah);

                connection.Open();
                i = comm.ExecuteNonQuery();
                connection.Close();
            }

            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }


        }

        public string DeleteKendaraan(int kendaraanid)
        {
            string result = "";

            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand comm = new SqlCommand("sp_DeleteKendaraan", connection);
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@KendaraanID", kendaraanid);
                comm.Parameters.Add("@OutputMessage", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;

                connection.Open();
                comm.ExecuteNonQuery();
                result = comm.Parameters["@OutputMessage"].Value.ToString();
                connection.Close();
            }

            return result;

        }
    }
}