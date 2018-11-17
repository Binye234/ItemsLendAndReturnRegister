using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DAL
{
    public class RegistrationDB
    {
        private string _conString;

        public RegistrationDB(string conString)
        {
            _conString = conString;
        }
        /// <summary>
        /// 查询最新记录
        /// </summary>
        /// <returns></returns>
        public string[] GetLast()
        {
            string[] result = new string[5];
            string sql = "SELECT TOP 1 Name,Reason,BeforeTime,BeforeAdvanceTime,AfterTime FROM RegistrationForm ORDER BY ID DESC";
            using(SqlConnection cn=new SqlConnection(_conString))
            {
                using(SqlCommand cm=new SqlCommand(sql, cn))
                {
                    cn.Open();
                    SqlDataReader reader = cm.ExecuteReader();
                    while (reader.Read())
                    {
                        result[0] = reader[0].ToString();
                        result[1] = reader[1].ToString();
                        string s = reader[2].ToString();
                        result[2] = s.Substring(0, s.LastIndexOf(':') );
                        s= reader[3].ToString();
                        result[3] = s.Substring(0, s.LastIndexOf(':'));
                        result[4] = reader[4].ToString();
                    }
                  
                }
            }
            return result;
        }

        public int GetNullIndex()
        {
            int result;
            string sql = "SELECT ID FROM RegistrationForm WHERE AfterTime IS NULL";
            using (SqlConnection cn = new SqlConnection(_conString))
            {
                using (SqlCommand cm = new SqlCommand(sql, cn))
                {
                    cn.Open();
                    if(int.TryParse(cm.ExecuteScalar().ToString(),out result))
                    {

                    }
                    else
                    {
                        result = 0;
                    }
                    
                }
            }
            return result;
        }
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="name"></param>
        /// <param name="reason"></param>
        /// <param name="time"></param>
        public void Insert(string name,string reason,string localTime,string advanceTime)
        {
            string sql = "INSERT INTO RegistrationForm(Name,Reason,BeforeTime,BeforeAdvanceTime) VALUES(@name,@reason,@localTime,@advanceTime)";
            using (SqlConnection cn=new SqlConnection(_conString))
            {
                using(SqlCommand cm=new SqlCommand(sql, cn))
                {
                    cm.Parameters.AddWithValue("@name", name);
                    cm.Parameters.AddWithValue("@reason", reason);
                    cm.Parameters.AddWithValue("@localTime", localTime);
                    cm.Parameters.AddWithValue("@advanceTime", advanceTime);
                    cn.Open();
                    cm.ExecuteNonQuery();
                }
            }
        }
        /// <summary>
        /// 更新时间
        /// </summary>
        /// <param name="time"></param>
        public void Update(string time)
        {
            int id = GetNullIndex();
            string sql = "UPDATE RegistrationForm SET AfterTime=@time WHERE ID=@id ";
            using (SqlConnection cn = new SqlConnection(_conString))
            {
                using (SqlCommand cm = new SqlCommand(sql, cn))
                {
                    cm.Parameters.AddWithValue("@time", time);
                    cm.Parameters.AddWithValue("@id", id);
                    cn.Open();
                    cm.ExecuteNonQuery();
                }
            }
        }
        /// <summary>
        /// 返回实际借出人姓名
        /// </summary>
        /// <returns></returns>
        public string GetNullName()
        {
            string result;
            string sql = "SELECT Name FROM RegistrationForm WHERE AfterTime IS NULL";
            using (SqlConnection cn = new SqlConnection(_conString))
            {
                using (SqlCommand cm = new SqlCommand(sql, cn))
                {
                    cn.Open();
                    result = cm.ExecuteScalar().ToString();
                }
            }
            return result;
        }
    }
}
