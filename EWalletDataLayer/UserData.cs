using System.Collections.Generic;
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

        public List<User> GetAllUser()
        {
            return sqlData.GetAllUsers();
        }

        public void ActivateUser(string email)
        {
            sqlData.Activate(email);
        }

        public void DeactivateUser(string accountnumber)
        {
            sqlData.Deactivate(accountnumber);
        }

        public void AddUser(string accountNumber, string userName, string pinNumber, string email)
        {
            sqlData.AddUser(accountNumber, userName, pinNumber, email);
        }

        public void UpdateUserPassword(string accountNumber, string pinNumber)
        {
            sqlData.UpdateUserPassword(accountNumber, pinNumber);
        }

        public void UpdateUsername(string accountNumber, string userName)
        {
            sqlData.UpdateUsername(accountNumber, userName);
        }

        public void UpdateEmail(string accountNumber, string email)
        {
            sqlData.UpdateEmail(accountNumber, email);
        }

        public void UpdateMoney(User user)
        {
            sqlData.UpdateMoney(user);
        }

    }
}
