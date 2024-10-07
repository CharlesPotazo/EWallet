using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using EWalletModels;

namespace EWalletDataLayer
{
    public class UserData
    {
        private SqlDbData sqlData;

        public UserData()
        {
            sqlData = new SqlDbData();
        }

        public List<User> GetUsers()
        {
            return sqlData.GetUsers();
        }

        public bool UpdateUserPassword(string accountNumber, string pinNumber)
        {
            var user = sqlData.GetUserByAccNum(accountNumber);
            if (user != null)
            {
                sqlData.UpdateUserPassword(accountNumber, pinNumber);
                return true;
            }
            return false;
        }

        public bool UpdateUsername(string accountNumber, string username)
        {
            var user = sqlData.GetUserByAccNum(accountNumber);
            if (user != null)
            {
                sqlData.UpdateUsername(accountNumber, username);
                return true;
            }
            return false;
        }

        public bool AddUser(string accountNumber, string username,string pinNumber, string email)
        {
            var user = sqlData.GetUserByAccNum(accountNumber);
            if (user == null)
            {
                sqlData.AddUser(accountNumber, username, pinNumber, email);
                return true;
            }
            return false;
            
        }

        public bool DeleteUser(string accountNumber)
        {
            var user = sqlData.GetUserByAccNum(accountNumber);
            if (user != null)
            {
                sqlData.DeleteUser(accountNumber);
                return true;
            }
            return false;
        }
    }
}
