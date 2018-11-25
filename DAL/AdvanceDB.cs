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
        /// <summary>
        /// 返回查询总数
        /// </summary>
        /// <param name="name"></param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        public int GetPageNums(string name, string beginTime, string endTime)
        {
            int result = 0;
            string sql = "SELECT COUNT(*) FROM InformInAdvance WHERE Name LIKE @name and AdvanceTime>=@beginTime and AdvanceTime <=@endTime";
            using (SqlConnection cn = new SqlConnection(_conString))
            {
                using (SqlCommand cm = new SqlCommand(sql, cn))
                {
                    cm.Parameters.AddWithValue("@name", "%" + name + "%");
                    cm.Parameters.AddWithValue("@beginTime", beginTime);
                    cm.Parameters.AddWithValue("@endTime", endTime);
                    cn.Open();
                    result =(int) cm.ExecuteScalar();
                }
            }
            return result;
        }
        /// <summary>
        /// 按页返回查询结果
        /// </summary>
        /// <param name="name"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public List<string[]> FindPage(string name, string beginTime, string endTime, string page)
        {
            List<string[]> result = new List<string[]>();
            string sql = "SELECT TOP (10*@page)  * FROM InformInAdvance WHERE Name LIKE @name and AdvanceTime>=@beginTime and AdvanceTime <=@endTime ORDER BY AdvanceTime DESC";
            using (SqlConnection cn = new SqlConnection(_conString))
            {
                using (SqlCommand cm = new SqlCommand(sql, cn))
                {
                    cm.Parameters.AddWithValue("@page", page);
                    cm.Parameters.AddWithValue("@name", "%" + name + "%");
                    cm.Parameters.AddWithValue("@beginTime", beginTime);
                    cm.Parameters.AddWithValue("@endTime", endTime);
                    cn.Open();
                    var reader = cm.ExecuteReader();
                    while (reader.Read())
                    {
                        string[] tmp = new string[5];
                        tmp[0] = reader[0].ToString();
                        tmp[1] = reader[1].ToString();
                        tmp[2] = reader[2].ToString();
                        tmp[3] = reader[3].ToString();
                        tmp[4] = reader[4].ToString();
                        result.Add(tmp);
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 删除方法
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteID(string id)
        {
            bool flag = false;
            try
            {
                string sql = "DELETE InformInAdvance WHERE ID=@ID";
                using (SqlConnection cn = new SqlConnection(_conString))
                {
                    using (SqlCommand cm = new SqlCommand(sql, cn))
                    {
                        cm.Parameters.AddWithValue("@ID", id);          
                        cn.Open();
                       int n= cm.ExecuteNonQuery();
                        if (1 == n)
                        {
                            flag = true;
                        }
                    }
                }
                return flag;
            }
            catch
            {
                return false;
            }
        }
    }
}
