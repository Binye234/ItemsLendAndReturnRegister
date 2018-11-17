using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DAL
{
    /// <summary>
    /// 预约表数据库操作类
    /// </summary>
    public class AdvanceDB
    {
        private string _conString; //数据库字符串

        public AdvanceDB(string conString)
        {
            _conString = conString;
        }
        /// <summary>
        /// 插入方法
        /// </summary>
        /// <param name="name">姓名</param>
        /// <param name="reason">原因</param>
        /// <param name="time">时间</param>
        public void Insert(string name,string reason,string lendTime, string returnTime)
        {
            string sql = "INSERT INTO InformInAdvance VALUES(@name,@reason,@lendTime,@returnTime)";
            using(SqlConnection cn=new SqlConnection(_conString))
            {
                using(SqlCommand cm=new SqlCommand(sql,cn))
                {
                    cm.Parameters.AddWithValue("@name", name);
                    cm.Parameters.AddWithValue("@reason", reason);
                    cm.Parameters.AddWithValue("@lendTime", lendTime);
                    cm.Parameters.AddWithValue("@returnTime", returnTime);
                    cn.Open();
                    cm.ExecuteNonQuery();
                }
            }
        }
        /// <summary>
        /// 查询最新时间10条
        /// </summary>
        /// <returns></returns>
        public List<String[]> GetTen()
        {
            List<String[]> result = new List<string[]>();
            string sql = "SELECT TOP 10 *   FROM InformInAdvance ORDER BY AdvanceTime desc";
            using (SqlConnection cn = new SqlConnection(_conString))
            {
                using (SqlCommand cm = new SqlCommand(sql, cn))
                {                 
                    cn.Open();
                    SqlDataReader reader = cm.ExecuteReader();
                    while (reader.Read())
                    {
                        string[] tmp = new string[4];
                        tmp[0] = reader[1].ToString();
                        tmp[1] = reader[2].ToString();
                        tmp[2] = reader[3].ToString();
                        tmp[3] = reader[4].ToString();
                        result.Add(tmp);
                    }
                }
            }
            return result;
        }
    }
}
