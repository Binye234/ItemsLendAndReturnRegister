using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BAL;
using RegisterSystem.Models;

namespace RegisterSystem.Controllers
{
    public class LookController : Controller
    {
        // GET: Look
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ShowAdvance()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetAdvancePageNum(string name, string beginTime, string endTime)
        {
            string constring = ConfigurationManager.AppSettings["connectionString"];
            if (beginTime == string.Empty)
            {
                beginTime = "2000.1.1 00:00";
            }
            if (endTime == string.Empty)
            {
                endTime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
            }
            int nums = new Advance(constring).GetPageNums(name, beginTime, endTime);
            int pages = 0;
            if (nums != 0)
            {
                if (nums % 10 == 0)
                {
                    pages = nums / 10;
                }
                else
                {
                    pages = nums / 10 + 1;
                }
            }
            return Json(pages);
        }

        [HttpPost]
        public ActionResult FindAdvance(string name, string beginTime, string endTime, string page)
        {
            string constring = ConfigurationManager.AppSettings["connectionString"];
            if (beginTime == string.Empty)
            {
                beginTime = "2000.1.1 00:00";
            }
            if (endTime == string.Empty)
            {
                endTime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
            }
            List<string[]> data = new Advance(constring).FindPage(name, beginTime, endTime, page);
            List<AdvanceModel> result = new List<AdvanceModel>();
            foreach(var item in data)
            {
                AdvanceModel model = new AdvanceModel();
                model.ID = int.Parse(item[0]);
                model.Name = item[1];
                model.Reason = item[2];
                model.LendTime = item[3];
                model.ReturnTime = item[4];
                result.Add(model);
            }
            return Json(result);
        }

        [HttpPost]
        public ActionResult DeleteAdvance(string id)
        {
            string constring = ConfigurationManager.AppSettings["connectionString"];
            bool flag = new Advance(constring).Delete(id);
            if (flag)
            {
                return Json("true");
            }
            else
            {
                return Json("false");
            }
        }

        public ActionResult ShowRegistration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetRegistrationPageNum(string name, string beginTime, string endTime)
        {
            string constring = ConfigurationManager.AppSettings["connectionString"];
            if (beginTime == string.Empty)
            {
                beginTime = "2000.1.1 00:00";
            }
            if (endTime == string.Empty)
            {
                endTime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
            }
            int nums = new Registration(constring).GetPageNums(name, beginTime, endTime);
            int pages = 0;
            if (nums != 0)
            {
                if (nums % 10 == 0)
                {
                    pages = nums / 10;
                }
                else
                {
                    pages = nums / 10 + 1;
                }
            }
            return Json(pages);
        }
        [HttpPost]
        public ActionResult FindRegistration(string name, string beginTime, string endTime, string page)
        {
            string constring = ConfigurationManager.AppSettings["connectionString"];
            if (beginTime == string.Empty)
            {
                beginTime = "2000.1.1 00:00";
            }
            if (endTime == string.Empty)
            {
                endTime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
            }
            List<string[]> data = new Registration(constring).FindPage(name, beginTime, endTime, page);
            List<RegistrationModel> result = new List<RegistrationModel>();
            foreach (var item in data)
            {
                RegistrationModel model = new RegistrationModel();
                model.ID = item[0];
                model.Name = item[1];
                model.Reason = item[2];
                model.BeforeTime = item[3];
                model.BeforeAdvanceTime = item[4];
                model.AfterTime = item[5];
                result.Add(model);
            }
            return Json(result);
        }

        [HttpPost]
        public ActionResult DeleteRegistration(string id)
        {
            string constring = ConfigurationManager.AppSettings["connectionString"];
            bool flag = new Registration(constring).Delete(id);
            if (flag)
            {
                return Json("true");
            }
            else
            {
                return Json("false");
            }
        }
    }
}