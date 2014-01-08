using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web.Configuration;
using System.Data.SqlClient;

namespace DataAccess
{
    public class LotteryDataAccess
    {
        public static DataTable GetLotteryHistory(string sPhoneNumber)
        {
            DataBase dataBase = new DataBase();
            string sql = "select a.cellphoneno,a.lotterytime,b.awardname,a.cardcode from LotteryHistory a join Awards b on a.AwardId = b.AwardId";
            if (!string.IsNullOrWhiteSpace(sPhoneNumber))
            {
                sql += "where CellPhoneNo = " + sPhoneNumber;
            }
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
            string sql = "exec spNewPlayer @CellPhoneNo = @CellPhoneNo";
            SqlParameter[] sqlparameter = new SqlParameter[1];
            sqlparameter[0] = new SqlParameter("@CellPhoneNo", sPhoneNumber);
            dataBase.ExcuteSqlReturnInt(sql, sqlparameter);
        }

        public static DataTable ReduceCountAndSaveHistory(string sPhoneNumber,string sAwardId)
        {
            DataBase dataBase = new DataBase();
            string sql = "exec spLotteryProcess @CellPhoneNo = @CellPhoneNo , @AwardID = @AwardID ";
            SqlParameter[] sqlparameter = new SqlParameter[2];
            sqlparameter[0] = new SqlParameter("@CellPhoneNo",sPhoneNumber);
            sqlparameter[1] = new SqlParameter("@AwardID", sAwardId);
            return dataBase.ExcuteSqlReturnDataTable(sql, sqlparameter);
        }
    }
}
