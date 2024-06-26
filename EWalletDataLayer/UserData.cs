﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using EWalletModels;
using System.Numerics;

namespace EWalletDataLayer
{
    public class UserData
    {
        List<User> users;
        SqlDbData sqlData;
        public UserData()
        {
            users = new List<User>();
            sqlData = new SqlDbData();
        }

        public List<User> GetUsers()
        {
            users = sqlData.GetUsers();
            return users;
        }

        public void UpdateUserPassword(int accountNumber,string pinNumber)
        {
            sqlData.UpdateUserPassword(accountNumber, pinNumber);
        }

        public void UpdateUsername(int accountNumber, string username)
        {
            sqlData.UpdateUsername(accountNumber, username);
        }

        public void DeleteUser(int accountNumber)
        {
            sqlData.DeleteUser(accountNumber);
        }
    }
}
