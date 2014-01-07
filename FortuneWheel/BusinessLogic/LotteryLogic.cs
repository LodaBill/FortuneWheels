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
        public static LotteryAwards StartLottery()
        {
            LotteryAwards result = Random();
            //LotteryDataAccess.ExecuteSP();
            return result;
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

        public static List<LotteryHistory> GetLotteryHistory(string sPhoneNumber)
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

        public static List<LotteryAwards> GetLotteryAwards()
        {
            return LotteryDataAccess.GetAllLotteryAwards().ConvertToModel<LotteryAwards>();
        }

        private static LotteryAwards Random()
        {
            Random random = new Random();
            int i = random.Next(1, 10000);
            if (resultList == null)
            {
                resultList = GetLotteryAwards();
            }

            foreach (LotteryAwards result in resultList)
            {
                //if (result.MinNumber <= i && i < result.MaxNumber)
                //{
                //    return result;
                //}
            }
            return null;
        }

    }
}
