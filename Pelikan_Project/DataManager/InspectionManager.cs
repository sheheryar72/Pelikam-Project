using Pelikan_Project.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Pelikan_Project.DataManager
{
    public class InspectionManager
    {
        string _connectionString = "";
        public InspectionManager()
        {
            _connectionString = "Data Source=KHI-ITECK-TDD26;Initial Catalog=Pelikan_Project;Integrated Security=True;";
            //_connectionString = "Data Source=RAZA\\SQLEXPRESS;Initial Catalog=Pelikan_Project;Integrated Security=True;";
        }
        public string AddInspection(Inspection inspection)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "Insert into Inspection (FromTime, ToTime, CHK, DEF, Inspection_Date, Operator_ID, Operation) values (@FromTime, @ToTime, @CHK, @DEF, @Inspection_Date, @Operator_ID, @Operation)";
                cmd.Parameters.AddWithValue("@FromTime", inspection.FromTime);
                cmd.Parameters.AddWithValue("@ToTime", inspection.ToTime);
                cmd.Parameters.AddWithValue("@CHK", inspection.CHK);
                cmd.Parameters.AddWithValue("@DEF", inspection.DEF);
                cmd.Parameters.AddWithValue("@Inspection_Date", DateTime.Now);
                cmd.Parameters.AddWithValue("@Operation", inspection.Operation);
                cmd.Parameters.AddWithValue("@Operator_ID", inspection.Operator_ID);
                conn.Open();
                int rawAffected = cmd.ExecuteNonQuery();

                if (rawAffected > 0)
                {
                    return inspection.Operator_ID.ToString();
                    conn.Close();
                }
                return null;
            }
        }
        public List<Inspection> GetAllInspectionByDate(DateTime inspection_Date)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT Operator.ID, Operator.OperatorID, Inspection.Operation, Inspection.FromTime, " +
                    "Inspection.ToTime, Operator.Name, Inspection.Inspection_Date, Inspection.CHK, Inspection.DEF " +
                    "FROM Inspection " +
                    "INNER JOIN Operator ON Operator.OperatorID = Inspection.Operator_ID " +
                    "WHERE CONVERT(DATE, Inspection.Inspection_Date) = @Inspection_Date";

                cmd.Parameters.AddWithValue("@Inspection_Date", inspection_Date.Date);
                conn.Open();
                var reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                if (dt != null && dt.Rows.Count > 0)
                {
                    List<Inspection> st = new List<Inspection>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        var inspection = new Inspection();
                        inspection.Operator_ID = Convert.ToInt32(dt.Rows[i]["OperatorID"]);
                        inspection.Name = dt.Rows[i]["Name"].ToString();
                        inspection.Operation = dt.Rows[i]["Operation"].ToString();
                        inspection.FromTime = dt.Rows[i]["FromTime"].ToString();
                        inspection.ToTime = dt.Rows[i]["ToTime"].ToString();
                        inspection.Inspection_Date = Convert.ToDateTime(dt.Rows[i]["Inspection_Date"]);
                        inspection.CHK = Convert.ToInt32(dt.Rows[i]["CHK"]);
                        inspection.DEF = Convert.ToInt32(dt.Rows[i]["DEF"]);
                        st.Add(inspection);
                    }
                    conn.Close();
                    return st;
                }
                return null;
            }
        }
    }
}
