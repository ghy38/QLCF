using QLCF.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace QLCF.DTO
{
    public class AccountDAO
    {
        private static AccountDAO instance;

        public static AccountDAO Instance
        {
            get { if (instance == null) instance = new AccountDAO(); return instance; }
            private set { instance = value; }
        }

        private AccountDAO() { }

        public bool Login(string userName, string passWord)
        {
            byte[] temp = ASCIIEncoding.ASCII.GetBytes(passWord);
            byte[] hasData = new MD5CryptoServiceProvider().ComputeHash(temp);

            string hasPass = "";

            foreach (byte item in hasData)
            {
                hasPass += item;
            }

            string query = "USP_Login @userName , @passWord";
                
            DataTable result = DataProvider.Instance.ExecuteQuery(query, new object[] { userName, hasPass });

            return result.Rows.Count > 0;
        }

        public bool UpdateAccount(string userName, string displayName, string pass, string newPass)
        {
            byte[] temp = ASCIIEncoding.ASCII.GetBytes(pass);
            byte[] hasData = new MD5CryptoServiceProvider().ComputeHash(temp);

            byte[] temp2 = ASCIIEncoding.ASCII.GetBytes(newPass);
            byte[] hasData2 = new MD5CryptoServiceProvider().ComputeHash(temp2);

            string hasPass = "";
            string hasnewPass = "";

            foreach (byte item in hasData)
            {
                hasPass += item;
            }

            foreach (byte item2 in hasData2)
            {
                hasnewPass += item2;
            }

            int result = DataProvider.Instance.ExecuteNonQuery("EXEC USP_UpdateAccount @userName , @displayName , @password , @newPassword", new object[] { userName, displayName, hasPass, hasnewPass });

            return result > 0;
        }

        public DataTable GetListAccount()
        {
            return DataProvider.Instance.ExecuteQuery("SELECT Username, DisplayName, Type FROM dbo.Account");
        }

        public Account GetAccountByUserName(string userName)
        {
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT * FROM Account WHERE userName = '"+userName+"'");

            foreach (DataRow item in data.Rows)
            {
                return new Account(item);
            }

            return null;
        }

        public bool InsertAccount(string userName, string displayName, int type)
        {
            string query = string.Format("INSERT dbo.Account(UserName, DisplayName, Type, password) VALUES (N'{0}', N'{1}', {2}, N'{3}')", userName, displayName, type, "3244185981728979115075721453575112");
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public bool UpdateAccount(string userName, string displayName, int type)
        {
            string query = string.Format("UPDATE dbo.Account SET DisplayName = N'{0}', Type = {1} WHERE UserName = N'{2}'", displayName, type, userName);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public bool DeleteAccount(string userName)
        {
            string query = string.Format("DELETE Account WHERE UserName = N'{0}'", userName);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public bool ResetPassword(string userName)
        {
            string query = string.Format("UPDATE Account SET password = N'3244185981728979115075721453575112' WHERE UserName = N'{0}'", userName);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
    }
}
