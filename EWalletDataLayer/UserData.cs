using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using EWalletModels;

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
    }
}
