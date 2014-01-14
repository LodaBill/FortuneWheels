﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entity;
using DataAccess;
using System.Data;
using Entity.Model;
using System.Data.SqlClient;
using System.Net;
using System.Collections.Specialized;
using System.Web;

namespace BusinessLogic
{
    public class LotteryLogic
    {
        private static List<LotteryAwards> resultList;
        private static readonly string sSendSMSUrl = @"http://122.224.203.33/lenjoy/index.php/Outface/sendSMS?userphone={0}&sms={1}&chanid=yycj";
        private static readonly string sQueryDownLoadUrl = "http://122.224.203.33/lenjoy/index.php/Outface/queryAppDown?userphone={0}&appname={1}";
        private static readonly string sSMSText = "恭喜您在玩游戏、抽大奖活动中抽中{0}元话费！充值卡密码为 {1}，您可以复制该号码直接在玩库内充值";
        private static readonly string sAppName = "微信";
        private static readonly int MAXNUMBER = 10000;

        public static List<LotteryAwards> ResultList
        {
            get
            {
                if (resultList == null)
                {
                    resultList = LotteryDataAccess.GetAllLotteryAwards().ConvertToModel<LotteryAwards>();

                    int min = 0;
                    foreach (LotteryAwards award in resultList)
                    {
                        min += 1;
                        award.MinNumber = min;
                        min += Convert.ToInt32(MAXNUMBER * award.Rate);
                        award.MaxNumber = min;
                        min -= 1;
                    }
                }
                return resultList;
            }
            set
            {
                resultList = value;
            }
        }


        public static LotteryAwards StartLottery(string sPhoneNumber)
        {
            LotteryAwards result = Random();
            DataTable dt = LotteryDataAccess.ReduceCountAndSaveHistory(sPhoneNumber, result.AwardId);
            string AwardId = dt.Rows[0][0].ToString();
            string sCardCode = dt.Rows[0][1].ToString();
            if (!string.IsNullOrWhiteSpace(sCardCode))
            {
                SendSMS(sPhoneNumber, AwardId, sCardCode);
            }
            return ResultList.Where(x => x.AwardId == AwardId).FirstOrDefault();
        }

        public static bool CheckLotteryTime(string sPhoneNumber)
        {
            return GetLotteryTime(sPhoneNumber) > 0;
        }

        public static int GetLotteryTime(string sPhoneNumber)
        {
            LotteryUser user = GetLotteryUser(sPhoneNumber);
            return user == null ? 0 : user.LotteryCount;
        }

        public static List<LotteryHistory> GetLotteryHistory(string sPhoneNumber = "")
        {
            return LotteryDataAccess.GetLotteryHistory(sPhoneNumber).ConvertToModel<LotteryHistory>();
        }

        public static void StoreLotteryUser(string sPhoneNumber)
        {
            LotteryDataAccess.StoreLotteryUser(sPhoneNumber);
        }

        public static LotteryUser GetLotteryUser(string sPhoneNumber)
        {
            return LotteryDataAccess.GetLotteryUser(sPhoneNumber).ConvertToModel<LotteryUser>().FirstOrDefault();
        }

        public static List<LotteryUser> GetLotteryAllUser()
        {
            return LotteryDataAccess.GetLotteryAllUser().ConvertToModel<LotteryUser>();
        }


        public static bool UpdateAward(LotteryAwards awards)
        {
            return LotteryDataAccess.UpdateAward(awards.AwardId, awards.AwardName, awards.Rate.ToString(), awards.TotalCount, awards.SurplusCount);
        }

        public static void SendSMS(string sPhoneNumber, string sCardId, string sCardCode)
        {
            using (WebClient webClient = new WebClient())
            {
                webClient.Encoding = Encoding.UTF8;
                string response = webClient.UploadString(string.Format(sSendSMSUrl, sPhoneNumber,  HttpUtility.UrlEncode(string.Format(sSMSText, sCardId, sCardCode))), "GET");
                string retCode = response.IndexOf("Retcode") != -1 && response.IndexOf("Retcode") + 12 <= response.Length
                    ? response.Substring(response.IndexOf("Retcode") + 9, 3) : string.Empty;
                if (retCode != "100")
                {
                    throw new HttpException();
                }
            }
        }

        public static bool QueryIsDownLoad(string sPhoneNumber)
        {
            using (WebClient webClient = new WebClient())
            {
                webClient.Encoding = Encoding.UTF8;
                string response = webClient.UploadString(string.Format(sQueryDownLoadUrl, sPhoneNumber, HttpUtility.UrlEncode(sAppName)), "GET");
                string retCode = response.IndexOf("Retcode") != -1 && response.IndexOf("Retcode") + 12 <= response.Length
                    ? response.Substring(response.IndexOf("Retcode") + 9, 3) : string.Empty;
                string appName = response.IndexOf("APPNAME") != -1 && response.IndexOf("APPNAME") + 11 <= response.Length
                    ? response.Substring(response.IndexOf("APPNAME") + 10, 2) : string.Empty;
                if (retCode == "100" && appName == sAppName)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private static LotteryAwards Random()
        {
            Random random = new Random();
            int i = random.Next(1, MAXNUMBER);

            foreach (LotteryAwards result in ResultList)
            {
                if (result.MinNumber <= i && i < result.MaxNumber)
                {
                    return result;
                }
            }
            return ResultList.Where(x => x.Angle == 180).FirstOrDefault();
        }

    }
}
