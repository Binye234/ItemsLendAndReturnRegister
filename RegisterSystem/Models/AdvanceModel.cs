using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RegisterSystem.Models
{
    public class AdvanceModel
    {
        public int ID { set; get; }

        public string Name { set; get; }

        public string Reason { set; get; }

        public string LendTime { set; get; }

        public string ReturnTime { set; get; }
    }
}