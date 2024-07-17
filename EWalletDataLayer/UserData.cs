using System;
using System.Collections.Generic;
using System.Linq;
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

        public void UpdateUserPassword(string accountNumber, string pinNumber)
        {
            sqlData.UpdateUserPassword(accountNumber, pinNumber);
        }

        public void UpdateUsername(string accountNumber, string username)
        {
            sqlData.UpdateUsername(accountNumber, username);
        }

        public void AddUser(string accountNumber, string username,string pinNumber)
        {
            sqlData.AddUser(accountNumber, username, pinNumber);
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
