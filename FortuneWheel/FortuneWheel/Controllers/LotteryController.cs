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
            string sPhoneNumber = this.Request.QueryString["PhoneNumber"];
            //TODO If is authentication
            if (string.IsNullOrWhiteSpace(sPhoneNumber))
            {
                ViewBag.IsAuthenticate = false;
            }
            else
            {
                ViewBag.IsAuthenticate = true;
                LotteryLogic.StoreLotteryUser(sPhoneNumber);
            }
            return View();
        }

        public ActionResult Begin(string sPhoneNumber)
        {
            int sAngle = 300;
            string sErrorMessage = string.Empty;
            try
            {
                if (string.IsNullOrWhiteSpace(sPhoneNumber))
                {
                    throw new FormatException();
                }
                //check number of time
                if (LotteryLogic.CheckLotteryTime(sPhoneNumber))
                {
                    sAngle = LotteryLogic.StartLottery(sPhoneNumber).Angle;
                    Thread.Sleep(3000);
                }
                else
                {
                    sErrorMessage = "已经没有抽奖次数";
                }

            }
            catch (FormatException e)
            {
                sErrorMessage = " 该手机号无法参加活动";
            }
            catch (Exception e)
            {
                sErrorMessage = " 发生错误请重试";
            }
            return Json(new {
                result = sAngle,
                error = sErrorMessage                
            },JsonRequestBehavior.AllowGet);
        }

        public ActionResult Refresh(string sPhoneNumber)
        {
            int num = LotteryLogic.GetLotteryTime(sPhoneNumber);
            return Json(new
            {
                num = num
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LotteryHistory()
        {
            List<LotteryHistory> historyList = LotteryLogic.GetLotteryHistory();
            return View(historyList);
        }

    }
}
