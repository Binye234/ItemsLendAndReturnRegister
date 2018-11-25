using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BAL
{
    public class Advance
    {
        private string _constring;
        public Advance(string constring)
        {
            _constring = constring;
        }
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="name"></param>
        /// <param name="reason"></param>
        /// <param name="time"></param>
        public void Insert(string name, string reason, string lendTime,string returnTime)
        {
            new AdvanceDB(_constring).Insert(name, reason, lendTime, returnTime);
        }
        /// <summary>
        /// 取前10
        /// </summary>
        /// <returns></returns>
        public List<string[]> GetTen()
        {
            return new AdvanceDB(_constring).GetTen();
        }
        /// <summary>
        /// 返回查询总数
        /// </summary>
        /// <param name="name"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public int GetPageNums(string name, string beginTime, string endTime)
        {
            return new AdvanceDB(_constring).GetPageNums( name,  beginTime,  endTime);
        }
        /// <summary>
        /// 返回按页查询结果
        /// </summary>
        /// <param name="name"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="page">第几页</param>
        /// <returns></returns>
        public List<string[]> FindPage(string name, string beginTime, string endTime, string page)
        {
            return new AdvanceDB(_constring).FindPage(name, beginTime, endTime, page);
        }
        /// <summary>
        /// 删除方法
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(string id)
        {
            return new AdvanceDB(_constring).DeleteID(id);
        }
    }
}
