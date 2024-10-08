﻿using EWalletModels;
using EWalletDataLayer;
using System.Collections.Generic;

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

        public bool RegisterUser(string accountNumber, string userName, string pinNumber, string email)
        {
            return data.AddUser(accountNumber, userName, pinNumber, email);
        }

        public bool UpdateUserPassword(string accountNumber,string pinNumber)
        {
                return data.UpdateUserPassword(accountNumber,pinNumber);
                
        }

        public bool UpdateUsername(string accountNumber, string username)
        {
            return data.UpdateUsername(accountNumber, username);
        }

        public bool DeleteUser(string accountNumber, string pinNumber)
        {
            return data.DeleteUser(accountNumber);
        }
    }
}