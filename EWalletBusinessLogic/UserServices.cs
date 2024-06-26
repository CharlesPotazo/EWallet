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
        UserData data = new UserData();
        SqlDbData sqlDbData = new SqlDbData();
        public UserServices()
        {
            UserData data = new UserData();
            SqlDbData sqlDbData = new SqlDbData();
        }


        public List<User> GetAllUser()
        {
            return data.GetUsers();
        }


        public bool verifyUser(int accountNumber, string pinNumber)
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

        public User GetUserByAccNum(int accountNumber)
        {
            return sqlDbData.GetUserByAccNum(accountNumber);
        }

        public void RegisterUser(int accountNumber, string userName, string pinNumber)
        {
            sqlDbData.AddUser(accountNumber, userName, pinNumber);
        }

        public void UpdateUserPassword(int accountNumber,string pinNumber)
        {
            data.UpdateUserPassword(accountNumber, pinNumber);
        }


    }
}