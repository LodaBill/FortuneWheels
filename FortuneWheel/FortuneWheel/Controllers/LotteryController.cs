﻿using System;
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
            if (!LotteryLogic.QueryIsDownLoad(sPhoneNumber))
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
                if (!LotteryLogic.QueryIsDownLoad(sPhoneNumber))
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
                    sErrorMessage = "您已参与过此次活动，谢谢关注！";
                }

            }
            catch (FormatException e)
            {
                sErrorMessage = "请点击“火速前往”按钮下载安装游戏参与抽奖";
            }
            catch (HttpException e)
            {
                sErrorMessage = "发送短信失败";
            }
            catch (Exception e)
            {
                sErrorMessage = " 发生错误请重试";
            }
            return Json(new
            {
                result = sAngle,
                error = sErrorMessage
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Refresh(string sPhoneNumber)
        {
            string sErrorMessage = string.Empty;
            int num = 0;
            try
            {
                num = LotteryLogic.GetLotteryTime(sPhoneNumber);
            }
            catch (Exception e)
            {
                sErrorMessage = " 发生错误请重试";
            }
            return Json(new
            {
                num = num,
                error = sErrorMessage
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string sUserName, string sPassword)
        {
            if (sUserName == "administrator" && sPassword == "Dkdk75490")
            {
                Session["user"] = "Authorized";
                return RedirectToAction("Detail");
            }
            return View();
        }

        public ActionResult Detail()
        {
            if (Session["user"] == null || Session["user"].ToString() != "Authorized")
            {
                return RedirectToAction("Login");
            }
            List<LotteryAwards> userList = LotteryLogic.ResultList;
            return View(userList);
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            if (Session["user"] == null || Session["user"].ToString() != "Authorized")
            {
                return RedirectToAction("Login");
            }
            LotteryAwards model = LotteryLogic.ResultList.Where(t => t.AwardId == id).FirstOrDefault();
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(LotteryAwards model)
        {
            if (Session["user"] == null || Session["user"].ToString() != "Authorized")
            {
                return RedirectToAction("Login");
            }
            if (LotteryLogic.UpdateAward(model))
            {
                LotteryLogic.ResultList = null;
                return RedirectToAction("Detail");
            }
            return View();
        }
    }
}
