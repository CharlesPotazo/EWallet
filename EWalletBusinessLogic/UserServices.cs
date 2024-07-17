using EWalletModels;
using EWalletDataLayer;
using System.Diagnostics.CodeAnalysis;
using System.Data.SqlClient;
using System.Security.Principal;
using System.Numerics;

namespace EWalletBusinessLogic
{
    public class UserServices
    {
        UserData data;
        private SqlDbData sqlDbData;

        public UserServices()
        {
            data = new UserData();
            sqlDbData = new SqlDbData();
        }


        public List<User> GetAllUser()
        {
            return data.GetUsers();
        }


        public bool verifyUser(string accountNumber, string pinNumber)
        {
            bool result = new bool();
            foreach (var users in GetAllUser())
            {
                if (users.accountNumber == accountNumber && users.pinNumber == pinNumber)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public User GetUserByAccNum(string accountNumber)
        {
            return sqlDbData.GetUserByAccNum(accountNumber);
        }

        public void RegisterUser(string accountNumber, string userName, string pinNumber)
        {
            data.AddUser(accountNumber, userName, pinNumber);
        }

        public void UpdateUserPassword(string accountNumber,string pinNumber)
        {
            data.UpdateUserPassword(accountNumber, pinNumber);
        }

        public void UpdateUsername(string accountNumber, string username)
        {
            data.UpdateUsername(accountNumber, username);
        }

        public bool DeleteUser(string accountNumber, string pinNumber)
        {
            var user = sqlDbData.GetUserByAccNum(accountNumber);
            if (user != null && user.pinNumber == pinNumber && user.money <= 0)
            {
                data.DeleteUser(accountNumber);
                return true;
            }
            return false;
        }
    }
}