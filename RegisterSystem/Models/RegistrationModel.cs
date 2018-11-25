using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RegisterSystem.Models
{
    public class RegistrationModel
    {
        public string ID { set; get; }

        public string Name { set; get; }

        public string Reason { set; get; }
        /// <summary>
        /// 实际借出时间
        /// </summary>
        public string BeforeTime { set; get; }
        /// <summary>
        /// 预约归还时间
        /// </summary>
        public string BeforeAdvanceTime { set; get; }
        /// <summary>
        /// 实际归还时间
        /// </summary>
        public string AfterTime { set; get; }
    }
}