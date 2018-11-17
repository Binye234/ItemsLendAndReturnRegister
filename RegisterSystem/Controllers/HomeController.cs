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
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            string constring =  ConfigurationManager.AppSettings["connectionString"];
            Registration registration = new Registration(constring);
            if (registration.IsItemReturn())
            {
                ViewBag.state = "设备状态: 已归还";
            }
            else
            {
                ViewBag.state = registration.State;
            }     
            List<string[]> tmp = new Advance(constring).GetTen();
            List<AdvanceModel> result = new List<AdvanceModel>();
            int i = 1;
            foreach(var item in tmp)
            {
                AdvanceModel model = new AdvanceModel();
                model.ID = i++;
                model.Name = item[0];
                model.Reason = item[1];
                model.LendTime = item[2];
                model.ReturnTime = item[3];
                result.Add(model);
            }
            return View(result);
        }

        public ActionResult Lend()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AdvanceLend(string beforeName,string beforeReason,string beforeTime,string returnTime)
        {
            string constring = ConfigurationManager.AppSettings["connectionString"];
            string time1 = DateTime.Parse(beforeTime).ToString("yyyy-MM-dd HH:mm:ss");
            string time2= DateTime.Parse(returnTime).ToString("yyyy-MM-dd HH:mm:ss");
            new Advance(constring).Insert(beforeName, beforeReason, time1,time2);
            return Redirect("Index");
        }

        [HttpPost]
        public ActionResult RegistrationLend(string afterName, string afterReason,string advanceTime)
        {

            string constring = ConfigurationManager.AppSettings["connectionString"];
            string lendTime = DateTime.Now.ToString();
            string time = DateTime.Parse(advanceTime).ToString("yyyy-MM-dd HH:mm:ss");
            afterName = afterName.Trim(' ');
            if (!new Registration(constring).Insert(afterName, afterReason, lendTime, time))
            {
                return Redirect("ErrorLend");
            }
            return Redirect("Index");
        }

     

        [HttpPost]
        public ActionResult RegistrationGiveBack(string GiveName)
        {
            string constring = ConfigurationManager.AppSettings["connectionString"];
            string time = DateTime.Now.ToString();
            GiveName = GiveName.Trim(' ');
            if (!new Registration(constring).Update(GiveName,time))
            {
                return Redirect("ErrorGive");
            }
            return Redirect("Index");
        }

        public ActionResult ErrorGive()
        {
            return View();
        }

        public ActionResult ErrorLend()
        {
            return View();
        }

        public ActionResult GiveBack()
        {
            return View();
        }
    }
}