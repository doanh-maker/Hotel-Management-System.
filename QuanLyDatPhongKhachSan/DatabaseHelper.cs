using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyDatPhongKhachSan
{
    public static class DatabaseHelper
    {
        private static string connectionString =
            @"Data Source=localhost\SQLEXPRESS04;
              Initial Catalog=QuanLyKhachSan;
              Integrated Security=True;
              Encrypt=False";

        public static string ConnectionString { get; internal set; }

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        public static DataTable ExecuteQuery(string query, SqlParameter[] parameters = null)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                if (parameters != null)
                    cmd.Parameters.AddRange(parameters);

                try
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi truy vấn dữ liệu:\n" + ex.Message);
                }
            }

            return dt;
        }

        public static DataTable ExecuteStoredProcedure(string query, SqlParameter[] parameters = null)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                if (parameters != null)
                    cmd.Parameters.AddRange(parameters);

                try
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi truy vấn dữ liệu:\n" + ex.Message);
                }
            }

            return dt;
        }
        public static object ExecuteScalar(string query,SqlParameter[] parameters=null,CommandType cmdType = CommandType.Text)
        {
            using(SqlConnection conn = GetConnection())
                using(SqlCommand cmd = new SqlCommand(query,conn))
                {
                    cmd.CommandType = cmdType;
                    if(parameters != null)
                        cmd.Parameters.AddRange(parameters);
                    try
                    {
                        conn.Open();
                        return cmd.ExecuteScalar();
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show("Lỗi truy vấn dữ liệu:\n" + ex.Message);
                        return null;
                    }
            }
        }
        public static int ExecuteNonQuery(string query, SqlParameter[] parameters = null, CommandType cmdType = CommandType.Text)
        {
            using (SqlConnection conn = GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                
                cmd.CommandType = cmdType;
                if (parameters != null)
                    cmd.Parameters.AddRange(parameters);
                try
                {
                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi truy vấn dữ liệu:\n" + ex.Message);
                    return -1;
                }
            }
        }
        // 🔐 Hash mật khẩu
        public static string HashPassword(string password)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(
                    System.Text.Encoding.UTF8.GetBytes(password)
                );
                return BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }
        }
        public static int ExecuteNonQueryStoredProcedure(string procName, SqlParameter[] parameters)
        {
            using (SqlConnection conn = GetConnection())
            using (SqlCommand cmd = new SqlCommand(procName, conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                if (parameters != null)
                    cmd.Parameters.AddRange(parameters);
                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }

    }
}