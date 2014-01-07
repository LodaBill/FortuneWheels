using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entity;
using BusinessLogic;
using System.Threading;
using Entity.Model;

namespace FortuneWheel.Controllers
{
    public class LotteryController : Controller
    {
        //
        // GET: /Lottery/

        public ActionResult Lottery()
        {
            return View();
        }

        public ActionResult Begin(string sPhoneNumber)
        {
            LotteryAwards result = new LotteryAwards();
            string sErrorMessage = string.Empty;
            //check number of time
            if (LotteryLogic.CheckLotteryTime(sPhoneNumber))
            {
                result = LotteryLogic.StartLottery();
            }
            else
            {
                sErrorMessage = "已经没有抽奖次数";
            }
            Thread.Sleep(5000);
            return Json(new {
                result = result.Angle,
                error = sErrorMessage                
            },JsonRequestBehavior.AllowGet);
        }

        public ActionResult Refresh(string sPhoneNumber)
        {
            int num = LotteryLogic.GetLotteryTime(sPhoneNumber);
            return Json(new { 
                num = num
            }, JsonRequestBehavior.AllowGet);
        }

    }
}
