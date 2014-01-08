using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entity;
using DataAccess;
using System.Data;
using Entity.Model;

namespace BusinessLogic
{
    public class LotteryLogic
    {
        private static List<LotteryAwards> resultList;
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
        }

        private static readonly int MAXNUMBER = 10000;

        public static LotteryAwards StartLottery(string sPhoneNumber)
        {
            LotteryAwards result = Random();
            DataTable dt = LotteryDataAccess.ReduceCountAndSaveHistory(sPhoneNumber, result.AwardId);
            string AwardId = dt.Rows[0][0].ToString();
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
            return null;
        }

    }
}
