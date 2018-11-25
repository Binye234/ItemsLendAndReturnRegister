using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BAL
{
    public class Registration
    {
        private string _conString;

        public string State {
            get;
            private set;
        }

        public Registration(string conString)
        {
            _conString = conString;
        }
        /// <summary>
        /// 查询是否已借出
        /// </summary>
        /// <returns></returns>
        public bool IsItemReturn()
        {
            bool flag = false;
            string[] tmp = new RegistrationDB(_conString).GetLast();
            if (tmp[0] == null)
            {
                flag = true;
            }else if (tmp[4] != string.Empty)
            {
                flag = true;
            }
            else
            {
                flag = false;
                State = "借出人：" + tmp[0] + "  借出原因：" + tmp[1] + "  实际借出时间：" + tmp[2] + "  预计归还时间：" + tmp[3];
            }
            return flag;
        }

        public bool Insert(string name,string reason ,string localTime,string advanceTime)
        {
            if (!IsItemReturn())
            {
                return false;
            }
            new RegistrationDB(_conString).Insert(name, reason, localTime, advanceTime);
            return true;
        }
        /// <summary>
        /// 核对姓名
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool IsName(string name)
        {
            bool flag = false;
            string tmp = new RegistrationDB(_conString).GetNullName();
            if (tmp == string.Empty)
            {
                flag = true;
            }else if (tmp == name)
            {
                flag = true;
            }
            else
            {
                flag = false;
            }
            return flag;
        }
        /// <summary>
        /// 更新时间
        /// </summary>
        /// <param name="name"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public bool Update(string name,string time)
        {
            if (IsName(name))
            {
                new RegistrationDB(_conString).Update(time);
                return true;
            }
            else
            {
                return false;
            }
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
            return new RegistrationDB(_conString).GetPageNums(name, beginTime, endTime);
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
            return new RegistrationDB(_conString).FindPage(name, beginTime, endTime, page);
        }
        /// <summary>
        /// 删除方法
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(string id)
        {
            return new RegistrationDB(_conString).DeleteID(id);
        }
    }
}
