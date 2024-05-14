using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System;
using System.Data.SqlClient;
using Pelikan_Project.Models;

namespace Pelikan_Project.DataManager
{
    public class OperatorManager
    {
        string _connectionString = "";

        public OperatorManager()
        {
            _connectionString = "Data Source=KHI-ITECK-TDD26;Initial Catalog=Pelikan_Project;Integrated Security=True;";
            //_connectionString = "Data Source=RAZA\\SQLEXPRESS;Initial Catalog=Pelikan_Project;Integrated Security=True;";
        }
        public Operator GetOperatorByID(int OperatorID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "Select * from Operator Where OperatorID = @OperatorID";
                cmd.Parameters.AddWithValue("@OperatorID", OperatorID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                Operator op = new Operator();
                if (dt != null && dt.Rows.Count > 0)
                {
                    op.ID = Convert.ToInt32(dt.Rows[0]["ID"]);
                    op.OperatorID = Convert.ToInt32(dt.Rows[0]["OperatorID"]);
                    op.Name = dt.Rows[0]["Name"].ToString();
                    op.Department = dt.Rows[0]["Department"].ToString();
                    op.Password = dt.Rows[0]["Password"].ToString();
                    op.Designation = dt.Rows[0]["Designation"].ToString();
                }
                return op;
            }
        }
        public string AddOperator(Operator Operator)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "Insert into Operator (OperatorID, Name, Department, Password, Designation) values (@OperatorID, @Name, @Department, @Password, @Designation)";
                cmd.Parameters.AddWithValue("@OperatorID", Operator.OperatorID);
                cmd.Parameters.AddWithValue("@Name", Operator.Name);
                cmd.Parameters.AddWithValue("@Department", Operator.Department);
                cmd.Parameters.AddWithValue("@Password", Operator.Password);
                cmd.Parameters.AddWithValue("@Designation", Operator.Designation);
                conn.Open();
                int rawAffected = cmd.ExecuteNonQuery();

                if (rawAffected > 0)
                {
                    return Operator.Name;
                    conn.Close();
                }
                return null;
            }
        }

        public Operator AuthenticateOperator(int OperatorID, string Password)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "Select * From Operator Where OperatorID = @OperatorID and Password = @Password";
                cmd.Parameters.AddWithValue("@OperatorID", OperatorID);
                cmd.Parameters.AddWithValue("@Password", Password);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                Operator op = new Operator();
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (Convert.ToInt32(dt.Rows[0]["OperatorID"]) == OperatorID && dt.Rows[0]["Password"].ToString() == Password)
                    {
                        op.ID = Convert.ToInt32(dt.Rows[0]["ID"]);
                        op.OperatorID = Convert.ToInt32(dt.Rows[0]["OperatorID"]);
                        op.Name = dt.Rows[0]["Name"].ToString();
                        op.Department = dt.Rows[0]["Department"].ToString();
                        op.Password = dt.Rows[0]["Password"].ToString();
                        op.Designation = dt.Rows[0]["Designation"].ToString();
                    }
                }
                return op;
            }
        }

    }
}
