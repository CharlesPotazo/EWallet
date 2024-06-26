using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EWalletModels;
using EWalletDataLayer;
using System.Data.SqlClient;
using System.Security.Principal;

namespace EWalletBusinessLogic
{
    public class CashServices
    {
        UserServices userServices = new UserServices();
        SqlDbData sqlDbData = new SqlDbData();
        public void CashIn(int accountNumber, int amount)
        {
            var account = userServices.GetUserByAccNum(accountNumber);
            if (account != null)
            {
                account.money = amount + account.money;
                sqlDbData.UpdateMoney(account);
            }
        }

        public bool CashOut(int accountNumber, int amount)
        {
            bool result = false;
            var account = userServices.GetUserByAccNum(accountNumber);
            if (account != null && account.money >= amount)
            {
                account.money = account.money - amount;
                sqlDbData.UpdateMoney(account);
                result = true;
                return result;
            }
            return result;
        }

        public void transferMoney( int transferTo, int amount)
        {
            User accountTransfer = userServices.GetUserByAccNum(transferTo);

            if (accountTransfer.accountNumber.Equals(transferTo))
            {
                accountTransfer.money = accountTransfer.money + amount;
                sqlDbData.UpdateMoney(accountTransfer);
            }
        }

    }
}
