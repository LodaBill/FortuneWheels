using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web.Configuration;

namespace DataAccess
{
    public class LotteryDataAccess
    {
        public static DataTable GetLotteryHistory(string sPhoneNumber)
        {
            DataBase dataBase = new DataBase();
            string sql = "select * from LotteryHistory where CellPhoneNo = "+sPhoneNumber;
            return dataBase.ExcuteSqlReturnDataTable(sql);
        }

        public static DataTable GetAllLotteryAwards()
        {
            DataBase dataBase = new DataBase();
            string sql = "select * from Awards";
            return dataBase.ExcuteSqlReturnDataTable(sql);
        }

        public static DataTable GetLotteryUser(string sPhoneNumber)
        {
            DataBase dataBase = new DataBase();
            string sql = "select * from Players where CellPhoneNo = " + sPhoneNumber;
            return dataBase.ExcuteSqlReturnDataTable(sql);
        }

        public static void StoreLotteryUser(string sPhoneNumber)
        {
            DataBase dataBase = new DataBase();
            string sql = "select * from Players where CellPhoneNo = " + sPhoneNumber;
            dataBase.ExcuteSqlReturnInt(sql);
        }
    }
}
