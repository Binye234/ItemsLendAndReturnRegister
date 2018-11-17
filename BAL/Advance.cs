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
    }
}
