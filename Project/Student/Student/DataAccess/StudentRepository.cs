using Student.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Student.DataAccess
{
    public class StudentRepository
    {
        private string connectionstring = ConfigurationManager.ConnectionStrings["DBCon"].ToString();
        public StudentRepository() { }

        public List<DropdownModel> GetCityList()
        {
            List <DropdownModel> CityList= new List <DropdownModel>();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionstring))
                {
                    SqlCommand cmd = new SqlCommand("SELECT id, city FROM Tbl_City", con);
                    con.Open();
                    SqlDataReader reader= cmd.ExecuteReader();
                    while (reader.Read()) 
                    {
                        DropdownModel DM = new DropdownModel() { Key = Convert.ToInt32(reader["id"]), Value = reader["city"].ToString() };
                        CityList.Add(DM);
                    }
                }
                return CityList;
            }
            catch (Exception ex)
            {
                return CityList;
            }
        }

        public List<DropdownModel> GetSubjectList()
        {
            List<DropdownModel> SubjectList = new List<DropdownModel>();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionstring))
                {
                    SqlCommand cmd = new SqlCommand("SELECT id, Subject FROM Tbl_Subject", con);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        DropdownModel DM = new DropdownModel() { Key = Convert.ToInt32(reader["id"]), Value = reader["Subject"].ToString() };
                        SubjectList.Add(DM);
                    }
                }
                return SubjectList;
            }
            catch (Exception ex)
            {
                return SubjectList;
            }
        }

        public bool SaveStudent(StudentModel SM)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionstring))
                {
                    SqlCommand cmd = new SqlCommand("USP_InsertOrUpdateStudent", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@S_ID", SM.s_id);
                    cmd.Parameters.AddWithValue("@S_name", SM.s_name);
                    cmd.Parameters.AddWithValue("@S_Age", SM.s_Age);
                    cmd.Parameters.AddWithValue("@S_Address", SM.s_Address_id);
                    cmd.Parameters.AddWithValue("@S_Subject", SM.s_SubjectID);
                    cmd.Parameters.AddWithValue("@opsection", SM.opsection);

                    cmd.Parameters.Add("@ERRORCODE",SqlDbType.Int);
                    cmd.Parameters["@ERRORCODE"].Direction = ParameterDirection.Output;

                    con.Open();
                    cmd.ExecuteNonQuery();

                    int ERRORCODE = Convert.ToInt32(cmd.Parameters["@ERRORCODE"].Value);

                    if (ERRORCODE == 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<StudentList> GetAllStudent()
        {
            List <StudentList> Students = new List<StudentList>();    
            try
            {
                using (SqlConnection con = new SqlConnection(connectionstring))
                {
                    SqlCommand cmd = new SqlCommand(@"SELECT S_id, S_Name, S_Age, ct.City AS Address, sub.Subject FROM TBL_Student st
                                                        LEFT JOIN Tbl_City ct ON ct.id = st.s_Address
                                                        LEFT JOIN Tbl_Subject sub ON sub.id = st.Subject_opted", con);
                    con.Open();
                    SqlDataReader reader= cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Students.Add(new StudentList()
                        {
                            s_id = Convert.ToInt32(reader["S_id"]),
                            s_name = reader["S_Name"].ToString(),
                            s_Age = Convert.ToInt32(reader["S_Age"]),
                            s_Address = reader["Address"].ToString(),
                            s_Subject = reader["Subject"].ToString()
                        });
                    }
                }

                return Students;
            }
            catch (Exception ex)
            {
                return Students;
            }
        }

        public StudentModel GetStudent(int Studentid)
        {
            StudentModel SM = new StudentModel();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionstring))
                {
                    SqlCommand cmd = new SqlCommand(@"SELECT S_id, S_Name, S_Age, S_Address, Subject_opted
                                                        FROM TBL_Student WHERE S_id = @S_id", con);

                    cmd.Parameters.AddWithValue("@s_id", Studentid);

                    con.Open();
                    SqlDataReader reader= cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        SM.s_id = Convert.ToInt32(reader["S_id"]);
                        SM.s_name = reader["S_Name"].ToString();
                        SM.s_Age = Convert.ToInt32(reader["S_Age"]);
                        SM.s_Address_id = Convert.ToInt32(reader["S_Address"]);
                        SM.s_SubjectID = Convert.ToInt32(reader["Subject_opted"]);
                    }
                }
                return SM;
            }
            catch (Exception ex)
            {
                return SM;
            }
        }

        public bool RemoveStudentDetails(int studentid)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionstring))
                {
                    SqlCommand cmd = new SqlCommand("DELETE FROM TBL_Student WHERE S_id = @S_id", con);
                    cmd.Parameters.AddWithValue("@S_id",studentid);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}